using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ASRSDBME;
using ASRSDBBLL;
namespace ASRS
{
    public partial class wizardForm : Form
    {
        /// <summary>
        /// 仓储数据库接口对象
        /// </summary>
        private WarehouseStoreBLL _warestoreBll = new WarehouseStoreBLL();
        private ASRSModel _model = ASRSModel.GetInstance();
        #region 设置向导用户输入的数据
        private string _dbServerIP = "127.0.0.1";
        private string _dbUserName = "sa";
        private string _dbPwd = string.Empty;
        private int _layerCount = 4;
        private int _channelCount = 2;
        private int _columnCount = 50;
        public string dbServerIP
        {
            get
            {
                return _dbServerIP;
            }
            set
            {
                _dbServerIP = value;
            }
        }
        public string dbUserName
        {
            get
            {
                return _dbUserName;
            }
            set
            {
                _dbUserName = value;
            }
        }
        public string dbPwd
        {
            get
            {
                return _dbPwd;
            }
            set
            {
                _dbPwd = value;
            }
        }
        public int layerCount
        {
            get
            {
                return _layerCount;
            }
            set
            {
                _layerCount = value;
            }
        }
        public int columnCount
        {
            get
            {
                return _columnCount;
            }
            set
            {
                _columnCount = value;
            }
        }
        public int channelCount
        {
            get
            {
                return _channelCount;
            }
            set
            {
                _channelCount = value;
            }
        }
        #endregion
        public wizardForm()
        {
            InitializeComponent();
        }

        private void buttonXPrepage_Click(object sender, EventArgs e)
        {

        }

        private void buttonXNextPage_Click(object sender, EventArgs e)
        {

        }

