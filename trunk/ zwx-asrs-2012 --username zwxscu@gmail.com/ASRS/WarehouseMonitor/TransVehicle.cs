using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using WareHouseControl;
using System.Windows.Forms;
namespace ASRS
{
    /// <summary>
    /// 托管：小车状态、位置更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void delegateTMUpdated(object sender, msgTMEventArg e);

    //小车状态事件参数
    public class msgTMEventArg : System.EventArgs
    {
        public bool ErrorHappened = false;
        public string errorString;
        public int MachineNo; //小车ID，从1开始
        public MachineWorkStatus TMStatus; //堆垛机工作状态
        public TMCoord TMPos; //小车当前坐标
        public int targetL = 0;
        public int targetR = 0;
        public int targetC = 0;
        public msgTMEventArg()
        {
            MachineNo = 1;
            TMStatus = MachineWorkStatus.MACHINE_IDLE;
        }
    }

    /// <summary>
    ///实现堆垛机小车控制的代理类，负责指令的执行，状态的监视
    /// </summary>
    public class TransVehicle
    {
        #region 属性

        /// <summary>
        /// 设备编号
        /// </summary>
        public int MachineNo { get; set; }

        private int _instScanInterval = 200; //取指令周期,单位：毫秒
        /// <summary>
        /// 小车工作状态
        /// </summary>
        public MachineWorkStatus MStatus{get;set;}

        /// <summary>
        /// 小车当前坐标
        /// </summary>
        private TMCoord _currentCoord = new TMCoord();
        public TMCoord currentCoord
        {
            get
            {
                return _currentCoord;
            }
            set
            {
                _currentCoord = value;
            }
        }

        /// <summary>
        /// 当前任务的流水号
        /// </summary>
        private string _currentTaskID = string.Empty;

        /// <summary>
        /// 当前任务
        /// </summary>
        private BaseTaskInfo _currentTaskObj = null;
        public BaseTaskInfo currentTaskObj
        {
            get
            {
                return _currentTaskObj;
            }
            set
            {
                _currentTaskObj = value;
            }
        }
        /// <summary>
        /// 当前自动模式下，指令列表中正在执行的指令的序号，从0开始
        /// </summary>
        private int _currentAutoInstIndex = -1;
        public int currentAutoInstIndex
        {
            get
            {
                return _currentAutoInstIndex;
            }
            set
            {
                _currentAutoInstIndex = value;
            }
        }
        /// <summary>
        /// 当前任务的指令列表
        /// </summary>
        private List<BaseInstInfo> _instList = new List<BaseInstInfo>();

        /// <summary>
        /// 指令执行线程
        /// </summary>
        private Thread _instExeThread = null;

        /// <summary>
        /// 指令扫描线程锁定对象,控制线程的启动、停止
        /// </summary>
        private object _instExeLock = new object();

        /// <summary>
        /// 等待当前指令执行完毕的锁对象
        /// </summary>
        private object _instExeWaitLock = new object();

        private object _criticalZoneLock = new object();
        /// <summary>
        /// 是否退出指令执行线程
        /// </summary>
        private bool _exitInstExeThread = false;

        /// <summary>
        /// 后台线程
        /// </summary>
        private BackgroundWorker _bkgWorker;

        /// <summary>
        ///  小车工作状态(小车任务里程碑事件）更新事件
        /// </summary>
        public event delegateTMUpdated EventTMStatusUpdated;

        /// <summary>
        /// 小车位置更新事件
        /// </summary>
        public event delegateTMUpdated EventTMPosUpdated;

