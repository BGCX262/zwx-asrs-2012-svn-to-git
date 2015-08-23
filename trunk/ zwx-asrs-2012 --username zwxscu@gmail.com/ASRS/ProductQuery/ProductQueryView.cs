using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASRSDBBLL;
namespace ASRS
{
    public partial class ProductQueryView : Form
    {
#region 数据
        /// <summary>
        /// 出入库记录
        /// </summary>
        private HouseInOutBll _houseInOutRdBll = new HouseInOutBll();
        private WarehouseStoreBLL _storeBll = new WarehouseStoreBLL(); 
        private DataTable _currentDt;
#endregion
        public IASRSViewComn IParentView { get; set; }
        public ProductQueryView(IASRSViewComn IParent)
        {
            InitializeComponent();
            IParentView = IParent;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void ProductQueryView_Load(object sender, EventArgs e)
        {
            this.comboBoxExSelDT.SelectedIndex = 0;
            this.comboBoxExLayer1.SelectedIndex = 0;
            this.comboBoxExLayer2.SelectedIndex = 0;

            this.comboBoxExLogLayer1.SelectedIndex = 0;
            this.comboBoxExLogLayer2.SelectedIndex = 0;

            this.comboBoxExRow1.SelectedIndex = 0;
            this.comboBoxExRow2.SelectedIndex = 0;

            this.comboBoxExLogRow1.SelectedIndex = 0;
            this.comboBoxExLogRow2.SelectedIndex = 0;

            this.comboBoxExCol1.SelectedIndex = 0;
            this.comboBoxExCol2.SelectedIndex = 0;

            this.comboBoxExLogCol1.SelectedIndex = 0;
            this.comboBoxExLogCol2.SelectedIndex = 0;

        }

        private void comboBoxExSelDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxExSelDT.Text == "在库产品")
            {
                DataTable dt = _storeBll.GetAllData();
                if(dt != null)
                {
                    _currentDt = dt;
                    this.dataGridViewX1.DataSource = _currentDt;
                }
            }
            else if (this.comboBoxExSelDT.Text == "出入库记录")
            {
                DataTable dt = _houseInOutRdBll.GetAllRecord();
                if(dt != null)
                {
                    _currentDt = dt;
                    this.dataGridViewX1.DataSource = _currentDt;
                }
            }
        }
    }
}
