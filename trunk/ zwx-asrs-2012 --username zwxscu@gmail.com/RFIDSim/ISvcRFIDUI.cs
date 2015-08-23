using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDSim
{
    /// <summary>
    /// 服务员RFID信息现实的接口
    /// </summary>
    public interface ISvcRFIDUI
    {
        bool SendRemsg(udpPackage package, string ip, int port);
        /// <summary>
        /// 动态生成运行信息
        /// </summary>
        /// <param name="MachineIndex">小车编号，从1开始</param>
        /// <param name="str">动态运行信息</param>
        void AppendRuninfo(string str);

        /// <summary>
        /// 是否显示扫描物件
        /// </summary>
        /// <param name="bShow">true：实体显示，false：边框显示</param>
        void ShowScanobj(bool bShow);
    }
}
