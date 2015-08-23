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
	/// 数据访问类:ProductDAL
	/// </summary>
	public partial class ProductDAL:IProductDAL
	{
		public ProductDAL()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string productID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Product");
			strSql.Append(" where productID=@productID ");
			SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = productID;

            return DbHelperSQL.Exists(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ASRSME.Product.ProductME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Product(");
			strSql.Append("productID,name,process,param1,param2)");
			strSql.Append(" values (");
			strSql.Append("@productID,@name,@process,@param1,@param2)");
			SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@process", SqlDbType.NVarChar,50),
					new SqlParameter("@param1", SqlDbType.Int,4),
					new SqlParameter("@param2", SqlDbType.Int,4)};
			parameters[0].Value = model.productID;
			parameters[1].Value = model.name;
			parameters[2].Value = model.process;
			parameters[3].Value = model.param1;
			parameters[4].Value = model.param2;

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
		public bool Update(ASRSME.Product.ProductME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Product set ");
			strSql.Append("name=@name,");
			strSql.Append("process=@process,");
			strSql.Append("param1=@param1,");
			strSql.Append("param2=@param2");
			strSql.Append(" where productID=@productID ");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@process", SqlDbType.NVarChar,50),
					new SqlParameter("@param1", SqlDbType.Int,4),
					new SqlParameter("@param2", SqlDbType.Int,4),
					new SqlParameter("@productID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.process;
			parameters[2].Value = model.param1;
			parameters[3].Value = model.param2;
			parameters[4].Value = model.productID;

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
		public bool Delete(string productID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Product ");
			strSql.Append(" where productID=@productID ");
			SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = productID;

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
		public bool DeleteList(string productIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Product ");
			strSql.Append(" where productID in ("+productIDlist + ")  ");
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
		public ASRSME.Product.ProductME GetModel(string productID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 productID,name,process,param1,param2 from Product ");
			strSql.Append(" where productID=@productID ");
			SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = productID;

			ASRSME.Product.ProductME model=new ASRSME.Product.ProductME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringProductDB, strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["productID"]!=null && ds.Tables[0].Rows[0]["productID"].ToString()!="")
				{
					model.productID=ds.Tables[0].Rows[0]["productID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["name"]!=null && ds.Tables[0].Rows[0]["name"].ToString()!="")
				{
					model.name=ds.Tables[0].Rows[0]["name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["process"]!=null && ds.Tables[0].Rows[0]["process"].ToString()!="")
				{
					model.process=ds.Tables[0].Rows[0]["process"].ToString();
				}
				if(ds.Tables[0].Rows[0]["param1"]!=null && ds.Tables[0].Rows[0]["param1"].ToString()!="")
				{
					model.param1=int.Parse(ds.Tables[0].Rows[0]["param1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["param2"]!=null && ds.Tables[0].Rows[0]["param2"].ToString()!="")
				{
					model.param2=int.Parse(ds.Tables[0].Rows[0]["param2"].ToString());
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
			strSql.Append("select productID,name,process,param1,param2 ");
			strSql.Append(" FROM Product ");
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
			strSql.Append(" productID,name,process,param1,param2 ");
			strSql.Append(" FROM Product ");
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
			strSql.Append("select count(1) FROM Product ");
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
				strSql.Append("order by T.productID desc");
			}
			strSql.Append(")AS Row, T.*  from Product T ");
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
			parameters[0].Value = "Product";
			parameters[1].Value = "productID";
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

