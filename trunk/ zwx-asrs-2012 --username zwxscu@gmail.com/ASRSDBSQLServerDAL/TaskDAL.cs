using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ASRSDBIDAL;
using ASRSDBME;
namespace ASRSDBSQLServerDAL
{
    /// <summary>
    /// 数据访问类:TaskDAL
    /// </summary>
    public partial class TaskDAL : ITaskDAL
    {
        private DBAssist _dbAssist = null;
        public TaskDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region  Method

        /// <summary>
        /// 得到最大的流水号（转换成int类型排序）
        /// </summary>
        /// <returns></returns>
        public string GetMaxTaskID()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 taskID from Task order by cast(taskID as numeric) DESC");
            DataSet ds = _dbAssist.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["taskID"].ToString();
            }
            else
            {
                string str = new string('0', 16);
                return str;
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string taskID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Task");
            strSql.Append(" where taskID=@taskID ");
            SqlParameter[] parameters = {
					new SqlParameter("@taskID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = taskID;

            return _dbAssist.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TaskME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Task(");
            strSql.Append("taskID,taskCode,taskObj,taskExeStatus)");
            strSql.Append(" values (");
            strSql.Append("@taskID,@taskCode,@taskObj,@taskExeStatus)");
            SqlParameter[] parameters = {
					new SqlParameter("@taskID", SqlDbType.NVarChar,50),
					new SqlParameter("@taskCode", SqlDbType.Int,4),
					new SqlParameter("@taskObj", SqlDbType.Xml,-1),
					new SqlParameter("@taskExeStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.taskID;
            parameters[1].Value = model.taskCode;
            parameters[2].Value = model.taskObj;
            parameters[3].Value = model.taskExeStatus;

            int rows =_dbAssist.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TaskME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Task set ");
            strSql.Append("taskCode=@taskCode,");
            strSql.Append("taskObj=@taskObj,");
            strSql.Append("taskExeStatus=@taskExeStatus");
            strSql.Append(" where taskID=@taskID ");
            SqlParameter[] parameters = {
					new SqlParameter("@taskCode", SqlDbType.Int,4),
					new SqlParameter("@taskObj", SqlDbType.Xml,-1),
					new SqlParameter("@taskExeStatus", SqlDbType.Int,4),
					new SqlParameter("@taskID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.taskCode;
            parameters[1].Value = model.taskObj;
            parameters[2].Value = model.taskExeStatus;
            parameters[3].Value = model.taskID;

            int rows = _dbAssist.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string taskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Task ");
            strSql.Append(" where taskID=@taskID ");
            SqlParameter[] parameters = {
					new SqlParameter("@taskID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = taskID;

            int rows = _dbAssist.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string taskIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Task ");
            strSql.Append(" where taskID in (" + taskIDlist + ")  ");
            int rows = _dbAssist.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 有条件的删除语句
        /// </summary>
        /// <param name="strWhere">条件语句</param>
        /// <returns></returns>
        public bool ConditionedDelete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Task ");
            strSql.AppendFormat(" where {0}",strWhere.Trim());
            int rows = _dbAssist.ExecuteSql(strSql.ToString());
            if(rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TaskME GetModel(string taskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 taskID,taskCode,taskObj,taskExeStatus from Task ");
            strSql.Append(" where taskID=@taskID ");
            SqlParameter[] parameters = {
					new SqlParameter("@taskID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = taskID;

            TaskME model = new TaskME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["taskID"] != null && ds.Tables[0].Rows[0]["taskID"].ToString() != "")
                {
                    model.taskID = ds.Tables[0].Rows[0]["taskID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["taskCode"] != null && ds.Tables[0].Rows[0]["taskCode"].ToString() != "")
                {
                    model.taskCode = int.Parse(ds.Tables[0].Rows[0]["taskCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["taskObj"] != null && ds.Tables[0].Rows[0]["taskObj"].ToString() != "")
                {
                    model.taskObj=ds.Tables[0].Rows[0]["taskObj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["taskExeStatus"] != null && ds.Tables[0].Rows[0]["taskExeStatus"].ToString() != "")
                {
                    model.taskExeStatus = int.Parse(ds.Tables[0].Rows[0]["taskExeStatus"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据条件查找符合的第一条记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public TaskME GetConditionedModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 taskID,taskCode,taskObj,taskExeStatus  ");
            strSql.Append(" FROM Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = _dbAssist.Query(strSql.ToString());
            TaskME model = new TaskME();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["taskID"] != null && ds.Tables[0].Rows[0]["taskID"].ToString() != "")
                {
                    model.taskID = ds.Tables[0].Rows[0]["taskID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["taskCode"] != null && ds.Tables[0].Rows[0]["taskCode"].ToString() != "")
                {
                    model.taskCode = int.Parse(ds.Tables[0].Rows[0]["taskCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["taskObj"] != null && ds.Tables[0].Rows[0]["taskObj"].ToString() != "")
                {
                    model.taskObj = ds.Tables[0].Rows[0]["taskObj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["taskExeStatus"] != null && ds.Tables[0].Rows[0]["taskExeStatus"].ToString() != "")
                {
                    model.taskExeStatus = int.Parse(ds.Tables[0].Rows[0]["taskExeStatus"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select taskID,taskCode,taskObj,taskExeStatus");
            strSql.Append(" FROM Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return _dbAssist.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" taskID,taskCode,taskObj,taskExeStatus ");
            strSql.Append(" FROM Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return _dbAssist.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = _dbAssist.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.taskID desc");
            }
            strSql.Append(")AS Row, T.*  from Task T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return _dbAssist.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
