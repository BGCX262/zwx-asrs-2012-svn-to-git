using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASRSDBBLL;
namespace ASRS
{
    public partial class ModifyPWDForm : Form
    {
        private AccountBLL _accountBll = new AccountBLL();

        /// <summary>
        /// 用户名
        /// </summary>
        private string _userName;
        public  string userName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
        public ModifyPWDForm(string userName)
        {
            _userName = userName;
            InitializeComponent();
        }
        private void buttonXApply_Click(object sender, EventArgs e)
        {
            if(-1 == _accountBll.Login(this.textBoxUserName.Text, this.textBoxOldPaswd.Text))
            {
                this.labelXResultTip.ForeColor = Color.Red;
                this.labelXResultTip.Text = "原始密码不正确，请输入正确密码";
                return;
            }
            if(this.textBoxNewPWD.Text  != this.textBoxNewRePWD.Text)
            {
                this.labelXResultTip.ForeColor = Color.Red;
                this.labelXResultTip.Text = "新密码重复验证失败，请重新确认";
                return;
            }
            _accountBll.ModifyPaswd(this.textBoxUserName.Text, this.textBoxOldPaswd.Text, this.textBoxNewPWD.Text);
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("密码修改成功!");
            this.Close();
        }

        private void buttonXReset_Click(object sender, EventArgs e)
        {
            this.textBoxNewPWD.Text = string.Empty;
            this.textBoxNewRePWD.Text = string.Empty;
            this.textBoxOldPaswd.Text = string.Empty;
        }

        private void buttonXCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyPWDForm_Load(object sender, EventArgs e)
        {
            this.textBoxUserName.Text = _userName;
            this.textBoxUserName.Enabled = false;
        }
    }
}
