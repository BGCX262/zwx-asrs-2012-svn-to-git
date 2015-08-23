using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WareHouseControl;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using WareHouseControl;
using ASRSDBBLL;
using ASRSDBME;
namespace ASRS
{
    //自定义事件，当收到请求数据时,的委托
    public delegate void msgReceiptHandler(object sender, msgReceiptEventArg e);
    public struct udpPackage
    {
        public byte frameType; //命令帧:1, 应答帧:2
        public byte len;
        public byte addr;
        public byte cmd;
        //public byte lsb;
        // public byte msb;
        public byte[] data;
        public byte status;

        /// <summary>
        /// 打包成网络数据流
        /// </summary>
        /// <returns></returns>
        public byte[] UnPack()
        {
            byte[] byteSteam = new byte[len + 1];
            byteSteam[0] = len;
            byteSteam[1] = frameType;
            byteSteam[2] = addr;
            byteSteam[3] = cmd;
            if (frameType == 1 )
            {
                //命令帧
                if(data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        byteSteam[i + 4] = data[i];
                    }
                }
                
            }
            else if (frameType == 2)
            {
                //应答帧
                byteSteam[4] = status;
                if(data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        byteSteam[i + 5] = data[i];
                    }
                }
            }
            else
            {
                return null;
            }
            return byteSteam;
        }
        /// <summary>
        /// 将数据流解包
        /// </summary>
        /// <param name="byteStream"></param>
        /// <returns></returns>
        public bool Pack(byte[] byteStream)
        {
            len = byteStream[0];
            if ((len + 1) != byteStream.Length)
                return false;
            frameType = byteStream[1];
            addr = byteStream[2];
            cmd = byteStream[3];
           
         
            if (frameType == 1)
            {
                if (len > 3)
                {
                    data = new byte[len - 3];
                }
                if(data != null)
                {
                    for (int i = 0; i < len - 3; i++)
                    {
                        data[i] = byteStream[i + 4];
                    }
                }
                
            }
            else if (frameType == 2)
            {
                if (len > 4)
                {
                    data = new byte[len -4];
                }
                status = byteStream[4];
                if(data != null)
                {
                    for (int i = 0; i < len - 4; i++)
                    {
                        data[i] = byteStream[i + 5];
                    }
                }
                
            }
            else
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 工作模式：手动，自动
    /// </summary>
    public enum WorkMode
    {
        MODE_AUTO = 1,
        MODE_MANUAL
    }
    /// <summary>
    /// 小车状态枚举
    /// </summary>
    public enum MachineWorkStatus
    {
        MACHINE_IDLE = 0, //闲置
        MACHINE_TASK_BEGIN, //开始执行任务
        MACHINE_TASK_END, //任务执行完毕
        PRODUCT_HOUSEIN, //产品正在入库
        PRODUCT_HOUSEOUT, //正在出库
        PRODUCT_INHOUSE_OK, // 产品入库完成
        PRODUCT_OUTHOUSE_OK, //产品出库完成
        PALLETE_HOUSEIN, //空板正在入库
        PALLETE_HOUSEOUT, //空板正在出库
        PALLETE_INHOUSE_OK, // 空板入库完成
        PALLETE_OUTHOUSE_OK, //空板出库完成
        MACHINE_FAULT  //故障
    }
    /// <summary>
    /// 生产线状态
    /// </summary>
    public enum EnumLineRunStatus
    {
        LINE_STOP = 0, //停止未启动
        LINE_RUN, //正在运行
        LINE_PAUSE //暂停
    }
    /// <summary>
    /// 通信协议的命令码
    /// </summary>
    public enum HouseCmdCode
    {
        CMD_EMPTY=0,
        CMD_QUERY ,
        CMD_PRODUCT_INHOUSE,
        CMD_PRODUCT_OUTHOUSE,
        CMD_PALLETE_INHOUSE,
        CMD_PALLETE_OUTHOUSE
    }

    /// <summary>
    /// 指令执行的状态
    /// </summary>
    public enum CmdExecutedStatus
    {
        CMDSTAT_SEND=1, //指令已发送
        CMDSTAT_RESOK, //指令已经成功收到
        CMDSTAT_RESFAILED, //指令发送失败，
        CMDSTAT_RUNNING, //指令运行中
        CMDSTAT_COMPLETED,//指令成功执行完毕
        CMDSTAT_RUNFAILED //指令执行过程中发生错误

    }

    /// <summary>
    /// 出入库指令
    /// </summary>
    public class HouseInOutCmd
    {
        public int Index ;
        public HouseCmdCode Cmdcode;
        public CellPos CellTarget;
        public string ProductNo;
        public CmdExecutedStatus ExecutedStatus;
    }
    //自定义事件参数
    public class msgReceiptEventArg : System.EventArgs
    {
        public bool ErrorHappened = false;
        public string errorString;
        //   public string data;
        public udpPackage package;
        public string requestIP;
        public int requestPort;

    }
    public class ASRSModel
    {
        #region 配置类对象
        /// <summary>
        /// 数据库服务器，PLC,RFID等通讯设置对象
        /// </summary>
        public DBComDevSetting dbComDevSet =new DBComDevSetting();

        /// <summary>
        /// 仓库设置对象
        /// </summary>
        public WarehouseSetting wareHouseSet = new WarehouseSetting();

        /// <summary>
        /// 常规设置对象
        /// </summary>
        public GeneralSetting generalSet = new GeneralSetting();

        private Dictionary<int, TaskingStatusRecord> _taskingRecordDic = new Dictionary<int, TaskingStatusRecord>();
         /// <summary>
        /// 任务记录
        /// </summary>
        public Dictionary<int, TaskingStatusRecord> taskingRecordDic
        {
            get
            {
                return _taskingRecordDic;
            }
            set
            {
                _taskingRecordDic = value;
            }
        }
        #endregion
        #region 包含的数据对象
        /// <summary>
        /// 当前用户角色
        /// </summary>
        public AccountRole currentUserRole{get;set;}

        /// <summary>
        /// 当前用户名
        /// </summary>
        public string currentUserName{get;set;}
       /// <summary>
        ///主程序所在目录
        /// </summary>
        public static string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      
        /// <summary>
        /// 仓储产品数据库接口对象
        /// </summary>
        private WarehouseStoreBLL _warestoreBll = new WarehouseStoreBLL();
        public WarehouseStoreBLL warestoreBll
        {
            get
            {
                return _warestoreBll;
            }
        }

        /// <summary>
        /// 产品数据库接口对象
        /// </summary>
        private ProductBLL _productBll = new ProductBLL();
        public ProductBLL productBll
        {
            get
            {
                return _productBll;
            }
        }

        /// <summary>
        /// 任务列表数据库接口对象
        /// </summary>
        private TaskBLL _taskBll = new TaskBLL();
        public TaskBLL taskBll
        {
            get
            {
                return _taskBll;
            }
        }

        /// <summary>
        /// 消息输出数据库接口对象
        /// </summary>
        private MessageBLL _mesBll = new MessageBLL();
        public MessageBLL mesBll
        {
            get
            {
                return _mesBll;
            }
        }
        /// <summary>
        /// 堆垛机代理对象存储,根据编号索引，从1开始编号
        /// </summary>
        private Dictionary<int, TransVehicle> _transVehicleDic = new Dictionary<int, TransVehicle>();
        public  Dictionary<int, TransVehicle> transVehicleDic
        {
            get
            {
                return _transVehicleDic;
            }
            set
            {
                _transVehicleDic = value;
            }
        }
        
        /// <summary>
        /// 小车任务描述映射表
        /// </summary>
        private Dictionary<int, string> _MachineTaskDic = new Dictionary<int, string>();
        public Dictionary<int,string> MachineTaskDic
        {
            get
            {
                return _MachineTaskDic;
            }
        }

        /// <summary>
        /// 小车状态描述映射表
        /// </summary>
        private Dictionary<int, string> _MachineStatusDic = new Dictionary<int, string>();
        public Dictionary<int,string> MachineStatusDic
        {
            get
            {
                return _MachineStatusDic;
            }
        }

        #endregion
      //多线程下的单例模式
        private static ASRSModel _instance;
        private static object _lock = new object();
        private ASRSModel()
        {
            try
            {
                //int SavedInst1 = XMLConfigRW.ReadSavedInstIndex(1);
                //int SavedInst2 = XMLConfigRW.ReadSavedInstIndex(2);
                TransVehicle transV1 = null;
                TransVehicle transV2 = null;
                if (ReadTaskingRecordFile() == 0)
                {
                    transV1 = new TransVehicle(1, _taskingRecordDic[1].currentInstIndex);
                    transV2 = new TransVehicle(2, _taskingRecordDic[2].currentInstIndex);
                }
                else
                {
                    transV1 = new TransVehicle(1,1);
                    transV2 = new TransVehicle(2, 1);
                }
                _transVehicleDic[1] = transV1;
                _transVehicleDic[2] = transV2;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //小车任务及状态映射表建立
            _MachineTaskDic[(int)TaskCode.TASK_EMPTY] = "空任务";
            _MachineTaskDic[(int)TaskCode.TASK_PRODUCT_INHOUSE] = "产品入库";
            _MachineTaskDic[(int)TaskCode.TASK_PRODUCT_OUTHOUSE] = "产品出库";
            _MachineTaskDic[(int)TaskCode.TASK_PALLETE_INHOUSE] = "空板入库";
            _MachineTaskDic[(int)TaskCode.TASK_PALLETE_OUTHOUSE] = "空板出库";

  
            _MachineStatusDic[(int)MachineWorkStatus.MACHINE_FAULT] = "故障";
            _MachineStatusDic[(int)MachineWorkStatus.MACHINE_IDLE] = "空闲";
            _MachineStatusDic[(int)MachineWorkStatus.MACHINE_TASK_BEGIN] = "开始执行任务";
            _MachineStatusDic[(int)MachineWorkStatus.MACHINE_TASK_END] = "任务执行完毕";
            _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_HOUSEIN] = "产品正在入库";
            _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_HOUSEOUT] = "正在出库";
            _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_INHOUSE_OK] = "入库完成，小车返回";
            _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUTHOUSE_OK] = "出库完成，小车返回";
            _MachineStatusDic[(int)MachineWorkStatus.PALLETE_HOUSEIN] = "空板正在入库";
            _MachineStatusDic[(int)MachineWorkStatus.PALLETE_HOUSEOUT] = "空板正在出库";
            _MachineStatusDic[(int)MachineWorkStatus.PALLETE_INHOUSE_OK] = "空板入库完成";
            _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUTHOUSE_OK] = "空板出库完成，小车返回";

        }
        public static ASRSModel GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ASRSModel();
                    }
                }
            }
            return _instance;
        }

