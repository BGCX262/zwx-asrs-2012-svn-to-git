using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using WareHouseControl;
using ASRSDBBLL;
using ASRSDBME;
namespace ASRS
{
    public enum WarehouseMonitorMesID
    {
        INFO_TASKWAIT = 301, //有任务代执行
        INFO_TASKBEGIN, //任务开始
        INFO_TASKEND, //任务结束
        INFO_LINESTART, //生产线启动
        INFO_LINEPAUSE, //生产线暂停
        INFO_LINESTOP, //生产线停止
        INFO_MANUALMODE, //切换到手动模式
        INFO_AUTOMODE, //切换到自动模式
        INFO_MHOUSEOUT_ADD, //添加手动出库任务
        ERROR_UILOAD = 360 //UI资源加载失败

    }
    /// <summary>
    /// 托管:入库请求
    /// </summary>
    /// <param name="strInfo"></param>
    //public delegate bool DelegateHouseInRequire(string strInfo);
    public class WarehouseMonitorPresenter : Presenter<IWarehouseMonitorView>
    {
        #region 数据成员
        /// <summary>
        /// 调色用：出入库任务分配，奇数入库，偶数出库
        /// </summary>
        private int _chanceCount = 0;
        private DataTable _productTypesTable = null;

        /// <summary>
        /// 模拟入库定时器
        /// </summary>
        private System.Timers.Timer _houseInTimer = null;

        /// <summary>
        /// 模拟出库定时器
        /// </summary>
        private System.Timers.Timer _houseOutTimer = null;

        /// <summary>
        /// 未执行的任务列表
        /// </summary>
        private IList<TaskME> _unexedTaskList = null;

        /// <summary>
        /// 当前未处理任务队列的任务索引
        /// </summary>
        private int _unexedTaskIndex = 0;
        public int unexedTaskIndex
        {
            get
            {
                return _unexedTaskIndex;
            }
        }
        /// <summary>
        /// 后台加载线程
        /// </summary>
        private BackgroundWorker bgwLoad;

        /// <summary>
        /// 生产线状态，初始化时默认为停止状态
        /// </summary>
        private EnumLineRunStatus _lineRunStatus = EnumLineRunStatus.LINE_STOP;

        private int _taskScanInterval = 100; //任务扫描周期
        /// <summary>
        /// 任务监控线程
        /// </summary>
        private Thread _taskMonitorThread = null;

        /// <summary>
        /// 任务监控线程锁对象
        /// </summary>
        private object _taskMonitorLock = new object();

        /// <summary>
        /// 任务列表访问锁，确保同一时刻只有一个线程访问该表
        /// </summary>
        private object _taskCriticalZoneLock = new object();
        /// <summary>
        /// 是否退出任务监控
        /// </summary>
        private bool _exitTaskMonior = false;
        
        /// <summary>
        /// 工作模式
        /// </summary>
        private WorkMode _workMode = WorkMode.MODE_AUTO;

        /// <summary>
        /// 仓储数据库接口对象
        /// </summary>
        private WarehouseStoreBLL _warestoreBll = new WarehouseStoreBLL();

        /// <summary>
        /// 出入库记录数据库接口对象
        /// </summary>
        private HouseInOutBll _houseInOutBll = new HouseInOutBll();
        /// <summary>
        /// 产品数据库接口对象
        /// </summary>
        private ProductBLL _productBll = new ProductBLL();

        /// <summary>
        /// 任务数据库接口对象
        /// </summary>
        private TaskBLL _taskBll = new TaskBLL();

        /// <summary>
        /// 指令数据库接口对象
        /// </summary>
        private InstBLL _instBll = new InstBLL();

        private Hashtable _cellTable=new Hashtable();
        private int _trsmCmdCount = 0;
        public int trsmCmdCount
        {
            get
            {
                return _trsmCmdCount;
            }
            set
            {
                _trsmCmdCount = value;
            }
        }

        
        /*********************************************************************************/
        public int cmdMonitorInterval { get; set; }

        private ASRSModel _Model = null;
        private TransVehicle _transMachine1;
        private TransVehicle _transMachine2;
        #endregion

