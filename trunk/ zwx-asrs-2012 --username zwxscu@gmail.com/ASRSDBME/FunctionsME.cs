using System;
namespace ASRSME.Account
{
	/// <summary>
	/// FunctionsME:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FunctionsME
	{
		public FunctionsME()
		{}
		#region Model
		private int _functionid;
		private string _functionname;
		private int? _moduleno;
		/// <summary>
		/// 
		/// </summary>
		public int functionid
		{
			set{ _functionid=value;}
			get{return _functionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string functionName
		{
			set{ _functionname=value;}
			get{return _functionname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? moduleNo
		{
			set{ _moduleno=value;}
			get{return _moduleno;}
		}
		#endregion Model

	}
}