        #endregion
        #region 私有行为
        private void ExecuteInst(BaseInstInfo baseInst)
        {           
            if (baseInst == null)
            {
                return;
            }
            switch (baseInst.instCode)
            {
                case InstCode.INST_BEGIN:
                    {
                        InstBegin inst = baseInst as InstBegin;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstBegin(inst);
                        break;
                    }
                case InstCode.INST_END:
                    {
                        InstEnd inst = baseInst as InstEnd;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstEnd(inst);
                        break;
                    }
                case InstCode.INST_MOVL:
                    {
                        InstMovL inst = baseInst as InstMovL;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstMovl(inst);
                        break;
                    }
                case InstCode.INST_MOVC:
                    {
                        InstMovC inst = baseInst as InstMovC;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstMovc(inst);
                        break;
                    }
                case InstCode.INST_LOAD:
                    {
                        InstLoad inst = baseInst as InstLoad;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstLoad(inst);
                        break;
                    }
                case InstCode.INST_UNLOAD:
                    {
                        InstUnload inst = baseInst as InstUnload;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstUnload(inst);
                        break;
                    }
                default:
                    break;
            }
          
        }
        /// <summary>
        /// 后台线程dowork事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bkgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            BaseInstInfo baseInst = e.Argument as BaseInstInfo;
            if (baseInst == null)
            {
                e.Result = 100;
                return;
            }
            switch (baseInst.instCode)
            {
                case InstCode.INST_BEGIN:
                    {
                        InstBegin inst = baseInst as InstBegin;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstBegin(inst);
                        break;
                    }
                case InstCode.INST_END:
                    {
                        InstEnd inst = baseInst as InstEnd;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstEnd(inst);
                        break;
                    }
                case InstCode.INST_MOVL:
                    {
                        InstMovL inst = baseInst as InstMovL;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstMovl(inst);
                        break;
                    }
                case InstCode.INST_MOVC:
                    {
                        InstMovC inst = baseInst as InstMovC;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstMovc(inst);
                        break;
                    }
                case InstCode.INST_LOAD:
                    {
                        InstLoad inst = baseInst as InstLoad;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstLoad(inst);
                        break;
                    }
                case InstCode.INST_UNLOAD:
                    {
                        InstUnload inst = baseInst as InstUnload;
                        if (inst == null)
                        {
                            break;
                        }
                        ExecuteInstUnload(inst);
                        break;
                    }
                default:
                    break;
            }
            e.Result = 100;
        }

