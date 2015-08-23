using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ASRSDBME;
using ASRSDBIDAL;

namespace ASRSDBSQLServerDAL
{
    /// <summary>
    /// 数据访问类:InstDAL
    /// </summary>
    public partial class InstDAL : IInstDAL
    {
        private DBAssist _dbAssist = null;
        public InstDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region  Method
        public string GetMaxInstID()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 instID from Inst order by cast(instID as numeric) DESC");
            DataSet ds = _dbAssist.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["instID"].ToString();
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
        public bool Exists(string instID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Inst");
            strSql.Append(" where instID=@instID ");
            SqlParameter[] parameters = {
					new SqlParameter("@instID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = instID;

            return _dbAssist.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(InstME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Inst(");
            strSql.Append("instID,instCode,instObj,vehicleAlloc)");
            strSql.Append(" values (");
            strSql.Append("@instID,@instCode,@instObj,@vehicleAlloc)");
            SqlParameter[] parameters = {
					new SqlParameter("@instID", SqlDbType.NVarChar,50),
					new SqlParameter("@instCode", SqlDbType.Int,4),
					new SqlParameter("@instObj", SqlDbType.Xml,-1),
					new SqlParameter("@vehicleAlloc", SqlDbType.Int,4)};
            parameters[0].Value = model.instID;
            parameters[1].Value = model.instCode;
            parameters[2].Value = model.instObj;
            parameters[3].Value = model.vehicleAlloc;

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
        public bool Update(InstME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Inst set ");
            strSql.Append("instCode=@instCode,");
            strSql.Append("instObj=@instObj,");
            strSql.Append("vehicleAlloc=@vehicleAlloc");
            strSql.Append(" where instID=@instID ");
            SqlParameter[] parameters = {
					new SqlParameter("@instCode", SqlDbType.Int,4),
					new SqlParameter("@instObj", SqlDbType.Xml,-1),
					new SqlParameter("@vehicleAlloc", SqlDbType.Int,4),
					new SqlParameter("@instID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.instCode;
            parameters[1].Value = model.instObj;
            parameters[2].Value = model.vehicleAlloc;
            parameters[3].Value = model.instID;

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
        public bool Delete(string instID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Inst ");
            strSql.Append(" where instID=@instID ");
            SqlParameter[] parameters = {
					new SqlParameter("@instID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = instID;

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
        public bool DeleteList(string instIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Inst ");
            strSql.Append(" where instID in (" + instIDlist + ")  ");
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
            strSql.Append("delete from Inst ");
            strSql.AppendFormat(" where {0}", strWhere.Trim());
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
        public InstME GetModel(string instID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 instID,instCode,instObj,vehicleAlloc from Inst ");
            strSql.Append(" where instID=@instID ");
            SqlParameter[] parameters = {
					new SqlParameter("@instID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = instID;

            InstME model = new InstME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["instID"] != null && ds.Tables[0].Rows[0]["instID"].ToString() != "")
                {
                    model.instID = ds.Tables[0].Rows[0]["instID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["instCode"] != null && ds.Tables[0].Rows[0]["instCode"].ToString() != "")
                {
                    model.instCode = int.Parse(ds.Tables[0].Rows[0]["instCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["instObj"] != null && ds.Tables[0].Rows[0]["instObj"].ToString() != "")
                {
                    model.instObj=ds.Tables[0].Rows[0]["instObj"].ToString();
                }
                if (ds.Tables[0].Rows[0]["vehicleAlloc"] != null && ds.Tables[0].Rows[0]["vehicleAlloc"].ToString() != "")
                {
                    model.vehicleAlloc = int.Parse(ds.Tables[0].Rows[0]["vehicleAlloc"].ToString());
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
            strSql.Append("select instID,instCode,instObj,vehicleAlloc ");
            strSql.Append(" FROM Inst ");
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
            strSql.Append(" instID,instCode,instObj,vehicleAlloc ");
            strSql.Append(" FROM Inst ");
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
            strSql.Append("select count(1) FROM Inst ");
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
                strSql.Append("order by T.instID desc");
            }
            strSql.Append(")AS Row, T.*  from Inst T ");
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
            parameters[0].Value = "Inst";
            parameters[1].Value = "instID";
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
