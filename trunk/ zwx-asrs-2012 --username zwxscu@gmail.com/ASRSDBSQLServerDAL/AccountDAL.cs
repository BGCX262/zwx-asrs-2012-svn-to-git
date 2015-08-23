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
    /// 账户类数据库操作
    /// </summary>
    public class AccountDAL: IAccountDAL
    {
        private DBAssist _dbAssist = null;
        public AccountDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Account");
            strSql.Append(" where userName=@userName ");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = userName;

            return _dbAssist.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AccountME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Account(");
            strSql.Append("userName,userPassword,role)");
            strSql.Append(" values (");
            strSql.Append("@userName,@userPassword,@role)");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50),
					new SqlParameter("@userPassword", SqlDbType.NChar,6),
					new SqlParameter("@role", SqlDbType.Int,4)};
            parameters[0].Value = model.userName;
            parameters[1].Value = model.userPassword;
            parameters[2].Value = model.role;

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
        public bool Update(AccountME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Account set ");
            strSql.Append("userPassword=@userPassword,");
            strSql.Append("role=@role");
            strSql.Append(" where userName=@userName ");
            SqlParameter[] parameters = {
					new SqlParameter("@userPassword", SqlDbType.NChar,6),
					new SqlParameter("@role", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.userPassword;
            parameters[1].Value = model.role;
            parameters[2].Value = model.userName;

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
        public bool Delete(string userName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Account ");
            strSql.Append(" where userName=@userName ");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = userName;

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
        public bool DeleteList(string userNamelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Account ");
            strSql.Append(" where userName in (" + userNamelist + ")  ");
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
        public AccountME GetModel(string userName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 userName,userPassword,role from Account ");
            strSql.Append(" where userName=@userName ");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
            parameters[0].Value = userName;

            AccountME model = new AccountME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userName"] != null && ds.Tables[0].Rows[0]["userName"].ToString() != "")
                {
                    model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["userPassword"] != null && ds.Tables[0].Rows[0]["userPassword"].ToString() != "")
                {
                    model.userPassword = ds.Tables[0].Rows[0]["userPassword"].ToString();
                }
                if (ds.Tables[0].Rows[0]["role"] != null && ds.Tables[0].Rows[0]["role"].ToString() != "")
                {
                    model.role = int.Parse(ds.Tables[0].Rows[0]["role"].ToString());
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
            strSql.Append("select userName,userPassword,role ");
            strSql.Append(" FROM Account ");
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
            strSql.Append(" userName,userPassword,role ");
            strSql.Append(" FROM Account ");
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
            strSql.Append("select count(1) FROM Account ");
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

        #endregion  Method
    }
}
