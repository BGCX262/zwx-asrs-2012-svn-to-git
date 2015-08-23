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
    /// 数据访问类:MessageDefineDAL
    /// </summary>
    public partial class MessageDefineDAL : IMessageDefineDAL
    {
        private DBAssist _dbAssist = null;
        public MessageDefineDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return _dbAssist.GetMaxID("messageID", "messageDefine");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int messageID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from messageDefine");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.Int,4)			};
            parameters[0].Value = messageID;

            return _dbAssist.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MessageDefineME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into messageDefine(");
            strSql.Append("messageID,messageContent,messageType)");
            strSql.Append(" values (");
            strSql.Append("@messageID,@messageContent,@messageType)");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.Int,4),
					new SqlParameter("@messageContent", SqlDbType.VarChar,255),
					new SqlParameter("@messageType", SqlDbType.Int,4)};
            parameters[0].Value = model.messageID;
            parameters[1].Value = model.messageContent;
            parameters[2].Value = model.messageType;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(MessageDefineME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update messageDefine set ");
            strSql.Append("messageContent=@messageContent,");
            strSql.Append("messageType=@messageType");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageContent", SqlDbType.VarChar,255),
					new SqlParameter("@messageType", SqlDbType.Int,4),
					new SqlParameter("@messageID", SqlDbType.Int,4)};
            parameters[0].Value = model.messageContent;
            parameters[1].Value = model.messageType;
            parameters[2].Value = model.messageID;

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
        public bool Delete(int messageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from messageDefine ");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.Int,4)			};
            parameters[0].Value = messageID;

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
        public bool DeleteList(string messageIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from messageDefine ");
            strSql.Append(" where messageID in (" + messageIDlist + ")  ");
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
        /// 得到一个对象实体
        /// </summary>
        public MessageDefineME GetModel(int messageID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 messageID,messageContent,messageType from messageDefine ");
            strSql.Append(" where messageID=@messageID ");
            SqlParameter[] parameters = {
					new SqlParameter("@messageID", SqlDbType.Int,4)			};
            parameters[0].Value = messageID;

           MessageDefineME model = new MessageDefineME();
           DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["messageID"] != null && ds.Tables[0].Rows[0]["messageID"].ToString() != "")
                {
                    model.messageID = int.Parse(ds.Tables[0].Rows[0]["messageID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["messageContent"] != null && ds.Tables[0].Rows[0]["messageContent"].ToString() != "")
                {
                    model.messageContent = ds.Tables[0].Rows[0]["messageContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["messageType"] != null && ds.Tables[0].Rows[0]["messageType"].ToString() != "")
                {
                    model.messageType = int.Parse(ds.Tables[0].Rows[0]["messageType"].ToString());
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
            strSql.Append("select messageID,messageContent,messageType ");
            strSql.Append(" FROM messageDefine ");
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
            strSql.Append(" messageID,messageContent,messageType ");
            strSql.Append(" FROM messageDefine ");
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
            strSql.Append("select count(1) FROM messageDefine ");
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
                strSql.Append("order by T.messageID desc");
            }
            strSql.Append(")AS Row, T.*  from messageDefine T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return _dbAssist.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "messageDefine";
            parameters[1].Value = "messageID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}
