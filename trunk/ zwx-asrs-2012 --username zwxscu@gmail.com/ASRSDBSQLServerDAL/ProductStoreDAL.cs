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
    /// 产品信息类数据库操作
    /// </summary>
    public class ProductStoreDAL:IProductStoreDAL
    {
        //public string connectionString = null;
        private DBAssist _dbAssist =null;
        public ProductStoreDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region method
        public bool Exists(string productID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from productStore ");
            strSql.Append(" where productID=@productID ");
            SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = productID;
            return _dbAssist.Exists(strSql.ToString(), parameters);
        }
        // <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProductStoreME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into productStore(");
            strSql.Append("productID,productType)");
            strSql.Append(" values (");
            strSql.Append("@productID,@productType)");
            SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@productType", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.productID;
            parameters[1].Value = model.productType;
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
        public bool Update(ProductStoreME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update productStore set ");
            strSql.Append("productType=@productType");
            strSql.Append(" where productID=@productID ");
            SqlParameter[] parameters = {
					new SqlParameter("@productType", SqlDbType.NVarChar,50),
					new SqlParameter("@productID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.productType;
            parameters[1].Value = model.productID;
            int rows = _dbAssist.ExecuteSql(strSql.ToString(), parameters);
            if(rows>0)
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
        public bool Delete(string productID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from productStore ");
            strSql.Append(" where productID=@productID ");
            SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = productID;
            int rows = _dbAssist.ExecuteSql(strSql.ToString(), parameters);
            if(rows>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteList(string productIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from productStore ");
            strSql.Append(" where productID in (" + productIDlist + ")  ");
            int rows = _dbAssist.ExecuteSql(strSql.ToString());
            if(rows>0)
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
        public ProductStoreME GetModel(string productID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 productID,productType from productStore ");
            strSql.Append(" where productID=@productID ");
            SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = productID;
            ProductStoreME model = new ProductStoreME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 if (ds.Tables[0].Rows[0]["productID"] != null && ds.Tables[0].Rows[0]["productID"].ToString() != "")
                 {
                     model.productID = ds.Tables[0].Rows[0]["productID"].ToString();
                 }
                 if (ds.Tables[0].Rows[0]["productType"] != null && ds.Tables[0].Rows[0]["productType"].ToString() != "")
                 {
                     model.productType = ds.Tables[0].Rows[0]["productType"].ToString();
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
            strSql.Append("select productID,productType ");
            strSql.Append(" FROM productStore ");
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
            strSql.Append(" productID,productType ");
            strSql.Append(" FROM productStore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return _dbAssist.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得满足条件的记录个数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM productStore ");
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
        
        #endregion
      
    }
}
