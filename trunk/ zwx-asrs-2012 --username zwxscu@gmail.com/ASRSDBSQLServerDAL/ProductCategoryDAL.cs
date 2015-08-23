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
    public class ProductCategoryDAL:IProductCategoryDAL
    {
        private DBAssist _dbAssist = null;
        public ProductCategoryDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string productType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProductCategory");
            strSql.Append("where productType =@productType");
            SqlParameter[] parameters ={
                    new SqlParameter("@productType",SqlDbType.NVarChar,50)};
            parameters[0].Value = productType;
            return _dbAssist.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProductCategoryME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProductCategory(");
            strSql.Append("productType,name,process,param1,param2)");
            strSql.Append(" values (");
            strSql.Append("@productType,@name,@process,@param1,@param2)");
            SqlParameter[] parameters = {
					new SqlParameter("@productType", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@process", SqlDbType.NVarChar,50),
					new SqlParameter("@param1", SqlDbType.Int,4),
					new SqlParameter("@param2", SqlDbType.Int,4)};
            parameters[0].Value = model.productType;
            parameters[1].Value = model.name;
            parameters[2].Value = model.process;
            parameters[3].Value = model.param1;
            parameters[4].Value = model.param2;
            int rows = _dbAssist.ExecuteSql( strSql.ToString(), parameters);
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
        public bool Update(ProductCategoryME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProductCategory set ");
            strSql.Append("name=@name,");
            strSql.Append("process=@process,");
            strSql.Append("param1=@param1,");
            strSql.Append("param2=@param2");
            strSql.Append(" where productType=@productType ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@process", SqlDbType.NVarChar,50),
					new SqlParameter("@param1", SqlDbType.Int,4),
					new SqlParameter("@param2", SqlDbType.Int,4),
					new SqlParameter("@productType", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.process;
            parameters[2].Value = model.param1;
            parameters[3].Value = model.param2;
            parameters[4].Value = model.productType;
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
        /// <param name="productTypeID">产品型号</param>
        /// <returns></returns>
        public bool Delete(string productType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProductCategory ");
            strSql.Append(" where productType=@productType ");
            SqlParameter[] parameters = {
					new SqlParameter("@productType", SqlDbType.NVarChar,50)			};
            parameters[0].Value = productType;
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
        public bool DeleteList(string productTypelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProductCategory ");
            strSql.Append(" where productType in (" + productTypelist + ")  ");
            int rows = _dbAssist.ExecuteSql( strSql.ToString());
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
        /// <param name="productTypeID">产品型号</param>
        /// <returns></returns>
        public ProductCategoryME GetModel(string productType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 productType,name,process,param1,param2 from ProductCategory ");
            strSql.Append(" where productType=@productType ");
            SqlParameter[] parameters = {
					new SqlParameter("@productType", SqlDbType.NVarChar,50)			};
            parameters[0].Value = productType;

            ProductCategoryME model = new ProductCategoryME();
            DataSet ds = _dbAssist.Query( strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["productType"] != null && ds.Tables[0].Rows[0]["productType"].ToString() != "")
                {
                    model.productType = ds.Tables[0].Rows[0]["productType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["process"] != null && ds.Tables[0].Rows[0]["process"].ToString() != "")
                {
                    model.process = ds.Tables[0].Rows[0]["process"].ToString();
                }
                if (ds.Tables[0].Rows[0]["param1"] != null && ds.Tables[0].Rows[0]["param1"].ToString() != "")
                {
                    model.param1 = int.Parse(ds.Tables[0].Rows[0]["param1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["param2"] != null && ds.Tables[0].Rows[0]["param2"].ToString() != "")
                {
                    model.param2 = int.Parse(ds.Tables[0].Rows[0]["param2"].ToString());
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
            strSql.Append("select productType,name,process,param1,param2 ");
            strSql.Append(" FROM ProductCategory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return _dbAssist.Query( strSql.ToString());
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
           strSql.Append(" productType,name,process,param1,param2 ");
           strSql.Append(" FROM ProductCategory ");
           if (strWhere.Trim() != "")
           {
               strSql.Append(" where " + strWhere);
           }
           strSql.Append(" order by " + filedOrder);
           return _dbAssist.Query( strSql.ToString());
       }

        /// <summary>
        /// 获得满足条件的记录个数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ProductCategory ");
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
