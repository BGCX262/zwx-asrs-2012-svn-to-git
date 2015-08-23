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
	/// 数据访问类:FunctionModuleDAL
	/// </summary>
	public partial class FunctionModuleDAL:IFunctionModuleDAL
	{
		public FunctionModuleDAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FunctionModuleME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into FunctionModule(");
			strSql.Append("moduleId,moduleName,moduleNum)");
			strSql.Append(" values (");
			strSql.Append("@moduleId,@moduleName,@moduleNum)");
			SqlParameter[] parameters = {
					new SqlParameter("@moduleId", SqlDbType.Int,4),
					new SqlParameter("@moduleName", SqlDbType.NVarChar,50),
					new SqlParameter("@moduleNum", SqlDbType.Int,4)};
			parameters[0].Value = model.moduleId;
			parameters[1].Value = model.moduleName;
			parameters[2].Value = model.moduleNum;

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
		public bool Update(FunctionModuleME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update FunctionModule set ");
			strSql.Append("moduleId=@moduleId,");
			strSql.Append("moduleName=@moduleName,");
			strSql.Append("moduleNum=@moduleNum");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@moduleId", SqlDbType.Int,4),
					new SqlParameter("@moduleName", SqlDbType.NVarChar,50),
					new SqlParameter("@moduleNum", SqlDbType.Int,4)};
			parameters[0].Value = model.moduleId;
			parameters[1].Value = model.moduleName;
			parameters[2].Value = model.moduleNum;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from FunctionModule ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

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
		/// 得到一个对象实体
		/// </summary>
		public FunctionModuleME GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 moduleId,moduleName,moduleNum from FunctionModule ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			FunctionModuleME model=new FunctionModuleME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["moduleId"]!=null && ds.Tables[0].Rows[0]["moduleId"].ToString()!="")
				{
					model.moduleId=int.Parse(ds.Tables[0].Rows[0]["moduleId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["moduleName"]!=null && ds.Tables[0].Rows[0]["moduleName"].ToString()!="")
				{
					model.moduleName=ds.Tables[0].Rows[0]["moduleName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["moduleNum"]!=null && ds.Tables[0].Rows[0]["moduleNum"].ToString()!="")
				{
					model.moduleNum=int.Parse(ds.Tables[0].Rows[0]["moduleNum"].ToString());
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
			strSql.Append("select moduleId,moduleName,moduleNum ");
			strSql.Append(" FROM FunctionModule ");
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
			strSql.Append(" moduleId,moduleName,moduleNum ");
			strSql.Append(" FROM FunctionModule ");
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
			strSql.Append("select count(1) FROM FunctionModule ");
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
				strSql.Append("order by T.userName desc");
			}
			strSql.Append(")AS Row, T.*  from FunctionModule T ");
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
			parameters[0].Value = "FunctionModule";
			parameters[1].Value = "userName";
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

