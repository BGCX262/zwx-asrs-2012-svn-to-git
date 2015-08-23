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
    public class WareProductViewDAL:IWareProductViewDAL
    {
        private DBAssist _dbAssist = null;
        public WareProductViewDAL(string ConnectionString)
        {
            _dbAssist = new DBAssist(ConnectionString);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WareProductViewME GetModel(int houseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 productType,productID,name,process,param1,param2,houseID,houseLayerID,houseRowID,houseColumnID,useStatus from WareProductView ");
            strSql.Append(" where  houseID=@houseID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@houseID", SqlDbType.Int,4)};
            parameters[0].Value = houseID;
            WareProductViewME model = new WareProductViewME();
            DataSet ds = _dbAssist.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["productType"] != null && ds.Tables[0].Rows[0]["productType"].ToString() != "")
                {
                    model.productType = ds.Tables[0].Rows[0]["productType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["productID"] != null && ds.Tables[0].Rows[0]["productID"].ToString() != "")
                {
                    model.productID = ds.Tables[0].Rows[0]["productID"].ToString();
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
                if (ds.Tables[0].Rows[0]["useStatus"] != null && ds.Tables[0].Rows[0]["useStatus"].ToString() != "")
                {
                    model.useStatus = int.Parse(ds.Tables[0].Rows[0]["useStatus"].ToString());
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
            strSql.Append("select productType,productID,name,process,param1,param2,houseID,houseLayerID,houseRowID,houseColumnID,useStatus ");
            strSql.Append(" FROM WareProductView ");
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
            strSql.Append(" productType,productID,name,process,param1,param2,houseID,houseLayerID,houseRowID,houseColumnID,useStatus ");
            strSql.Append(" FROM WareProductView ");
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
            strSql.Append("select count(1) FROM WareProductView ");
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
        /// 第一个符合条件的仓位
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public WareProductViewME GetConditionedModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * FROM  WareProductView");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = _dbAssist.Query(strSql.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                WareProductViewME model = new WareProductViewME();
                if (ds.Tables[0].Rows[0]["productType"] != null && ds.Tables[0].Rows[0]["productType"].ToString() != "")
                {
                    model.productType = ds.Tables[0].Rows[0]["productType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["productID"] != null && ds.Tables[0].Rows[0]["productID"].ToString() != "")
                {
                    model.productID = ds.Tables[0].Rows[0]["productID"].ToString();
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
                if (ds.Tables[0].Rows[0]["useStatus"] != null && ds.Tables[0].Rows[0]["useStatus"].ToString() != "")
                {
                    model.useStatus = int.Parse(ds.Tables[0].Rows[0]["useStatus"].ToString());
                }
                return model;
            }
            else
                return null;
        }
    }
}
