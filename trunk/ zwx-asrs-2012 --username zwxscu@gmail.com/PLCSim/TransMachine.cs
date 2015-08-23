using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers; 
namespace PLCSim
{
    /// <summary>
    /// 通信协议的命令码
    /// </summary>
    public enum HouseCmdCode
    {
        CMD_EMPTY = 0,
        CMD_QUERY,
        CMD_PRODUCT_INHOUSE,
        CMD_PRODUCT_OUTHOUSE,
        CMD_PALLETE_INHOUSE,
        CMD_PALLETE_OUTHOUSE
    }
    public enum MachineWorkStatus
    {
        MACHINE_IDLE=0, //闲置
        PRODUCT_IN_PREPARE , //产品入库准备
        PRODUCT_IN_LOADED, //取货完成——产品入库
        PRODUCT_INCOMING, //产品正在入库
        PRODUCT_IN_UNLOADED,
        PRODUCT_INHOUSE_OK, // 产品入库完成
        PRODUCT_OUT_PREPARE, //产品出库准备
        PRODUCT_OUT_LOADED, //取货完成——产品出库
        PRODUCT_OUTCOMING, //产品正在出库
        PRODUCT_OUT_UNLOADED,
        PRODUCT_OUTHOUSE_OK, //产品出库完成
        PALLETE_IN_PREPARE, //空板入库准备
        PALLETE_IN_LOADED, //取空板完成——空板入库
        PALLETE_INCOMMING, //空板正在入库
        PALLETE_IN_UNLOADED,
        PALLETE_INHOUSE_OK, //空板入库完成
        PALLETE_OUT_PREPARE, //空板出库准备
        PALLETE_OUT_LOADED, //取空板完成——空板出库
        PALLETE_OUTCOMMING, //空板正在出库
        PALLETE_OUT_UNLOADED,
        PALLETE_OUTHOUSE_OK, //空板出库完成
        MACHINE_FAULT  //故障
    }
    /// <summary>
    /// 货架单元坐标结构体
    /// </summary>
    public struct CellPos
    {
        public CellPos(int l, int r, int c)
        { this._L = l; this._R = r; this._C = c; }
        /// <summary>
        /// 层序号，从1开始编号
        /// </summary>
        private int _L;
        public int L
        {
            get
            {
                return _L;
            }
            set
            {
                _L = value;
            }
        }

