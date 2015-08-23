using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 仓储信息接口类
    /// </summary>
    public interface IWarehouseStoreDAL
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int houseID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(WarehouseStoreME model);

        // <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(WarehouseStoreME model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int houseID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        WarehouseStoreME GetModel(int houseID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="houseLayerID"></param>
        /// <param name="houseRowID"></param>
        /// <param name="houseColumnID"></param>
        /// <returns></returns>
        WarehouseStoreME GetModel(int houseLayerID, int houseRowID, int houseColumnID);

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获得满足条件的记录个数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 清理掉所有数据
        /// </summary>
        void ClearAllData();
    }
}
