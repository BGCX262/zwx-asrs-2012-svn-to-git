namespace PLCSim
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelUdpstatus = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelCurrentCoord1 = new System.Windows.Forms.Label();
            this.labelCurrentStatus1 = new System.Windows.Forms.Label();
            this.labelTargetCoord1 = new System.Windows.Forms.Label();
            this.labelCurrentTask1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCurrentCoord2 = new System.Windows.Forms.Label();
            this.labelCurrentStatus2 = new System.Windows.Forms.Label();
            this.labelTargetCoord2 = new System.Windows.Forms.Label();
            this.labelCurrentTask2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.splitContainer1.Panel1.Controls.Add(this.labelUdpstatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(771, 386);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelUdpstatus
            // 
            this.labelUdpstatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUdpstatus.AutoSize = true;
            this.labelUdpstatus.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUdpstatus.ForeColor = System.Drawing.Color.Cornsilk;
            this.labelUdpstatus.Location = new System.Drawing.Point(645, 3);
            this.labelUdpstatus.Name = "labelUdpstatus";
            this.labelUdpstatus.Size = new System.Drawing.Size(120, 16);
            this.labelUdpstatus.TabIndex = 0;
            this.labelUdpstatus.Text = "通讯服务未启动";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer2.Size = new System.Drawing.Size(771, 327);
            this.splitContainer2.SplitterDistance = 381;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelCurrentCoord1);
            this.groupBox1.Controls.Add(this.labelCurrentStatus1);
            this.groupBox1.Controls.Add(this.labelTargetCoord1);
            this.groupBox1.Controls.Add(this.labelCurrentTask1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 140);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // labelCurrentCoord1
            // 
            this.labelCurrentCoord1.AutoSize = true;
            this.labelCurrentCoord1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentCoord1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentCoord1.Location = new System.Drawing.Point(85, 93);
            this.labelCurrentCoord1.Name = "labelCurrentCoord1";
            this.labelCurrentCoord1.Size = new System.Drawing.Size(47, 14);
            this.labelCurrentCoord1.TabIndex = 4;
            this.labelCurrentCoord1.Text = "1,1,1";
            // 
            // labelCurrentStatus1
            // 
            this.labelCurrentStatus1.AutoSize = true;
            this.labelCurrentStatus1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentStatus1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentStatus1.Location = new System.Drawing.Point(85, 67);
            this.labelCurrentStatus1.Name = "labelCurrentStatus1";
            this.labelCurrentStatus1.Size = new System.Drawing.Size(37, 14);
            this.labelCurrentStatus1.TabIndex = 4;
            this.labelCurrentStatus1.Text = "闲置";
            // 
            // labelTargetCoord1
            // 
            this.labelTargetCoord1.AutoSize = true;
            this.labelTargetCoord1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTargetCoord1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelTargetCoord1.Location = new System.Drawing.Point(85, 45);
            this.labelTargetCoord1.Name = "labelTargetCoord1";
            this.labelTargetCoord1.Size = new System.Drawing.Size(47, 14);
            this.labelTargetCoord1.TabIndex = 4;
            this.labelTargetCoord1.Text = "1,1,1";
            // 
            // labelCurrentTask1
            // 
            this.labelCurrentTask1.AutoSize = true;
            this.labelCurrentTask1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentTask1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentTask1.Location = new System.Drawing.Point(86, 19);
            this.labelCurrentTask1.Name = "labelCurrentTask1";
            this.labelCurrentTask1.Size = new System.Drawing.Size(22, 14);
            this.labelCurrentTask1.TabIndex = 4;
            this.labelCurrentTask1.Text = "无";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "当前位置：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "当前状态：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "目标位置：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "当前任务：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Olive;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "1号小车";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Olive;
            this.pictureBox1.Location = new System.Drawing.Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(354, 27);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(-1, 181);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(377, 145);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Olive;
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "2号小车";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Olive;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(356, 27);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(3, 181);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(378, 142);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelCurrentCoord2);
            this.groupBox2.Controls.Add(this.labelCurrentStatus2);
            this.groupBox2.Controls.Add(this.labelTargetCoord2);
            this.groupBox2.Controls.Add(this.labelCurrentTask2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(15, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 140);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // labelCurrentCoord2
            // 
            this.labelCurrentCoord2.AutoSize = true;
            this.labelCurrentCoord2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentCoord2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentCoord2.Location = new System.Drawing.Point(85, 93);
            this.labelCurrentCoord2.Name = "labelCurrentCoord2";
            this.labelCurrentCoord2.Size = new System.Drawing.Size(47, 14);
            this.labelCurrentCoord2.TabIndex = 4;
            this.labelCurrentCoord2.Text = "1,1,1";
            // 
            // labelCurrentStatus2
            // 
            this.labelCurrentStatus2.AutoSize = true;
            this.labelCurrentStatus2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentStatus2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentStatus2.Location = new System.Drawing.Point(85, 67);
            this.labelCurrentStatus2.Name = "labelCurrentStatus2";
            this.labelCurrentStatus2.Size = new System.Drawing.Size(37, 14);
            this.labelCurrentStatus2.TabIndex = 4;
            this.labelCurrentStatus2.Text = "闲置";
            // 
            // labelTargetCoord2
            // 
            this.labelTargetCoord2.AutoSize = true;
            this.labelTargetCoord2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTargetCoord2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelTargetCoord2.Location = new System.Drawing.Point(85, 45);
            this.labelTargetCoord2.Name = "labelTargetCoord2";
            this.labelTargetCoord2.Size = new System.Drawing.Size(47, 14);
            this.labelTargetCoord2.TabIndex = 4;
            this.labelTargetCoord2.Text = "1,1,1";
            // 
            // labelCurrentTask2
            // 
            this.labelCurrentTask2.AutoSize = true;
            this.labelCurrentTask2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentTask2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelCurrentTask2.Location = new System.Drawing.Point(86, 19);
            this.labelCurrentTask2.Name = "labelCurrentTask2";
            this.labelCurrentTask2.Size = new System.Drawing.Size(22, 14);
            this.labelCurrentTask2.TabIndex = 4;
            this.labelCurrentTask2.Text = "无";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "当前位置：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "当前状态：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "目标位置：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "当前任务：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 386);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "PLCSim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelUdpstatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelCurrentCoord1;
        private System.Windows.Forms.Label labelCurrentStatus1;
        private System.Windows.Forms.Label labelTargetCoord1;
        private System.Windows.Forms.Label labelCurrentTask1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelCurrentCoord2;
        private System.Windows.Forms.Label labelCurrentStatus2;
        private System.Windows.Forms.Label labelTargetCoord2;
        private System.Windows.Forms.Label labelCurrentTask2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}

