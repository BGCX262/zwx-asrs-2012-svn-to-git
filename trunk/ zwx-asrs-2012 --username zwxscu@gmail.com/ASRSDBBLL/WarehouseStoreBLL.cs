using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ASRSDBME;
using ASRSDBFactory;
using ASRSDBIDAL;
namespace ASRSDBBLL
{
    public enum WarehouseStatus
    {
        HOUSE_EMPTY=1,
        HOSUE_FULL,
        HOUSE_ERROR
        
    }
    public class WarehouseStoreBLL
    {
        private readonly IWarehouseStoreDAL _dal = DALFactory.CreateWarehouseStoreDAL();
        private readonly IWareProductViewDAL _wpViewDal = DALFactory.CreateWareProductViewDAL();
#region 公共方法

        /// <summary>
        /// 锁定仓位,不允许其它任务对其进行出入库
        /// </summary>
        /// <param name="CellIndex"></param>
        /// <returns></returns>
        public bool LockHousecell(int CellIndex)
        {
            return true;
        }
        public bool LockHousecell(int L,int R,int C)
        {
            return true;
        }

        /// <summary>
        /// 仓位是否处于锁定状态
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public bool IsHousecellLocked(int cellIndex)
        {
            return true;
        }
        public bool IsHousecellLocked(int L,int R,int C)
        {
            return true;
        }
        public void ClearAllData()
        {
            _dal.ClearAllData();
        }
        public int GetCellCount()
        {
            return _dal.GetRecordCount(" ");
        }
        /// <summary>
        /// 获取所有仓位信息
        /// </summary>
        /// <returns></returns>
        public IList<WarehouseStoreME> GetAllHouseCells()
        {
            DataSet ds = _dal.GetList("  ");
            if(ds.Tables.Count> 0 && ds.Tables[0] != null)
            {
                List<WarehouseStoreME> cellList = new List<WarehouseStoreME>();
                
                foreach(DataRow rw in ds.Tables[0].Rows)
                {
                    WarehouseStoreME m = new WarehouseStoreME();
                    m.houseID = int.Parse(rw["houseID"].ToString());
                    m.name = rw["name"].ToString();
                    m.productID = rw["productID"].ToString();
                    m.houseLayerID = int.Parse(rw["houseLayerID"].ToString());
                    m.houseRowID = int.Parse(rw["houseRowID"].ToString());
                    m.houseColumnID = int.Parse(rw["houseColumnID"].ToString());
                    m.useStatus = int.Parse(rw["useStatus"].ToString());
                    cellList.Add(m);
                }
                return cellList;
            }
            return null;
        }

