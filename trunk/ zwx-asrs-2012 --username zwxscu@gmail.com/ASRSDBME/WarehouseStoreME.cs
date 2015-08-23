using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRSDBME
{
    /// <summary>
    /// WarehouseStoreME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WarehouseStoreME
    {
        public WarehouseStoreME()
        { }
        #region Model
        private int _houseid;
        private int _houselayerid;
        private int _houserowid;
        private int _housecolumnid;
        private string _name;
        private int _usestatus;
        private string _productid;
        /// <summary>
        /// 货架单元唯一索引ID
        /// </summary>
        public int houseID
        {
            set { _houseid = value; }
            get { return _houseid; }
        }
        /// <summary>
        /// 仓位层号：从1开始编号
        /// </summary>
        public int houseLayerID
        {
            set { _houselayerid = value; }
            get { return _houselayerid; }
        }
        /// <summary>
        /// 仓位行号：从1开始编号
        /// </summary>
        public int houseRowID
        {
            set { _houserowid = value; }
            get { return _houserowid; }
        }
        /// <summary>
        /// 仓位列号：从1开始编号
        /// </summary>
        public int houseColumnID
        {
            set { _housecolumnid = value; }
            get { return _housecolumnid; }
        }
        /// <summary>
        /// 仓位名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 仓位状态
        /// </summary>
        public int useStatus
        {
            set { _usestatus = value; }
            get { return _usestatus; }
        }
        /// <summary>
        /// 该仓位的产品ID
        /// </summary>
        public string productID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        #endregion Model

    }
}
