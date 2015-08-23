using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using System.Data;
namespace ASRSDBIDAL
{
    public interface IWareProductViewDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        WareProductViewME GetModel(int houseID);

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
        /// 第一个符合条件的仓位
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        WareProductViewME GetConditionedModel(string strWhere);
    }
}
