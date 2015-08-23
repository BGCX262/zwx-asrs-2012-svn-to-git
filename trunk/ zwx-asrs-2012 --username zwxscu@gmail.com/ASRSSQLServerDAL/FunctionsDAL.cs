using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ASRSIDAL.Account;
using ASRSME.Account;
using Maticsoft.DBUtility;//Please add references
namespace ASRSSQLServerDAL.Account
{
	/// <summary>
	/// 数据访问类:FunctionsDAL
	/// </summary>
	public partial class FunctionsDAL:IFunctionsDAL
	{
		public FunctionsDAL()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("functionid", "Functions",PubConstant.ConnectionStringAccountDB); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int functionid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Functions");
			strSql.Append(" where functionid=@functionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@functionid", SqlDbType.Int,4)};
			parameters[0].Value = functionid;

            return DbHelperSQL.Exists(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FunctionsME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Functions(");
			strSql.Append("functionid,functionName,moduleNo)");
			strSql.Append(" values (");
			strSql.Append("@functionid,@functionName,@moduleNo)");
			SqlParameter[] parameters = {
					new SqlParameter("@functionid", SqlDbType.Int,4),
					new SqlParameter("@functionName", SqlDbType.NVarChar,50),
					new SqlParameter("@moduleNo", SqlDbType.Int,4)};
			parameters[0].Value = model.functionid;
			parameters[1].Value = model.functionName;
			parameters[2].Value = model.moduleNo;

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
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
		public bool Update(FunctionsME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Functions set ");
			strSql.Append("functionName=@functionName,");
			strSql.Append("moduleNo=@moduleNo");
			strSql.Append(" where functionid=@functionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@functionName", SqlDbType.NVarChar,50),
					new SqlParameter("@moduleNo", SqlDbType.Int,4),
					new SqlParameter("@functionid", SqlDbType.Int,4)};
			parameters[0].Value = model.functionName;
			parameters[1].Value = model.moduleNo;
			parameters[2].Value = model.functionid;

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
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
		public bool Delete(int functionid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Functions ");
			strSql.Append(" where functionid=@functionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@functionid", SqlDbType.Int,4)			};
			parameters[0].Value = functionid;

            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
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
		public bool DeleteList(string functionidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Functions ");
			strSql.Append(" where functionid in ("+functionidlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
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
		public FunctionsME GetModel(int functionid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 functionid,functionName,moduleNo from Functions ");
			strSql.Append(" where functionid=@functionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@functionid", SqlDbType.Int,4)			};
			parameters[0].Value = functionid;

			FunctionsME model=new FunctionsME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["functionid"]!=null && ds.Tables[0].Rows[0]["functionid"].ToString()!="")
				{
					model.functionid=int.Parse(ds.Tables[0].Rows[0]["functionid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["functionName"]!=null && ds.Tables[0].Rows[0]["functionName"].ToString()!="")
				{
					model.functionName=ds.Tables[0].Rows[0]["functionName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["moduleNo"]!=null && ds.Tables[0].Rows[0]["moduleNo"].ToString()!="")
				{
					model.moduleNo=int.Parse(ds.Tables[0].Rows[0]["moduleNo"].ToString());
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
			strSql.Append("select functionid,functionName,moduleNo ");
			strSql.Append(" FROM Functions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DbHelperSQL.Query(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
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
			strSql.Append(" functionid,functionName,moduleNo ");
			strSql.Append(" FROM Functions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Functions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
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
				strSql.Append("order by T.functionid desc");
			}
			strSql.Append(")AS Row, T.*  from Functions T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
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
			parameters[0].Value = "Functions";
			parameters[1].Value = "functionid";
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

