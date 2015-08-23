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
    /// 仓储信息类数据库操作
    /// </summary>
    public class WarehouseStoreDAL:IWarehouseStoreDAL
    {
        private DBAssist _dbAssist = null;
        public WarehouseStoreDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        #region method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int houseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WarehouseStore ");
            strSql.Append("where houseID=@houseID");
            SqlParameter[] parameters = {
					new SqlParameter("@houseID", SqlDbType.Int,4)			};
            parameters[0].Value = houseID;
            return _dbAssist.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(WarehouseStoreME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WarehouseStore(");
            strSql.Append("houseID,houseLayerID,houseRowID,houseColumnID,name,useStatus,productID)");
            strSql.Append(" values (");
            strSql.Append("@houseID,@houseLayerID,@houseRowID,@houseColumnID,@name,@useStatus,@productID)");
            SqlParameter[] parameters = {
					new SqlParameter("@houseID", SqlDbType.Int,4),
					new SqlParameter("@houseLayerID", SqlDbType.Int,4),
					new SqlParameter("@houseRowID", SqlDbType.Int,4),
					new SqlParameter("@houseColumnID", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NChar,50),
					new SqlParameter("@useStatus", SqlDbType.TinyInt,1),
					new SqlParameter("@productID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.houseID;
            parameters[1].Value = model.houseLayerID;
            parameters[2].Value = model.houseRowID;
            parameters[3].Value = model.houseColumnID;
            parameters[4].Value = model.name;
            parameters[5].Value = model.useStatus;
            parameters[6].Value = model.productID;
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

        // <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WarehouseStoreME model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WarehouseStore set ");
            strSql.Append("houseLayerID=@houseLayerID,");
            strSql.Append("houseRowID=@houseRowID,");
            strSql.Append("houseColumnID=@houseColumnID,");
            strSql.Append("name=@name,");
            strSql.Append("useStatus=@useStatus,");
            strSql.Append("productID=@productID");
            strSql.Append(" where houseID=@houseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@houseLayerID", SqlDbType.Int,4),
					new SqlParameter("@houseRowID", SqlDbType.Int,4),
					new SqlParameter("@houseColumnID", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NChar,50),
					new SqlParameter("@useStatus", SqlDbType.TinyInt,1),
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@houseID", SqlDbType.Int,4)};
            parameters[0].Value = model.houseLayerID;
            parameters[1].Value = model.houseRowID;
            parameters[2].Value = model.houseColumnID;
            parameters[3].Value = model.name;
            parameters[4].Value = model.useStatus;
            parameters[5].Value = model.productID;
            parameters[6].Value = model.houseID;
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
        public bool Delete(int houseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WarehouseStore ");
            strSql.Append(" where houseID=@houseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@houseID", SqlDbType.Int,4)			};
            parameters[0].Value = houseID;
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
        /// 得到一个对象实体
        /// </summary>
        public WarehouseStoreME GetModel(int houseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 houseID,houseLayerID,houseRowID,houseColumnID,name,useStatus,productID from WarehouseStore ");
            strSql.Append(" where houseID=@houseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@houseID", SqlDbType.Int,4)			};
            parameters[0].Value = houseID;
            WarehouseStoreME model = new WarehouseStoreME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["houseID"] != null && ds.Tables[0].Rows[0]["houseID"].ToString() != "")
                {
                    model.houseID = int.Parse(ds.Tables[0].Rows[0]["houseID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseLayerID"] != null && ds.Tables[0].Rows[0]["houseLayerID"].ToString() != "")
                {
                    model.houseLayerID = int.Parse(ds.Tables[0].Rows[0]["houseLayerID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseRowID"] != null && ds.Tables[0].Rows[0]["houseRowID"].ToString() != "")
                {
                    model.houseRowID = int.Parse(ds.Tables[0].Rows[0]["houseRowID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseColumnID"] != null && ds.Tables[0].Rows[0]["houseColumnID"].ToString() != "")
                {
                    model.houseColumnID = int.Parse(ds.Tables[0].Rows[0]["houseColumnID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["useStatus"] != null && ds.Tables[0].Rows[0]["useStatus"].ToString() != "")
                {
                    model.useStatus = int.Parse(ds.Tables[0].Rows[0]["useStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["productID"] != null && ds.Tables[0].Rows[0]["productID"].ToString() != "")
                {
                    model.productID = ds.Tables[0].Rows[0]["productID"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="houseLayerID"></param>
        /// <param name="houseRowID"></param>
        /// <param name="houseColumnID"></param>
        /// <returns></returns>
        public WarehouseStoreME GetModel(int houseLayerID, int houseRowID, int houseColumnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 houseID,houseLayerID,houseRowID,houseColumnID,name,useStatus,productID from WarehouseStore ");
            strSql.Append(" where houseLayerID=@houseLayerID and houseRowID=@houseRowID and houseColumnID=@houseColumnID");
            SqlParameter[] parameters = {
					new SqlParameter("@houseLayerID", SqlDbType.Int,4),
                    new SqlParameter("@houseRowID", SqlDbType.Int,4),
                    new SqlParameter("@houseColumnID", SqlDbType.Int,4)};
            parameters[0].Value = houseLayerID;
            parameters[1].Value = houseRowID;
            parameters[2].Value = houseColumnID;
            WarehouseStoreME model = new WarehouseStoreME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["houseID"] != null && ds.Tables[0].Rows[0]["houseID"].ToString() != "")
                {
                    model.houseID = int.Parse(ds.Tables[0].Rows[0]["houseID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseLayerID"] != null && ds.Tables[0].Rows[0]["houseLayerID"].ToString() != "")
                {
                    model.houseLayerID = int.Parse(ds.Tables[0].Rows[0]["houseLayerID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseRowID"] != null && ds.Tables[0].Rows[0]["houseRowID"].ToString() != "")
                {
                    model.houseRowID = int.Parse(ds.Tables[0].Rows[0]["houseRowID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["houseColumnID"] != null && ds.Tables[0].Rows[0]["houseColumnID"].ToString() != "")
                {
                    model.houseColumnID = int.Parse(ds.Tables[0].Rows[0]["houseColumnID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["useStatus"] != null && ds.Tables[0].Rows[0]["useStatus"].ToString() != "")
                {
                    model.useStatus = int.Parse(ds.Tables[0].Rows[0]["useStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["productID"] != null && ds.Tables[0].Rows[0]["productID"].ToString() != "")
                {
                    model.productID = ds.Tables[0].Rows[0]["productID"].ToString();
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
            strSql.Append("select houseID,houseLayerID,houseRowID,houseColumnID,name,useStatus,productID ");
            strSql.Append(" FROM WarehouseStore ");
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
            strSql.Append(" houseID,houseLayerID,houseRowID,houseColumnID,name,useStatus,productID ");
            strSql.Append(" FROM WarehouseStore ");
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
            strSql.Append("select count(1) FROM WarehouseStore ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = _dbAssist.GetSingle( strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public void ClearAllData()
        {
            _dbAssist.Query("truncate table warehouseStore");
        }
        #endregion
    }
}