        /// <summary>
        /// 后台线程执行状态改变事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bkgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /// <summary>
        /// 后台线程执行完毕事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bkgworker_ProgressCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock(_instExeWaitLock)
            {
                Monitor.Pulse(_instExeWaitLock);
            }  
        }

        /// <summary>
        /// 执行开始指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstBegin(InstBegin inst)
        {
            Thread.Sleep(200);
            MStatus = MachineWorkStatus.MACHINE_TASK_BEGIN;
            msgTMEventArg e = new msgTMEventArg();
            e.MachineNo = this.MachineNo;
            e.ErrorHappened = false;
            e.TMStatus = MStatus;
           
            if (EventTMStatusUpdated != null)
            {
                EventTMStatusUpdated(this, e);
            }
        }

        /// <summary>
        /// 执行结束指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstEnd(InstEnd inst)
        {
            MStatus = MachineWorkStatus.MACHINE_TASK_END;
            msgTMEventArg e = new msgTMEventArg();
            e.MachineNo = this.MachineNo;
            e.ErrorHappened = false;
            e.TMStatus = MStatus;
            if (EventTMStatusUpdated != null)
            {
                EventTMStatusUpdated(this, e);
            }
            Thread.Sleep(500);
            MStatus = MachineWorkStatus.MACHINE_IDLE;
            e = new msgTMEventArg();
            e.MachineNo = this.MachineNo;
            e.ErrorHappened = false;
            e.TMStatus = MStatus;
            if (EventTMStatusUpdated != null)
            {
                EventTMStatusUpdated(this, e);
            }
            Thread.Sleep(500);
        }

        /// <summary>
        /// 执行MOVL指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstMovl(InstMovL inst)
        {
            Thread.Sleep(500);
            int startL= currentCoord.L;
            if (inst.targetL > startL)
            {
                for (int i = startL; i <= inst.targetL; i++)
                {
                    _currentCoord.L = i;
                    msgTMEventArg e = new msgTMEventArg();
                    e.MachineNo = this.MachineNo;
                    e.ErrorHappened = false;
                    e.TMPos = _currentCoord;
                    if (EventTMPosUpdated != null)
                    {
                        EventTMPosUpdated(this, e);
                    }
                    Thread.Sleep(300);
                }
            }
            else if (inst.targetL < startL)
            {
                for (int i = startL; i >= inst.targetL; i--)
                {
                    _currentCoord.L= i;
                    msgTMEventArg e = new msgTMEventArg();
                    e.MachineNo = this.MachineNo;
                    e.ErrorHappened = false;
                    e.TMPos = _currentCoord;
                    if (EventTMPosUpdated != null)
                    {
                        EventTMPosUpdated(this, e);
                    }
                    Thread.Sleep(300);
                }
            }
        }

        /// <summary>
        /// 执行MOVC指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstMovc(InstMovC inst)
        {
            Thread.Sleep(500);
            int startC = currentCoord.C;
            if (inst.targetC > startC)
            {
                for (int i = startC; i <= inst.targetC; i++)
                {
                    _currentCoord.C = i;
                    msgTMEventArg e = new msgTMEventArg();
                    e.MachineNo = this.MachineNo;
                    e.ErrorHappened = false;
                    e.TMPos =_currentCoord;
                    if (EventTMPosUpdated != null)
                    {
                        EventTMPosUpdated(this, e);
                    }
                    Thread.Sleep(300);
                }
            }
            else if (inst.targetC < startC)
            {
                for (int i = startC; i >= inst.targetC; i--)
                {
                    _currentCoord.C = i;
                    msgTMEventArg e = new msgTMEventArg();
                    e.MachineNo = this.MachineNo;
                    e.ErrorHappened = false;
                    e.TMPos = _currentCoord;
                    if (EventTMPosUpdated != null)
                    {
                        EventTMPosUpdated(this, e);
                    }
                    Thread.Sleep(300);
                }
            }
        }

        /// <summary>
        /// 执行LOAD指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstLoad(InstLoad inst)
        {
            if (_currentTaskObj.taskCode == TaskCode.TASK_PRODUCT_OUTHOUSE)
            {
                MStatus = MachineWorkStatus.PRODUCT_OUTHOUSE_OK;
                msgTMEventArg e = new msgTMEventArg();
                e.MachineNo = this.MachineNo;
                e.ErrorHappened = false;
                e.TMStatus = MStatus;
                if (EventTMStatusUpdated != null)
                {
                    EventTMStatusUpdated(this, e);
                }
            }
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 执行UNLOAD指令
        /// </summary>
        /// <param name="inst"></param>
        private void ExecuteInstUnload(InstUnload inst)
        {
            Thread.Sleep(1000);
            if(_currentTaskObj.taskCode == TaskCode.TASK_PRODUCT_INHOUSE)
            {
                MStatus = MachineWorkStatus.PRODUCT_INHOUSE_OK;
                msgTMEventArg e = new msgTMEventArg();
                e.MachineNo = this.MachineNo;
                e.ErrorHappened = false;
                e.TMStatus = MStatus;
                if (EventTMStatusUpdated != null)
                {
                    EventTMStatusUpdated(this, e);
                }
            }
        }
 
        /// <summary>
        /// 指令执行扫描线程
        /// </summary>
        private void InstExeThreadProc()
        {
            while(!_exitInstExeThread)
            {
                try
                {
                    
                    Monitor.Enter(_instExeLock);
                    //Console.WriteLine("Monitor.Enter(_instExeLock) {0}",MachineNo);
                    Thread.Sleep(_instScanInterval);
                    if ((_currentAutoInstIndex < 0) || (_instList.Count()<=0))
                    {
                        //指令列表为空，或者指令指针
                        continue;
                    }
                    BaseInstInfo baseInst = _instList[_currentAutoInstIndex];
                    ExecuteInst(baseInst);
                    _currentAutoInstIndex++;
                    if (_currentAutoInstIndex >= _instList.Count())
                    {
                        _instList.Clear();
                        _currentAutoInstIndex = 0;
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("InstExeThreadProc error:" + e.Message);
                }
                finally
                {
                  //  Console.WriteLine("Monitor.Exit(_instExeLock)");
                    Monitor.Exit(_instExeLock);
                }  
            }
        }
        #endregion
        #region 公共行为
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public TransVehicle(int machineNo,int SavedInstIndex)
        {
            MachineNo = machineNo;
            _currentCoord.Ch = machineNo;
            _currentAutoInstIndex = SavedInstIndex;
            _instExeThread = new Thread(new ThreadStart(InstExeThreadProc));
            _instExeThread.Name = string.Format("{0}号小车指令监控线程", MachineNo);
            _instExeThread.IsBackground = true;
            _instExeThread.Start();
            Stop();
           // _instExeThread.Suspend();
          

            _bkgWorker = new BackgroundWorker();
            _bkgWorker.WorkerSupportsCancellation = true;//允许中止 
            _bkgWorker.WorkerReportsProgress = true;//允许报告进度
            _bkgWorker.DoWork += bkgworker_DoWork;
            _bkgWorker.ProgressChanged += bkgworker_ProgressChanged;
            _bkgWorker.RunWorkerCompleted += bkgworker_ProgressCompleted;
           
        }
        /// <summary>
        /// 初始化指令列表
        /// </summary>
        /// <param name="instList"></param>
        /// <param name="instBegin">开始的指令序号</param>
        /// <param name="taskObj"></param>
        /// <param name="taskID"></param>
        public void InitInstList(IList<BaseInstInfo> instList, int instBegin,BaseTaskInfo taskObj, string taskID)
        {
            _currentAutoInstIndex = instBegin;
            _instList.Clear();
            _currentTaskID = taskID;
            _currentTaskObj = taskObj;
            foreach (BaseInstInfo instObj in instList)
            {
                this._instList.Add(instObj);
            }
        }
        /// <summary>
        /// 填充指令列表
        /// </summary>
        /// <param name="instList"></param>
        /// <param name="taskObj"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool FillInstList(IList<BaseInstInfo> instList, BaseTaskInfo taskObj, string taskID)
        {
            try
            {
               
                bool reLock = false;
                Monitor.Enter(_instExeLock,ref reLock);
               // Console.WriteLine("Monitor.Enter(_instExeLock), 3");
             //   Console.WriteLine(" Monitor.Enter(_instExeLock,ref reLock),re ={0}",reLock);
                if (MStatus != MachineWorkStatus.MACHINE_IDLE)
                {
                    return false;
                }
                _instList.Clear();
                _currentTaskID = taskID;
                _currentTaskObj = taskObj;
                foreach (BaseInstInfo instObj in instList)
                {
                    this._instList.Add(instObj);
                }
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
            finally
            {
                //Console.WriteLine("Monitor.Exit(_instExeLock)");
                Monitor.Exit(_instExeLock);  
            }  
        }

        /// <summary>
        /// 是否能接受新的任务
        /// </summary>
        /// <returns></returns>
        public bool NewTaskAccepted()
        {
            if(_instList.Count>0)
            {
                return false;
            }
            if(MStatus != MachineWorkStatus.MACHINE_IDLE)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
         //   Console.WriteLine("Monitor.Exit(_instExeLock),start");
            //Monitor.Exit(_instExeLock);
            _instExeThread.Resume();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
           
           // Monitor.Enter(_instExeLock);
            //Console.WriteLine("Monitor.Enter(_instExeLock),stop");
            _instExeThread.Suspend();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            //if(_instExeThread != null && _instExeThread.IsAlive)
            //{
            //    _exitInstExeThread = true;
            //    _instExeThread.Abort();
            //    _instExeThread.Join();
            //}
            //Monitor.Exit(_instExeLock);
        }
        #endregion
    }
}
