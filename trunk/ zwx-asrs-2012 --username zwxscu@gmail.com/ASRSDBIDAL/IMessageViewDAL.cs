using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 接口层MessageViewBLL
    /// </summary>
    public interface IMessageViewDAL
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(DateTime happenTime);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(MessageViewME model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(MessageViewME model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(DateTime happenTime);
       
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        MessageViewME GetModel(DateTime happenTime);
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
