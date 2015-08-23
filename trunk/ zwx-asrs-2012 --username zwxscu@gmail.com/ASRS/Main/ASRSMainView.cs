using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using ASRSDBFactory;
namespace ASRS
{
    public enum ChildFormEnum
    {
        FORM_NONE = 0,
        FORM_USER, //用户登录
        FORM_WAREHOUSE, //仓位监控
        FORM_PRODUCT, //产品管理
        FORM_DEVICE, //设备监控
        FORM_REPORT //报表
    
    }

    /// <summary>
    /// 托管:跨线程刷新UI操作
    /// </summary>
    /// <param name="strInfo"></param>
    public delegate void delegateUIDiaplayInfo(string strInfo);

    /// <summary>
    ///托管: 跨线程刷新UI状态栏提示信息
    /// </summary>
    /// <param name="labelIndex"></param>
    /// <param name="strText"></param>
    public delegate void delegateRefreshStatusbarLabel(int labelIndex,string strText);
    
    /// <summary>
    /// 托管：跨线程刷新UI状态栏进度条信息
    /// </summary>
    /// <param name="progress"></param>
    public delegate void delegateRefreshStatusbarProgress(int progress);
    public partial class ASRSMainView :viewBase,IASRSMainView,IASRSViewComn
    {
        private int _outputMesMax = 2000;
        private ChildFormEnum _curChildSelect = ChildFormEnum.FORM_NONE;
        private ASRSModel _model = ASRSModel.GetInstance();
        #region 子窗体
        private LoginView _loginView = null;
        private WarehouseMonitorView _warehouseView = null;
        private ProductQueryView _productView = null;
     //   private ReportView _reportView = null;
        private Form _activeChildForm = null;
        private DeviceMonitorView _DeviceView = null;
       // private MultiHouseOutForm _MultiHouseOutFrm = null;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public ASRSMainView()
        {
            //this.Hide();
            InitializeComponent();

        }
        #region UI事件
        /// <summary>
        /// 启动检查
        /// </summary>
        /// <returns>若通过检查，则返回true，否则返回false</returns>
        private bool StartupCheck()
        {
            Register register = new Register("software\\ASRS", RegDomain.LocalMachine);
            if (!register.IsRegeditKeyExist("sysStartCount"))
            {
                return false;
            }
            
            object re = register.ReadRegeditKey("sysStartCount");
            if(re != null)
            {
                int startCount = int.Parse(re.ToString());
                if(startCount <=0)
                {
                    //调用初始化配置向导
                    wizardForm setForm = new wizardForm();
                    DialogResult dlgRe = setForm.ShowDialog();
                    if(dlgRe == DialogResult.OK)
                    {
                       
                        _model.dbComDevSet.dbServerIP = setForm.dbServerIP;
                        _model.dbComDevSet.dbUser = setForm.dbUserName;
                        _model.dbComDevSet.dbPwd = setForm.dbPwd;
                        _model.wareHouseSet.layerCount = setForm.layerCount;
                        _model.wareHouseSet.channelCount = setForm.channelCount;
                        _model.wareHouseSet.columnCount = setForm.columnCount;
                  
                        //参数存文件
                        _model.SaveDBComSetFile();
                        _model.SaveWarehouseSetFile();
                       
                        //修改注册表值
                        register.WriteRegeditKey("sysStartCount", 1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //已经配置过了，则返回true
                    _model.ReadDBComSetFile();
                    _model.ReadWarehouseSetFile();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 窗体初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ASRSMainView_Load(object sender, EventArgs e)
        {
            if(!StartupCheck())
            {
                MessageBox.Show("启动错误，程序将退出");
                this.Close();
               Application.Exit();
               return;
            }
            //加载数据库连接信息
            
            ASRSDBFactory.PubConstant.dbServerIP = _model.dbComDevSet.dbServerIP;
            ASRSDBFactory.PubConstant.dbUser = _model.dbComDevSet.dbUser;
            ASRSDBFactory.PubConstant.dbPwd = _model.dbComDevSet.dbPwd;

            Thread loadThread = new Thread(LoadThreadWork);
            loadThread.Start();
            //创建子窗口

            //用户登录窗口
            _loginView = new LoginView(this);
            //_loginView.MdiParent = this;
           // splitContainer2.Panel2.Controls.Add(_loginView);
          //  _loginView.Dock = DockStyle.Fill;
            _loginView.Hide();

            //仓位监控窗口
            _warehouseView = new WarehouseMonitorView(this);
            _warehouseView.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(_warehouseView);
            _warehouseView.Dock = DockStyle.Fill;
            _warehouseView.Hide();

            //设备监视窗口
            _DeviceView = new DeviceMonitorView(this);
            _DeviceView.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(_DeviceView);
            _DeviceView.Dock = DockStyle.Fill;
            _DeviceView.Hide();

            //产品管理窗口
            _productView = new ProductQueryView(this);
            _productView.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(_productView);
            _productView.Dock = DockStyle.Fill;
            _productView.Hide();

            //报表窗口
//             _reportView = new ReportView(this);
//             _reportView.MdiParent = this;
//             splitContainer2.Panel2.Controls.Add(_reportView);
//             _reportView.Dock = DockStyle.Fill;
//             _reportView.Hide();

            this.toolStripProgressBar1.Visible = false;
            DisableAllMenu();
            HideAllChildFrm();
            CheckFormSelectBtn(ChildFormEnum.FORM_USER);

            //启动线程退出
            loadThread.Abort();
            this._loginView.StartPosition = FormStartPosition.CenterScreen; 
            DialogResult logRe = this._loginView.ShowDialog();
            if(logRe != DialogResult.OK)
            {
                Application.Exit();
                return;
            }

            this.toolStripComboBoxScale.SelectedIndex = 3;
            this.WindowState = FormWindowState.Maximized;

        }
        /// <summary>
        /// 调出用户登录窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void buttonLogin_Click(object sender, EventArgs e)
        //{
        //    DispStatusbarLabelInfo(1, "用户登陆");
        //    //ShowChildFrm(_loginView);
        //    DispStatusbarLabelInfo(2, "就绪");
        //    //this.splitContainer2.Panel1Collapsed = true;
        //    UncheckFormSelectBtn(_curChildSelect);
        //    CheckFormSelectBtn(ChildFormEnum.FORM_USER);
        //}
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ASRSView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this._warehouseView != null)
            {
                this._warehouseView.OnExit();
            }
           
        }
        /// <summary>
        /// 调出仓位监控窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_WarehouseMonitor_Click(object sender, EventArgs e)
        {
            DispStatusbarLabelInfo(1, "仓位监控");
            DispStatusbarLabelInfo(2, "正在加载仓位信息,请稍后");
            //ShowChildFrm(_warehouseView);

            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_WAREHOUSE);
        }
        /// <summary>
        /// 调出设备监控窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeviceMonitor_Click(object sender, EventArgs e)
        {
            DispStatusbarLabelInfo(1, "设备监控");
            DispStatusbarLabelInfo(2, "正在加载设备信息,,请稍后");
            //ShowChildFrm(_DeviceView);
            DispStatusbarLabelInfo(2, "就绪");
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_DEVICE);
        }

        /// <summary>
        /// 调出产品查询窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWareProductView_Click(object sender, EventArgs e)
        {
            DispStatusbarLabelInfo(1, "产品查询");
            DispStatusbarLabelInfo(2, "正在加载产品信息,,请稍后");
            // ShowChildFrm(_productView);
            DispStatusbarLabelInfo(2, "就绪");
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_PRODUCT);
        }

