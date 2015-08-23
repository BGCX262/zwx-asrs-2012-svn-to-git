using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// ProductStoreME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductStoreME
    {
        public ProductStoreME()
        { }
        #region Model
        private string _productid;
        private string _producttype;
        /// <summary>
        /// 产品ID，主键
        /// </summary>
        public string productID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string productType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        #endregion Model

    }
}