        public WarehouseMonitorPresenter(IWarehouseMonitorView view):base(view)
        {
            _lineRunStatus = EnumLineRunStatus.LINE_STOP;
            _Model = ASRSModel.GetInstance();
            _transMachine1 = _Model.transVehicleDic[1];
            _transMachine2 = _Model.transVehicleDic[2];
            _Model.transVehicleDic[1].EventTMPosUpdated += TMPosUpdatedEventHandler;
            _Model.transVehicleDic[2].EventTMPosUpdated += TMPosUpdatedEventHandler;
            _Model.transVehicleDic[1].EventTMStatusUpdated += TMStatusUpdatedEventHandler;
            _Model.transVehicleDic[2].EventTMStatusUpdated += TMStatusUpdatedEventHandler;
            

            //this._Model.RFIDDic[1].RfidReadEvent += AutoHouseInRequire;
         
            //生成货架的单元ID映射表
            for(int L= 1;L<=_Model.wareHouseSet.layerCount; L++)
            {
                for (int R = 1; R <= _Model.wareHouseSet.channelCount * 2; R++)
                {
                    for(int C=1;C<=_Model.wareHouseSet.columnCount;C++)
                    {
                        CellPos pos = new CellPos(L, R, C);
                        int houseID = HouseCoordConvertID(pos);
                        if(houseID>0)
                        {
                            _cellTable[houseID]=pos;
                        }
                    }
                }
            }
            _houseInTimer = new System.Timers.Timer(1000);
            _houseOutTimer = new System.Timers.Timer(1000);

            _houseInTimer.AutoReset = true;
            _houseInTimer.Elapsed += HouseInTimerHandler;

            _houseOutTimer.AutoReset = true;
            _houseOutTimer.Elapsed += HouseOutTimerHandler;
        }
        private void HouseInTimerHandler(object source, ElapsedEventArgs e)
        {
            try
            {
                Monitor.Enter(_taskMonitorLock);
                _chanceCount++;
                if (_chanceCount % 2 == 0)
                {
                    return;
                }
                if (_taskBll.GetWaitTaskCount() > 3)
                    return;
                Random rdm = new Random();
                int i = rdm.Next(0, _productTypesTable.Rows.Count);
                ProductStoreME m = new ProductStoreME();
                m.productID = System.DateTime.Now.ToString();
                m.productType = _productTypesTable.Rows[i]["productType"].ToString();
                _productBll.AddProductStore(m);
                AutoHouseInRequire(m.productID, m.productType);
            }
            catch (System.Exception e1)
            {
            	
            }
            finally
            {
                Monitor.Exit(_taskMonitorLock);
            }

        }
        private void HouseOutTimerHandler(object source, ElapsedEventArgs e)
        {
            try
            {
                Monitor.Enter(_taskMonitorLock);
              // _chanceCount++;
                if (_chanceCount % 2 > 0)
                {
                    return;
                }
                if (_taskBll.GetWaitTaskCount() > 5)
                    return;
                Random rdm = new Random();
                int i = rdm.Next(0, _productTypesTable.Rows.Count);
                string productType = _productTypesTable.Rows[i]["productType"].ToString();
                AutoHouseOutRequire(productType);
            }
            catch (System.Exception e1)
            {

            }
            finally
            {
                Monitor.Exit(_taskMonitorLock);
            }
        }
        //订阅view的事件
        protected override void OnViewSet()
        {
            this.View.eventStartPause += StartPauseEventHandler;
            this.View.eventExit += ExitEventHandler;
            this.View.eventInit += InitEventHandler;
       
            this.View.eventStop += StopEventHandler;
            this.View.eventSwitchMode += SwitchModeEventHandler;
            this.View.eventCellPopupDisp += CellPopupDispEventHandler;
            this.View.eventHouseoutRequire += CellHouseoutRequireEventHandler;
            this.View.eventHouseoutApply += ManualHouseoutRequireEventHandler;
            this.View.eventWHouseAttrModify += WareHouseAttrModifyEventHandler;
        }

