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
namespace RFIDSim
{
    public partial class Form1 : Form,ISvcRFIDUI
    {
        delegate void ThreadUIHandle(msgReceiptEventArg eventData);
        private UDPServer _udpServer;
        private ReaderDev _rfidReader;
        public Form1()
        {
            InitializeComponent();
            _rfidReader = new ReaderDev(this,1);
        }
        private void UdprecvEventHandler(object sender, msgReceiptEventArg e)
        {
            this.Invoke(new ThreadUIHandle(ThreadUIHandleMethod), e);
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
                 if (package.Pack(eventData.data))
                 {
                     _rfidReader.RecvCmd(package, eventData.requestIP, eventData.requestPort);
                 }
             }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
          //  this.buttonClear.Enabled = false;
           // IPHostEntry tempHost = new IPHostEntry();
            //tempHost = Dns.Resolve(Dns.GetHostName()); 
            _udpServer = new UDPServer(IPAddress.Any.ToString(), 20000);
            if (!_udpServer.StartListen())
            {
                this.labelUdpstatus.Text = "通讯服务启动失败";

            }
            else
            {
                this._udpServer.msgReceiptEvent += UdprecvEventHandler;
                this.labelUdpstatus.Text = "通讯服务已启动";
            }
        }
#region svcRFIDUI接口实现
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
        public void AppendRuninfo(string str)
        {
            this.richTextBox1.AppendText(str);
        }
        public  void ShowScanobj(bool bShow)
        {

        }
#endregion
        private void radioMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if(radio.Checked == true)
            {
                switch(radio.Text)
                {
                    case "手动":
                        {
                            this._rfidReader.WorkMode = ModeEnum.MODE_MANUAL;
                         
                            this.buttonRestart.Enabled = false;
                         //   this.buttonClear.Enabled = true;
                            break;
                        }
                    case "自动":
                        {
                            this._rfidReader.WorkMode = ModeEnum.MODE_AUTO;
                           // this.buttonClear.Enabled = false;
                         
                            this.buttonRestart.Enabled = true;
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _udpServer.CloseListen();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _rfidReader.Clear();
        }
    }
}
