using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ASRSDBBLL;
using ASRSDBME;
namespace RFIDSim
{
    public enum CmdCode
    {
        CMD_READ = 1, //读数据
        CMD_OBJREMOVE, //通知目标已移除
        CMD_CLEAR  //清除线上对象
    }
    public enum ModeEnum
    {
        MODE_MANUAL = 1, //手动模式
        MODE_AUTO  //自动模式
    }
    public struct RfidObj
    {
        public string id; 
        public bool bScaned;//标记是否已经被扫描过
    }
    public class ReaderDev
    {
        private WarehouseStoreBLL _warestoreBll = new WarehouseStoreBLL();
        private ProductBLL _productBll = new ProductBLL();
        private string[]  _productCateArray = null;
        private string _requestIP; //请求端的IP地址
        private int _requestPort; //请求端的端口号
        public byte Addr = 1;
        private ModeEnum _WorkMode = ModeEnum.MODE_AUTO;
        public ModeEnum WorkMode
        {
            get
            {
                return _WorkMode;
            }
            set
            {
                _WorkMode = value;
            }
        }
        private ISvcRFIDUI _svc;
        private RfidObj _rfidObj = new RfidObj();
        private bool _bEnableIn = true; //有空位，允许物件进入扫描区
        private Timer _AutoTimer; //自动生成待扫描对象的定时器
        public int TimerInterval{get;set;}
        public ReaderDev(ISvcRFIDUI ISvc,byte  addr)
        {
            Addr = addr;
            _svc = ISvc;
            TimerInterval = 1000;
            _AutoTimer = new Timer(TimerInterval);
            _AutoTimer.Elapsed += AutoTimerHandler;
            _AutoTimer.Start();
            DataSet ds = _productBll.GetProductInfoList();
            DataTable dt = ds.Tables[0];
            _productCateArray = new string[dt.Rows.Count];
            int i=0;
            foreach(DataRow dr in dt.Rows)
            {
                _productCateArray[i++] = dr["productType"].ToString();
            }

        }
        public void Clear()
        {
             //通知物件已经移走
            _svc.ShowScanobj(false);
            _bEnableIn = true;

            _svc.AppendRuninfo("产品:" + _rfidObj.id + "产品ID 信息错误，产品已经清除" + "\r\n");
        }
         public void RecvCmd( udpPackage package,string requestIP,int requestPort)
         {
             lock(this)
             {
                 _requestIP = requestIP;
                _requestPort = requestPort;
                 if(package.addr == Addr)
                 {
                     switch(package.cmd)
                     {
                         case (byte)CmdCode.CMD_READ:
                             {
                                 //查询
                                 udpPackage rePackage = new udpPackage();
                                 rePackage.len = 0x18;
                                 rePackage.frameType = 0x02;
                                 rePackage.addr = Addr;
                                 rePackage.cmd = package.cmd;
                                 if(_rfidObj.bScaned == false)
                                 {
                                     //扫描到
                                     rePackage.status = 0x00;
                                     rePackage.data = new byte[20];
                                     string s;
                                     if (_rfidObj.id.Length >= 20)
                                     {
                                         s = _rfidObj.id.Substring(0, 20);
                                     }
                                     else
                                     {
                                         s = _rfidObj.id.PadLeft(20, '0');
                                     }
                                     rePackage.data = System.Text.ASCIIEncoding.UTF8.GetBytes(s);
                                     _rfidObj.bScaned = true;
                                     //_rfidObj.id = string.Empty;
                                     _svc.AppendRuninfo("识别ID:" + s + "\r\n");
                                 }
                                 else
                                 {
                                     //该产品已经扫描过了
                                   
                                     rePackage.status = 0x01;
                                     string s = new string('0', 20);
                                     rePackage.data = System.Text.ASCIIEncoding.UTF8.GetBytes(s);
                                 }
                                 _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                                 break;
                             }
                         case (byte)CmdCode.CMD_OBJREMOVE:
                             {
                                 //通知物件已经移走
                                 _svc.ShowScanobj(false);
                                 _bEnableIn = true;
                                 
                                 //应答
                                 udpPackage rePackage = new udpPackage();
                                 rePackage.len = 0x04;
                                 rePackage.frameType = 0x02;
                                 rePackage.addr = Addr;
                                 rePackage.cmd = package.cmd;
                                 rePackage.status = 0x00;
                                 _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                                 _svc.AppendRuninfo("产品:" + _rfidObj.id + "已经取走" + "\r\n");
                                 break;
                             }
                         case (byte)CmdCode.CMD_CLEAR:
                             {
                                 //通知物件已经移走
                                 _svc.ShowScanobj(false);
                                 _bEnableIn = true;

                                 //应答
                                 udpPackage rePackage = new udpPackage();
                                 rePackage.len = 0x04;
                                 rePackage.frameType = 0x02;
                                 rePackage.addr = Addr;
                                 rePackage.cmd = package.cmd;
                                 rePackage.status = 0x00;
                                 _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                                 _svc.AppendRuninfo("产品:" + _rfidObj.id + "产品ID 信息错误，产品已经清除" + "\r\n");
                                 break;
                             }
                         default:
                             break;
                     }
                 }
             }
         }
        private void  AutoTimerHandler(object source, ElapsedEventArgs e)
        {
            if (_WorkMode != ModeEnum.MODE_AUTO)
                return;
            lock(this)
            {
                if (_bEnableIn)
                {
                    _rfidObj.id = DateTime.UtcNow.ToString();
                    //产品记录到数据库
                    ProductStoreME m = new ProductStoreME();
                    string s;
                    if (_rfidObj.id.Length >= 20)
                    {
                        s = _rfidObj.id.Substring(0, 20);
                    }
                    else
                    {
                        s = _rfidObj.id.PadLeft(20, '0');
                    }
                    m.productID = s;
                 
                    Random rdm = new Random();
                    int i=rdm.Next(0, _productCateArray.Length);
                    m.productType = _productCateArray[i];
                    _productBll.AddProductStore(m);
                    _rfidObj.bScaned = false;
                    _bEnableIn = false;
                }
            }
         
        }
    }
}
