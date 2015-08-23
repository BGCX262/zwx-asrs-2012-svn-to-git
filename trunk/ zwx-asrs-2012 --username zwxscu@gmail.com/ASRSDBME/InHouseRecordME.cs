using System;
namespace ASRSDBME
{
    /// <summary>
    /// InHouseRecordME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class InHouseRecordME
    {
        public InHouseRecordME()
        { }
        #region Model
        private string _productid;
        private DateTime _inhousetime;
        private int _houseid;
        /// <summary>
        /// 
        /// </summary>
        public string productID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime inHouseTime
        {
            set { _inhousetime = value; }
            get { return _inhousetime; }
        }
        /// <summary>
        /// 仓位ID
        /// </summary>
        public int houseID
        {
            set { _houseid = value; }
            get { return _houseid; }
        }
        #endregion Model

    }
}

