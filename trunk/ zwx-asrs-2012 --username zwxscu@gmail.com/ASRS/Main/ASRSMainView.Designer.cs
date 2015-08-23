namespace ASRS
{
    partial class ASRSMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ASRSMainView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonProductView = new System.Windows.Forms.Button();
            this.buttonDeviceMonitor = new System.Windows.Forms.Button();
            this.button_WarehouseMonitor = new System.Windows.Forms.Button();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.listBoxOutput = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxScale = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSysSet = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBkgdDB = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSysQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUserChange = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoginoff = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUserManage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemModifyPWD = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWarehouseMonitorWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDevMonitorWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemQueryWindow = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Panel2.Controls.Add(this.panelOutput);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(815, 463);
            this.splitContainer1.SplitterDistance = 369;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Teal;
            this.splitContainer2.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer2.Panel1.BackgroundImage")));
            this.splitContainer2.Panel1.Controls.Add(this.buttonProductView);
            this.splitContainer2.Panel1.Controls.Add(this.buttonDeviceMonitor);
            this.splitContainer2.Panel1.Controls.Add(this.button_WarehouseMonitor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer2.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer2.Panel2.BackgroundImage")));
            this.splitContainer2.Size = new System.Drawing.Size(815, 369);
            this.splitContainer2.SplitterDistance = 111;
            this.splitContainer2.TabIndex = 0;
            // 
            // buttonProductView
            // 
            this.buttonProductView.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProductView.Location = new System.Drawing.Point(0, 68);
            this.buttonProductView.Name = "buttonProductView";
            this.buttonProductView.Size = new System.Drawing.Size(111, 34);
            this.buttonProductView.TabIndex = 1;
            this.buttonProductView.Text = "查询与报表";
            this.buttonProductView.UseVisualStyleBackColor = true;
            this.buttonProductView.Click += new System.EventHandler(this.buttonWareProductView_Click);
            // 
            // buttonDeviceMonitor
            // 
            this.buttonDeviceMonitor.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonDeviceMonitor.Location = new System.Drawing.Point(0, 34);
            this.buttonDeviceMonitor.Name = "buttonDeviceMonitor";
            this.buttonDeviceMonitor.Size = new System.Drawing.Size(111, 34);
            this.buttonDeviceMonitor.TabIndex = 2;
            this.buttonDeviceMonitor.Text = "设备监控";
            this.buttonDeviceMonitor.UseVisualStyleBackColor = true;
            this.buttonDeviceMonitor.Click += new System.EventHandler(this.buttonDeviceMonitor_Click);
            // 
            // button_WarehouseMonitor
            // 
            this.button_WarehouseMonitor.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_WarehouseMonitor.Location = new System.Drawing.Point(0, 0);
            this.button_WarehouseMonitor.Name = "button_WarehouseMonitor";
            this.button_WarehouseMonitor.Size = new System.Drawing.Size(111, 34);
            this.button_WarehouseMonitor.TabIndex = 0;
            this.button_WarehouseMonitor.Text = "仓位监控";
            this.button_WarehouseMonitor.UseVisualStyleBackColor = true;
            this.button_WarehouseMonitor.Click += new System.EventHandler(this.button_WarehouseMonitor_Click);
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.listBoxOutput);
            this.panelOutput.Controls.Add(this.toolStrip1);
            this.panelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutput.Location = new System.Drawing.Point(0, 0);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(815, 68);
            this.panelOutput.TabIndex = 1;
            // 
            // listBoxOutput
            // 
            this.listBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOutput.FormattingEnabled = true;
            this.listBoxOutput.ItemHeight = 12;
            this.listBoxOutput.Location = new System.Drawing.Point(0, 25);
            this.listBoxOutput.Name = "listBoxOutput";
            this.listBoxOutput.Size = new System.Drawing.Size(815, 43);
            this.listBoxOutput.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripComboBoxScale});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(815, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(29, 22);
            this.toolStripLabel2.Text = "缩放";
            // 
            // toolStripComboBoxScale
            // 
            this.toolStripComboBoxScale.Items.AddRange(new object[] {
            "50",
            "70",
            "90",
            "100",
            "120",
            "140",
            "160",
            "180",
            "200"});
            this.toolStripComboBoxScale.Name = "toolStripComboBoxScale";
            this.toolStripComboBoxScale.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBoxScale.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxScale_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripProgressBar1,
            this.toolStripStatusLabelTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 68);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(815, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.ToolTipText = "打开进度";
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabelTime.Spring = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.用户ToolStripMenuItem,
            this.窗口ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(815, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSysSet,
            this.MenuItemBkgdDB,
            this.MenuItemSysQuit});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // MenuItemSysSet
            // 
            this.MenuItemSysSet.Name = "MenuItemSysSet";
            this.MenuItemSysSet.Size = new System.Drawing.Size(118, 22);
            this.MenuItemSysSet.Text = "设置 ";
            this.MenuItemSysSet.Click += new System.EventHandler(this.MenuItemSysSet_Click);
            // 
            // MenuItemBkgdDB
            // 
            this.MenuItemBkgdDB.Name = "MenuItemBkgdDB";
            this.MenuItemBkgdDB.Size = new System.Drawing.Size(118, 22);
            this.MenuItemBkgdDB.Text = "后台数据";
            this.MenuItemBkgdDB.Click += new System.EventHandler(this.MenuItemBkgdDB_Click);
            // 
            // MenuItemSysQuit
            // 
            this.MenuItemSysQuit.Name = "MenuItemSysQuit";
            this.MenuItemSysQuit.Size = new System.Drawing.Size(118, 22);
            this.MenuItemSysQuit.Text = "退出";
            this.MenuItemSysQuit.Click += new System.EventHandler(this.MenuItemSysQuit_Click);
            // 
            // 用户ToolStripMenuItem
            // 
            this.用户ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemUserChange,
            this.MenuItemLoginoff,
            this.MenuItemUserManage,
            this.MenuItemModifyPWD});
            this.用户ToolStripMenuItem.Name = "用户ToolStripMenuItem";
            this.用户ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.用户ToolStripMenuItem.Text = "用户";
            // 
            // MenuItemUserChange
            // 
            this.MenuItemUserChange.Name = "MenuItemUserChange";
            this.MenuItemUserChange.Size = new System.Drawing.Size(118, 22);
            this.MenuItemUserChange.Text = "切换用户";
            this.MenuItemUserChange.ToolTipText = "重新选择登录用户";
            this.MenuItemUserChange.Click += new System.EventHandler(this.MenuItemUserChange_Click);
            // 
            // MenuItemLoginoff
            // 
            this.MenuItemLoginoff.Name = "MenuItemLoginoff";
            this.MenuItemLoginoff.Size = new System.Drawing.Size(118, 22);
            this.MenuItemLoginoff.Text = "用户注销";
            this.MenuItemLoginoff.ToolTipText = "注销当前用户";
            this.MenuItemLoginoff.Click += new System.EventHandler(this.MenuItemLoginoff_Click);
            // 
            // MenuItemUserManage
            // 
            this.MenuItemUserManage.Name = "MenuItemUserManage";
            this.MenuItemUserManage.Size = new System.Drawing.Size(118, 22);
            this.MenuItemUserManage.Text = "用户管理";
            this.MenuItemUserManage.ToolTipText = "只有在系统维护人员登录后才可以执行此功能，可以增加或删除管理员用户，重置密码等操作。";
            this.MenuItemUserManage.Click += new System.EventHandler(this.MenuItemUserManage_Click);
            // 
            // MenuItemModifyPWD
            // 
            this.MenuItemModifyPWD.Name = "MenuItemModifyPWD";
            this.MenuItemModifyPWD.Size = new System.Drawing.Size(118, 22);
            this.MenuItemModifyPWD.Text = "修改密码";
            this.MenuItemModifyPWD.Click += new System.EventHandler(this.MenuItemModifyPWD_Click);
            // 
            // 窗口ToolStripMenuItem
            // 
            this.窗口ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemWarehouseMonitorWindow,
            this.MenuItemDevMonitorWindow,
            this.MenuItemQueryWindow});
            this.窗口ToolStripMenuItem.Name = "窗口ToolStripMenuItem";
            this.窗口ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.窗口ToolStripMenuItem.Text = "窗口";
            // 
            // MenuItemWarehouseMonitorWindow
            // 
            this.MenuItemWarehouseMonitorWindow.Name = "MenuItemWarehouseMonitorWindow";
            this.MenuItemWarehouseMonitorWindow.Size = new System.Drawing.Size(130, 22);
            this.MenuItemWarehouseMonitorWindow.Text = "仓位监控";
            this.MenuItemWarehouseMonitorWindow.Click += new System.EventHandler(this.MenuItemWarehouseMonitorWindow_Click);
            // 
            // MenuItemDevMonitorWindow
            // 
            this.MenuItemDevMonitorWindow.Name = "MenuItemDevMonitorWindow";
            this.MenuItemDevMonitorWindow.Size = new System.Drawing.Size(130, 22);
            this.MenuItemDevMonitorWindow.Text = "设备监控";
            this.MenuItemDevMonitorWindow.Click += new System.EventHandler(this.MenuItemDevMonitorWindow_Click);
            // 
            // MenuItemQueryWindow
            // 
            this.MenuItemQueryWindow.Name = "MenuItemQueryWindow";
            this.MenuItemQueryWindow.Size = new System.Drawing.Size(130, 22);
            this.MenuItemQueryWindow.Text = "查询与报表";
            this.MenuItemQueryWindow.Click += new System.EventHandler(this.MenuItemQueryWindow_Click);
            // 
            // ASRSMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 487);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ASRSMainView";
            this.Text = "ASRS管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ASRSView_FormClosed);
            this.Load += new System.EventHandler(this.ASRSMainView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button_WarehouseMonitor;
        private System.Windows.Forms.Button buttonProductView;
        private System.Windows.Forms.Button buttonDeviceMonitor;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxScale;
        private System.Windows.Forms.ListBox listBoxOutput;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.ToolStripMenuItem 窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWarehouseMonitorWindow;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDevMonitorWindow;
        private System.Windows.Forms.ToolStripMenuItem MenuItemQueryWindow;
        private System.Windows.Forms.ToolStripMenuItem 用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUserChange;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoginoff;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUserManage;
        private System.Windows.Forms.ToolStripMenuItem MenuItemModifyPWD;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSysQuit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSysSet;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBkgdDB;
    }
}

