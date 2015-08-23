using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// ProductCategoryME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductCategoryME
    {
        public ProductCategoryME()
        { }
        #region Model
        private string _producttype;
        private string _name;
        private string _process;
        private int _param1;
        private int  _param2;
        /// <summary>
        /// 产品型号，一个型号可以有多个产品条码号对应
        /// </summary>
        public string productType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 该类产品型号的名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string process
        {
            set { _process = value; }
            get { return _process; }
        }
        /// <summary>
        /// 工艺参数1
        /// </summary>
        public int  param1
        {
            set { _param1 = value; }
            get { return _param1; }
        }
        /// <summary>
        /// 工艺参数2
        /// </summary>
        public int  param2
        {
            set { _param2 = value; }
            get { return _param2; }
        }
        #endregion Model

    }
}
