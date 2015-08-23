using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ASRSDBME;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 接口层InstBLL
    /// </summary>
    public interface IInstDAL
    {
        #region  成员方法
        /// <summary>
        /// 得到最大的指令流水号（转换成int类型排序）
        /// </summary>
        /// <returns></returns>
        string GetMaxInstID();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string instID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(InstME model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(InstME model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string instID);
        bool DeleteList(string instIDlist);

        /// <summary>
        /// 有条件的删除语句
        /// </summary>
        /// <param name="strWhere">条件语句</param>
        /// <returns></returns>
        bool ConditionedDelete(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        InstME GetModel(string instID);
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
