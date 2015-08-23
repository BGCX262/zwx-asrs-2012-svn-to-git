using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRS
{
    /// <summary>
    /// 主view的公共接口，主要功能是使子view能够向父view传递信息
    /// </summary>
    public interface IASRSViewComn
    {
        #region 子view->父view
       // void LogRecord(string strinfo);

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <returns></returns>
        void OutputMessage(string[] strList);
        
        /// <summary>
        /// 用户登录结果
        /// </summary>
        /// <param name="bOk"></param>
        /// <param name="strUser"></param>
        /// <param name="role"></param>
        void UserLoginRe(bool bOk, string strUser, AccountRole role);

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <param name="strUser"></param>
        void AccountExit(string strUser);


        /// <summary>
        /// 显示状态栏信息
        /// </summary>
        /// <param name="index">状态栏编号</param>
        /// <param name="str">从1开始编号</param>
        void DispStatusbarLabelInfo(int index, string str);

        /// <summary>
        ///状态栏上显示进度条信息
        /// </summary>
        /// <param name="progress"></param>
        void DispStatusbarProgressInfo(int progress);

        /// <summary>
        /// 显示/隐藏状态栏进度条
        /// </summary>
        /// <param name="bShow"></param>
        void ShowStatusbarProgressbar(bool bShow);
        #endregion
        #region 父view->子view
        #endregion
    }
}