        private void buttonXApply_Click(object sender, EventArgs e)
        {

        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {

        }

        private void wizard1_NextButtonClick(object sender, CancelEventArgs e)
        {
            
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            this._dbServerIP = this.ipAddressInput1.Value;
            this._dbUserName = this.textBoxDBUser.Text;
            this._dbPwd = this.textBoxXPwd.Text;
            this._layerCount = this.integerInputLayer.Value;
            this._channelCount = this.integerInputChannels.Value;
            this._columnCount = this.integerInputColumns.Value;
            labelWarehouseSetInfo.Text = "正在数据库中创建立库的仓位信息，请稍后...";
            WarehouseSetting oldWarehouseSet = _model.wareHouseSet.Clone() as WarehouseSetting;
            _model.wareHouseSet.channelCount = this._channelCount;
            _model.wareHouseSet.layerCount = this._layerCount;
            _model.wareHouseSet.columnCount = this._columnCount;

            int re = InitWarehouse();
            if(re == 1)
            {
                e.Cancel = true;
                _model.wareHouseSet = oldWarehouseSet;
                
            }
            else 
            {
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 初始化仓位
        /// </summary>
        /// <returns>0:成功初始化，1：取消当前操作</returns>
        private int InitWarehouse()
        {
            this.progressBarXHouseInit.Visible = true;
            if(_warestoreBll.GetCellCount() == 0)
            {
                for (int L = 1; L <= this._layerCount; L++)
                {
                    for (int R = 1; R <= this._channelCount * 2; R++)
                    {
                        for (int C = 1; C <= this._columnCount; C++)
                        {
                            WarehouseStoreME m = new WarehouseStoreME();
                            m.houseID = _model.HouseCoordConvertID(L, R, C);
                            m.houseLayerID = L;
                            m.houseRowID = R;
                            m.houseColumnID = C;
                            m.productID = string.Empty;
                            m.name = L.ToString() + "-" + R.ToString() + "-" + C.ToString();
                            m.useStatus = 1;
                            if(!this._warestoreBll.AddHousecell(m))
                            {
                                MessageBox.Show("初始化仓位数据库失败");
                                return 1;
                            }
                            int n = (L - 1) * this._channelCount * 2 * this._columnCount + (R - 1) * this._columnCount + C;
                            this.progressBarXHouseInit.Value = (int)(n * 100.0f / (this._layerCount * this._channelCount * 2 * this._columnCount));
                        }
                    }
                }
                return 0;
            }
            else if(_warestoreBll.GetCellCount()>0)
            {
                //立库仓位在数据库中有存储记录，给出提示
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult re = MessageBox.Show("是否保留现有记录","提示" ,buttons, icon);
                if(re == DialogResult.Yes)
                {
                    //保持现有的,仓位的ID号可能会变化
                    for (int L = 1; L <= this._layerCount; L++)
                    {
                        for (int R = 1; R <= this._channelCount* 2; R++)
                        {
                            for (int C = 1; C <= this._columnCount; C++)
                            {
                                WarehouseStoreME m = _warestoreBll.GetHouseCell(L, R, C);
                                if (m == null)
                                {
                                    //增加
                                    m = new WarehouseStoreME();
                                    m.houseLayerID = L;
                                    m.houseRowID = R;
                                    m.houseColumnID = C;
                                    m.houseID = _model.HouseCoordConvertID(L, R, C);
                                    m.useStatus = 1;
                                    m.name = L.ToString() + "-" + R.ToString() + "-" + C.ToString();
                                    m.productID = string.Empty;
                                    //增加之前先检查是否存在相同的ID，若存在则修改
                                    WarehouseStoreME oldM = _warestoreBll.GetHouseCell(m.houseID);
                                    if (oldM != null)
                                    {
                                        _warestoreBll.DeleteHouseCell(m.houseID);
                                        oldM.houseID = _model.HouseCoordConvertID(oldM.houseLayerID, oldM.houseRowID, oldM.houseColumnID);
                                        _warestoreBll.AddHousecell(oldM);
                                    }
                                    _warestoreBll.AddHousecell(m);
                                    
                                }
                                else
                                {
                                    //改变ID号
                                    int houseID = _model.HouseCoordConvertID(L, R, C);
                                    if (m.houseID != houseID)
                                    {
                                        _warestoreBll.DeleteHouseCell(m.houseID);
                                        m.houseID = houseID;
                                        _warestoreBll.AddHousecell(m);
                                    }
                                }
                                
                                int n = (L - 1) * this._channelCount * 2 * this._columnCount + (R - 1) * this._columnCount + C;
                                this.progressBarXHouseInit.Value = (int)(n * 100.0f / (this._layerCount * this._channelCount * 2 * this._columnCount));
                            }
                        }
                    }
                    return 0;
                }
                else if(re == DialogResult.No)
                {
                    //清理掉
                    _warestoreBll.ClearAllData();
                    for (int L = 1; L <= this._layerCount; L++)
                    {
                        for (int R = 1; R <= this._channelCount * 2; R++)
                        {
                            for (int C = 1; C <= this._columnCount; C++)
                            {
                                WarehouseStoreME m = new WarehouseStoreME();
                                m.houseID = _model.HouseCoordConvertID(L, R, C);
                                m.houseLayerID = L;
                                m.houseRowID = R;
                                m.houseColumnID = C;
                                m.productID = string.Empty;
                                m.name = L.ToString() + "-" + R.ToString() + "-" + C.ToString();
                                m.useStatus = 1;
                                this._warestoreBll.AddHousecell(m);
                                int n = (L - 1) * this._channelCount * 2 * this._columnCount + (R - 1) * this._columnCount + C;
                                this.progressBarXHouseInit.Value = (int)(n * 100.0f / (this._layerCount * this._channelCount * 2 * this._columnCount));
                            }
                        }
                    }
                    return 0;
                }
                else
                {
                    //取消
                    return 1;
                }
            }
            return 1;
        }

        private void wizard1_WizardPageChanging(object sender, DevComponents.DotNetBar.WizardCancelPageChangeEventArgs e)
        {
            if (e.OldPage == this.wizardPage2 && e.NewPage == this.wizardPage3)
            {
                //
                StringBuilder strBuilder = new StringBuilder();
                string IP = this.ipAddressInput1.Value;
                string UserName = this.textBoxDBUser.Text;
                string Pwd = this.textBoxXPwd.Text;
                strBuilder.AppendFormat("server={0};database=ASRSAccountDB;uid={1};pwd={2}",IP,UserName,Pwd);
                SqlConnection conn = new SqlConnection(strBuilder.ToString());
                try
                {
                    conn.Open();
                    //身份验证信息通过
                    this.labelDBConTip.Text ="身份验证信息通过,请点击“下一步”继续";
                    this.labelDBConTip.ForeColor = Color.BlueViolet;
                }
                catch (System.Exception ex)
                {
                	//身份验证信息错误
                    this.labelDBConTip.Text = "身份验证信息错误，请输入正确身份信息";
                    this.labelDBConTip.ForeColor = Color.Red;
                    //e.NewPage = e.OldPage;
                    e.Cancel = true;
                }
               
            }
        }

        private void wizardPage2_dbSetInput(object sender, EventArgs e)
        {
            this.labelDBConTip.Text = string.Empty;
        }
    }
}
