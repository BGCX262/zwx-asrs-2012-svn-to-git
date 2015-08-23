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
    /// 数据访问类:MessageViewDAL
    /// </summary>
    public partial class MessageViewDAL : IMessageViewDAL
    {
        private DBAssist _dbAssist = null;
        public MessageViewDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(DateTime happenTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from messageView");
            strSql.Append(" where happenTime=@happenTime ");
            SqlParameter[] parameters = {
					new SqlParameter("@happenTime", SqlDbType.DateTime)			};
            parameters[0].Value = happenTime;

            return _dbAssist.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MessageViewME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into messageView(");
            strSql.Append("happenTime,messageID,messageContent,messageType,dynamicContent)");
            strSql.Append(" values (");
            strSql.Append("@happenTime,@messageID,@messageContent,@messageType,@dynamicContent)");
            SqlParameter[] parameters = {
					new SqlParameter("@happenTime", SqlDbType.DateTime),
					new SqlParameter("@messageID", SqlDbType.Int,4),
					new SqlParameter("@messageContent", SqlDbType.VarChar,255),
					new SqlParameter("@messageType", SqlDbType.Int,4),
					new SqlParameter("@dynamicContent", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.happenTime;
            parameters[1].Value = model.messageID;
            parameters[2].Value = model.messageContent;
            parameters[3].Value = model.messageType;
            parameters[4].Value = model.dynamicContent;

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
        public bool Update(MessageViewME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update messageView set ");
            strSql.Append("happenTime=@happenTime,");
            strSql.Append("messageID=@messageID,");
            strSql.Append("messageContent=@messageContent,");
            strSql.Append("messageType=@messageType,");
            strSql.Append("dynamicContent=@dynamicContent");
            strSql.Append(" where happenTime=@happenTime ");
            SqlParameter[] parameters = {
					new SqlParameter("@happenTime", SqlDbType.DateTime),
					new SqlParameter("@messageID", SqlDbType.Int,4),
					new SqlParameter("@messageContent", SqlDbType.VarChar,255),
					new SqlParameter("@messageType", SqlDbType.Int,4),
					new SqlParameter("@dynamicContent", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.happenTime;
            parameters[1].Value = model.messageID;
            parameters[2].Value = model.messageContent;
            parameters[3].Value = model.messageType;
            parameters[4].Value = model.dynamicContent;

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
        public bool Delete(DateTime happenTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from messageView ");
            strSql.Append(" where happenTime=@happenTime ");
            SqlParameter[] parameters = {
					new SqlParameter("@happenTime", SqlDbType.DateTime)			};
            parameters[0].Value = happenTime;

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
        /// 得到一个对象实体
        /// </summary>
        public MessageViewME GetModel(DateTime happenTime)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 happenTime,messageID,messageContent,messageType,dynamicContent from messageView ");
            strSql.Append(" where happenTime=@happenTime ");
            SqlParameter[] parameters = {
					new SqlParameter("@happenTime", SqlDbType.DateTime)			};
            parameters[0].Value = happenTime;

            MessageViewME model = new MessageViewME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["happenTime"] != null && ds.Tables[0].Rows[0]["happenTime"].ToString() != "")
                {
                    model.happenTime = DateTime.Parse(ds.Tables[0].Rows[0]["happenTime"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["dynamicContent"] != null && ds.Tables[0].Rows[0]["dynamicContent"].ToString() != "")
                {
                    model.dynamicContent = ds.Tables[0].Rows[0]["dynamicContent"].ToString();
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
            strSql.Append("select happenTime,messageID,messageContent,messageType ");
            strSql.Append(" FROM messageView ");
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
            strSql.Append(" happenTime,messageID,messageContent,messageType ");
            strSql.Append(" FROM messageView ");
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
            strSql.Append("select count(1) FROM messageView ");
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
                strSql.Append("order by T.happenTime desc");
            }
            strSql.Append(")AS Row, T.*  from messageView T ");
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
            parameters[0].Value = "messageView";
            parameters[1].Value = "happenTime";
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
