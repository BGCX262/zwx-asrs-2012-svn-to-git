using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASRS
{
    public partial class LoadForm : Form
    {
        public string labelTipText { get; set; }
        public LoadForm()
        {
            InitializeComponent();
            labelTipText = "正在启动...";
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            this.label1.Text = labelTipText;
        }
    }
}
