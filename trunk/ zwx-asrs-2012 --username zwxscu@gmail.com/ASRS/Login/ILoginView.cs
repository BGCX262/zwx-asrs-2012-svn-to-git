using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRS
{
    /// <summary>
    /// 用户角色枚举
    /// </summary>
    public enum AccountRole
    {
        OPERATOR = 1,
        ADMINITOR,
        SYSTEM_MANAGER
    }
    public class LoginEventArg : EventArgs
    {
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 当前用户密码
        /// </summary>
        public string Paswd { get; set; }

        /// <summary>
        /// 当前用户角色
        /// </summary>
        public AccountRole CurRole { get; set; }
    }
    public interface ILoginView:IViewBase
    {
#region UI事件
        /// <summary>
        /// 用户登录事件
        /// </summary>
        event EventHandler<LoginEventArg> eventUserLogin;

        /// <summary>
        /// 登录重置事件
        /// </summary>
        event EventHandler eventUserReset;

        /// <summary>
        /// 登录角色改变事件
        /// </summary>
        event EventHandler<LoginEventArg> eventRoleChange;
#endregion
#region 功能函数
        /// <summary>
        /// 更新用户列表
        /// </summary>
        /// <param name="UserList"> 用户名列表</param>
        void RefreshUserList(IList<string> UserList);

        /// <summary>
        /// 登录结果
        /// </summary>
        ///<param name="logRe">用户名及密码匹配是否成功</param>
        ///<param name="strName">用户名</param>
        ///<param role="role">角色</param>
        void LoginResult(bool logRe,string strName,int role);
#endregion
    }
}
