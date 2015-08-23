using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WareHouseControl;
using System.Data;
namespace ASRS
{
    /// <summary>
    /// 工作模式事件参数
    /// </summary>
    public class  SwitchModeEventArgs: EventArgs
    {
        public int workMode = 1; //1:手动，2：自动
    }

    /// <summary>
    /// 仓位信息显示事件参数
    /// </summary>
    public class CellEventArgs:EventArgs
    {
         public readonly CellPos CurCellCoord;
         public CellEventArgs(CellPos cellCoord)
        {
            CurCellCoord = cellCoord;
        }
    }

    /// <summary>
    /// 手动出库生效事件的仓位列表
    /// </summary>
    public class CellHouseoutListEventArgs:EventArgs
    {
        public readonly IList<CellPos> cellList;
        public CellHouseoutListEventArgs(IList<CellPos> cells)
        {
            cellList = cells;
        }
    }
    /// <summary>
    /// 货架设置修改消息参数
    /// </summary>
    public class WareHouseAttrModifyEventArgs : EventArgs
    {
        public int layerCount = 4;
        public int channelCount = 2;
        public int columnCount = 50;
        public WareHouseAttrModifyEventArgs()
        {

        }
        public WareHouseAttrModifyEventArgs(int layer, int ch, int col)
        {
            layerCount = layer;
            channelCount = ch;
            columnCount = col;
        }
    }
    /// <summary>
    /// 仓位显示信息类
    /// </summary>
    public class CellDispInfo
    {
        public string titleText; //标题
        public string contentText; //正文
        //public string param1LabelText; //参数1标签
        //public string param1ValueText; //参数1数据
        //public string param2LabelText; //参数2标签
        //public string param2ValueText; //参数2数据
    }

    public interface IWarehouseMonitorView:IViewBase
    {
#region UI事件
        /// <summary>
        /// 初始化事件
        /// </summary>
        event EventHandler eventInit;

        /// <summary>
        /// 启动/暂停事件
        /// </summary>
        event EventHandler eventStartPause;

        /// <summary>
        /// 退出事件
        /// </summary>
        event EventHandler eventExit;

        /// <summary>
        /// 停止事件
        /// </summary>
        event EventHandler eventStop;

        /// <summary>
        /// 切换工作模式事件
        /// </summary>
        event EventHandler eventSwitchMode;

        /// <summary>
        /// 仓位信息弹出显示事件
        /// </summary>
        event EventHandler<CellEventArgs> eventCellPopupDisp;

        /// <summary>
        /// 单仓位出库请求，设置某仓位手动出库时，先发送此事件以求证是否允许
        /// </summary>
        event EventHandler<CellEventArgs> eventHouseoutRequire;

        /// <summary>
        /// 手动出库应用的事件，包含了出库仓位的列表
        /// </summary>
        event EventHandler<CellHouseoutListEventArgs> eventHouseoutApply;

        /// <summary>
        /// 货架属性修改事件
        /// </summary>
        event EventHandler<WareHouseAttrModifyEventArgs> eventWHouseAttrModify;
#endregion
#region 功能函数
        /// <summary>
        /// 刷新视图
        /// </summary>
        void RefreshWarehouse();
        /// <summary>
        /// 刷新小车位置
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="pos"></param>
        void RefreshMachineCoord(int MachineNo,TMCoord  pos);

        /// <summary>
        /// 刷新小车状态
        /// </summary>
        void RefreshMachineStatus(int MachineNo, string strStatus);

        /// <summary>
        /// 刷新货架单元状态
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        /// <param name="C"></param>
        /// <param name="s"></param>
        void RefreshHouseCellStatus(int L, int R, int C, CellStoreStatus s);

        /// <summary>
        /// 刷新小车任务
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <param name="strTask"></param>
        void RefreshMachineTask(int MachineNo, string strTask);

        /// <summary>
        /// 弹出显示仓位信息
        /// </summary>
        /// <param name="cellInfo"></param>
        void PopupDispCellinfo(CellPos pos,CellDispInfo cellInfo);

        /// <summary>
        /// 刷新工作模式：手动或自动
        /// </summary>
        /// <param name="mode"></param>
        void RefreshWorkMode(WorkMode mode);

        /// <summary>
        /// 刷新生产线状态
        /// </summary>
        /// <param name="lineStatus">生产线状态:启动，暂停，停止</param>
        void RefreshLineStatus(EnumLineRunStatus lineStatus);

        /// <summary>
        /// 增加出库列表项，对出库请求的应答，若通过则会调用此接口，实现列表项增加
        /// </summary>
        /// <param name="houseID"></param>
        /// <param name="cellL"></param>
        /// <param name="cellR"></param>
        /// <param name="cellC"></param>
        void HouseoutListitemAdd(int houseID, int cellL, int cellR, int cellC);

        /// <summary>
        /// 入库列表请求结果应答
        /// </summary>
        /// <param name="re"> 0:列表指令成功添加，>0：有误的列表项序号</param>
        void ResHouseoutListApply(int re);

        /// <summary>
        /// 弹出提示框，一般用来提示，或者需要用户的确认
        /// </summary>
        /// <param name="titleText">标题信息</param>
        /// <param name="contentText">正文信息</param>
        /// <param name="buttonNum">显示按钮个数：1：只有确认按钮，
        /// 2："是，否"两个按钮，3："是，否，取消"3个按钮</param>
           ///< return> 0:确定，1：是，2：否，3：取消 </return>
        int ShowReMessageBox(string titleText, string contentText, int buttonNum);

        /// <summary>
        /// 显示初始化进度
        /// </summary>
        /// <param name="progress"></param>
        void DispInitProgress(int progress);

        /// <summary>
        /// 显示状态信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str"></param>
        void DispStatusbarInfo(int index, string str);

        /// <summary>
        /// 刷新任务列表
        /// </summary>
        /// <param name="dt"></param>
        void RefreshTaskDisp(DataTable dt);

        /// <summary>
        /// 摔性能指令列表
        /// </summary>
        /// <param name="machineIndex">小车编号，从1开始</param>
        void RefreshInstDisp(int machineIndex, DataTable dt);
#endregion
    }
}
