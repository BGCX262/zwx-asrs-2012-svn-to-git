using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// MessageDefineME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MessageDefineME
    {
        public MessageDefineME()
        { }
        #region Model
        private int _messageid;
        private string _messagecontent;
        private int _messagetype;
        /// <summary>
        /// 
        /// </summary>
        public int messageID
        {
            set { _messageid = value; }
            get { return _messageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string messageContent
        {
            set { _messagecontent = value; }
            get { return _messagecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int messageType
        {
            set { _messagetype = value; }
            get { return _messagetype; }
        }
        #endregion Model

    }
}
