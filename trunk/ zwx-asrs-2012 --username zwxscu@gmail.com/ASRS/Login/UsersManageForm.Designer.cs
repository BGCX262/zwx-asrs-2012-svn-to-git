namespace ASRS
{
    partial class UsersManageForm
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
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.buttonXApply = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxXUserRole = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxXPWD2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxXPWD1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxXUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.buttonXQuit = new DevComponents.DotNetBar.ButtonX();
            this.buttonXRefresh = new DevComponents.DotNetBar.ButtonX();
            this.radionAdd = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radionDel = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radionEdit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dataGridViewX1);
            this.panelEx1.Location = new System.Drawing.Point(0, 136);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(706, 338);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(706, 338);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.radionEdit);
            this.panelEx2.Controls.Add(this.radionDel);
            this.panelEx2.Controls.Add(this.radionAdd);
            this.panelEx2.Controls.Add(this.groupPanel1);
            this.panelEx2.Controls.Add(this.reflectionLabel1);
            this.panelEx2.Controls.Add(this.buttonXQuit);
            this.panelEx2.Controls.Add(this.buttonXRefresh);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(706, 123);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.buttonXApply);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.textBoxXUserRole);
            this.groupPanel1.Controls.Add(this.textBoxXPWD2);
            this.groupPanel1.Controls.Add(this.textBoxXPWD1);
            this.groupPanel1.Controls.Add(this.textBoxXUserName);
            this.groupPanel1.Location = new System.Drawing.Point(3, 50);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(700, 70);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.TabIndex = 2;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Location = new System.Drawing.Point(0, 32);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(42, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "用户级";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.Location = new System.Drawing.Point(198, 30);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(99, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "请再次输入密码";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.Location = new System.Drawing.Point(198, 0);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(42, 23);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "密码";
            // 
            // buttonXApply
            // 
            this.buttonXApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXApply.Location = new System.Drawing.Point(472, 28);
            this.buttonXApply.Name = "buttonXApply";
            this.buttonXApply.Size = new System.Drawing.Size(75, 23);
            this.buttonXApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXApply.TabIndex = 0;
            this.buttonXApply.Text = "提  交 ";
            this.buttonXApply.Click += new System.EventHandler(this.buttonXApply_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Location = new System.Drawing.Point(0, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "用户名";
            // 
            // textBoxXUserRole
            // 
            // 
            // 
            // 
            this.textBoxXUserRole.Border.Class = "TextBoxBorder";
            this.textBoxXUserRole.Location = new System.Drawing.Point(48, 32);
            this.textBoxXUserRole.Name = "textBoxXUserRole";
            this.textBoxXUserRole.Size = new System.Drawing.Size(125, 21);
            this.textBoxXUserRole.TabIndex = 0;
            // 
            // textBoxXPWD2
            // 
            // 
            // 
            // 
            this.textBoxXPWD2.Border.Class = "TextBoxBorder";
            this.textBoxXPWD2.Location = new System.Drawing.Point(303, 32);
            this.textBoxXPWD2.Name = "textBoxXPWD2";
            this.textBoxXPWD2.Size = new System.Drawing.Size(133, 21);
            this.textBoxXPWD2.TabIndex = 0;
            // 
            // textBoxXPWD1
            // 
            // 
            // 
            // 
            this.textBoxXPWD1.Border.Class = "TextBoxBorder";
            this.textBoxXPWD1.Location = new System.Drawing.Point(303, 2);
            this.textBoxXPWD1.Name = "textBoxXPWD1";
            this.textBoxXPWD1.Size = new System.Drawing.Size(133, 21);
            this.textBoxXPWD1.TabIndex = 0;
            // 
            // textBoxXUserName
            // 
            // 
            // 
            // 
            this.textBoxXUserName.Border.Class = "TextBoxBorder";
            this.textBoxXUserName.Location = new System.Drawing.Point(48, 3);
            this.textBoxXUserName.Name = "textBoxXUserName";
            this.textBoxXUserName.Size = new System.Drawing.Size(125, 21);
            this.textBoxXUserName.TabIndex = 0;
            // 
            // reflectionLabel1
            // 
            // 
            // 
            // 
            this.reflectionLabel1.BackgroundStyle.Class = "";
            this.reflectionLabel1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reflectionLabel1.Location = new System.Drawing.Point(0, 3);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(164, 39);
            this.reflectionLabel1.TabIndex = 1;
            this.reflectionLabel1.Text = "<b><font size=\"+6\"><i>用户</i><font color=\"#B02B2C\">管理</font></font></b>";
            // 
            // buttonXQuit
            // 
            this.buttonXQuit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXQuit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXQuit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXQuit.Location = new System.Drawing.Point(619, 21);
            this.buttonXQuit.Name = "buttonXQuit";
            this.buttonXQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonXQuit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXQuit.TabIndex = 0;
            this.buttonXQuit.Text = "退  出";
            this.buttonXQuit.Click += new System.EventHandler(this.buttonXQuit_Click);
            // 
            // buttonXRefresh
            // 
            this.buttonXRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRefresh.Location = new System.Drawing.Point(527, 21);
            this.buttonXRefresh.Name = "buttonXRefresh";
            this.buttonXRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonXRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXRefresh.TabIndex = 0;
            this.buttonXRefresh.Text = "刷  新";
            this.buttonXRefresh.Click += new System.EventHandler(this.buttonXRefresh_Click);
            // 
            // radionAdd
            // 
            // 
            // 
            // 
            this.radionAdd.BackgroundStyle.Class = "";
            this.radionAdd.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radionAdd.Location = new System.Drawing.Point(216, 18);
            this.radionAdd.Name = "radionAdd";
            this.radionAdd.Size = new System.Drawing.Size(49, 23);
            this.radionAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radionAdd.TabIndex = 3;
            this.radionAdd.Text = "增加";
            // 
            // radionDel
            // 
            // 
            // 
            // 
            this.radionDel.BackgroundStyle.Class = "";
            this.radionDel.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radionDel.Location = new System.Drawing.Point(271, 18);
            this.radionDel.Name = "radionDel";
            this.radionDel.Size = new System.Drawing.Size(49, 23);
            this.radionDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radionDel.TabIndex = 3;
            this.radionDel.Text = "删除";
            // 
            // radionEdit
            // 
            // 
            // 
            // 
            this.radionEdit.BackgroundStyle.Class = "";
            this.radionEdit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radionEdit.Location = new System.Drawing.Point(326, 18);
            this.radionEdit.Name = "radionEdit";
            this.radionEdit.Size = new System.Drawing.Size(49, 23);
            this.radionEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radionEdit.TabIndex = 3;
            this.radionEdit.Text = "编辑";
            // 
            // UsersManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(706, 474);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "UsersManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.UsersManageForm_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX buttonXRefresh;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private DevComponents.DotNetBar.ButtonX buttonXQuit;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX buttonXApply;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXUserRole;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPWD2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXPWD1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXUserName;
        private DevComponents.DotNetBar.Controls.CheckBoxX radionEdit;
        private DevComponents.DotNetBar.Controls.CheckBoxX radionDel;
        private DevComponents.DotNetBar.Controls.CheckBoxX radionAdd;
    }
}