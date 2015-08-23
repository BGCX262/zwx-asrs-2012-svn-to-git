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
	/// 数据访问类:InHouseRecordDAL
	/// </summary>
	public partial class InHouseRecordDAL:IInHouseRecordDAL
	{
		public InHouseRecordDAL()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("count", "InHouseRecord",PubConstant.ConnectionStringProductDB); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int count)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from InHouseRecord");
			strSql.Append(" where count=@count ");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4)			};
			parameters[0].Value = count;

            return DbHelperSQL.Exists(PubConstant.ConnectionStringProductDB,strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(InHouseRecordME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into InHouseRecord(");
			strSql.Append("count,productID,inHouseTime,houseID)");
			strSql.Append(" values (");
			strSql.Append("@count,@productID,@inHouseTime,@houseID)");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4),
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@inHouseTime", SqlDbType.Timestamp,8),
					new SqlParameter("@houseID", SqlDbType.Int,4)};
			parameters[0].Value = model.count;
			parameters[1].Value = model.productID;
			parameters[2].Value = model.inHouseTime;
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
		public bool Update(InHouseRecordME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update InHouseRecord set ");
			strSql.Append("productID=@productID,");
			strSql.Append("houseID=@houseID");
			strSql.Append(" where count=@count ");
			SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@houseID", SqlDbType.Int,4),
					new SqlParameter("@count", SqlDbType.Int,4)};
			parameters[0].Value = model.productID;
			parameters[1].Value = model.houseID;
			parameters[2].Value = model.count;

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
		public bool Delete(int count)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from InHouseRecord ");
			strSql.Append(" where count=@count ");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4)			};
			parameters[0].Value = count;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string countlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from InHouseRecord ");
			strSql.Append(" where count in ("+countlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),PubConstant.ConnectionStringProductDB);
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
		public InHouseRecordME GetModel(int count)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 count,productID,inHouseTime,houseID from InHouseRecord ");
			strSql.Append(" where count=@count ");
			SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4)			};
			parameters[0].Value = count;

			InHouseRecordME model=new InHouseRecordME();
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
				if(ds.Tables[0].Rows[0]["inHouseTime"]!=null && ds.Tables[0].Rows[0]["inHouseTime"].ToString()!="")
				{
					model.inHouseTime=DateTime.Parse(ds.Tables[0].Rows[0]["inHouseTime"].ToString());
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
			strSql.Append("select count,productID,inHouseTime,houseID ");
			strSql.Append(" FROM InHouseRecord ");
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
			strSql.Append(" count,productID,inHouseTime,houseID ");
			strSql.Append(" FROM InHouseRecord ");
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
			strSql.Append("select count(1) FROM InHouseRecord ");
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
			strSql.Append(")AS Row, T.*  from InHouseRecord T ");
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
			parameters[0].Value = "InHouseRecord";
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