        /// <summary>
        /// 调出报表窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReportview_Click(object sender, EventArgs e)
        {
            DispStatusbarLabelInfo(1, "报表生成");
            DispStatusbarLabelInfo(2, "正在加载报表信息,,请稍后");
            // ShowChildFrm(_reportView);
            DispStatusbarLabelInfo(2, "就绪");
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_REPORT);
        }
        /// <summary>
        /// 批量出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 批量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UncheckFormSelectBtn(_curChildSelect);
           // CheckFormSelectBtn(ChildFormEnum.FORM_WAREHOUSE);
            //ShowChildFrm(_MultiHouseOutFrm);
        }

        /// <summary>
        /// 单仓位出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 单仓位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_WAREHOUSE);
            //ShowChildFrm(_warehouseView);
            _warehouseView.ShowHouseoutPanel();
        }
        /// <summary>
        /// 手动设置监控页面的缩放比例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._warehouseView.Visible == true)
            {
                string strval = this.toolStripComboBoxScale.SelectedItem.ToString();
                float scale = float.Parse(strval);
                this._warehouseView.zoomDisp(scale);
            }
        }
        /// <summary>
        /// 用户注销菜单响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemLoginoff_Click(object sender, EventArgs e)
        {
            DispStatusbarLabelInfo(1, "选定用户级别：" + "操作员");
            DispStatusbarLabelInfo(3, "用户：" + "操作员");
            _model.currentUserName = "操作员";
            _model.currentUserRole = AccountRole.OPERATOR;
            UserLoginRe(true, "操作员", AccountRole.OPERATOR);
        }

        /// <summary>
        /// 切换用户菜单响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemUserChange_Click(object sender, EventArgs e)
        {
            DialogResult re =this._loginView.ShowDialog();
            
        }
        /// <summary>
        /// 用户管理菜单响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemUserManage_Click(object sender, EventArgs e)
        {
            if(_model.currentUserRole != AccountRole.SYSTEM_MANAGER)
            {
                MessageBox.Show("当前用户级别禁用此功能，要求系统维护员执行此功能！");
                return;
            }
            UsersManageForm manageForm = new UsersManageForm();

            manageForm.ShowDialog(this);
        }
        
        private void MenuItemSysSetting_Click(object sender, EventArgs e)
        {
          
        }
      
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemModifyPWD_Click(object sender, EventArgs e)
        {
            if(_model.currentUserRole != AccountRole.ADMINITOR)
            {
                //只有管理员密码可更改，操作员做为默认登录用户，要求密码为空，系统维护员密码不允许修改。
                MessageBox.Show("当前用户级别不允许修改密码!");
                return;
            }
            ModifyPWDForm modifyForm = new ModifyPWDForm(_model.currentUserName);
            modifyForm.Show(this);
        }


        private void MenuItemWarehouseMonitorWindow_Click(object sender, EventArgs e)
        {
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_WAREHOUSE);

        }

        private void MenuItemDevMonitorWindow_Click(object sender, EventArgs e)
        {
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_DEVICE);
        }

        private void MenuItemQueryWindow_Click(object sender, EventArgs e)
        {
            UncheckFormSelectBtn(_curChildSelect);
            CheckFormSelectBtn(ChildFormEnum.FORM_PRODUCT);
        }

        private void MenuItemSysSet_Click(object sender, EventArgs e)
        {
            if (_model.currentUserRole != AccountRole.SYSTEM_MANAGER)
            {
                MessageBox.Show("当前用户级别禁用此功能，要求系统维护员执行此功能！");
                return;
            }
            SettingForm setForm = new SettingForm();
            setForm.ShowDialog(this);
        }

        /// <summary>
        /// 后台数据管理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemBkgdDB_Click(object sender, EventArgs e)
        {
            if (_model.currentUserRole != AccountRole.SYSTEM_MANAGER)
            {
                MessageBox.Show("当前用户级别禁用此功能，要求系统维护员执行此功能！");
                return;
            }
            DBManageForm dbManageForm = new DBManageForm();
            dbManageForm.ShowDialog(this);
        }
