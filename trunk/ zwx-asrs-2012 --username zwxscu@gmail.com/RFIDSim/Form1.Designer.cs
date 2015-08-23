namespace RFIDSim
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.labelUdpstatus = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.labelUdpstatus);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 395);
            this.panel1.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(9, 49);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(135, 31);
            this.buttonClear.TabIndex = 0;
            this.buttonClear.Text = "清理";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonRestart);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 149);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(6, 11);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(138, 32);
            this.buttonRestart.TabIndex = 7;
            this.buttonRestart.Text = "重新启动";
            this.buttonRestart.UseVisualStyleBackColor = true;
            // 
            // labelUdpstatus
            // 
            this.labelUdpstatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUdpstatus.AutoSize = true;
            this.labelUdpstatus.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUdpstatus.ForeColor = System.Drawing.Color.Cornsilk;
            this.labelUdpstatus.Location = new System.Drawing.Point(417, 9);
            this.labelUdpstatus.Name = "labelUdpstatus";
            this.labelUdpstatus.Size = new System.Drawing.Size(120, 16);
            this.labelUdpstatus.TabIndex = 2;
            this.labelUdpstatus.Text = "通讯服务未启动";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.richTextBox1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox1.Location = new System.Drawing.Point(3, 176);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(636, 219);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 395);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "RFIDSim";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelUdpstatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRestart;
    }
}

