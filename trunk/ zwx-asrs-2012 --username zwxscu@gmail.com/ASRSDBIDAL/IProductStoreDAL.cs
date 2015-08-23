using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 产品信息接口类
    /// </summary>
    public interface IProductStoreDAL
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string productID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(ProductStoreME model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ProductStoreME model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string productID);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string productIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ProductStoreME GetModel(string productID);

       
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
    }
}
