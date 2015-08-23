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
    public partial class ReportView : Form
    {
        public IASRSViewComn IParentView { get; set; }
        public ReportView(IASRSViewComn IParent)
        {
            InitializeComponent();
            IParentView = IParent;
        }
    }
}
