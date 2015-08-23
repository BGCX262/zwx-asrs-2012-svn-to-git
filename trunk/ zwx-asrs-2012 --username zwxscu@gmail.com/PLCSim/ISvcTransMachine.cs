using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCSim
{
    
    /// <summary>
    /// 为穿梭车提供服务的接口，发送应答帧，状态更新显示
    /// </summary>
    public interface ISvcTransMachine
    {
        bool SendRemsg(udpPackage package,string ip,int port);

        /// <summary>
        /// 动态生成运行信息
        /// </summary>
        /// <param name="MachineIndex">小车编号，从1开始</param>
        /// <param name="str">动态运行信息</param>
        void AppendRuninfo(int MachineIndex, string str);

        /// <summary>
        /// 更新显示小车工作状态
        /// </summary>
        /// <param name="workStatus"></param>
        void DispMachineStatus(int MachineIndex,MachineWorkStatus workStatus);

        /// <summary>
        /// 刷新小车位置显示
        /// </summary>
        /// <param name="MachineIndex"></param>
        /// <param name="L"></param>
        /// <param name="C"></param>
        void RefreshMachinePos(int MachineIndex, int L, int C);

        /// <summary>
        /// 刷新小车任务描述
        /// </summary>
        /// <param name="MachineIndex"></param>
        /// <param name="targetPos"></param>
        /// <param name="taskDes"></param>
        void RefreshTaskDisp(int MachineIndex, CellPos targetPos, string taskDes);
    }
}