        /// <summary>
        /// 得到所有数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllData()
        {
            DataSet ds = _wpViewDal.GetList(" ");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }
        /// <summary>
        /// 设置仓位状态
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool SetHousecellStatus(int cellIndex,WarehouseStatus s)
        {
            IWarehouseStoreDAL _dal = DALFactory.CreateWarehouseStoreDAL();
            WarehouseStoreME m = _dal.GetModel(cellIndex);
            if (m != null)
            {
                m.useStatus = (int)s;
                return _dal.Update(m);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得仓位存储的产品ID
        /// </summary>
        /// <param name="houseID"></param>
        /// <returns></returns>
        public string GetCellProductID(int houseID)
        {
            WareProductViewME m  = _wpViewDal.GetModel(houseID);
            if(m != null)
            {
                return m.productID;
            }
            return string.Empty;
        }
        /// <summary>
        /// 增加仓位信息
        /// </summary>
        /// <param name="cell">仓位信息记录单元</param>
        /// <returns>若已存在或不满足一致性约束则返回false</returns>
        public bool AddHousecell(WarehouseStoreME cell)
        {
            if (_dal.Exists(cell.houseID))
            {
                return false;
            }
            return _dal.Add(cell);
        }

        /// <summary>
        /// 删除所有货架信息记录，慎用此接口，只允许系统维护级用户调用
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllcells()
        {
            return false;
        }
        public bool DeleteHouseCell(int houseID)
        {
            return _dal.Delete(houseID);
        }
        /// <summary>
        /// 更新货架仓储信息
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="ProductID"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool UpdateHousecellStore(int cellIndex,string ProductID,WarehouseStatus s)
        {
           WarehouseStoreME m = _dal.GetModel(cellIndex);
           if (m != null)
           {
               m.productID = ProductID;
               m.useStatus = (int)s;
               return _dal.Update(m);
           }
           else
           {
               return false;
           }
        }

        /// <summary>
        /// 清空货架仓储信息
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public bool ClearHousecell(int cellIndex)
        {
            WarehouseStoreME m = _dal.GetModel(cellIndex);
            if(m != null)
            {
                m.productID = string.Empty;
                m.useStatus = (int)WarehouseStatus.HOUSE_EMPTY;
                return _dal.Update(m);
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 得到仓位状态
        /// </summary>
        /// <param name="L">层号</param>
        /// <param name="R">行号</param>
        /// <param name="C">列号</param>
        /// <returns></returns>
        public WarehouseStatus GetHousecellStatus(int L,int R,int C)
        {
            WarehouseStoreME m = _dal.GetModel(L,R,C);
            if (m != null)
            {
                return (WarehouseStatus)m.useStatus;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 得到仓位状态
        /// </summary>
        /// <param name="cellIndex">仓位ID</param>
        /// <returns></returns>
        public WarehouseStatus GetHousecellStatus(int cellIndex)
        {
            WarehouseStoreME m = _dal.GetModel(cellIndex);
            if(m != null)
            {
                return (WarehouseStatus)m.useStatus;
            }
            else
            {
                return 0;
            }
            
        }
       

        /// <summary>
        /// 得到layerIndex层，rowIndex行货架满载单元数
        /// </summary>
        /// <param name="layerIndex">层编号，从1开始</param>
        /// <param name="rowIndex">行编号，从1开始</param>
        /// <returns></returns>
        public int GetLayerRowFullcellNum(int layerIndex,int rowIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("houseLayerID={0} and houseRowID={1}", layerIndex, rowIndex);
            return _dal.GetRecordCount(strWhere.ToString());
        }

        /// <summary>
        /// 得到每个行货架的某型号产品数量
        /// </summary>
        /// <param name="channelIndex">行编号，从1开始</param>
        /// <param name="productType">产品型号</param>
        /// <returns></returns>
        public int GetProductNum(int rowIndex,string productType)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("houseID = {0} and  productType='{1}'", rowIndex, productType);
            return _wpViewDal.GetRecordCount(strWhere.ToString());
        }

        /// <summary>
        /// 获取第一个装载该型号产品的仓位
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public bool GetHouseCell(string productType,out int L,out int R,out int C)
        {
            L = 0;
            R = 0;
            C = 0;
            string strWhere = string.Format(" productType='{0}' and useStatus ={1}", productType, (int)WarehouseStatus.HOSUE_FULL);
            WareProductViewME m = _wpViewDal.GetConditionedModel(strWhere);
            if (m != null)
            {
                L = m.houseLayerID;
                R = m.houseRowID;
                C = m.houseColumnID;
                return true;
            }
            else
                return false;
        }
        public WarehouseStoreME GetHouseCell(int L, int R, int C)
        {
            WarehouseStoreME m = _dal.GetModel(L, R, C);
            return m;
        }
        public WarehouseStoreME GetHouseCell(int houseID)
        {
            return _dal.GetModel(houseID);
        }
        /// <summary>
        /// 获取某产品型号的所有仓位信息
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public DataTable GetHouseListByProducttype(string productType)
        {
            string strWhere = string.Format(" productType='{0}'",productType);
            DataSet ds = _wpViewDal.GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }
       
#endregion
    }
}
