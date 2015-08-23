using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using ASRSDBFactory;
using ASRSDBIDAL;
using System.Data;
namespace ASRSDBBLL
{
    /// <summary>
    /// 任务表业务层
    /// </summary>
    public class TaskBLL
    {
        private readonly ITaskDAL _taskDAL = DALFactory.CreateTaskDAL();

        #region 未完成任务表接口

        /// <summary>
        /// 获得待执行的任务数量
        /// </summary>
        /// <returns></returns>
        public int GetWaitTaskCount()
        {
            return _taskDAL.GetRecordCount("  taskExeStatus=0 ");
        }
        /// <summary>
        /// 根据当前任务数据库生成新的任务流水号
        /// </summary>
        /// <returns></returns>
        public string GenerateNewTaskSerialNo()
        {
            string nowMaxID = _taskDAL.GetMaxTaskID();
            string nowDt = System.DateTime.Now.ToString("yyyyMMddHHmm");
            string newTaskID = string.Empty;
            if(nowMaxID.Substring(0,12).Equals(nowDt.Substring(0,12)))
            {
                int idPlus = Convert.ToInt32(nowMaxID.Substring(12)) + 1;
                newTaskID = nowMaxID.Substring(0, 12) + Convert.ToString(idPlus).PadLeft(4, '0');
            }
            else
            {
                newTaskID = nowDt + "0001";
            }
            return newTaskID;
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool AddTask(TaskME m)
        {
            if (_taskDAL.Exists(m.taskID))
            {
                return false;
            }
            return _taskDAL.Add(m);
        }

        /// <summary>
        /// 根据任务流水号查找任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public TaskME GetTask(string taskID)
        {
            return _taskDAL.GetModel(taskID);
        }

        /// <summary>
        /// 获取未执行的任务列表
        /// </summary>
        /// <returns></returns>
        public IList<TaskME> GetAllUnexedTaskList()
        {
            
            DataSet ds = _taskDAL.GetList("  taskExeStatus=0 ");
            if (ds != null && ds.Tables.Count > 0)
            {
                IList<TaskME> taskList = new List<TaskME>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TaskME model = new TaskME();
                    if (dr["taskID"] != null && dr["taskID"].ToString() != "")
                    {
                        model.taskID = dr["taskID"].ToString();
                    }
                    if( dr["taskCode"] != null && dr["taskCode"].ToString() != "")
                    {
                        model.taskCode = int.Parse(dr["taskCode"].ToString());
                    }
                    if (dr["taskObj"] != null && dr["taskObj"].ToString() != "")
                    {
                        model.taskObj = dr["taskObj"].ToString();
                    }
                    if (dr["taskExeStatus"] != null && dr["taskExeStatus"].ToString() != "")
                    {
                        model.taskExeStatus = int.Parse(dr["taskExeStatus"].ToString());
                    }
                    taskList.Add(model);
                }
                return taskList;
            }
            else
                return null;
        }
        /// <summary>
        /// 从任务数据库中获取第一条未执行的自动任务
        /// </summary>
        /// <returns></returns>
        public TaskME GetFirstUnexeAutoTask()
        {
            return _taskDAL.GetConditionedModel("  taskExeStatus=0 ");
        }

        /// <summary>
        /// 从任务数据库中获取第一条未执行的手动任务
        /// </summary>
        /// <returns></returns>
        public TaskME GetFirstUnexeManualTask()
        {
            return _taskDAL.GetConditionedModel(" operateMode = 2 and taskExeStatus=0 ");
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskID">任务流水号</param>
        /// <param name="taskExeStatus">任务状态</param>
        /// <returns>若该任务不存在，则返回false</returns>
        public bool UpdateTaskStatus(string taskID, int taskExeStatus)
        {

            if (!_taskDAL.Exists(taskID))
                return false;
            TaskME taskM = _taskDAL.GetModel(taskID);
            taskM.taskExeStatus = taskExeStatus;
            return _taskDAL.Update(taskM);
        }

        /// <summary>
        /// 更新任务内容
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="xmlTaskObj"></param>
        /// <returns></returns>
        public bool UpdateTaskObj(string taskID,string xmlTaskObj)
        {
            if (!_taskDAL.Exists(taskID))
                return false;
            TaskME taskM = _taskDAL.GetModel(taskID);
            taskM.taskObj = xmlTaskObj;
            return _taskDAL.Update(taskM);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool DeleteTask(string taskID)
        {
            return _taskDAL.Delete(taskID);
        }
        public DataTable GetAllTask()
        {
             DataSet ds = _taskDAL.GetList("  ");
             if (ds != null && ds.Tables.Count > 0)
             {
                 return ds.Tables[0];
             }
             else
                 return null;
        }
        #endregion
        
        #region 已完成任务表接口

        #endregion
    }
}
