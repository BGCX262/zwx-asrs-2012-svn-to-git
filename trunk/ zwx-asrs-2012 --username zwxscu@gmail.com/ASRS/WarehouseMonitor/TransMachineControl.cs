using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using WareHouseControl;
namespace ASRS
{
    
   
    /// <summary>
    /// PLC通信命令码
    /// </summary>
    public enum PlcCmdEnum
    {
        CMD_QUERY = 1,
        CMD_PRODUCT_INHOUSE,
        CMD_PRODUCT_OUTHOUSE,
        CMD_PALLETE_INHOUSE,
        CMD_PALLETE_OUTHOUSE,
    }

  
   
    public class TransMachineControl
    {
        #region 属性

        private int _currentCmdIndex = -1;
        private int _currentManualCmdIndex = -1;
        /// <summary>
        ///  小车工作状态(小车任务里程碑事件）更新事件
        /// </summary>
        public event delegateTMUpdated EventTMStatusUpdated;

        /// <summary>
        /// 小车位置更新事件
        /// </summary>
        public event delegateTMUpdated EventTMPosUpdated;

        //public event msgReceiptHandler msgPlcRecvEvent;
        private int _heartCount = 0; //心跳计数
        private bool _bConnected = false;
        private UdpClient _udpClient;
        private string _plcIp; //远程plc的 ip地址
        private int _plcPort; //远程plc的端口号
        private int _localPort; //本地端口号
        System.Net.IPEndPoint C_Point;//请求信息的主机信息

      //  private Socket _sockPlc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private IPEndPoint _iepLocal;
        private EndPoint _epPlc;
       private  bool _conPlc = true;
        private System.Timers.Timer _MonitoringTimer;
        public int MonitoringInterval { get; set; }

        private Thread _hPlcRecvThread = null;
        public MachineWorkStatus MStatus;
        private List<HouseInOutCmd> _Cmdlist = new List<HouseInOutCmd>();
        public List<HouseInOutCmd> Cmdlist
        {
            get
            {
                return _Cmdlist;
            }
            set
            {
                _Cmdlist = value;
            }
        }

        /// <summary>
        /// 手动指令列表
        /// </summary>
        private List<HouseInOutCmd> _manualCmdList = new List<HouseInOutCmd>();
        public List<HouseInOutCmd> manualCmdList
        {
            get
            {
                return _manualCmdList;
            }
            set
            {
                _manualCmdList = value;
            }
        }
        private WorkMode _workMode = WorkMode.MODE_AUTO;
        public bool IsConnected { get; private set; }
        public int MachineNo { get; set; }
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
        private HouseInOutCmd _CurrentCmd;//当前正在执行的指令
        public HouseInOutCmd CurrentCmd
        {
            get 
            {
                return _CurrentCmd;
            }
            set
            {
                _CurrentCmd = value;
            }
        }
#endregion
        public TransMachineControl(int machineNo,string plcIP,int plcPort,int localPort)
        {
            MachineNo = MachineNo;
            _plcIp = plcIP;
            _plcPort = plcPort;
            _localPort = localPort;
            MonitoringInterval = 500;
            MStatus = MachineWorkStatus.MACHINE_IDLE;
        }
#region 行为
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            return true;
        }

