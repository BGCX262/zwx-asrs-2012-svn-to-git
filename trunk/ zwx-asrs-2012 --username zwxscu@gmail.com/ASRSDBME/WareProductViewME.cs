using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// WareProductViewME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WareProductViewME
    {
        public WareProductViewME()
        { }
        #region Model
        private string _producttype;
        private string _productid;
        private string _name;
        private string _process;
        private int _param1;
        private int _param2;
        private int _houseid;
        private int _houselayerid;
        private int _houserowid;
        private int _housecolumnid;
        private int _usestatus;
        /// <summary>
        /// 
        /// </summary>
        public string productType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string productID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public int  param1
        {
            set { _param1 = value; }
            get { return _param1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int  param2
        {
            set { _param2 = value; }
            get { return _param2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int houseID
        {
            set { _houseid = value; }
            get { return _houseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int houseLayerID
        {
            set { _houselayerid = value; }
            get { return _houselayerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int houseRowID
        {
            set { _houserowid = value; }
            get { return _houserowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int houseColumnID
        {
            set { _housecolumnid = value; }
            get { return _housecolumnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int useStatus
        {
            set { _usestatus = value; }
            get { return _usestatus; }
        }
        #endregion Model

    }
}
