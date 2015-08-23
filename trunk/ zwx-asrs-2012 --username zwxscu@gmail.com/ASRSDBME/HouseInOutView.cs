using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// HouseInOutViewME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HouseInOutViewME
    {
        public HouseInOutViewME()
        { }
        #region Model
        private string _productid;
        private string _producttype;
        private DateTime _inhousetime;
        private int _houseid;
        private DateTime _outhousetime;
        private int _houselayerid;
        private int _houserowid;
        private int _housecolumnid;
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
        public string productType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime inHouseTime
        {
            set { _inhousetime = value; }
            get { return _inhousetime; }
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
        public DateTime outHouseTime
        {
            set { _outhousetime = value; }
            get { return _outhousetime; }
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
        #endregion Model

    }
}