#region 公共行为
        /// <summary>
        /// 读数据库及硬件通讯参数设置文件
        /// </summary>
        /// <returns>0:成功，1:文件不存在，2:读取异常</returns>
        public int ReadDBComSetFile()
        {
            string setFile = appBaseDirectory + @"../config/comparam.bconf";
            if(!File.Exists(setFile))
            {
                return 1;
            }
            using (FileStream fs = new FileStream(setFile, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter(); 
                try
                {
                    dbComDevSet = bf.Deserialize(fs) as DBComDevSetting;
                    if (dbComDevSet == null)
                    {
                        return 2;
                    }
                    return 0;
                }
                catch (System.Exception ex)
                {
                	return 2;
                }
                
            }
            return 0;
        }
        /// <summary>
        /// 写数据库及硬件通讯参数设置文件
        /// </summary>
        /// <returns>0:成功，1:写入异常</returns>
        public int SaveDBComSetFile()
        {
            string setFile = appBaseDirectory + @"../config/comparam.bconf";
            using (FileStream fs = new FileStream(setFile, FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, dbComDevSet);
                }
                catch (System.Exception ex)
                {
                    return 1;
                }
               
            }
            return 0;
        }
        /// <summary>
        /// 读仓库设置文件
        /// </summary>
        /// <returns>0:成功，1:文件不存在，2:读取异常</returns>
        public int ReadWarehouseSetFile()
        {
            string setFile = appBaseDirectory + @"../config/wareHouse.bconf";
            if (!File.Exists(setFile))
            {
                return 1;
            }
            using (FileStream fs = new FileStream(setFile, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    wareHouseSet = bf.Deserialize(fs) as WarehouseSetting;
                    if (wareHouseSet == null)
                    {
                        return 2;
                    }
                    return 0;
                }
                catch (System.Exception ex)
                {
                    return 2;
                }

            }
            return 0;
        }
        /// <summary>
        /// 写仓库设置文件
        /// </summary>
        /// <returns>0:成功，1:写入异常</returns>
        public int SaveWarehouseSetFile()
        {
            string setFile = appBaseDirectory + @"../config/wareHouse.bconf";
            using (FileStream fs = new FileStream(setFile, FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, wareHouseSet);
                }
                catch (System.Exception ex)
                {
                    return 1;
                }

            }
            return 0;
        }

        /// <summary>
        /// 读任务记录文件
        /// </summary>
        /// <returns>0:成功，1:文件不存在，2:读取异常</returns>
        public int ReadTaskingRecordFile()
        {
            for(int i=1;i<=2;i++)
            {
                StringBuilder fstrBuilder = new StringBuilder();
                fstrBuilder.AppendFormat(@"../config/taskingRecord{0}.recrd",i);
                string recordFile = appBaseDirectory + fstrBuilder.ToString();
                if (!File.Exists(recordFile))
                {
                    return 1;
                }
                using (FileStream fs = new FileStream(recordFile, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        /// <summary>
                        /// 堆垛机1的任务记录
                        /// </summary>
                   
                        TaskingStatusRecord taskingRecord  = bf.Deserialize(fs) as TaskingStatusRecord;
                        if (taskingRecord == null)
                        {
                            return 2;
                        }
                        _taskingRecordDic[taskingRecord.machineID] = taskingRecord;
                    }
                    catch (System.Exception ex)
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 保存任务记录
        /// </summary>
        /// <returns></returns>
        public int SaveTaskingRecordFile(int machineID)
        {
            StringBuilder fstrBuilder = new StringBuilder();
            fstrBuilder.AppendFormat(@"../config/taskingRecord{0}.recrd", machineID);
            string recordFile = appBaseDirectory + fstrBuilder.ToString();
            using (FileStream fs = new FileStream(recordFile, FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, _taskingRecordDic[machineID]);
                }
                catch (System.Exception ex)
                {
                    return 1;
                }
            }
        
            return 0;
        }
        public int HouseCoordConvertID(int L,int R,int C)
        {
            if (L < 1 || R < 1 || C < 1)
                return -1;
            int houseID = (L - 1) * (wareHouseSet.channelCount * 2 * wareHouseSet.columnCount) + (R - 1) * wareHouseSet.columnCount + C;
            return houseID;
        }
#endregion
    }
}
