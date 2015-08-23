using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ASRSDBME;
namespace ASRSDBIDAL
{
    //// <summary>
    /// 接口层AccountBLL
    /// </summary>
    public interface IAccountDAL
    {
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string userName);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(AccountME model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(AccountME model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string userName);
        bool DeleteList(string userNamelist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        AccountME GetModel(string userName);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获取满足某条件的记录个数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRecordCount(string strWhere);
       
        #endregion  成员方法
    } 
}