        /// <summary>
        /// 初始化后台加载线程
        /// </summary>
        private void InitLoadBkw()
        {
            bgwLoad = new BackgroundWorker();
            bgwLoad.WorkerSupportsCancellation = true;//允许中止 
            bgwLoad.WorkerReportsProgress = true;//允许报告进度
            bgwLoad.DoWork +=bgwLoad_DoWork;
            bgwLoad.ProgressChanged += bgwLoad_ProgressChanged;
            bgwLoad.RunWorkerCompleted += bgwLoad_ProgressCompleted;
            bgwLoad.RunWorkerAsync();
        }
        private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            //从数据库中查询仓位状态
            try
            {
                int i = 0;
                foreach (DictionaryEntry de in _cellTable)
                {
                    i++;
                    int houseID = (int)de.Key;
                    CellPos pos = (CellPos)de.Value;
                    WarehouseStatus s = _warestoreBll.GetHousecellStatus(houseID);
                    View.RefreshHouseCellStatus(pos.L, pos.R, pos.C, (CellStoreStatus)s);
                    int progress = (int)((float)i / _cellTable.Count * 100.0f);
                    bgwLoad.ReportProgress(progress);
                    //View.DispInitProgress(progress);
                }
                e.Result = 100;
            }
            catch (System.Exception e1)
            {
                //View.DisplayRuninfo("初始化查询仓位状态失败:" + e1.Message);
            }
        }
        private void bgwLoad_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            View.DispInitProgress(e.ProgressPercentage);
        }

        /// <summary>
        /// 加载初始化完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLoad_ProgressCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            View.DispStatusbarInfo(2, "仓位信息加载完成");
            View.DispInitProgress(0);
            View.RefreshWarehouse();
            
        }

        /// <summary>
        /// 从未执行的任务队列中取一条任务,根据不同的取任务策略，此功能函数可扩展
        /// </summary>
        /// <param name="taskObj">out 任务对象</param>
        /// <param name="instList">指令列表</param>
        /// <returns>如成功获取任务及生产指令列表则返回true，否则返回false</returns>
        private bool FetchTask(out BaseTaskInfo taskObj,out IList<BaseInstInfo> instList)
        {
            taskObj = null;
            instList = null;
            if(_unexedTaskList == null)
            {
                _unexedTaskList = _taskBll.GetAllUnexedTaskList();
            }
            else if(_unexedTaskIndex >=_unexedTaskList.Count)
            {
                _unexedTaskIndex = 0;
                _unexedTaskList = _taskBll.GetAllUnexedTaskList();
            }
            if (_unexedTaskList != null)
            {
                while (_unexedTaskIndex < _unexedTaskList.Count)
                {
                    TaskME m = _unexedTaskList.ElementAt(_unexedTaskIndex);
                    if (m == null)
                    {
                        _unexedTaskIndex++;
                        continue;
                    }
                    taskObj = TaskSerializer.Deserialize(m.taskCode, m.taskObj);
                    if (taskObj != null)
                    {
                        bool autoAllocTM = !taskObj.manualAllocedTM;
                        bool autoAllocHouse = !taskObj.manualAllocedHouse;
                        int parseRe = TaskDisptach.ParseTask(autoAllocTM, autoAllocHouse, ref taskObj, out instList);
                        if (parseRe == 0)
                        {
                            _unexedTaskIndex++;
                            return true;
                        }
                        else if(parseRe ==1 )
                        {
                            return false;
                        }
                        else if(parseRe == 2)
                        {
                            _unexedTaskIndex++;
                            return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 任务监控线程函数,周期检查任务列表是否有数据,当有任务时给出处理
        /// </summary>
        private void TaskMonitorProc()
        {
            string[] strList = new string[4];
            strList.Count();
            TransVehicle m1 = _Model.transVehicleDic[1];
            TransVehicle m2 = _Model.transVehicleDic[2];
            while (!_exitTaskMonior)
            {
                try
                {
                    Monitor.Enter(_taskMonitorLock);
                    Thread.Sleep(_taskScanInterval);
                    if (_lineRunStatus != EnumLineRunStatus.LINE_RUN)
                    {
                        continue;
                    }
                    IList<BaseInstInfo> instList = null;
                     BaseTaskInfo taskObj = null;
                    if(!FetchTask(out taskObj,out instList))
                    {
                        continue;
                    }
                    TransVehicle m = null;
                    if (taskObj.machineAllocated == 1)
                    {
                        m = m1;
                    }
                    else if (taskObj.machineAllocated == 2)
                    {
                        m = m2;
                    }
                    if (!m.FillInstList(instList, taskObj, taskObj.taskID))
                    {
                        continue;
                    }
                    //提取任务：任务描述,分配资源：
                    MessageDefineME mesDefM = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_TASKWAIT);
                    string strID = mesDefM.messageID.ToString();
                    string[] s = new string[4];
                    s[0] = System.DateTime.Now.ToString();
                    s[1] = strID.PadLeft(3, '0');
                    s[2] = mesDefM.messageContent;
                    s[3] = string.Format("任务流水号：{0},任务描述:{1},分配小车编号:{2},分配仓位:({3},{4},{5})",
                        taskObj.taskID, _Model.MachineTaskDic[(int)taskObj.taskCode], taskObj.machineAllocated, taskObj.targetL, taskObj.targetR, taskObj.targetC);
                    View.OutputMessage(s);

                    //插入指令列表到指令数据库
                    //修改任务状态
                    foreach (BaseInstInfo inst in instList)
                    {
                        string instID = _instBll.GenerateNewInstID();
                        InstME instModel = new InstME();
                        instModel.instID = instID;
                        instModel.instCode = (int)inst.instCode;
                        instModel.instObj = InstSerializer.Serialize(inst);
                        instModel.vehicleAlloc = taskObj.machineAllocated;
                        _instBll.AddInst(instModel);
                    }
                    DataTable instDT1 = _instBll.GetListByVehicleNo(1);
                    View.RefreshInstDisp(1, instDT1);
                    DataTable instDT2 = _instBll.GetListByVehicleNo(2);
                    View.RefreshInstDisp(2, instDT2);

                    _Model.taskingRecordDic[taskObj.machineAllocated].currentTaskID = taskObj.taskID;
                    _Model.taskingRecordDic[taskObj.machineAllocated].machineID = taskObj.machineAllocated;
                    _Model.taskingRecordDic[taskObj.machineAllocated].taskExeStatus = (int)TaskExeStatus.TASK_RUN;
                    _Model.SaveTaskingRecordFile(taskObj.machineAllocated);
                   
                    string xmlTaskObj = TaskSerializer.Serialize(taskObj);
                    _taskBll.UpdateTaskObj(taskObj.taskID, xmlTaskObj);
                    _taskBll.UpdateTaskStatus(taskObj.taskID, (int)TaskExeStatus.TASK_RUN);
                    DataTable taskDT = _taskBll.GetAllTask();
                    View.RefreshTaskDisp(taskDT);
                }
                catch (System.Exception e1)
                {
                    Console.WriteLine("TaskMonitorProc error:" + e1.Message);
                }
                finally
                {
                    Monitor.Exit(_taskMonitorLock);
                }
            }
        }

        /// <summary>
        /// 启动任务监控
        /// </summary>
        private void StartTaskMonitoring()
        {
              //Monitor.Exit(_taskMonitorLock);
            _taskMonitorThread.Resume();
        }

        /// <summary>
        /// 停止任务监控
        /// </summary>
        private void StopTaskMonitoring()
        {
             // Monitor.Enter(_taskMonitorLock);
            _taskMonitorThread.Suspend();
        }

        /// <summary>
        /// 自动模式下入库请求事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns> 0:成功生成指令，1：无空闲仓位，2：小车均有故障,3:数据库无此产品信息,4:生产线不在运行状态，5: 不在自动模式 </returns>
        //private int AutoHouseInRequire(object sender,msgRFIDEventArg e)
        //{
        //    try
        //    {
        //        Monitor.Enter(_taskMonitorLock);
        //        if (_lineRunStatus != EnumLineRunStatus.LINE_RUN)
        //            return 4;
        //        //只有在自动模式下才接收入库请求,
        //        if (_workMode != WorkMode.MODE_AUTO)
        //            return 5;

        //        ///////////////////////////////////////////////////////////////////////////////////////
        //        StringBuilder strRequire = new StringBuilder();
        //        strRequire.AppendFormat("收到入库请求，产品ID:{0}", e.rfidInfo);
        //       // View.DisplayRuninfo(strRequire.ToString());

        //        //接收到入库信息(存储在strInfo中)
        //        //根据调度策略生成入库指令，添加到指令列表中
        //        string ProductID = e.rfidInfo;
        //        ProductStoreME ProductM = _productBll.GetProductStore(ProductID);
        //        if (ProductM == null)
        //        {
        //            return 3;
        //        }
        //        //////////////////////////////////////////////////////////////////////////
        //        lock (_taskCriticalZoneLock)
        //        {
        //            string TaskID = _taskBll.GenerateNewTaskSerialNo();
        //            TaskME taskM = new TaskME();
        //            taskM.taskID = TaskID;
        //            taskM.taskCode = (int)TaskCode.TASK_PRODUCT_INHOUSE;
        //            taskM.taskExeStatus = (int)TaskExeStatus.TASK_NEW;
        //            TaskProductInhouse taskObj = new TaskProductInhouse();
        //            taskObj.taskID = TaskID;
        //            taskObj.productID = ProductID;
        //            taskM.taskObj = TaskSerializer.Serialize(taskObj);
        //            _taskBll.AddTask(taskM); //添加任务
        //        }
        //        return 0;
        //    }
        //    catch (System.Exception e1)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        Monitor.Exit(_taskMonitorLock);
        //    }
           
        //}

        /// <summary>
        /// 自动模式下入库请求事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns> 0:成功生成指令，1：无空闲仓位，2：小车均有故障,3:数据库无此产品信息,4:生产线不在运行状态，5: 不在自动模式 </returns>
        private int AutoHouseInRequire(string productID,string productType)
        {
            try
            {
                Monitor.Enter(_taskMonitorLock);
                if (_lineRunStatus != EnumLineRunStatus.LINE_RUN)
                    return 4;
                //只有在自动模式下才接收入库请求,
                if (_workMode != WorkMode.MODE_AUTO)
                    return 5;

                //根据调度策略生成入库指令，添加到指令列表中
                
                ProductStoreME ProductM = _productBll.GetProductStore(productID);
                if (ProductM == null)
                {
                    return 3;
                }
                //////////////////////////////////////////////////////////////////////////
               // lock (_taskCriticalZoneLock)
               // {
                    string TaskID = _taskBll.GenerateNewTaskSerialNo();
                    TaskME taskM = new TaskME();
                    taskM.taskID = TaskID;
                    taskM.taskCode = (int)TaskCode.TASK_PRODUCT_INHOUSE;
                    taskM.taskExeStatus = (int)TaskExeStatus.TASK_NEW;
                    TaskProductInhouse taskObj = new TaskProductInhouse();
                    taskObj.taskID = TaskID;
                    taskObj.productID = productID;
                    taskM.taskObj = TaskSerializer.Serialize(taskObj);
                    _taskBll.AddTask(taskM); //添加任务
                    DataTable taskDT = _taskBll.GetAllTask();
                    View.RefreshTaskDisp(taskDT);
               // }
                return 0;
            }
            catch (System.Exception e1)
            {
               
            }
            finally
            {
                Monitor.Exit(_taskMonitorLock);

            }
            return 0;
        }

        /// <summary>
        /// 自动出库
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        private int AutoHouseOutRequire(string productType)
        {
            try
            {
                Monitor.Enter(_taskMonitorLock);
                if (_lineRunStatus != EnumLineRunStatus.LINE_RUN)
                    return 4;
                //只有在自动模式下才接收出库请求,
                if (_workMode != WorkMode.MODE_AUTO)
                    return 5;
                string TaskID = _taskBll.GenerateNewTaskSerialNo();
                TaskME taskM = new TaskME();
                taskM.taskID = TaskID;
                taskM.taskCode = (int)TaskCode.TASK_PRODUCT_OUTHOUSE;
                taskM.taskExeStatus = (int)TaskExeStatus.TASK_NEW;
                
                TaskProductOuthouse taskObj = new TaskProductOuthouse();
                taskObj.productType = productType;
                taskObj.taskID = TaskID;
                taskObj.manualAllocedHouse = false;
                taskObj.manualAllocedTM = false;
               
                //taskObj.productID = productID;
                taskM.taskObj = TaskSerializer.Serialize(taskObj);
                _taskBll.AddTask(taskM); //添加任务
                return 0;
            }
            catch (System.Exception e)
            {
            	
            }
            finally
            {
                Monitor.Exit(_taskMonitorLock);
            }
            return 0;
        }
        /// <summary>
        /// 选择可用仓位
        /// </summary>
       CellPos ChooseCellPos(int machineNo)
        {
             if(machineNo == 1)
            {
                int houseID = 1;
                for (int L = 1; L <= _Model.wareHouseSet.layerCount; L++)
                {
                    for (int C = 1; C <= _Model.wareHouseSet.columnCount;C++)
                    {
                        for(int R=1;R<=2;R++)
                        {
                            CellPos pos = new CellPos(L, R, C);
                            houseID = HouseCoordConvertID(pos);
                            WarehouseStatus s = _warestoreBll.GetHousecellStatus(houseID);
                            if(s == WarehouseStatus.HOUSE_EMPTY)
                            {
                                // Console.WriteLine();
                                return pos;
                            }
                        }
                    }
                }
                //无空位
                return null;
            }
             else if (machineNo == 2)
             {
                 int houseID = 1;
                 for (int L = 1; L <= _Model.wareHouseSet.layerCount; L++)
                 {
                     for (int C = 1; C <= _Model.wareHouseSet.columnCount; C++)
                     {
                         for (int R = 3; R <= 4; R++)
                         {
                             CellPos pos = new CellPos(L, R, C);
                             houseID = HouseCoordConvertID(pos);
                             WarehouseStatus s = _warestoreBll.GetHousecellStatus(houseID);
                             if (s == WarehouseStatus.HOUSE_EMPTY)
                             {
                                 return pos;
                             }
                         }
                     }
                 }
                 return null;
             }
             return null;
        }

       /// <summary>
       /// 小车状态更新事件处理过程
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void TMStatusUpdatedEventHandler(object sender,msgTMEventArg e)
        {
            try
            {
               // Monitor.Enter(_taskMonitorLock);
                BaseTaskInfo baseTaskObj = _Model.transVehicleDic[e.MachineNo].currentTaskObj;
                if (e.TMStatus == MachineWorkStatus.MACHINE_TASK_BEGIN)
                {

                    StringBuilder str = new StringBuilder();
                    str.Append(_Model.MachineTaskDic[(int)baseTaskObj.taskCode]);
                    int houseID = HouseCoordConvertID(new CellPos(baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC));
                    str.AppendFormat(",目标仓位:{0}({1},{2},{3})", houseID, baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC);
                    View.RefreshMachineTask(e.MachineNo, str.ToString());
                    View.RefreshMachineStatus(e.MachineNo, "任务开始");
                    MessageDefineME mesDefM = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_TASKBEGIN);
                    string strID = mesDefM.messageID.ToString();
                    string[] s = new string[4];
                    s[0] = System.DateTime.Now.ToString();
                    s[1] = strID.PadLeft(3, '0');
                    s[2] = mesDefM.messageContent;
                    s[3] = string.Format("任务流水号：{0},任务描述:{1}",
                        baseTaskObj.taskID, _Model.MachineTaskDic[(int)baseTaskObj.taskCode]);
                    View.OutputMessage(s);

                }
                else if (e.TMStatus == MachineWorkStatus.PRODUCT_INHOUSE_OK)
                {
                    //更新货架单元状态

                    int houseID = HouseCoordConvertID(new CellPos(baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC));
                    if (baseTaskObj.taskCode == TaskCode.TASK_PRODUCT_INHOUSE)
                    {
                        TaskProductInhouse ProductInTask = baseTaskObj as TaskProductInhouse;
                        _warestoreBll.UpdateHousecellStore(houseID, ProductInTask.productID, WarehouseStatus.HOSUE_FULL);
                        //增加入库记录
                        InHouseRecordME inhouseM = new InHouseRecordME();
                        inhouseM.houseID = houseID;
                        inhouseM.inHouseTime = DateTime.Now;
                        inhouseM.productID = ProductInTask.productID;
                        _houseInOutBll.AddProductInRecord(inhouseM);
                    }
                    View.RefreshHouseCellStatus(baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC, CellStoreStatus.CELL_FULL);
                    //_Model.RFIDDic[1].SendRemoveObjMsg();
                    View.RefreshMachineStatus(e.MachineNo, "已入库");
                }
                else if (e.TMStatus == MachineWorkStatus.PRODUCT_OUTHOUSE_OK)
                {
                    //更新货架单元状态

                    int houseID = HouseCoordConvertID(new CellPos(baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC));
                    if (baseTaskObj.taskCode == TaskCode.TASK_PRODUCT_OUTHOUSE)
                    {
                        TaskProductOuthouse ProductOutTask = baseTaskObj as TaskProductOuthouse;
                        _warestoreBll.UpdateHousecellStore(houseID, string.Empty, WarehouseStatus.HOUSE_EMPTY);
                       OutHouseRecordME outhouseM = new OutHouseRecordME();
                       outhouseM.houseID = houseID;
                       outhouseM.productID = ProductOutTask.productID;
                       outhouseM.outHouseTime = DateTime.Now;
                       _houseInOutBll.AddProductOutRecord(outhouseM);
                    }
                    
                    View.RefreshHouseCellStatus(baseTaskObj.targetL, baseTaskObj.targetR, baseTaskObj.targetC, CellStoreStatus.CELL_EMPTY);
                    View.RefreshMachineStatus(e.MachineNo, "已出库");
                }
                else if (e.TMStatus == MachineWorkStatus.MACHINE_TASK_END)
                {
                    //任务执行完毕,清掉数据库里的指令列表
                    _instBll.ClearInstList(e.MachineNo);

                    //更新任务状态
                    string taskID = _Model.transVehicleDic[e.MachineNo].currentTaskObj.taskID;
                    _taskBll.UpdateTaskStatus(taskID, (int)TaskExeStatus.TASK_COMPLETED);
                    _Model.taskingRecordDic[e.MachineNo].taskExeStatus = (int)TaskExeStatus.TASK_COMPLETED;
                    _Model.taskingRecordDic[e.MachineNo].currentTaskID = taskID;
                    _Model.SaveTaskingRecordFile(e.MachineNo);
                    //XMLConfigRW.SaveMachineCurrentTask(e.MachineNo, TaskExeStatus.TASK_COMPLETED, taskID);
                    View.RefreshMachineStatus(e.MachineNo, "任务完成");
                    MessageDefineME mesDefM = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_TASKEND);
                    string strID = mesDefM.messageID.ToString();
                    string[] s = new string[4];
                    s[0] = System.DateTime.Now.ToString();
                    s[1] = strID.PadLeft(3, '0');
                    s[2] = mesDefM.messageContent;
                    s[3] = string.Format("任务流水号：{0},任务描述:{1}",
                        baseTaskObj.taskID, _Model.MachineTaskDic[(int)baseTaskObj.taskCode]);
                    View.OutputMessage(s);
                    DataTable taskDT = _taskBll.GetAllTask();
                    View.RefreshTaskDisp(taskDT);
                }
            }
            catch (System.Exception e1)
            {
                Console.WriteLine("TMStatusUpdatedEventHandler :"+e1.Message);
            }
            finally
            {
               // Monitor.Exit(_taskMonitorLock);
            }
            
           // View.RefreshMachineStatus(e.MachineNo, _Model.MachineStatusDic[(int)e.TMStatus]);

        }

        /// <summary>
        /// 小车位置更新事件处理过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMPosUpdatedEventHandler(object sender,msgTMEventArg e)
       {
              View.RefreshMachineCoord(e.MachineNo, e.TMPos);
       }

        /// <summary>
        /// 仓位信息弹出显示事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellPopupDispEventHandler(object sender,CellEventArgs e)
        {
            CellDispInfo cellInfo = new CellDispInfo();
            StringBuilder strBuild = new StringBuilder();
            strBuild.AppendFormat("仓位ID:{0}({1},{2},{3})", HouseCoordConvertID(e.CurCellCoord), e.CurCellCoord.L, e.CurCellCoord.R, e.CurCellCoord.C);
            cellInfo.titleText =strBuild.ToString();

            strBuild.Clear();
            strBuild.AppendLine("老化项目1：210");
            strBuild.AppendLine("老化项目2：102");
            cellInfo.contentText = strBuild.ToString();
            View.PopupDispCellinfo(e.CurCellCoord, cellInfo);

        }

        /// <summary>
        /// 手动出库请求，仓位是否可以出库验证事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellHouseoutRequireEventHandler(object sender,CellEventArgs e)
        {
            if(_workMode == WorkMode.MODE_AUTO)
            {
                View.ShowReMessageBox("出库请求操作非法", "当前为自动运行模式，请切换到手动模式", 1);
                return;
            }
            int HouseID = HouseCoordConvertID(e.CurCellCoord);
            WarehouseStatus s = _warestoreBll.GetHousecellStatus(HouseID);
            if(s == WarehouseStatus.HOSUE_FULL)
            {
                //允许出库
                View.HouseoutListitemAdd(HouseID,e.CurCellCoord.L,e.CurCellCoord.R,e.CurCellCoord.C);
            }
            else if(s == WarehouseStatus.HOUSE_EMPTY)
            {
                //仓位为空，禁止出库
                View.ShowReMessageBox("出库请求操作非法","该仓位为空,禁止出库",1);
            }
        }

        /// <summary>
        /// 手动出库列表应用事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualHouseoutRequireEventHandler(object sender,CellHouseoutListEventArgs e)
        {
            try
            {
                Monitor.Enter(_taskMonitorLock);
                if (_workMode == WorkMode.MODE_AUTO)
                {
                    View.ShowReMessageBox("出库请求操作非法", "当前为自动运行模式，请切换到手动模式", 1);
                    return;
                }
                IList<CellPos> cellList = e.cellList;
                if (cellList.Count == 0)
                {
                    View.ShowReMessageBox("出库请求操作非法", "出库指令列表为空", 1);
                    return;
                }
                foreach (CellPos pos in cellList)
                {
                    TaskProductOuthouse taskObj = new TaskProductOuthouse();
                    taskObj.manualAllocedTM = true;
                    taskObj.manualAllocedHouse = true;
                    taskObj.targetL = pos.L;
                    taskObj.targetR = pos.R;
                    taskObj.targetC = pos.C;
                    if (pos.R < 3)
                    {
                        //1号小车出库
                        taskObj.machineAllocated = 1;
                    }
                    else
                    {
                        //2号小车出库
                        taskObj.machineAllocated = 2;
                    }

                    string TaskID = _taskBll.GenerateNewTaskSerialNo();
                    TaskME taskM = new TaskME();
                    taskM.taskID = TaskID;
                    taskM.taskCode = (int)TaskCode.TASK_PRODUCT_OUTHOUSE;
                    taskM.taskExeStatus = (int)TaskExeStatus.TASK_NEW;

                    taskObj.taskID = TaskID;
                    taskM.taskObj = TaskSerializer.Serialize(taskObj);
                    _taskBll.AddTask(taskM); //添加任务
                    
                }
                View.ResHouseoutListApply(0);
            }
            catch (System.Exception e1)
            {
            	
            }
            finally
            {
                Monitor.Exit(_taskMonitorLock);
            }
            
        }
        /// <summary>
        /// 货架单元ID到货架单元坐标的转换
        /// </summary>
        /// <param name="houseID">货架单元ID</param>
        /// <param name="pos">货架坐标</param>
        /// <returns>若单元ID参数有效，成功转换，则返回true，否则返回false</returns>
        private CellPos HouseIDConvertCoord(int houseID)
        {
            if(_cellTable.ContainsKey(houseID))
            {
                return  (CellPos)_cellTable[houseID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 货架单元坐标到单元ID的转换
        /// </summary>
        /// <param name="pos">货架单元坐标</param>
        /// <returns>转换后的货架单元ID,若失败则返回-1</returns>
        private int HouseCoordConvertID(CellPos pos)
        {
            //if (pos.L < 1 || pos.R < 1 || pos.C < 1)
            //    return -1;
            //int houseID = (pos.L - 1) * (_Model.wareHouseSet.channelCount * 2 * _Model.wareHouseSet.columnCount) + (pos.R - 1) * _Model.wareHouseSet.columnCount + pos.C;
            //return houseID;
            return _Model.HouseCoordConvertID(pos.L, pos.R, pos.C);
        }

        /// <summary>
        /// 系统启动时，做一系列检查工作，如有不一致情况存在，给出报警以及处理
        /// </summary>
        private void SysStartCheck()
       {
           //加载数据库中的指令列表
           if (_Model.taskingRecordDic.Count<=0)
           {
               return;
           }
           for (int i = 1; i <= _Model.taskingRecordDic.Count; i++)
           {
               //TaskExeStatus taskStatus = TaskExeStatus.TASK_NEW;
              // string taskID = string.Empty;
               //XMLConfigRW.ReadMachineCurrentTask(i, out taskStatus, out taskID);
             //  int SavedInstIndex = XMLConfigRW.ReadSavedInstIndex(i);
               string taskID = _Model.taskingRecordDic[i].currentTaskID;
               TaskExeStatus taskStatus = (TaskExeStatus)_Model.taskingRecordDic[i].taskExeStatus;
               int SavedInstIndex = _Model.taskingRecordDic[i].currentInstIndex;
               TaskME taskM = _taskBll.GetTask(taskID);
               if (taskM == null || (taskStatus == TaskExeStatus.TASK_COMPLETED))
               {
                   //指令列表对应的任务不存在，清掉数据库中的指令列表。
                   //约定，指令数据库中每台小车最多存储一个任务的指令列表
                   _instBll.ClearInstList(i);
                   continue;
               }
               BaseTaskInfo task = TaskSerializer.Deserialize(taskM.taskCode, taskM.taskObj);
               if (task != null && (taskStatus == TaskExeStatus.TASK_RUN))
               {
                   IList<InstME> instMEList = _instBll.GetInstList(i);
                   if (instMEList != null && instMEList.Count > 0)
                   {
                       List<BaseInstInfo> instObjList = new List<BaseInstInfo>();
                       foreach (InstME m in instMEList)
                       {
                           BaseInstInfo instObj = InstSerializer.Deserialize(m.instCode, m.instObj);
                           instObjList.Add(instObj);
                       }
                       //恢复指令列表
                      
                      _Model.transVehicleDic[i].InitInstList(instObjList,SavedInstIndex, task, taskID);
                    
                   }
               }
               else
               {
                   //指令列表对应的任务不存在，清掉数据库中的指令列表
                   _instBll.ClearInstList(i);
               }
           }
       }
        #region 事件响应
        /// <summary>
        /// 货架属性修改事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WareHouseAttrModifyEventHandler(object sender, WareHouseAttrModifyEventArgs e)
        {
            // if(e.layerCount != _Model.)
            //判断当前仓位在数据库中是否有记录，若有，则按照新的规则生成ID号，若没有则创建
            //更新完成后调用view接口给出反馈信息
            if(e.layerCount != _Model.wareHouseSet.layerCount || (e.layerCount != _Model.wareHouseSet.channelCount) ||(e.columnCount != _Model.wareHouseSet.columnCount))
            {
                for (int L = 1; L <= e.layerCount; L++)
                {
                    for (int R = 1; R <= e.channelCount * 2; R++)
                    {
                        for (int C = 1; C <= e.columnCount; C++)
                        {
                            WarehouseStoreME m = _warestoreBll.GetHouseCell(L, R, C);
                            if (m == null)
                            {
                                //增加
                                m = new WarehouseStoreME();
                                m.houseLayerID = L;
                                m.houseRowID = R;
                                m.houseColumnID = C;
                                m.houseID = HouseCoordConvertID(new CellPos(L, R, C));
                                m.useStatus = 1;
                                m.productID = string.Empty;
                                //增加之前先检查是否存在相同的ID，若存在则修改
                                WarehouseStoreME oldM = _warestoreBll.GetHouseCell(m.houseID);
                                if (oldM != null)
                                {

                                    _warestoreBll.DeleteHouseCell(m.houseID);
                                    oldM.houseID = HouseCoordConvertID(new CellPos(oldM.houseLayerID, oldM.houseRowID, oldM.houseColumnID));
                                    _warestoreBll.AddHousecell(oldM);
                                }
                                _warestoreBll.AddHousecell(m);
                            }
                            else
                            {
                                //改变ID号
                                int houseID = _Model.HouseCoordConvertID(L, R, C);
                                if (m.houseID == houseID)
                                {
                                    //id重复,修改id
                                    m.houseID = houseID;
                                }
                            }
                        }
                    }
                }
                _Model.wareHouseSet.layerCount = e.layerCount;
                _Model.wareHouseSet.channelCount = e.channelCount;
                _Model.wareHouseSet.columnCount = e.columnCount;
                _Model.SaveWarehouseSetFile();
            }
            
        }
        private void InitEventHandler(object sender, EventArgs e)
        {
            //初始化trans machine状态,如有不一致性存在，则处理，并给出报警
            SysStartCheck();

            InitLoadBkw(); //加载仓位信息
            DataTable taskDT = _taskBll.GetAllTask();
            View.RefreshTaskDisp(taskDT);
            DataTable instDT1 = _instBll.GetListByVehicleNo(1);
            View.RefreshInstDisp(1, instDT1);
            DataTable instDT2 = _instBll.GetListByVehicleNo(2);
            View.RefreshInstDisp(2, instDT2);
            _productTypesTable = _Model.productBll.GetProductInfoList();
            //启动任务监控线程
            _taskMonitorThread = new Thread(new ThreadStart(TaskMonitorProc));
            _taskMonitorThread.Name = "任务监控线程";
            _taskMonitorThread.IsBackground = true;

            //StopTaskMonitoring();
            _taskMonitorThread.Start();
            _taskMonitorThread.Suspend();
            View.RefreshWorkMode(_workMode);
        }
        private void StartPauseEventHandler(object sender, EventArgs e)
        {
            if (_lineRunStatus == EnumLineRunStatus.LINE_RUN)
            {
                _lineRunStatus = EnumLineRunStatus.LINE_PAUSE;
                //this._Model.RFIDDic[1].Pause();
                _houseInTimer.Stop();
                _houseOutTimer.Stop();
                this._Model.transVehicleDic[1].Stop();
                this._Model.transVehicleDic[2].Stop();
                StopTaskMonitoring();
                MessageDefineME m = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_LINEPAUSE);
                string strID = m.messageID.ToString();
                string[] s = new string[4];
                s[0] = System.DateTime.Now.ToString();
                s[1] = strID.PadLeft(3, '0');
                s[2] = m.messageContent;
                s[3] = string.Empty;
                View.OutputMessage(s);
            }
            else 
            {
                _lineRunStatus = EnumLineRunStatus.LINE_RUN;
                this._Model.transVehicleDic[1].Start();
                this._Model.transVehicleDic[2].Start();
                _houseInTimer.Start();
                _houseOutTimer.Start();
             //   this._Model.RFIDDic[1].Start();
                StartTaskMonitoring();
                MessageDefineME m = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_LINESTART);
                string strID = m.messageID.ToString();
                string[] s = new string[4];
                s[0] = System.DateTime.Now.ToString();
                s[1] = strID.PadLeft(3, '0');
                s[2] = m.messageContent;
                s[3] = string.Empty;
                View.OutputMessage(s);
            }
            View.RefreshLineStatus(_lineRunStatus);
      
        }
        private void ExitEventHandler(object sender,EventArgs e)
        {
           
            this._transMachine1.Stop();
            this._transMachine2.Stop();
            this._transMachine1.Exit();
            this._transMachine2.Exit();
            _houseOutTimer.Close();
            _houseInTimer.Close();
           // this._Model.RFIDDic[1].Close();
            this._exitTaskMonior = true;

        }
        private void StopEventHandler(object sender, EventArgs e)
        {
            if (_lineRunStatus == EnumLineRunStatus.LINE_STOP)
            {
                return;
            }
            else if (_lineRunStatus == EnumLineRunStatus.LINE_PAUSE || _lineRunStatus == EnumLineRunStatus.LINE_RUN)
            {
                _lineRunStatus = EnumLineRunStatus.LINE_STOP;
                this._transMachine1.Stop();
                this._transMachine2.Stop();
                this._houseInTimer.Stop();
                this._houseOutTimer.Stop();
               // this._Model.RFIDDic[1].Pause();
                StopTaskMonitoring();
               
                View.RefreshLineStatus(_lineRunStatus);
                MessageDefineME m = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_LINESTOP);
                string strID = m.messageID.ToString();
                string[] s = new string[4];
                s[0] = System.DateTime.Now.ToString();
                s[1] = strID.PadLeft(3, '0');
                s[2] = m.messageContent;
                s[3] = string.Empty;
                View.OutputMessage(s);
            }
        }
        private void SwitchModeEventHandler(object sender, EventArgs e)
        {
            //模式切换的处理工作
            if(_workMode == WorkMode.MODE_AUTO)
            {
                 _workMode= WorkMode.MODE_MANUAL;
                View.RefreshWorkMode(_workMode);
                MessageDefineME m = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_MANUALMODE);
                string strID = m.messageID.ToString();
                string[] s = new string[4];
                s[0] = System.DateTime.Now.ToString();
                s[1] = strID.PadLeft(3, '0');
                s[2] = m.messageContent;
                s[3] = string.Empty;
                View.OutputMessage(s);
            }
            else if(_workMode == WorkMode.MODE_MANUAL)
            {
                 _workMode= WorkMode.MODE_AUTO;
                 View.RefreshWorkMode(_workMode);
                 MessageDefineME m = _Model.mesBll.GetMesDef((int)WarehouseMonitorMesID.INFO_AUTOMODE);
                 string strID = m.messageID.ToString();
                 string[] s = new string[4];
                 s[0] = System.DateTime.Now.ToString();
                 s[1] = strID.PadLeft(3, '0');
                 s[2] = m.messageContent;
                 s[3] = string.Empty;
                 View.OutputMessage(s);
            }
        }
        #endregion
    }
}
