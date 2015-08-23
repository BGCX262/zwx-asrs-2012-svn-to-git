using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASRSDBFactory;
using ASRSDBIDAL;

namespace ASRS
{
    public partial class DBManageForm : Form
    {
#region 数据库接口对象
        private readonly IInHouseRecordDAL _inhouseRecordDAL = DALFactory.CreateInHouseDAL();
        private readonly IOutHouseRecordDAL _outhouseRecordDAL = DALFactory.CreateOutHouseDAL();
        private readonly IHouseInOutViewDAL _houseInOutViewDAL = DALFactory.CreateHouseInOutViewDAL();
        private readonly IInstDAL _instDAL = DALFactory.CreateInstDAL();
        private readonly IMessageDefineDAL _mesDefDAL = DALFactory.CreateMessageDefineDAL();
        private readonly IMessageRecordDAL _mesRecordDAL = DALFactory.CreateMessageRecordDAL();
        private readonly IMessageViewDAL _mesViewDAL = DALFactory.CreateMessageViewDAL();
        private readonly IProductStoreDAL _productStoreDAL = DALFactory.CreateProductStoreDAL();
        private readonly IProductCategoryDAL _productCategoryDAL = DALFactory.CreateProductCategoryDAL();
        private readonly ITaskDAL _taskDAL = DALFactory.CreateTaskDAL();
        private readonly IWarehouseStoreDAL _warehouseStoreDAL= DALFactory.CreateWarehouseStoreDAL();
        private readonly IWareProductViewDAL _wpViewDal = DALFactory.CreateWareProductViewDAL();
        private DataTable _currentDt;
#endregion
        public DBManageForm()
        {
            InitializeComponent();
        }

        private void buttonXQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXRefresh_Click(object sender, EventArgs e)
        {

        }

        private void buttonXApply_Click(object sender, EventArgs e)
        {

        }

        private void DBManageForm_Load(object sender, EventArgs e)
        {
            this.comboBoxEx1.SelectedIndex = 0;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.comboBoxEx1.Text)
            {
                case "入库记录表":
                    {
                        DataSet ds = _inhouseRecordDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "出库记录表":
                    {
                        DataSet ds = _outhouseRecordDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "报警提示信息定义数据表":
                    {
                        DataSet ds = _mesDefDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "报警提示消息记录表":
                    {
                        DataSet ds = _mesRecordDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "产品类型数据表":
                    {
                        DataSet ds = _productCategoryDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "立库产品数据表":
                    {
                        DataSet ds = _warehouseStoreDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "出入库记录视图表":
                    {
                        DataSet ds = _houseInOutViewDAL.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                case "立库产品存储视图表":
                    {
                        DataSet ds = _wpViewDal.GetList(" ");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            _currentDt = ds.Tables[0];
                        }
                        break;
                    }
                default:
                    break;
            }
            if(_currentDt != null)
            {
                this.dataGridViewX1.DataSource = _currentDt;
            }
        }
    }
}
