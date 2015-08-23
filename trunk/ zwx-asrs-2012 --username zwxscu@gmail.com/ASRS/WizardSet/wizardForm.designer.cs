namespace ASRS
{
    partial class wizardForm
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
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPage2 = new DevComponents.DotNetBar.WizardPage();
            this.labelDBConTip = new System.Windows.Forms.Label();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxXPwd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxDBUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ipAddressInput1 = new DevComponents.Editors.IpAddressInput();
            this.wizardPage3 = new DevComponents.DotNetBar.WizardPage();
            this.labelWarehouseSetInfo = new System.Windows.Forms.Label();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.integerInputColumns = new DevComponents.Editors.IntegerInput();
            this.integerInputChannels = new DevComponents.Editors.IntegerInput();
            this.integerInputLayer = new DevComponents.Editors.IntegerInput();
            this.progressBarXHouseInit = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressInput1)).BeginInit();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputLayer)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackButtonText = "< 上一步";
            this.wizard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
            this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.wizard1.CancelButtonText = "取消";
            this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "确定";
            // 
            // 
            // 
            this.wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.FooterStyle.Class = "";
            this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.wizard1.HeaderCaptionFont = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold);
            this.wizard1.HeaderDescriptionIndent = 62;
            this.wizard1.HeaderDescriptionVisible = false;
            this.wizard1.HeaderHeight = 90;
            this.wizard1.HeaderImageAlignment = DevComponents.DotNetBar.eWizardTitleImageAlignment.Left;
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.Class = "";
            this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.HeaderTitleIndent = 62;
            this.wizard1.HelpButtonVisible = false;
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.NextButtonText = "下一步 >";
            this.wizard1.Size = new System.Drawing.Size(894, 473);
            this.wizard1.TabIndex = 1;
            this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3});
            this.wizard1.NextButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_NextButtonClick);
            this.wizard1.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_FinishButtonClick);
            this.wizard1.CancelButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_CancelButtonClick);
            this.wizard1.WizardPageChanging += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wizard1_WizardPageChanging);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.label3);
            this.wizardPage1.Location = new System.Drawing.Point(7, 102);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.PageDescription = "欢迎页面";
            this.wizardPage1.Size = new System.Drawing.Size(880, 313);
            // 
            // 
            // 
            this.wizardPage1.Style.Class = "";
            // 
            // 
            // 
            this.wizardPage1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.wizardPage1.StyleMouseOver.Class = "";
            this.wizardPage1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(572, 66);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎使用XXX系统，在第一次使用之前需要完成下面的初始化！";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(210, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(655, 181);
            this.label2.TabIndex = 1;
            this.label2.Text = "通过此向导，您将完成：\r\n\r\n1数据库服务器验证信息初始化\r\n\r\n2仓位设置初始化";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(210, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "继续，请按下一步";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage2.AntiAlias = false;
            this.wizardPage2.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.wizardPage2.Controls.Add(this.labelDBConTip);
            this.wizardPage2.Controls.Add(this.labelX6);
            this.wizardPage2.Controls.Add(this.labelX2);
            this.wizardPage2.Controls.Add(this.labelX1);
            this.wizardPage2.Controls.Add(this.textBoxXPwd);
            this.wizardPage2.Controls.Add(this.textBoxDBUser);
            this.wizardPage2.Controls.Add(this.ipAddressInput1);
            this.wizardPage2.Location = new System.Drawing.Point(7, 102);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.PageDescription = "数据库身份信息验证";
            this.wizardPage2.PageTitle = "数据库验证信息设置";
            this.wizardPage2.Size = new System.Drawing.Size(880, 313);
            // 
            // 
            // 
            this.wizardPage2.Style.Class = "";
            // 
            // 
            // 
            this.wizardPage2.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.wizardPage2.StyleMouseOver.Class = "";
            this.wizardPage2.TabIndex = 8;
            // 
            // labelDBConTip
            // 
            this.labelDBConTip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDBConTip.BackColor = System.Drawing.Color.Transparent;
            this.labelDBConTip.Location = new System.Drawing.Point(155, 132);
            this.labelDBConTip.Name = "labelDBConTip";
            this.labelDBConTip.Size = new System.Drawing.Size(655, 155);
            this.labelDBConTip.TabIndex = 9;
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.Location = new System.Drawing.Point(156, 92);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(121, 23);
            this.labelX6.TabIndex = 7;
            this.labelX6.Text = "库登密码";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(157, 61);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(121, 23);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "登录名";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(157, 24);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(121, 23);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "数据库服务器IP地址";
            // 
            // textBoxXPwd
            // 
            // 
            // 
            // 
            this.textBoxXPwd.Border.Class = "TextBoxBorder";
            this.textBoxXPwd.Location = new System.Drawing.Point(283, 92);
            this.textBoxXPwd.Name = "textBoxXPwd";
            this.textBoxXPwd.Size = new System.Drawing.Size(177, 21);
            this.textBoxXPwd.TabIndex = 6;
            this.textBoxXPwd.UseSystemPasswordChar = true;
            this.textBoxXPwd.Enter += new System.EventHandler(this.wizardPage2_dbSetInput);
            // 
            // textBoxDBUser
            // 
            // 
            // 
            // 
            this.textBoxDBUser.Border.Class = "TextBoxBorder";
            this.textBoxDBUser.Location = new System.Drawing.Point(283, 58);
            this.textBoxDBUser.Name = "textBoxDBUser";
            this.textBoxDBUser.Size = new System.Drawing.Size(177, 21);
            this.textBoxDBUser.TabIndex = 6;
            this.textBoxDBUser.Text = "sa";
            this.textBoxDBUser.Enter += new System.EventHandler(this.wizardPage2_dbSetInput);
            // 
            // ipAddressInput1
            // 
            this.ipAddressInput1.AutoOverwrite = true;
            // 
            // 
            // 
            this.ipAddressInput1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ipAddressInput1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ipAddressInput1.ButtonFreeText.Visible = true;
            this.ipAddressInput1.Location = new System.Drawing.Point(284, 23);
            this.ipAddressInput1.Name = "ipAddressInput1";
            this.ipAddressInput1.Size = new System.Drawing.Size(176, 21);
            this.ipAddressInput1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ipAddressInput1.TabIndex = 5;
            this.ipAddressInput1.Value = "127.0.0.1";
            this.ipAddressInput1.Enter += new System.EventHandler(this.wizardPage2_dbSetInput);
            // 
            // wizardPage3
            // 
            this.wizardPage3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage3.AntiAlias = false;
            this.wizardPage3.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage3.Controls.Add(this.progressBarXHouseInit);
            this.wizardPage3.Controls.Add(this.labelWarehouseSetInfo);
            this.wizardPage3.Controls.Add(this.labelX5);
            this.wizardPage3.Controls.Add(this.labelX4);
            this.wizardPage3.Controls.Add(this.labelX3);
            this.wizardPage3.Controls.Add(this.integerInputColumns);
            this.wizardPage3.Controls.Add(this.integerInputChannels);
            this.wizardPage3.Controls.Add(this.integerInputLayer);
            this.wizardPage3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.wizardPage3.Location = new System.Drawing.Point(7, 102);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.PageDescription = "货架信息设置 ";
            this.wizardPage3.PageTitle = "货架信息设置 ";
            this.wizardPage3.Size = new System.Drawing.Size(880, 313);
            // 
            // 
            // 
            this.wizardPage3.Style.Class = "";
            // 
            // 
            // 
            this.wizardPage3.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.wizardPage3.StyleMouseOver.Class = "";
            this.wizardPage3.TabIndex = 9;
            // 
            // labelWarehouseSetInfo
            // 
            this.labelWarehouseSetInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarehouseSetInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelWarehouseSetInfo.Location = new System.Drawing.Point(105, 218);
            this.labelWarehouseSetInfo.Name = "labelWarehouseSetInfo";
            this.labelWarehouseSetInfo.Size = new System.Drawing.Size(532, 88);
            this.labelWarehouseSetInfo.TabIndex = 10;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.Location = new System.Drawing.Point(224, 99);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(42, 23);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "列数";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(224, 65);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(42, 23);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "巷道数";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(224, 26);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(42, 23);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "层数";
            // 
            // integerInputColumns
            // 
            // 
            // 
            // 
            this.integerInputColumns.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInputColumns.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInputColumns.Location = new System.Drawing.Point(274, 99);
            this.integerInputColumns.Name = "integerInputColumns";
            this.integerInputColumns.ShowUpDown = true;
            this.integerInputColumns.Size = new System.Drawing.Size(96, 21);
            this.integerInputColumns.TabIndex = 0;
            this.integerInputColumns.Value = 50;
            // 
            // integerInputChannels
            // 
            // 
            // 
            // 
            this.integerInputChannels.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInputChannels.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInputChannels.Location = new System.Drawing.Point(274, 62);
            this.integerInputChannels.Name = "integerInputChannels";
            this.integerInputChannels.ShowUpDown = true;
            this.integerInputChannels.Size = new System.Drawing.Size(96, 21);
            this.integerInputChannels.TabIndex = 0;
            this.integerInputChannels.Value = 2;
            // 
            // integerInputLayer
            // 
            // 
            // 
            // 
            this.integerInputLayer.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInputLayer.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInputLayer.Location = new System.Drawing.Point(274, 23);
            this.integerInputLayer.Name = "integerInputLayer";
            this.integerInputLayer.ShowUpDown = true;
            this.integerInputLayer.Size = new System.Drawing.Size(96, 21);
            this.integerInputLayer.TabIndex = 0;
            this.integerInputLayer.Value = 4;
            // 
            // progressBarXHouseInit
            // 
            // 
            // 
            // 
            this.progressBarXHouseInit.BackgroundStyle.Class = "";
            this.progressBarXHouseInit.Location = new System.Drawing.Point(138, 176);
            this.progressBarXHouseInit.Name = "progressBarXHouseInit";
            this.progressBarXHouseInit.Size = new System.Drawing.Size(467, 23);
            this.progressBarXHouseInit.TabIndex = 11;
            this.progressBarXHouseInit.Text = "progressBarX1";
            this.progressBarXHouseInit.Visible = false;
            // 
            // wizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 473);
            this.Controls.Add(this.wizard1);
            this.Name = "wizardForm";
            this.Text = "系统初始化配置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressInput1)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.integerInputColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.WizardPage wizardPage2;
        private DevComponents.DotNetBar.WizardPage wizardPage3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxDBUser;
        private DevComponents.Editors.IpAddressInput ipAddressInput1;
        private System.Windows.Forms.Label labelDBConTip;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput integerInputColumns;
        private DevComponents.Editors.IntegerInput integerInputChannels;
        private DevComponents.Editors.IntegerInput integerInputLayer;
        private System.Windows.Forms.Label labelWarehouseSetInfo;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPwd;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarXHouseInit;

    }
}