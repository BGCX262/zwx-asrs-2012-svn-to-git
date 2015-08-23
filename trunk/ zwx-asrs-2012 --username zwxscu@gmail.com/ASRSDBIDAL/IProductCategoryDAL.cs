using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    public interface IProductCategoryDAL
    {

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string productType);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(ProductCategoryME model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ProductCategoryME model);
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="productTypeID">产品型号</param>
        /// <returns></returns>
        bool Delete(string productTypeID);
        bool DeleteList(string productIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="productTypeID">产品型号</param>
        /// <returns></returns>
        ProductCategoryME GetModel(string productTypeID);

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
