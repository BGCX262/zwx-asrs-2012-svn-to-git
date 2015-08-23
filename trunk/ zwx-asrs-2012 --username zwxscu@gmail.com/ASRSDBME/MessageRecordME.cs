using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// MessageRecordME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MessageRecordME
    {
        public MessageRecordME()
        { }
        #region Model
        private DateTime  _happentime;
        private int _messageid;
        private string _dynamiccontent;
        /// <summary>
        /// 
        /// </summary>
        public string dynamicContent
        {
            set { _dynamiccontent = value; }
            get { return _dynamiccontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime happenTime
        {
            set { _happentime = value; }
            get { return _happentime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int messageID
        {
            set { _messageid = value; }
            get { return _messageid; }
        }
        #endregion Model

    }
}
