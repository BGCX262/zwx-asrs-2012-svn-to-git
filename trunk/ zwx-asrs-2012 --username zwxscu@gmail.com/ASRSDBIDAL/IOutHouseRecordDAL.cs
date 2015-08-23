using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ASRSDBME;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 接口层OutHouseRecordBLL
    /// </summary>
    public interface IOutHouseRecordDAL
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(DateTime outHouseTime, string productID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(OutHouseRecordME model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(OutHouseRecordME model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(DateTime outHouseTime, string productID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        OutHouseRecordME GetModel(DateTime outHouseTime, string productID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    } 
}