#endregion
        #region IASRSMainView接口实现
       
        #endregion
        #region IASRSViewComn接口实现
        public void ShowStatusbarProgressbar(bool bShow)
        {
            this.toolStripProgressBar1.Visible = bShow;
            this.statusStrip1.Invalidate();
        }
        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="mes"></param>
        public void OutputMessage(string[] strList)
        {
            string str = string.Empty;
            foreach (string s in strList)
            {
                str = (str + s + "  ");
            }
            if(this.listBoxOutput.InvokeRequired)
            {
                this.Invoke(new delegateUIDiaplayInfo(ThreadUIOutputMessage),str);
            }
            else
            {
               
                this.listBoxOutput.Items.Add(str);
            }
           
        }

        /// <summary>
        /// 用户登录结果
        /// </summary>
        /// <param name="bOk"></param>
        /// <param name="strUser"></param>
        /// <param name="role"></param>
       public  void UserLoginRe(bool bOk, string strUser, AccountRole role)
        {
            DisableAllMenu();
            HideAllChildFrm();
            this.button_WarehouseMonitor.Hide();
            this.buttonDeviceMonitor.Hide();
            this.buttonProductView.Hide();
          //  this.buttonReportview.Hide();
            if (bOk)
           {
               //根据用户级别，显示不同的界面
               this.button_WarehouseMonitor.Show();
               this.buttonDeviceMonitor.Show();
               if(role == AccountRole.ADMINITOR || role == AccountRole.SYSTEM_MANAGER)
               {
                   this.buttonProductView.Show();
                  // this.buttonReportview.Show();
                   //this.MannualHouseOutToolStripMenuItem.Enabled = true;
               
                   if(role == AccountRole.SYSTEM_MANAGER)
                   {
                      // this.SystemSetToolStripMenuItem.Enabled = true;
                   }
               }
           }
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="strUser"></param>
        public void AccountExit(string strUser)
        {
            //退出登陆之前，应该做好了生产线停线处理
            HideAllChildFrm();
        }
        /// <summary>
        /// 显示状态栏信息
        /// </summary>
        /// <param name="index">状态栏编号,从1开始</param>
        /// <param name="str">显示内容</param>
        public void DispStatusbarLabelInfo(int index, string str)
        {
            if(this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new delegateRefreshStatusbarLabel(ThreadUIDispStatusbarLabelInfo),index, str);
            }
            else
            {
                if (index == 1)
                {
                    this.toolStripStatusLabel1.Text = str;
       
                }
                else if (index == 2)
                {
                    this.toolStripStatusLabel2.Text = str;
                }
                else if (index == 3)
                {
                    this.toolStripStatusLabel3.Text = str;
                }
                this.toolStrip1.Refresh();
            }
         
        }
        /// <summary>
        ///状态栏上显示进度条信息
        /// </summary>
        /// <param name="progress"></param>
        public void DispStatusbarProgressInfo(int progress)
        {
            if(this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new delegateRefreshStatusbarProgress(ThreadUIDispStatusbarProgress), progress);
            }
            else
            {
                this.toolStripProgressBar1.Value = (int)((progress / 100.0f) * this.toolStripProgressBar1.Maximum);
            }
        }
        #endregion
        #region 功能函数
        protected override object CreatePresenter()
        {
            return new ASRSPresenter(this);
        }

        /// <summary>
        /// 刷新状态栏提示信息托管实例
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str"></param>
        private void ThreadUIDispStatusbarLabelInfo(int index, string str)
        {
            if (index == 1)
            {
                this.toolStripStatusLabel1.Text = str;
            }
            else if (index == 2)
            {
                this.toolStripStatusLabel2.Text = str;
            }
            else if (index == 3)
            {
                this.toolStripStatusLabel3.Text = str;
            }
        }

        /// <summary>
        /// 刷新状态栏进度条信息托管实例
        /// </summary>
        /// <param name="progress">百分比</param>
        private void ThreadUIDispStatusbarProgress(int progress)
        {
            this.toolStripProgressBar1.Value = (int)((progress / 100.0f) * this.toolStripProgressBar1.Maximum);
        }

        /// <summary>
        /// 运行日志显示托管函数
        /// </summary>
        /// <param name="strinfo"></param>
        private void ThreadUIOutputMessage(string strinfo)
        {
            if(this.listBoxOutput.Items.Count > _outputMesMax)
            {
                this.listBoxOutput.Items.RemoveAt(0);
            }
            this.listBoxOutput.Items.Add(strinfo);
        }
        /// <summary>
        /// 取消按钮标记
        /// </summary>
        /// <param name="formSelect"></param>
        private void UncheckFormSelectBtn(ChildFormEnum formSelect)
        {
            switch (formSelect)
            {
                //case ChildFormEnum.FORM_USER:
                //    {
                //        this.buttonLogin.BackColor = Button.DefaultBackColor;
                //        _loginView.Hide();
                //        break;
                //    }

                case ChildFormEnum.FORM_WAREHOUSE:
                    {
                        this.button_WarehouseMonitor.BackColor = Button.DefaultBackColor;
                        _warehouseView.Hide();
                        break;
                    }
                case ChildFormEnum.FORM_PRODUCT:
                    {
                        this.buttonProductView.BackColor = Button.DefaultBackColor;
                        _productView.Hide();
                        break;
                    }
                case ChildFormEnum.FORM_DEVICE:
                    {
                        this.buttonDeviceMonitor.BackColor = Button.DefaultBackColor;
                        _DeviceView.Hide();
                        break;
                    }
//                 case ChildFormEnum.FORM_REPORT:
//                     {
//                         //this.buttonReportview.BackColor = Button.DefaultBackColor;
//                         _reportView.Hide();
//                         break;
//                     }
                default:
                    break;
            }
        }

        /// <summary>
        /// 标记按钮按下
        /// </summary>
        /// <param name="formSelect"></param>
        private void CheckFormSelectBtn(ChildFormEnum formSelect)
        {

            switch (formSelect)
            {
                //case ChildFormEnum.FORM_USER:
                //    {
                //        this.buttonLogin.BackColor = Color.Orange;
                //        _loginView.Show();
                //        break;
                //    }

                case ChildFormEnum.FORM_WAREHOUSE:
                    {
                        this.button_WarehouseMonitor.BackColor = Color.Orange;
                        _warehouseView.Show();
                        break;
                    }
                case ChildFormEnum.FORM_PRODUCT:
                    {
                        this.buttonProductView.BackColor = Color.Orange;
                        _productView.Show();
                        break;
                    }
                case ChildFormEnum.FORM_DEVICE:
                    {
                        this.buttonDeviceMonitor.BackColor = Color.Orange;
                        _DeviceView.Show();
                        break;
                    }
//                 case ChildFormEnum.FORM_REPORT:
//                     {
//                         this.buttonReportview.BackColor = Color.Orange;
//                         _reportView.Show();
//                         break;
//                     }
                default:
                    break;

            }
            _curChildSelect = formSelect;
            // this.Refresh();
        }
        /// <summary>
        /// 禁用所有菜单
        /// </summary>
        private void DisableAllMenu()
        {
            
           // this.AccountSetToolStripMenuItem.Enabled = false;
        }
        /// <summary>
        /// 隐藏全部子窗体
        /// </summary>
        private void HideAllChildFrm()
        {
            this.button_WarehouseMonitor.Hide();
            this.buttonDeviceMonitor.Hide();
            this.buttonProductView.Hide();
           // this.buttonReportview.Hide();
            _productView.Hide();
            //_reportView.Hide();
            _warehouseView.Hide();
            _DeviceView.Hide();
        }
        /// <summary>
        /// 加载对话框提示
        /// </summary>
        private void LoadThreadWork()
        {
            //
            LoadForm fm = new LoadForm();
            fm.labelTipText = "系统正在启动...";
            fm.ShowDialog();
        }
        #endregion

        private void MenuItemSysQuit_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult re = MessageBox.Show("确定要退出系统？","提示", buttons, icon);
            
            if (this._warehouseView != null && re == DialogResult.Yes)
            {
                this._warehouseView.OnExit();
                Application.Exit();
                this.Close();
            }
        }

   }
}
