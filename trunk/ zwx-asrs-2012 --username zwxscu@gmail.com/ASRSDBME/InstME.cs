using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
namespace ASRSDBME
{
    /// <summary>
    /// InstME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class InstME
    {
        public InstME()
        { }
        #region Model
        private string _instid;
        private int _instcode;
        private string _instobj;
        private int _vehiclealloc;
        /// <summary>
        /// 指令ID
        /// </summary>
        public string instID
        {
            set { _instid = value; }
            get { return _instid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int instCode
        {
            set { _instcode = value; }
            get { return _instcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string instObj
        {
            set { _instobj = value; }
            get { return _instobj; }
        }
        /// <summary>
        /// 该指令分配的小车编号，从1开始
        /// </summary>
        public int vehicleAlloc
        {
            set { _vehiclealloc = value; }
            get { return _vehiclealloc; }
        }
        #endregion Model

    }
}
