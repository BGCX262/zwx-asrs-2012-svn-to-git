using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
namespace PLCSim
{
   
    public struct udpPackage
    {
        public byte frameType; //命令帧:1, 应答帧:2
        public byte len ;
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
        public  byte[] UnPack()
        {
            byte[] byteSteam = null;
            try
            {
                byteSteam = new byte[len + 1];
                byteSteam[0] = len;
                byteSteam[1] = frameType;
                byteSteam[2] = addr;
                byteSteam[3] = cmd;
                if (frameType == 1)
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
                
            }
            catch (System.Exception e)
            {
                Console.Write("plc comm Unpack: " + e.Message);
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
            try
            {
                len = byteStream[0];
                if ((len + 1) != byteStream.Length)
                    return false;
                frameType = byteStream[1];
                addr = byteStream[2];
                cmd = byteStream[3];
                if(len>3)
                    data = new byte[len - 3];
                if (frameType == 1)
                {
                    if (data != null)
                    {
                        for (int i = 0; i < len - 3; i++)
                        {
                            data[i] = byteStream[i + 4];
                        }
                    }

                }
                else if (frameType == 2)
                {
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
            }
            catch (System.Exception e)
            {
                Console.WriteLine("plc comm pack:" + e.Message);
            }
        
            return true;
        }
    }
    //自定义事件，当收到请求数据时,的委托
    public delegate void msgReceiptHandler(object sender, msgReceiptEventArg e);

    //自定义事件参数
    public class msgReceiptEventArg : System.EventArgs
    {
        public bool ErrorHappened = false;
        public string errorString;
     //   public string data;
        public byte[] data ;
        public string requestIP;
        public int requestPort;

    }

    class UDPServer
    {
        private string state = "close";

        public string State
        {
            get { return state; }
        }

        //自定义事件，当收到请求数据时
        public event msgReceiptHandler msgReceiptEvent;

        System.Net.IPAddress IP; //要进行监听的IP
        System.Net.IPEndPoint S_Point; //要进行监听的端口
        System.Net.Sockets.UdpClient sv;//用于监听的UDP实列

        System.Net.IPEndPoint C_Point;//请求信息的主机信息

        bool con = true;//是否继承监听
        private Thread _hRecvThread = null;
        //构造函数
        public UDPServer(int port) //string hostIP, 
        {
            //IP = System.Net.IPAddress.Parse(hostIP);
            S_Point = new System.Net.IPEndPoint(System.Net.IPAddress.Any, port);
            sv = new System.Net.Sockets.UdpClient(port);
        }
        public bool StartListen()
        {
            ThreadStart threadDelegat = new ThreadStart(recvProc);
            _hRecvThread = new Thread(threadDelegat);
            _hRecvThread.Name = "PLC端udp接收线程";
            _hRecvThread.Start();
            return true;
        }
        //停止监听
        public void CloseListen()
        {

            con = false;//是否继承监听标志为假
            sv.Close(); //关闭用于监听的UDP实列
            state = "close";
        }
        public bool SendData(byte[] dgram, int lenth,string TargetIP,int TargetPort)
        {
            IPAddress HostIP = IPAddress.Parse(TargetIP);
            IPEndPoint host = new IPEndPoint(HostIP, TargetPort);
            sv.Send(dgram, lenth, host);
            return true;
        }
        //开始监听
        private void recvProc()
        {
            while(con)
            {
                state = "open";
                try
                {
                    //Receive方法将阻塞进行，直到得到请求信息
                    //参数ref C_Point将得到请求信息的主机信息
                    byte[] msg = sv.Receive(ref C_Point);
                    //事件参数
                    msgReceiptEventArg eRecv = new msgReceiptEventArg();
                    eRecv.data = new byte[msg.Length];
                    eRecv.data = msg;
                    //e.data = System.Text.Encoding.UTF8.GetString(msg);
                    eRecv.requestIP = C_Point.Address.ToString();
                    eRecv.requestPort = C_Point.Port;
                    eRecv.ErrorHappened = false;
                    //如果有事件绑定，就触发事件
                    if (msgReceiptEvent != null)
                    {
                        msgReceiptEvent(this, eRecv); 
                    }
                }
                catch(Exception e)
                {
                    CloseListen();
                    //msgReceiptEventArg e1 = new msgReceiptEventArg();
                    //e1.ErrorHappened = true;
                    //e1.errorString = e.Message;
                    //if (msgReceiptEvent != null)
                    //{
                    //    msgReceiptEvent(this, e1);
                    //}
                    
                }
              
            }
        }
    }
}
