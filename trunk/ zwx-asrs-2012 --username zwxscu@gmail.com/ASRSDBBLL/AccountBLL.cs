using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using ASRSDBFactory;
using ASRSDBIDAL;
using System.Data;
namespace ASRSDBBLL
{
    public class AccountBLL
    {
        private readonly IAccountDAL _accountDAL = DALFactory.CreateAccountDAL();

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(AccountME user)
        {
            return _accountDAL.Add(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string userName)
        {
            return _accountDAL.Delete(userName);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPaswd">旧密码</param>
        /// <param name="newPaswd">新密码</param>
        /// <returns></returns>
        public bool ModifyPaswd(string userName,string oldPaswd,string newPaswd)
        {
            AccountME userM = _accountDAL.GetModel(userName);
            if (userM == null)
                return false;
            if (userM.userPassword != oldPaswd)
                return false;
            userM.userPassword = newPaswd;
            return _accountDAL.Update(userM);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paswd">密码</param>
        /// <returns>返回角色代号，若失败，则返回-1</returns>
        public int Login(string userName,string paswd)
        {
            AccountME userM = _accountDAL.GetModel(userName);
            if (userM == null)
                return -1;
            if(userM.userPassword == null)
            {
                 return userM.role;
            }
            if(userM.userPassword != paswd)
            {
                return -1;
            }
            return userM.role;
        }

        /// <summary>
        /// 得到某角色的用户列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<string> GetUserList(int role)
        {
            List<string> userList = new List<string>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("role={0} ", role);
            DataSet ds = _accountDAL.GetList(strWhere.ToString());
            if(ds != null)
            {
                foreach(DataRow drw in ds.Tables[0].Rows)
                {
                    userList.Add(drw["userName"].ToString());
                }
                return userList;
            }
            return null;
        }

        /// <summary>
        /// 得到所有的管理员级别用户
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdmUserList()
        {
            DataSet ds = _accountDAL.GetList("role = 2");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }

        public bool ExistUser(string userName)
        {
            return _accountDAL.Exists(userName);
        }
    }
}
