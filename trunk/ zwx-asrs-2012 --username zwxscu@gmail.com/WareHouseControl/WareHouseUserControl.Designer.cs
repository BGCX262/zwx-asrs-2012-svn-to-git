namespace WareHouseControl
{
    partial class WareHouseUserControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelWareCells = new WareHouseControl.MyPanel();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 1000;
            this.toolTip1.BackColor = System.Drawing.SystemColors.InfoText;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.StripAmpersands = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // panelWareCells
            // 
            this.panelWareCells.AutoScroll = true;
            this.panelWareCells.AutoSize = true;
            this.panelWareCells.BackColor = System.Drawing.SystemColors.ControlText;
            this.panelWareCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWareCells.Location = new System.Drawing.Point(0, 0);
            this.panelWareCells.Name = "panelWareCells";
            this.panelWareCells.Size = new System.Drawing.Size(588, 380);
            this.panelWareCells.TabIndex = 4;
            this.panelWareCells.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCells_Paint);
            this.panelWareCells.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseDown);
            this.panelWareCells.MouseEnter += new System.EventHandler(this.panelWareCells_MouseEnter);
            this.panelWareCells.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseMove);
            this.panelWareCells.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseUp);
            this.panelWareCells.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseWheel);
            // 
            // contextMenu1
            // 
            this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuTrans,
            this.toolStripMenuSelect});
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(153, 70);
            // 
            // toolStripMenuTrans
            // 
            this.toolStripMenuTrans.Name = "toolStripMenuTrans";
            this.toolStripMenuTrans.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuTrans.Text = "平移";
            this.toolStripMenuTrans.Click += new System.EventHandler(this.toolStripMenuTrans_Click);
            // 
            // toolStripMenuSelect
            // 
            this.toolStripMenuSelect.Name = "toolStripMenuSelect";
            this.toolStripMenuSelect.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuSelect.Text = "查询";
            this.toolStripMenuSelect.Click += new System.EventHandler(this.toolStripMenuSelect_Click);
            // 
            // WareHouseUserControl
            // 
            this.AutoSize = true;
            this.Controls.Add(this.panelWareCells);
            this.Name = "WareHouseUserControl";
            this.Size = new System.Drawing.Size(588, 380);
            this.contextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyPanel panelWareCells;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuTrans;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSelect;


    }
}
