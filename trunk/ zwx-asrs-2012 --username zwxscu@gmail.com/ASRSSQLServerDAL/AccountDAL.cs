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
	/// 数据访问类:AccountDAL
	/// </summary>
	public partial class AccountDAL:IAccountDAL
	{
		public AccountDAL()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string userName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Account");
			strSql.Append(" where userName=@userName ");
			SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = userName;

            return DbHelperSQL.Exists(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AccountME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Account(");
			strSql.Append("userID,userName,roleName,userPassword)");
			strSql.Append(" values (");
			strSql.Append("@userID,@userName,@roleName,@userPassword)");
			SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.Decimal,5),
					new SqlParameter("@userName", SqlDbType.NVarChar,50),
					new SqlParameter("@roleName", SqlDbType.NVarChar,50),
					new SqlParameter("@userPassword", SqlDbType.NChar,6)};
			parameters[0].Value = model.userID;
			parameters[1].Value = model.userName;
			parameters[2].Value = model.roleName;
			parameters[3].Value = model.userPassword;

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
		public bool Update(AccountME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Account set ");
			strSql.Append("userID=@userID,");
			strSql.Append("roleName=@roleName,");
			strSql.Append("userPassword=@userPassword");
			strSql.Append(" where userName=@userName ");
			SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.Decimal,5),
					new SqlParameter("@roleName", SqlDbType.NVarChar,50),
					new SqlParameter("@userPassword", SqlDbType.NChar,6),
					new SqlParameter("@userName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.userID;
			parameters[1].Value = model.roleName;
			parameters[2].Value = model.userPassword;
			parameters[3].Value = model.userName;

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
		public bool Delete(string userName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Account ");
			strSql.Append(" where userName=@userName ");
			SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = userName;

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
		public bool DeleteList(string userNamelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Account ");
			strSql.Append(" where userName in ("+userNamelist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(PubConstant.ConnectionStringAccountDB,strSql.ToString());
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
		public AccountME GetModel(string userName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 userID,userName,roleName,userPassword from Account ");
			strSql.Append(" where userName=@userName ");
			SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.NVarChar,50)			};
			parameters[0].Value = userName;

			AccountME model=new AccountME();
            DataSet ds = DbHelperSQL.Query(PubConstant.ConnectionStringAccountDB,strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["userID"]!=null && ds.Tables[0].Rows[0]["userID"].ToString()!="")
				{
					model.userID=decimal.Parse(ds.Tables[0].Rows[0]["userID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["userName"]!=null && ds.Tables[0].Rows[0]["userName"].ToString()!="")
				{
					model.userName=ds.Tables[0].Rows[0]["userName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["roleName"]!=null && ds.Tables[0].Rows[0]["roleName"].ToString()!="")
				{
					model.roleName=ds.Tables[0].Rows[0]["roleName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["userPassword"]!=null && ds.Tables[0].Rows[0]["userPassword"].ToString()!="")
				{
					model.userPassword=ds.Tables[0].Rows[0]["userPassword"].ToString();
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
			strSql.Append("select userID,userName,roleName,userPassword ");
			strSql.Append(" FROM Account ");
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
			strSql.Append(" userID,userName,roleName,userPassword ");
			strSql.Append(" FROM Account ");
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
			strSql.Append("select count(1) FROM Account ");
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
			strSql.Append(")AS Row, T.*  from Account T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), PubConstant.ConnectionStringAccountDB);
		}

		#endregion  Method
	}
}

