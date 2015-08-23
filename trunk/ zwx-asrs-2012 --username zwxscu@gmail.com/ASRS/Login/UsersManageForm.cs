using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents;
using ASRSDBME;
using ASRSDBBLL;
using DotNetBarCtls = DevComponents.DotNetBar.Controls;
namespace ASRS
{
    enum userOptype
    {
        OPNULL = 0,
        USER_ADD = 1,
        USER_DEL,
        USER_EDIT
    }
    public partial class UsersManageForm : Form
    {
        private AccountBLL _accountBll = new AccountBLL();
        private DataTable _admUsrlist = null;
        /// <summary>
        /// 当前操作类型，0:无，1：增加,2:删除，3：编辑
        /// </summary>
        private userOptype _curOpType = userOptype.OPNULL;
        public UsersManageForm()
        {
            InitializeComponent();
            this.radionAdd.CheckedChanged += radioOpt_CheckedChanged;
            this.radionDel.CheckedChanged += radioOpt_CheckedChanged;
            this.radionEdit.CheckedChanged += radioOpt_CheckedChanged;
        }
        private bool RefreshUserlist()
        {
            DataTable dt = _accountBll.GetAdmUserList();
            if (dt != null)
            {
                _admUsrlist = dt;
                this.dataGridViewX1.DataSource = _admUsrlist;
                return true;
            }
            else
                return false;
        }
        private void UsersManageForm_Load(object sender, EventArgs e)
        {
            this.groupPanel1.Hide();
            this.textBoxXUserRole.Enabled = false;
             RefreshUserlist();
        }

        private void buttonXQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXRefresh_Click(object sender, EventArgs e)
        {
            RefreshUserlist();
        }

        private void buttonXEdit_Click(object sender, EventArgs e)
        {
            this._curOpType = userOptype.USER_EDIT;
            this.labelX3.Text = "旧密码";
            this.labelX4.Text = "请输入新密码";
            this.textBoxXPWD2.Hide();
            this.groupPanel1.Show();
        }

        private void buttonXAdd_Click(object sender, EventArgs e)
        {
            

        }

        private void buttonXDel_Click(object sender, EventArgs e)
        {
          
        }

        private void buttonXApply_Click(object sender, EventArgs e)
        {
            switch(_curOpType)
            {
                case userOptype.USER_ADD:
                    {
                        string userName = this.textBoxXUserName.Text;
                        if(_accountBll.ExistUser(userName))
                        {
                            MessageBox.Show("当前用户名已存在，请更换用户名称!");
                            return;
                        }
                        if(this.textBoxXPWD1.Text != this.textBoxXPWD2.Text)
                        {
                            MessageBox.Show("两次输入的密码不一致，请重新输入");
                            return;
                        }
                        AccountME userM = new AccountME();
                        userM.userName = userName;
                        userM.role = 2;
                        userM.userPassword  = this.textBoxXPWD2.Text;
                        _accountBll.AddUser(userM);
                        RefreshUserlist();
                        break;
                    }
                case userOptype.USER_DEL:
                    {
                        _accountBll.DeleteUser(this.textBoxXUserName.Text);
                        RefreshUserlist();
                        break;
                    }
                case userOptype.USER_EDIT:
                    {
                        string userName = this.textBoxXUserName.Text;
                        string userOldPwd = this.textBoxXPWD1.Text;
                        string userNewPwd = this.textBoxXPWD2.Text;
                        _accountBll.ModifyPaswd(userName, userOldPwd, userNewPwd);
                        RefreshUserlist();
                        break;
                    }
                default:
                    break;
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DotNetBarCtls.DataGridViewX dtgrd = sender as DotNetBarCtls.DataGridViewX;
            if(dtgrd == this.dataGridViewX1)
            {
                DataRow dr = this._admUsrlist.Rows[e.RowIndex];
                if(dr != null)
                {
                    this.textBoxXUserName.Text = dr["userName"].ToString();
                    this.textBoxXPWD1.Text = dr["userPassword"].ToString();
                    this.textBoxXUserRole.Text = "管理员";
                }

            }
        }

        /// <summary>
        /// 切换操作类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void radioOpt_CheckedChanged(object sender,EventArgs e)
         {
             DotNetBarCtls.CheckBoxX radio = (DotNetBarCtls.CheckBoxX)sender;
               if(radio.Checked == true)
               {
                     switch(radio.Text)
                     {
                         case "增加":
                             {
                                 this._curOpType = userOptype.USER_ADD;
                                 this.labelX3.Text = "密码";
                                 this.labelX4.Text = "请再次输入密码";
                                 this.labelX4.Show();
                                 this.textBoxXPWD2.Show();
                                 this.groupPanel1.Show();
                                 break;
                             }
                         case "删除":
                             {
           
                                 this._curOpType = userOptype.USER_DEL;
                                 this.labelX3.Text = "密码";
                                 this.labelX4.Hide();
                                 this.textBoxXPWD2.Hide();
                                 break;
                             }
                         case "编辑":
                             {
                                 this._curOpType = userOptype.USER_EDIT;
                                 this.labelX3.Text = "旧密码";
                                 this.labelX4.Text = "请输入新密码";
                                 this.labelX4.Show();
                                 this.textBoxXPWD2.Show();
                                 break;
                             }
                         default:
                             break;
                     }
               }
         }
    }
}
