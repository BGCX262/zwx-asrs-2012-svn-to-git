using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ASRSIDAL.Product;
using ASRSME.Product;
using Maticsoft.DBUtility;//Please add references
namespace ASRSSQLServerDAL.Product
{
	/// <summary>
	/// 数据访问类:OutHouseRecordDAL
	/// </summary>
	public partial class OutHouseRecordDAL:IOutHouseRecordDAL
	{
		public OutHouseRecordDAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ASRSME.Product.OutHouseRecordME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OutHouseRecord(");
			strSql.Append("count,productID,outHouseTime,houseID)");
			strSql.Append(" values (");
			strSql.Append("@count,@productID,@outHouseTime,@houseID)");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4),
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@outHouseTime", SqlDbType.Timestamp,8),
					new SqlParameter("@houseID", SqlDbType.Int,4)};
			parameters[0].Value = model.count;
			parameters[1].Value = model.productID;
			parameters[2].Value = model.outHouseTime;
			parameters[3].Value = model.houseID;

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
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
		public bool Update(ASRSME.Product.OutHouseRecordME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OutHouseRecord set ");
			strSql.Append("count=@count,");
			strSql.Append("productID=@productID,");
			strSql.Append("houseID=@houseID");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4),
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@houseID", SqlDbType.Int,4)};
			parameters[0].Value = model.count;
			parameters[1].Value = model.productID;
			parameters[2].Value = model.houseID;

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OutHouseRecord ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
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
		public ASRSME.Product.OutHouseRecordME GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 count,productID,outHouseTime,houseID from OutHouseRecord ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

		    OutHouseRecordME model=new OutHouseRecordME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["count"]!=null && ds.Tables[0].Rows[0]["count"].ToString()!="")
				{
					model.count=int.Parse(ds.Tables[0].Rows[0]["count"].ToString());
				}
				if(ds.Tables[0].Rows[0]["productID"]!=null && ds.Tables[0].Rows[0]["productID"].ToString()!="")
				{
					model.productID=ds.Tables[0].Rows[0]["productID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["outHouseTime"]!=null && ds.Tables[0].Rows[0]["outHouseTime"].ToString()!="")
				{
					model.outHouseTime=DateTime.Parse(ds.Tables[0].Rows[0]["outHouseTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["houseID"]!=null && ds.Tables[0].Rows[0]["houseID"].ToString()!="")
				{
					model.houseID=int.Parse(ds.Tables[0].Rows[0]["houseID"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count,productID,outHouseTime,houseID ");
			strSql.Append(" FROM OutHouseRecord ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString(),PubConstant.ConnectionStringProductDB);
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" count,productID,outHouseTime,houseID ");
			strSql.Append(" FROM OutHouseRecord ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString(),PubConstant.ConnectionStringProductDB);
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM OutHouseRecord ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),PubConstant.ConnectionStringProductDB);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.count desc");
			}
			strSql.Append(")AS Row, T.*  from OutHouseRecord T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString(),PubConstant.ConnectionStringProductDB);
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
			parameters[0].Value = "OutHouseRecord";
			parameters[1].Value = "count";
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

