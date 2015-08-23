namespace WareHouseControl
{
    partial class WareHouseControl
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
            this.panel1 = new MyPanel();
            this.panelWareCells = new MyPanel();
            this.panelLeft = new MyPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(100, 120);
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.panelWareCells);
            this.panel1.Controls.Add(this.panelLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 380);
            this.panel1.TabIndex = 0;
            // 
            // panelWareCells
            // 
            this.panelWareCells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWareCells.AutoScroll = true;
            this.panelWareCells.AutoSize = true;
            this.panelWareCells.BackColor = System.Drawing.SystemColors.ControlText;
            this.panelWareCells.Location = new System.Drawing.Point(75, 0);
            this.panelWareCells.Name = "panelWareCells";
            this.panelWareCells.Size = new System.Drawing.Size(510, 380);
            this.panelWareCells.TabIndex = 3;
            this.panelWareCells.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCells_Paint);
            this.panelWareCells.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseDown);
            this.panelWareCells.MouseEnter += new System.EventHandler(this.panelWareCells_MouseEnter);
            this.panelWareCells.MouseLeave += new System.EventHandler(this.panelWareCells_MouseLeave);
            this.panelWareCells.MouseHover += new System.EventHandler(this.panelWareCells_MouseHover);
            this.panelWareCells.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseMove);
            this.panelWareCells.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelWareCells_MouseUp);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.ForeColor = System.Drawing.SystemColors.Desktop;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(75, 380);
            this.panelLeft.TabIndex = 0;
            // 
            // WareHouseControl
            // 
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "WareHouseControl";
            this.Size = new System.Drawing.Size(588, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyPanel panel1;
        private MyPanel panelLeft;
        private MyPanel panelWareCells;

    }
}
