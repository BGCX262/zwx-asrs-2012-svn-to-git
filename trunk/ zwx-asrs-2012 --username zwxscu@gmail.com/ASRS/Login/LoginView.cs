using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASRS
{
    public partial class LoginView : viewBase,ILoginView
    {
       /// <summary>
       /// 父类主窗口的公共接口,用来实现子窗口和父窗口之间传递消息，以及通过父窗口实现
       /// 子窗口之间传递消息
       /// </summary>
        public IASRSViewComn IParentView { get; set; }
        public LoginView(IASRSViewComn IParent)
        {
            InitializeComponent();
            this.radioButtonAdminitor.CheckedChanged += radioRole_CheckedChanged;
            this.radioButtonSystemManager.CheckedChanged += radioRole_CheckedChanged;
            this.radioButtonOperator.CheckedChanged += radioRole_CheckedChanged;
            
            IParentView = IParent;
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
            this.radioButtonOperator.Checked = true;
            OnRoleChange(AccountRole.OPERATOR);
        }
        protected override object CreatePresenter()
        {
            return new LoginPresenter(this);
        }
        #region UI事件

        /// <summary>
        /// 用户类别更改,触发用户列表刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioRole_CheckedChanged(object sender,EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if(IParentView != null && (radio.Checked == true))
            {
                
                IParentView.DispStatusbarLabelInfo(1, "选定用户级别：" + radio.Text);
                switch(radio.Text)
                {
                    case "系统维护":
                        {
                            OnRoleChange(AccountRole.SYSTEM_MANAGER);
                            break;
                        }
                    case "管理者":
                        {
                            OnRoleChange(AccountRole.ADMINITOR);
                            break;
                        }
                    case "操作员":
                        {
                            OnRoleChange(AccountRole.OPERATOR);
                            break;
                        }
                    default:
                        break;
                }
               
            }
          
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName = this.comboBoxUserlist.Text;
            string paswd = this.textBoxPaswd.Text;
           
            OnUserLogin(userName, paswd);
        }
        private void buttonXCancel_Click(object sender, EventArgs e)
        {

        }

        private void buttonXReset_Click(object sender, EventArgs e)
        {
            this.textBoxPaswd.Text = string.Empty;
        }
        #endregion
       #region 实现ILoginView 接口
       public  event EventHandler<LoginEventArg> eventUserLogin;
       public event EventHandler<LoginEventArg> eventRoleChange;
       public event EventHandler eventUserReset;
       
       public void RefreshUserList(IList<string> UserList)
       {
           this.comboBoxUserlist.Items.Clear();
           if (UserList.Count > 0)
           {
               foreach (string s in UserList)
               {
                   this.comboBoxUserlist.Items.Add(s);
               }
               this.comboBoxUserlist.SelectedIndex = 0;
           }
       }

       public void OutputMessage(string[] mes)
       {
           IParentView.OutputMessage(mes);
       }
        /// <summary>
        /// 得到用户登陆结果
        /// </summary>
        ///<param name="logRe">用户名及密码匹配是否成功</param>
        ///<param name="strName">用户名</param>
        ///<param role="role">角色</param>
        public void LoginResult(bool logRe,string strName,int role)
       {
           this.textBoxPaswd.Text = string.Empty;
           IParentView.UserLoginRe(logRe, strName, (AccountRole)role);
            if(logRe)
            {
                IParentView.DispStatusbarLabelInfo(3, "用户：" + strName);
                this.DialogResult = DialogResult.OK;
            }
       }

        /// <summary>
        /// 显示初始化进度
        /// </summary>
        /// <param name="progress"></param>
       public void DispInitProgress(int progress)
       {
           IParentView.DispStatusbarProgressInfo(progress);
       }

        /// <summary>
        /// 显示状态信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str"></param>
        public void DispStatusbarInfo(int index, string str)
        {
            IParentView.DispStatusbarLabelInfo(index, str);
        }

       #endregion
       #region ILoginView事件触发函数
        /// <summary>
        /// 触发用户登录事件
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="paswd"></param>
        private void OnUserLogin(string userName,string paswd)
        {
            LoginEventArg e = new LoginEventArg();
            e.UserName = userName;
            e.Paswd = paswd;
            eventUserLogin(this,e);
        }
        /// <summary>
        /// 触发用户角色改变事件
        /// </summary>
        /// <param name="role"></param>
        private void OnRoleChange(AccountRole role)
        {
            LoginEventArg e = new LoginEventArg();
            e.CurRole = role;
            if(role == AccountRole.OPERATOR)
            {
                this.textBoxPaswd.Enabled = false;
            }
            else
            {
                this.textBoxPaswd.Enabled = true;
            }
            if(eventRoleChange != null)
            {
                eventRoleChange(this, e);
            }
          
        }
       #endregion

        private void comboBoxUserlist_Click(object sender, EventArgs e)
        {
           // this.textBoxPaswd.Text = string.Empty;
        }

        /// <summary>
        /// 用户选择变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxUserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxPaswd.Text = string.Empty;
        }

    }
}
