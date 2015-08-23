using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
namespace ASRS
{
    /// <summary>
    /// 托管：rfid读取到信息后，调用此托管实例
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate int delegateRfidinfo(object sender, msgRFIDEventArg e);
    public enum RfidCmdEnum
    {
        CMD_READ = 1, //读数据
        CMD_OBJREMOVE, //通知目标已移除
        CMD_OBJCLEAR //通知清除有故障的目标
    }
     public class msgRFIDEventArg : System.EventArgs
     {
         public bool ErrorHappened = false;
         public string errorString;
         public string rfidInfo;
     }
    public class RFIDControl
    {
        //自定义事件，当收到请求数据时
      //  public event msgReceiptHandler msgRfidRecvEvent;
        public event delegateRfidinfo RfidReadEvent; //RFID读到数据事件
        public string idRead{get;set;} //读取到的产品id信息
        public int RfidScanInterval { get; set; }
        private string _rfidIP = "127.0.0.1";
        public string rfidIP
        {
            get
            {
                return _rfidIP;
            }
            set
            {
                _rfidIP = value;
            }
        }
        private int _heartCount = 0; //心跳计数
        private bool _bConnected = false;
        private string _rfidIp;
        private int _rfidPort;
        private int _localPort;
        private IPEndPoint _iepLocal;
        private EndPoint _epRfid;
        private UdpClient _udpClient;
        System.Net.IPEndPoint C_Point;//请求信息的主机信息
       // private Socket _sockRfid = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
       
        private bool _conRfid = true;//是否继承监听
        private Thread _hRfidRecvThread = null;
        private System.Timers.Timer _rfidScanTimer;
        //建立通信线程，收到产品信息后，调用托管HouseInArrive
        public RFIDControl(string rfidIp,int rfidPort,int localPort)
        {
            RfidScanInterval = 500; //毫秒
            _rfidIp = rfidIp;
            _rfidPort = rfidPort;
            _localPort = localPort;
            idRead = string.Empty;
        }
        public bool Start()
        {

            _iepLocal = new IPEndPoint(IPAddress.Any, _localPort);
            _udpClient = new UdpClient(_localPort);
            _udpClient.Connect(_rfidIp, _rfidPort);
            _bConnected = true;
            SendMsgClearObj();
            if(_rfidScanTimer == null)
            {
                _rfidScanTimer = new System.Timers.Timer(RfidScanInterval);
                _rfidScanTimer.Elapsed += RfidScanTimerHandler;
                _rfidScanTimer.Start();
            }
            else
            {
                _rfidScanTimer.Start();
            }
            if(_hRfidRecvThread == null)
            {
                ThreadStart threadDelegat = new ThreadStart(recvRfidProc);
                _hRfidRecvThread = new Thread(threadDelegat);
                _hRfidRecvThread.IsBackground = true;
                _hRfidRecvThread.Name = "RFID 接收线程";
                _hRfidRecvThread.Start();
                _hRfidRecvThread.Suspend();
            }
            else
            {
                _hRfidRecvThread.Resume();
            }
            return true;
        }
        public bool Pause()
        {
            _hRfidRecvThread.Suspend();
            _rfidScanTimer.Stop();
            _udpClient.Close();

            return true;
        }
        private void SendReadmsg()
        {
            udpPackage package = new udpPackage();
            package.addr = 1;
            package.cmd = (byte)RfidCmdEnum.CMD_READ;
            package.frameType = 0x01;
            package.len = 0x03;
            try
            {
                byte[] byteStream = package.UnPack();
                //_sockRfid.SendTo(byteStream, _epRfid);
                _udpClient.Send(byteStream, byteStream.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("SendReadmsg()"+e.Message);
            }   
        }
        /// <summary>
        /// 产品成功入库，应答
        /// </summary>
        public void SendRemoveObjMsg()
        {
            udpPackage package = new udpPackage();
            package.addr = 1;
            package.cmd = (byte)RfidCmdEnum.CMD_OBJREMOVE;
            package.frameType = 0x01;
            package.len = 0x03;
            try
            {
                byte[] byteStream = package.UnPack();
                //_sockRfid.SendTo(byteStream, _epRfid);
                _udpClient.Send(byteStream, byteStream.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("SendRemoveObjMsg:"+e.Message);
            }   
        }

        /// <summary>
        /// 清除线上产品
        /// </summary>
        public void SendMsgClearObj()
        {
            udpPackage package = new udpPackage();
            package.addr = 1;
            package.cmd = (byte)RfidCmdEnum.CMD_OBJCLEAR;
            package.frameType = 0x01;
            package.len = 0x03;
            try
            {
                byte[] byteStream = package.UnPack();
                //_sockRfid.SendTo(byteStream, _epRfid);
                _udpClient.Send(byteStream, byteStream.Length);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("SendClearObjMsg" + e.Message);
            }   
        }
        private void recvRfidProc()
        {
            //byte[] data = new byte[1024];
            byte[] byteStream = null;
            while (_conRfid)
            {
                int recv = 0;
                try
                {
                    //recv = _sockRfid.ReceiveFrom(data, ref _epRfid);
                    byteStream = _udpClient.Receive(ref C_Point);
                    _bConnected = true;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(" recvRfidProc"+e.Message);
                    continue;
                }
              
                //事件参数
                udpPackage package = new udpPackage();
                if (package.Pack(byteStream))
                {
                    switch(package.cmd)
                    {
                        case 0x01:
                            {
                                ProcessReCmd_query(package);
                                break;
                            }
                        case 0x02:
                            {
                                //正常移出的应答
                                break;
                            }
                        case 0x03:
                            {
                                //强制清除的应答
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    } 
                }
            }
        }

        private void ProcessReCmd_query(udpPackage package)
        {
            //查询命令的应答
            if (package.status != 0x00)
            {
                return;
            }
            if (package.data == null)
            {
                Console.WriteLine("rfid recv error:package.data为空");
                return;
            }
            string newID = System.Text.ASCIIEncoding.UTF8.GetString(package.data);
            if (idRead != newID)
            {
                idRead = newID;
                msgRFIDEventArg e = new msgRFIDEventArg();
                e.ErrorHappened = false;
                e.rfidInfo = newID;
                if (RfidReadEvent != null)
                {
                    int re = RfidReadEvent(this, e);//rfid扫描到新对象, 激活入库事件
                    switch (re)
                    {
                        case 0:
                            {
                                break;
                            }
                        case 1:
                            {
                                SendMsgClearObj();
                                break;
                            }
                        case 2:
                            {
                                SendMsgClearObj();
                                break;
                            }
                        case 3:
                            {
                                SendMsgClearObj();
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 查询RFID数据定时器
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void RfidScanTimerHandler(object source, ElapsedEventArgs e)
        {
            SendReadmsg();
            _heartCount++;
            if (_heartCount > 3)
            {
                _bConnected = false;
            }
        }
        //停止监听
        public void Close()
        {
            _conRfid = false;
            try
            {
                if(_bConnected)
                {
                    _udpClient.Close();
                    _bConnected = false;
                }
                if(this._hRfidRecvThread != null && this._hRfidRecvThread.IsAlive)
                {
                    this._hRfidRecvThread.Abort();
                    this._hRfidRecvThread.Join();
                }
            }
            catch (System.Exception e)
            {
            	
            }
            
           // _sockRfid.Close(200);
            if(_rfidScanTimer != null)
            {
                if(_rfidScanTimer.Enabled)
                {
                    _rfidScanTimer.Close();
                }
            }
            //if(_hRfidRecvThread != null)
            //{
               
            //    _hRfidRecvThread.Abort();
            //}
            
        }
    }
}
