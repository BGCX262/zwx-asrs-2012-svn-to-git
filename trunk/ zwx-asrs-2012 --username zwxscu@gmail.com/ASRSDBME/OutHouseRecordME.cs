using System;
namespace ASRSDBME
{
    /// <summary>
    /// OutHouseRecordME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OutHouseRecordME
    {
        public OutHouseRecordME()
        { }
        #region Model
        private DateTime _outhousetime;
        private string _productid;
        private int _houseid;
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime outHouseTime
        {
            set { _outhousetime = value; }
            get { return _outhousetime; }
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string productID
        {
            set { _productid = value; }
            get { return _productid; }
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