        /// <summary>
        /// 暂停 
        /// </summary>
        /// <returns></returns>
        public bool Pause()
        {
            _hPlcRecvThread.Suspend();
            _MonitoringTimer.Stop();
            _udpClient.Close();
            return true;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {

            _udpClient = new UdpClient(_localPort);
            _udpClient.Connect(_plcIp, _plcPort);
            _bConnected = false;
        
            //创建接收线程
            if(_hPlcRecvThread == null)
            {
                ThreadStart threadDelegat = new ThreadStart(recvPlcProc);
                _hPlcRecvThread = new Thread(threadDelegat);
                _hPlcRecvThread.Name = "PLC接收线程";
                _hPlcRecvThread.Start();
            }
            else
            {
                _hPlcRecvThread.Resume();
            }

            //启动小车状态监控定时器
            if(_MonitoringTimer == null)
            {
                _MonitoringTimer = new System.Timers.Timer(MonitoringInterval);
                _MonitoringTimer.Elapsed += MachineMonitor;
                _MonitoringTimer.Start();
            }
            else
            {
                _MonitoringTimer.Start();
            }
            return true;
        }

        public HouseInOutCmd GetNewCmd()
        {
            if (Cmdlist.Count > (_currentCmdIndex + 1))
            {
                _currentCmdIndex++;
                return Cmdlist[_currentCmdIndex];
            }
            else
                return null;
        }

        public HouseInOutCmd GetNewManualCmd()
        {
            if (_manualCmdList.Count > (_currentManualCmdIndex + 1))
            {
                _currentManualCmdIndex++;
                return _manualCmdList[_currentManualCmdIndex];
            }
            else
                return null;
        }
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool ExecuteCmd(HouseInOutCmd cmd)
        {
            if (cmd == null)
                return false;
            CurrentCmd = cmd;
            udpPackage package = new udpPackage();
            package.frameType = 0x01;
            package.len = 0x07;
            package.cmd = (byte)cmd.Cmdcode;
            package.addr = (byte)MachineNo;
            package.data = new byte[4];
            package.data[0] = (byte)cmd.CellTarget.L;
            package.data[1] = (byte)cmd.CellTarget.R;
            package.data[2] = (byte)(cmd.CellTarget.C&0xff);
            package.data[3] = (byte)((cmd.CellTarget.C>>8)&0xff);
            byte[] byteStream = package.UnPack();
            try
            {
                _udpClient.Send(byteStream, byteStream.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Send ExecuteCmd failed:" + e.Message);
            }
           // _sockPlc.SendTo(byteStream, _epPlc);
            return true;
        }

       
        
        public void Close()
        {

            if (_bConnected)
            {
                _udpClient.Close();
                _bConnected = false;
            }

            _conPlc = false;
     
           // _sockPlc.Close(200);
            if(_MonitoringTimer != null)
            {
                if (_MonitoringTimer.Enabled)
                {
                    _MonitoringTimer.Close();
                }
            }
            if(_hPlcRecvThread != null)
            {
                _hPlcRecvThread.Abort();
            }
        }
        /// <summary>
        /// 切换执行模式,手动/自动
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool SwitchWorkMode(WorkMode m)
        {
            //
            _workMode = m;
            return true;
        }

        /// <summary>
        /// 是否可执行新的任务
        /// </summary>
        /// <returns></returns>
        public bool IsEnableExecuteNewtask()
        {
            if (_workMode == WorkMode.MODE_AUTO && (MStatus == MachineWorkStatus.MACHINE_IDLE))
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// 发送查询命令
        /// </summary>
        private void SendQuerymsg()
        {
            udpPackage package1 = new udpPackage();
            package1.addr = (byte)MachineNo;
            package1.cmd = (byte)PlcCmdEnum.CMD_QUERY;
            package1.frameType = 0x01;
            package1.len = 0x03;
            byte[] byteStream = package1.UnPack();
            //_sockPlc.SendTo(byteStream, _epPlc);
            try
            {
                
                _udpClient.Send(byteStream, byteStream.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("SendQuerymsg failed:" + e.Message);
            }
        }

        /// <summary>
        /// 接收数据线程函数
        /// </summary>
        private void recvPlcProc()
        {
            byte[] data = new byte[1024];
            while (_conPlc)
            {
                 int recv =0;
                 byte[] byteStream = null;
                try
                {
                    
                  //  recv= _sockPlc.ReceiveFrom(data, ref _epPlc);
                    byteStream = _udpClient.Receive(ref  C_Point);
                    _heartCount = 0; //收到数据包，清零，标识连接建立
                    _bConnected = true;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("plc comm:"+e.Message);
                    continue;
                }
               
                //事件参数
               
                udpPackage package = new udpPackage();
              
                for (int i = 0; i < recv; i++)
                {
                    byteStream[i] = data[i];
                }
                if (package.Pack(byteStream))
                {
                    if ((package.frameType == 0x02) && (package.addr == this.MachineNo))
                    {
                        //应答帧,地址正确
                        if (package.cmd == (byte)HouseCmdCode.CMD_QUERY)
                        {
                            MachineWorkStatus newS = (MachineWorkStatus)package.data[3];
                            TMCoord newPos = new TMCoord(package.data[0], this.MachineNo, (package.data[1] + (package.data[2] << 8)));

                            if (newS != MStatus)
                            {
                                if(MStatus == MachineWorkStatus.PRODUCT_INHOUSE_OK)
                                {
                                    string ss = "入库任务"+(_currentCmdIndex+1).ToString()+"完成";
                                    Console.WriteLine(ss);

                                }
                                MStatus = newS;
                            //    string strS = "小车"+MachineNo.ToString()+"状态:"+MStatus.ToString();
                           //     Console.WriteLine(strS);
                                msgTMEventArg e = new msgTMEventArg();
                                e.MachineNo = this.MachineNo;
                                e.ErrorHappened = false;
                                e.TMStatus = newS;
                                if (EventTMStatusUpdated != null)
                                {
                                    EventTMStatusUpdated(this, e);
                                }

                            }
                            if(newPos != this.currentCoord)
                            {
                                msgTMEventArg e = new msgTMEventArg();
                                e.MachineNo = this.MachineNo;
                                e.ErrorHappened = false;
                                e.TMPos = newPos;
                                if(EventTMPosUpdated != null)
                                {
                                    EventTMPosUpdated(this,e);
                                }
                            }
                        }
                        else if (package.cmd == (byte)HouseCmdCode.CMD_PRODUCT_INHOUSE)
                        {

                        }
                        else if (package.cmd == (byte)HouseCmdCode.CMD_PRODUCT_OUTHOUSE)
                        {

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 定时器，监视小车状态
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void MachineMonitor(object source, ElapsedEventArgs e)
        {
            //周期发送查询命令
            SendQuerymsg();
            _heartCount++;
            if(_heartCount>3)
            {
                _bConnected = false;
            }
        }

#endregion
    }
}
