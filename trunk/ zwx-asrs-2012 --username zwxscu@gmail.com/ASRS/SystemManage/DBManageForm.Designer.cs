namespace ASRS
{
    partial class DBManageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonXQuit = new DevComponents.DotNetBar.ButtonX();
            this.buttonXApply = new DevComponents.DotNetBar.ButtonX();
            this.buttonXRefresh = new DevComponents.DotNetBar.ButtonX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.buttonXQuit);
            this.panelEx1.Controls.Add(this.buttonXApply);
            this.panelEx1.Controls.Add(this.buttonXRefresh);
            this.panelEx1.Controls.Add(this.comboBoxEx1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(716, 82);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX1.Location = new System.Drawing.Point(32, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "选择数据表";
            // 
            // buttonXQuit
            // 
            this.buttonXQuit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXQuit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXQuit.Location = new System.Drawing.Point(445, 40);
            this.buttonXQuit.Name = "buttonXQuit";
            this.buttonXQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonXQuit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXQuit.TabIndex = 1;
            this.buttonXQuit.Text = "退出";
            this.buttonXQuit.Click += new System.EventHandler(this.buttonXQuit_Click);
            // 
            // buttonXApply
            // 
            this.buttonXApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXApply.Location = new System.Drawing.Point(364, 40);
            this.buttonXApply.Name = "buttonXApply";
            this.buttonXApply.Size = new System.Drawing.Size(75, 23);
            this.buttonXApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXApply.TabIndex = 1;
            this.buttonXApply.Text = "提交";
            this.buttonXApply.Click += new System.EventHandler(this.buttonXApply_Click);
            // 
            // buttonXRefresh
            // 
            this.buttonXRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRefresh.Location = new System.Drawing.Point(283, 40);
            this.buttonXRefresh.Name = "buttonXRefresh";
            this.buttonXRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonXRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXRefresh.TabIndex = 1;
            this.buttonXRefresh.Text = "刷新";
            this.buttonXRefresh.Click += new System.EventHandler(this.buttonXRefresh_Click);
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8});
            this.comboBoxEx1.Location = new System.Drawing.Point(32, 40);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 0;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "入库记录表";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "出库记录表";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "报警提示信息定义数据表";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "报警提示消息记录表";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "产品类型数据表";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "立库产品数据表";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "出入库记录视图表";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "立库产品存储视图表";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dataGridViewX1);
            this.panelEx2.Controls.Add(this.reflectionLabel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 82);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(716, 381);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(716, 338);
            this.dataGridViewX1.TabIndex = 1;
            // 
            // reflectionLabel1
            // 
            // 
            // 
            // 
            this.reflectionLabel1.BackgroundStyle.Class = "";
            this.reflectionLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionLabel1.Location = new System.Drawing.Point(0, 0);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(716, 43);
            this.reflectionLabel1.TabIndex = 0;
            this.reflectionLabel1.Text = "<b><font size=\"+6\"><i>ASRS</i><font color=\"#B02B2C\">后台数据表</font></font></b>";
            // 
            // DBManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 463);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "DBManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "后台数据管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DBManageForm_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonXQuit;
        private DevComponents.DotNetBar.ButtonX buttonXApply;
        private DevComponents.DotNetBar.ButtonX buttonXRefresh;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
    }
}