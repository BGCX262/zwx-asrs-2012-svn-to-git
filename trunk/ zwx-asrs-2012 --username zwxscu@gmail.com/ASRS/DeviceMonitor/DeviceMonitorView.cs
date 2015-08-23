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
    public partial class DeviceMonitorView : Form
    {
        public IASRSViewComn IParentView { get; set; }
        public DeviceMonitorView(IASRSViewComn IParent)
        {
            InitializeComponent();
            IParentView = IParent;
        }
    }
}
