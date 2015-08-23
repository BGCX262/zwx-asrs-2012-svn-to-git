using System;
namespace ASRSDBME
{
    /// <summary>
    /// AccountME:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AccountME
    {
        public AccountME()
        { }
        #region Model
        private string _username;
        private string _userpassword;
        private int _role;
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        /// <summary>
        /// 用户角色：1：普通用户，2：管理员，3：系统维护员
        /// </summary>
        public int role
        {
            set { _role = value; }
            get { return _role; }
        }
        #endregion Model

    }
}

