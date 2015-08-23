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
	/// 数据访问类:RolesDAL
	/// </summary>
	public partial class RolesDAL:IRolesDAL
	{
		public RolesDAL()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string rolename)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Roles");
			strSql.Append(" where rolename=@rolename ");
			SqlParameter[] parameters = {
					new SqlParameter("@rolename", SqlDbType.NVarChar,50)			};
			parameters[0].Value = rolename;

            return DbHelperSQL.Exists(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(RolesME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Roles(");
			strSql.Append("roleid,rolename,rolevalue)");
			strSql.Append(" values (");
			strSql.Append("@roleid,@rolename,@rolevalue)");
			SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4),
					new SqlParameter("@rolename", SqlDbType.NVarChar,50),
					new SqlParameter("@rolevalue", SqlDbType.VarChar)};
			parameters[0].Value = model.roleid;
			parameters[1].Value = model.rolename;
			parameters[2].Value = model.rolevalue;

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
		public bool Update(RolesME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Roles set ");
			strSql.Append("roleid=@roleid,");
			strSql.Append("rolevalue=@rolevalue");
			strSql.Append(" where rolename=@rolename ");
			SqlParameter[] parameters = {
					new SqlParameter("@roleid", SqlDbType.Int,4),
					new SqlParameter("@rolevalue", SqlDbType.VarChar),
					new SqlParameter("@rolename", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.roleid;
			parameters[1].Value = model.rolevalue;
			parameters[2].Value = model.rolename;

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
		public bool Delete(string rolename)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Roles ");
			strSql.Append(" where rolename=@rolename ");
			SqlParameter[] parameters = {
					new SqlParameter("@rolename", SqlDbType.NVarChar,50)			};
			parameters[0].Value = rolename;

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
		public bool DeleteList(string rolenamelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Roles ");
			strSql.Append(" where rolename in ("+rolenamelist + ")  ");
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
		public RolesME GetModel(string rolename)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 roleid,rolename,rolevalue from Roles ");
			strSql.Append(" where rolename=@rolename ");
			SqlParameter[] parameters = {
					new SqlParameter("@rolename", SqlDbType.NVarChar,50)			};
			parameters[0].Value = rolename;

			RolesME model=new RolesME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["roleid"]!=null && ds.Tables[0].Rows[0]["roleid"].ToString()!="")
				{
					model.roleid=int.Parse(ds.Tables[0].Rows[0]["roleid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["rolename"]!=null && ds.Tables[0].Rows[0]["rolename"].ToString()!="")
				{
					model.rolename=ds.Tables[0].Rows[0]["rolename"].ToString();
				}
				if(ds.Tables[0].Rows[0]["rolevalue"]!=null && ds.Tables[0].Rows[0]["rolevalue"].ToString()!="")
				{
					model.rolevalue=ds.Tables[0].Rows[0]["rolevalue"].ToString();
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
			strSql.Append("select roleid,rolename,rolevalue ");
			strSql.Append(" FROM Roles ");
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
			strSql.Append(" roleid,rolename,rolevalue ");
			strSql.Append(" FROM Roles ");
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
			strSql.Append("select count(1) FROM Roles ");
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
				strSql.Append("order by T.rolename desc");
			}
			strSql.Append(")AS Row, T.*  from Roles T ");
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
			parameters[0].Value = "Roles";
			parameters[1].Value = "rolename";
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

