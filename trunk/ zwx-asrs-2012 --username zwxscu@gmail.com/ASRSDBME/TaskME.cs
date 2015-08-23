using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// TaskME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TaskME
    {
        public TaskME()
        { }
        #region Model
        private string _taskid;
        private int _taskcode;
        private string _taskobj;
        private int _taskexestatus;
        /// <summary>
        /// 任务流水号（唯一）
        /// </summary>
        public string taskID
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 任务码
        /// </summary>
        public int taskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }
        /// <summary>
        /// 任务对象
        /// </summary>
        public string taskObj
        {
            set { _taskobj = value; }
            get { return _taskobj; }
        }

        /// <summary>
        /// 任务执行状态。0：新建，1：已分配资源，正在执行，2：执行完毕
        /// </summary>
        public int taskExeStatus
        {
            set { _taskexestatus = value; }
            get { return _taskexestatus; }
        }
        #endregion Model

    }
}