        /// <summary>
        /// 行序号，从1开始编号
        /// </summary>
        private int _R;
        public int R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
            }
        }

        /// <summary>
        /// 列序号，从1开始编号
        /// </summary>
        private int _C;
        public int C
        {
            get
            {
                return _C;
            }
            set
            {
                _C = value;
            }

        }
    }
    public class TransMachine
    {
        private int _MachineIndex = 1;
        public int MachineIndex
        {
            get
            {
                return _MachineIndex;
            }
            private set 
            {
                _MachineIndex = value;
                if (_MachineIndex < 1)
                    _MachineIndex = 1;
            }
        }
        private ISvcTransMachine _svc; //
        private string _requestIP; //请求端的IP地址
        private int _requestPort; //请求端的端口号
        private MachineWorkStatus _workStatus = MachineWorkStatus.MACHINE_IDLE;
        private HouseCmdCode  _CurrentCmd = 0; //当前任务指令码
        private CellPos _CurrentCoord = new CellPos(); //当前坐标(L,R,C)
        private CellPos _TargetCoord = new CellPos(); //目标
        private Timer _MotionTimer;
        private int _LoadTime = 1000; //装货事件(取货和取工板时间相同）
        public int LoadTime
        {
            get
            {
                return _LoadTime;
            }
            set
            {
                _LoadTime = value;
                if (_LoadTime < 500)
                    _LoadTime = 500;
            }
        }
        private int _UnloadTime = 1000;
        public int UnloadTime
        {
            get
            {
                return _UnloadTime;
            }
            set
            {
                _UnloadTime = value;
                if (_UnloadTime < 500)
                    _UnloadTime = 500;
            }
        }
        private int _CellMoveTime = 1000;
        public int CellMoveTime
        {
            get
            {
                return _CellMoveTime;
            }
            set
            {
                _CellMoveTime = value;
                if (_CellMoveTime < 500)
                    _CellMoveTime = 500;
            }
        }
        private int _TimeElapse = 0; //计时
        public int MotionTimerInterval { get; set; }
        public float SimClockRate = 1.0f; //仿真时钟倍率越大，仿真速度越快，相当于加大仿真时钟
        /// <summary>
        /// 接收udp数据包，更新状态
        /// </summary>
        /// <param name="package"></param>
        public void RecvCmd( udpPackage package,string requestIP,int requestPort)
        {
            lock(this)
            {
                _requestIP = requestIP;
                _requestPort = requestPort;
                if (package.frameType == 2)
                    return;
                if (package.addr != _MachineIndex)
                    return;

                switch (package.cmd)
                {
                    case (byte)HouseCmdCode.CMD_QUERY:
                        {
                            udpPackage rePackage = new udpPackage();
                            rePackage.len = 0x09;
                            rePackage.frameType = 0x02;
                            rePackage.addr = (byte)_MachineIndex;
                            rePackage.cmd = (byte)HouseCmdCode.CMD_QUERY;
                            rePackage.status = 0;
                            rePackage.data = new byte[5];
                            rePackage.data[0] = (byte)_CurrentCoord.L;
                            rePackage.data[1] = (byte)(_CurrentCoord.C & 0xff);
                            rePackage.data[2] = (byte)((_CurrentCoord.C >> 8) & 0xff);
                            rePackage.data[3] = (byte)_workStatus;
                            rePackage.data[4] = 0x00;
                            _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            break;
                        }
                    case (byte)HouseCmdCode.CMD_PRODUCT_INHOUSE:
                        {
                            if (_workStatus == MachineWorkStatus.MACHINE_IDLE)
                            {
                                //如小车空闲则接收该指令
                                _workStatus = MachineWorkStatus.PRODUCT_IN_PREPARE;
                                _CurrentCmd = (HouseCmdCode)package.cmd;
                                _TargetCoord.L = package.data[0];
                                _TargetCoord.R = package.data[1];
                                _TargetCoord.C = package.data[2] + (package.data[3] << 8);

                                //应答
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x00;
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                                _svc.RefreshTaskDisp(this.MachineIndex, _TargetCoord, "入库");
                            }
                            else
                            {
                                //应答，由于小车正在作业，该指令拒绝接收
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x01; //拒绝执行
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            break;
                        }
                    case (byte)HouseCmdCode.CMD_PRODUCT_OUTHOUSE:
                        {
                            if (_workStatus == MachineWorkStatus.MACHINE_IDLE)
                            {
                                //如小车空闲则接收该指令
                                _workStatus = MachineWorkStatus.PRODUCT_OUT_PREPARE;
                                _CurrentCmd = (HouseCmdCode)package.cmd;
                                _TargetCoord.L = package.data[0];
                                _TargetCoord.R = package.data[1];
                                _TargetCoord.C = package.data[2] + (package.data[3] << 8);

                                //应答
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x00;
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                                _svc.RefreshTaskDisp(this.MachineIndex, _TargetCoord, "出库");
                            }
                            else
                            {
                                //应答，由于小车正在作业，该指令拒绝接收
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x01; //拒绝执行
                               _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            break;
                        }

                    case (byte)HouseCmdCode.CMD_PALLETE_INHOUSE:
                        {
                            if (_workStatus == MachineWorkStatus.MACHINE_IDLE)
                            {
                                //如小车空闲则接收该指令
                                _workStatus = MachineWorkStatus.PALLETE_IN_PREPARE;
                                _CurrentCmd = (HouseCmdCode)package.cmd;
                                _TargetCoord.L = package.data[0];
                                _TargetCoord.R = package.data[1];
                                _TargetCoord.C = package.data[2] + (package.data[3] << 8);

                                //应答
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x00;
                               _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            else
                            {
                                //应答，由于小车正在作业，该指令拒绝接收
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x01; //拒绝执行
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            break;
                        }

                    case (byte)HouseCmdCode.CMD_PALLETE_OUTHOUSE:
                        {
                            if (_workStatus == MachineWorkStatus.MACHINE_IDLE)
                            {
                                //如小车空闲则接收该指令
                                _workStatus = MachineWorkStatus.PALLETE_OUT_PREPARE;
                                _CurrentCmd = (HouseCmdCode)package.cmd;
                                _TargetCoord.L = package.data[0];
                                _TargetCoord.R = package.data[1];
                                _TargetCoord.C = package.data[2] + (package.data[3] << 8);

                                //应答
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x00;
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            else
                            {
                                //应答，由于小车正在作业，该指令拒绝接收
                                udpPackage rePackage = new udpPackage();
                                rePackage.len = 0x06;
                                rePackage.frameType = 0x02;
                                rePackage.addr = (byte)_MachineIndex;
                                rePackage.cmd = package.cmd;
                                rePackage.status = 0x01; //拒绝执行
                                _svc.SendRemsg(rePackage, _requestIP, _requestPort);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private void MotionTimerHandler(object source, ElapsedEventArgs e)
        {
            lock(this)
            {
                try
                {
                    _svc.DispMachineStatus(_MachineIndex, _workStatus);
                    _svc.RefreshMachinePos(_MachineIndex, _CurrentCoord.L, _CurrentCoord.C);
                }
                catch (System.Exception e1)
                {
                    Console.WriteLine(" _svc.DispMachineStatus执行错误:"+e1.Message);
                }
             
                if(_workStatus == MachineWorkStatus.MACHINE_FAULT)
                {
                    return;
                }
                switch(_CurrentCmd)
                {
                    case HouseCmdCode.CMD_PRODUCT_INHOUSE:
                        {
                            RefreshProductInhouse();
                            break;
                        }
                    case HouseCmdCode.CMD_PRODUCT_OUTHOUSE:
                        {
                            RefreshProductOuthouse();
                            break;
                        }
                    case HouseCmdCode.CMD_PALLETE_INHOUSE:
                        {
                            RefreshPalleteInhouse();
                            break;
                        }
                    case HouseCmdCode.CMD_PALLETE_OUTHOUSE:
                        {
                            RefreshPalleteOuthouse();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private void RefreshProductInhouse()
        {
            switch(_workStatus)
            {
                case MachineWorkStatus.MACHINE_IDLE:
                    {
                        _TimeElapse = 0; //计时清零
                        _workStatus = MachineWorkStatus.PRODUCT_IN_PREPARE;
                        break;
                    }
                case MachineWorkStatus.PRODUCT_IN_PREPARE:
                    {
                        CellPos targetPos = new CellPos();
                        targetPos.L = 1;
                        targetPos.C = 1;
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        int CellMoveMax = _TimeElapse /_CellMoveTime; //最多能移动格数
                        //先移动到目标层，再移动到目标列
                        if (Math.Abs(targetPos.L - _CurrentCoord.L) < CellMoveMax)
                        {
                            _CurrentCoord.L = targetPos.L;
                            int CellMoveColumnMax = CellMoveMax - Math.Abs(targetPos.L - _CurrentCoord.L);
                            if (Math.Abs(targetPos.C - _CurrentCoord.C) < CellMoveColumnMax)
                            {
                                _CurrentCoord.C = targetPos.C;
                                _workStatus = MachineWorkStatus.PRODUCT_IN_LOADED;
                                _TimeElapse = 0; //计时清零
                            }
                            else
                            {
                                _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                                if (targetPos.C > _CurrentCoord.C)
                                {
                                    _CurrentCoord.C += CellMoveColumnMax;
                                }
                                else 
                                {
                                    _CurrentCoord.C -= CellMoveColumnMax;
                                }
                            }
                        }
                        else
                        {
                            _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                            if(targetPos.L>_CurrentCoord.L)
                            {
                                _CurrentCoord.L += CellMoveMax;
                            }
                            else
                            {
                                _CurrentCoord.L -= CellMoveMax;
                            }
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_IN_LOADED:
                    {
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        if(_TimeElapse>=_LoadTime)
                        {
                            _workStatus = MachineWorkStatus.PRODUCT_INCOMING;
                            _TimeElapse = 0; //计时器清零
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_INCOMING:
                    {
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        int CellMoveMax = _TimeElapse / _CellMoveTime; //最多能移动格数
                        //先移动到目标层，再移动到目标列
                        if (Math.Abs(_TargetCoord.L - _CurrentCoord.L) < CellMoveMax)
                        {
                            _CurrentCoord.L = _TargetCoord.L;
                            int CellMoveColumnMax = CellMoveMax - Math.Abs(_TargetCoord.L - _CurrentCoord.L);
                            if (Math.Abs(_TargetCoord.C - _CurrentCoord.C) < CellMoveColumnMax)
                            {
                                _CurrentCoord.C = _TargetCoord.C;
                                _workStatus = MachineWorkStatus.PRODUCT_IN_UNLOADED;
                                _TimeElapse = 0; //计时清零
                            }
                            else
                            {
                                _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                                if (_TargetCoord.C > _CurrentCoord.C)
                                {
                                    _CurrentCoord.C += CellMoveColumnMax;
                                }
                                else
                                {
                                    _CurrentCoord.C -= CellMoveColumnMax;
                                }
                            }
                        }
                        else
                        {
                            _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                            if(_TargetCoord.L>_CurrentCoord.L)
                            {
                                _CurrentCoord.L += CellMoveMax;
                            }
                            else
                            {
                                _CurrentCoord.L -= CellMoveMax;
                            }
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_IN_UNLOADED:
                    {
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        if(_TimeElapse>=_UnloadTime)
                        {
                            _workStatus = MachineWorkStatus.PRODUCT_INHOUSE_OK;
                            _TimeElapse = 0;
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_INHOUSE_OK:
                    {
                        _workStatus = MachineWorkStatus.MACHINE_IDLE;
                        _CurrentCmd = 0;
                        _TimeElapse = 0;
                        break;
                    }
                default:
                    break;
            }
        }
        private void RefreshProductOuthouse()
        {
            switch(_workStatus)
            {
                case MachineWorkStatus.MACHINE_IDLE:
                    {
                        _TimeElapse = 0; //计时清零
                        _workStatus = MachineWorkStatus.PRODUCT_OUT_PREPARE;
                        break;
                    }
                case MachineWorkStatus.PRODUCT_OUT_PREPARE:
                    {
                      
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        int CellMoveMax = _TimeElapse / _CellMoveTime; //最多能移动格数
                        if (Math.Abs(_TargetCoord.L - _CurrentCoord.L) < CellMoveMax)
                        {
                            _CurrentCoord.L = _TargetCoord.L;
                            int CellMoveColumnMax = CellMoveMax - Math.Abs(_TargetCoord.L - _CurrentCoord.L);
                            if (Math.Abs(_TargetCoord.C - _CurrentCoord.C) < CellMoveColumnMax)
                            {
                                _CurrentCoord.C = _TargetCoord.C;
                                _workStatus = MachineWorkStatus.PRODUCT_OUT_LOADED;
                                _TimeElapse = 0; //计时清零
                            }
                            else
                            {
                                _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                                if (_TargetCoord.C > _CurrentCoord.C)
                                {
                                    _CurrentCoord.C += CellMoveColumnMax;
                                }
                                else
                                {
                                    _CurrentCoord.C -= CellMoveColumnMax;
                                }
                            }
                        }
                        else
                        {
                            _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                            if (_TargetCoord.L > _CurrentCoord.L)
                            {
                                _CurrentCoord.L += CellMoveMax;
                            }
                            else
                            {
                                _CurrentCoord.L -= CellMoveMax;
                            }
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_OUT_LOADED:
                    {
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        if (_TimeElapse >= _LoadTime)
                        {
                            _workStatus = MachineWorkStatus.PRODUCT_OUTCOMING;
                            _TimeElapse = 0; //计时器清零
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_OUTCOMING:
                    {
                        CellPos targetPos = new CellPos();
                        targetPos.L = 1;
                        targetPos.C = 50;
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        int CellMoveMax = _TimeElapse / _CellMoveTime; //最多能移动格数
                        //先移动到目标层，再移动到目标列
                        if (Math.Abs(targetPos.L - _CurrentCoord.L) < CellMoveMax)
                        {
                            _CurrentCoord.L = targetPos.L;
                            int CellMoveColumnMax = CellMoveMax - Math.Abs(targetPos.L - _CurrentCoord.L);
                            if (Math.Abs(targetPos.C - _CurrentCoord.C) < CellMoveColumnMax)
                            {
                                _CurrentCoord.C = targetPos.C;
                                _workStatus = MachineWorkStatus.PRODUCT_OUT_UNLOADED;
                                _TimeElapse = 0; //计时清零
                            }
                            else
                            {
                                _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                                if (targetPos.C > _CurrentCoord.C)
                                {
                                    _CurrentCoord.C += CellMoveColumnMax;
                                }
                                else
                                {
                                    _CurrentCoord.C -= CellMoveColumnMax;
                                }
                            }
                        }
                        else
                        {
                            _TimeElapse -= (int)(CellMoveMax * MotionTimerInterval * SimClockRate);
                            if (targetPos.L > _CurrentCoord.L)
                            {
                                _CurrentCoord.L += CellMoveMax;
                            }
                            else
                            {
                                _CurrentCoord.L -= CellMoveMax;
                            }
                        }

                        break;
                    }
                case MachineWorkStatus.PRODUCT_OUT_UNLOADED:
                    {
                        _TimeElapse += (int)(MotionTimerInterval * SimClockRate);
                        if(_TimeElapse>= _UnloadTime)
                        {
                            _workStatus = MachineWorkStatus.PRODUCT_OUTHOUSE_OK;
                            _TimeElapse = 0;
                        }
                        break;
                    }
                case MachineWorkStatus.PRODUCT_OUTHOUSE_OK:
                    {
                       _workStatus = MachineWorkStatus.MACHINE_IDLE;
                        _CurrentCmd = 0;
                        _TimeElapse = 0;
                        break;
                    }
            }
        }
        private void RefreshPalleteInhouse()
        {

        }
        private void RefreshPalleteOuthouse()
        {

        }
        public TransMachine(ISvcTransMachine ISvc,int machineNo)
        {
            MotionTimerInterval = 500;//运动定时器周期
            MachineIndex = machineNo;
            _CurrentCoord.L = 1;
            _CurrentCoord.R = 1;
            _CurrentCoord.C = 1;
            _svc = ISvc;
            _MotionTimer = new Timer(MotionTimerInterval);
         
            _MotionTimer.Elapsed += MotionTimerHandler;
            _MotionTimer.Start();
        }
    }
}
