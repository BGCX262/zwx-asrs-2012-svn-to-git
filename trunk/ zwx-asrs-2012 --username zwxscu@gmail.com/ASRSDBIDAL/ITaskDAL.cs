using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    /// <summary>
    /// 接口层TaskBLL
    /// </summary>
    public interface ITaskDAL
    {
        #region  成员方法
         /// <summary>
        /// 得到最大的流水号（转换成int类型排序）
        /// </summary>
        /// <returns></returns>
        string GetMaxTaskID();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string taskID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(TaskME model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(TaskME model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string taskID);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="taskIDlist"></param>
        /// <returns></returns>
        bool DeleteList(string taskIDlist);

        /// <summary>
        /// 有条件的删除语句
        /// </summary>
        /// <param name="strWhere">条件语句</param>
        /// <returns></returns>
        bool ConditionedDelete(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        TaskME GetModel(string taskID);

        // <summary>
        /// 根据条件查找符合的第一条记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        TaskME GetConditionedModel(string strWhere);

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
       
        #endregion  成员方法
    } 
}
