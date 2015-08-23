using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ASRS
{
    /// <summary>
    ///实现堆垛机小车控制的代理类，负责指令的执行，状态的监视
    /// </summary>
    public class TansVehicle
    {
        #region 属性
        private int _instScanInterval = 200; //取指令周期,单位：毫秒
        /// <summary>
        /// 小车工作状态
        /// </summary>
        public MachineWorkStatus MStatus{get;set;}

        /// <summary>
        /// 当前任务的流水号
        /// </summary>
        private string _currentTaskID = string.Empty;

        /// <summary>
        /// 当前任务
        /// </summary>
        private BaseTaskInfo _currentTaskObj = null;

        /// <summary>
        /// 当前自动模式下，指令列表中正在执行的指令的序号，从0开始
        /// </summary>
        private int _currentAutoInstIndex = -1;

        /// <summary>
        /// 当前任务的指令列表
        /// </summary>
        private List<BaseInstInfo> _instList = new List<BaseInstInfo>();

        /// <summary>
        /// 指令执行线程
        /// </summary>
        private Thread _instExeThread = null;

        /// <summary>
        /// 是否退出指令执行线程
        /// </summary>
        private bool _exitInstExeThread = false;
        #endregion
        #region 私有行为
        private void InstExeThreadProc()
        {
            while(!_exitInstExeThread)
            {
                Thread.Sleep(_instScanInterval);
            }
        }
        #endregion
        #region 公共行为
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public TansVehicle()
        {
            _instExeThread = new Thread(new ThreadStart(InstExeThreadProc));
            _instExeThread.Suspend(); //挂起
        }
        /// <summary>
        /// 填充指令列表
        /// </summary>
        /// <param name="instList"></param>
        /// <param name="taskObj"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool FillInstList(IList<BaseInstInfo> instList, BaseTaskInfo taskObj, string taskID)
        {
            if (MStatus == MachineWorkStatus.MACHINE_IDLE)
            {
                _currentAutoInstIndex = 0;
                this._instList.Clear();
                foreach (BaseInstInfo instObj in instList)
                {
                    this._instList.Add(instObj);
                }
                _currentTaskID = taskID;
                _currentTaskObj = taskObj;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {

        }
        #endregion
    }
}
