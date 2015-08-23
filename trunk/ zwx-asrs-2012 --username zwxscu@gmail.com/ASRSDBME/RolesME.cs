using System;
namespace ASRSDBME
{
	/// <summary>
	/// RolesME:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RolesME
	{
		public RolesME()
		{}
		#region Model
		private int _roleid;
		private string _rolename;
		private string _rolevalue;
		/// <summary>
		/// 
		/// </summary>
		public int roleid
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rolename
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rolevalue
		{
			set{ _rolevalue=value;}
			get{return _rolevalue;}
		}
		#endregion Model

	}
}

