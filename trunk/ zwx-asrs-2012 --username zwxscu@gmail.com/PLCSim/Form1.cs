using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
namespace PLCSim
{
    public delegate void delegateRefreshUI(int machineIndex,string str);
    public delegate void ThreadUIHandle(msgReceiptEventArg eventData);
    public partial class Form1 : Form,ISvcTransMachine
    {
        /// <summary>
        /// 小车任务描述映射表
        /// </summary>
        private Dictionary<int, string> _MachineTaskDic = new Dictionary<int, string>();
        public Dictionary<int, string> MachineTaskDic
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
        public Dictionary<int, string> MachineStatusDic
        {
            get
            {
                return _MachineStatusDic;
            }
        }
        private TransMachine _transMachine1 = null;//new TransMachine(this,1);
        private TransMachine _transMachine2 = null;//new TransMachine(this,2);
        private UDPServer _udpServer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _transMachine1 = new TransMachine(this,1);
            _transMachine2 = new TransMachine(this,2);
        //   IPHostEntry   tempHost   =   new   IPHostEntry();   
         //  tempHost   =   Dns.Resolve(Dns.GetHostName()); 
       //    _udpServer = new UDPServer(tempHost.AddressList[0].ToString(), 10000);
            _udpServer = new UDPServer(10000);
           if(!_udpServer.StartListen())
           {
               this.labelUdpstatus.Text = "通讯服务启动失败";
               
           }
           else
           {
               this._udpServer.msgReceiptEvent += UdprecvEventHandler;
               this.labelUdpstatus.Text = "通讯服务已启动";
           }

           //小车任务及状态映射表建立
           _MachineTaskDic[(int)HouseCmdCode.CMD_EMPTY] = "空任务";
           _MachineTaskDic[(int)HouseCmdCode.CMD_PRODUCT_INHOUSE] = "产品入库";
           _MachineTaskDic[(int)HouseCmdCode.CMD_PRODUCT_OUTHOUSE] = "产品出库";
           _MachineTaskDic[(int)HouseCmdCode.CMD_PALLETE_INHOUSE] = "空板入库";
           _MachineTaskDic[(int)HouseCmdCode.CMD_PALLETE_OUTHOUSE] = "空板出库";

           _MachineStatusDic[(int)MachineWorkStatus.MACHINE_FAULT] = "故障";
           _MachineStatusDic[(int)MachineWorkStatus.MACHINE_IDLE] = "空闲";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_IN_PREPARE] = "产品入库准备";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_IN_LOADED] = "已装货—入库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_INCOMING] = "产品正在入库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_IN_UNLOADED] = "正在卸货—入库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_INHOUSE_OK] = "入库完成，小车返回";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUT_PREPARE] = "产品出库准备";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUT_LOADED] = "已装货—出库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUTCOMING] = "产品正在出库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUT_UNLOADED] = "正在卸货—出库";
           _MachineStatusDic[(int)MachineWorkStatus.PRODUCT_OUTHOUSE_OK] = "出库完成，小车返回";

           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_IN_PREPARE] = "空板入库准备";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_IN_LOADED] = "已装板—空板入库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_INCOMMING] = "空板正在入库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_IN_UNLOADED] = "正在卸板—空板入库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_INHOUSE_OK] = "空板入库完成，小车返回";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUT_PREPARE] = "空板出库准备";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUT_LOADED] = "已装板—出库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUTCOMMING] = "空板正在出库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUT_UNLOADED] = "正在卸板—空板出库";
           _MachineStatusDic[(int)MachineWorkStatus.PALLETE_OUTHOUSE_OK] = "空板出库完成，小车返回";
        }
        private void UdprecvEventHandler(object sender,msgReceiptEventArg e)
        {
            this.Invoke(new ThreadUIHandle(ThreadUIHandleMethod),e);
        }

        private void ThreadUIHandleMethod(msgReceiptEventArg eventData)
        {
            if (eventData.ErrorHappened)
            {
                this.labelUdpstatus.Text = "通讯服务发生错误，已断开";
            }
            else
            {
                udpPackage package = new udpPackage();
                if(package.Pack(eventData.data))
                {
                    if(package.addr == 1)
                    {
                        try
                        {
                            _transMachine1.RecvCmd(package, eventData.requestIP, eventData.requestPort);
                        }
                        catch (System.Exception e)
                        {
                            Console.WriteLine("_transMachine1.RecvCmd:" + e.Message);
                        }
                       
                    }
                    else if(package.addr == 2)
                    {
                        _transMachine2.RecvCmd(package, eventData.requestIP, eventData.requestPort);
                    }
                }

            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._udpServer.CloseListen();
        }
#region 穿梭车服务接口实现部分
        public bool SendRemsg(udpPackage package, string ip, int port)
        {
            byte[] byteStream = package.UnPack();
            if (byteStream == null)
            {
                return false;
            }
            else
            {
                return _udpServer.SendData(byteStream, byteStream.Length, ip, port);
            }
          
        }
        public void AppendRuninfo(int MachineIndex, string str)
        {
            if(MachineIndex == 1)
            {
                this.richTextBox1.AppendText(str);
            }
            else if(MachineIndex == 2)
            {
                this.richTextBox2.AppendText(str);
            }
        }
        private void ThreadUIDispMachineStatus(int MachineIndex,string str)
        {
            if(MachineIndex == 1)
            {
                this.labelCurrentStatus1.Text = str;
            }
            else if(MachineIndex == 2)
            {
                this.labelCurrentStatus2.Text = str;
            }
        }
        public void DispMachineStatus(int MachineIndex,MachineWorkStatus workStatus)
        {

            this.Invoke(new delegateRefreshUI(ThreadUIDispMachineStatus), MachineIndex, _MachineStatusDic[(int)workStatus]);
        }
         private void ThreadUIDispMachinePos(int MachineIndex,string str)
         {
             if (MachineIndex == 1)
             {
                 this.labelCurrentCoord1.Text = str;
             }
             else if (MachineIndex == 2)
             {
                 this.labelCurrentCoord2.Text = str;
             }
         }
        public void RefreshMachinePos(int MachineIndex, int L, int C)
        {
            string strPos = L.ToString() + "," + "  " + "," + C.ToString();
            Console.WriteLine(MachineIndex.ToString()+"号小车:"+strPos);
            this.Invoke(new delegateRefreshUI(ThreadUIDispMachinePos), MachineIndex,strPos );
        }
        private void ThreadUIRefreshTaskDisp(int MachineIndex,string str)
        {
            if (MachineIndex == 1)
            {
                  this.labelCurrentTask1.Text = str;
                
            }
            else if (MachineIndex == 2)
            {
                 this.labelCurrentTask2.Text = str;
                //this.labelTargetCoord2.Text = targetPos.L.ToString() + "," + targetPos.R.ToString() + "," + targetPos.C.ToString();
            }
        }
        private void ThreadUIRefreshTargetCell(int MachineIndex,string str)
        {
            if(MachineIndex == 1)
            {
                this.labelTargetCoord1.Text = str;
            }
            else if(MachineIndex == 2)
            {
                this.labelTargetCoord2.Text = str;
            }
        }
        public void RefreshTaskDisp(int MachineIndex, CellPos targetPos, string taskDes)
        {
                this.Invoke(new delegateRefreshUI(ThreadUIRefreshTaskDisp), MachineIndex,taskDes);
                this.Invoke(new delegateRefreshUI(ThreadUIRefreshTargetCell), MachineIndex,targetPos.L.ToString() + "," + targetPos.R.ToString() + "," + targetPos.C.ToString());
        }
#endregion
    }
}
