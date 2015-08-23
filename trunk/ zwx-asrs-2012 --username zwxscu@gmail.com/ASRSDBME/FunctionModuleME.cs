using System;
namespace ASRSDBME
{
	/// <summary>
	/// FunctionModuleME:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FunctionModuleME
	{
		public FunctionModuleME()
		{}
		#region Model
		private int? _moduleid;
		private string _modulename;
		private int? _modulenum;
		/// <summary>
		/// 
		/// </summary>
		public int? moduleId
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string moduleName
		{
			set{ _modulename=value;}
			get{return _modulename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? moduleNum
		{
			set{ _modulenum=value;}
			get{return _modulenum;}
		}
		#endregion Model

	}
}

