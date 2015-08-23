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
    public partial class SettingForm : Form
    {
        ASRSModel _model = ASRSModel.GetInstance();
        public SettingForm()
        {
            InitializeComponent();
        }

        private void buttonItemCom_Click(object sender, EventArgs e)
        {
            if(_model != null)
            {
                DBComDevSetting cloneComSet = _model.dbComDevSet.Clone() as DBComDevSetting;
                this.advPropertyGrid1.SelectedObject = cloneComSet;
            }
        }

        private void buttonItemWarehouse_Click(object sender, EventArgs e)
        {
            if(_model != null)
            {
                WarehouseSetting warehouseSet = _model.wareHouseSet.Clone() as WarehouseSetting;
                this.advPropertyGrid1.SelectedObject = warehouseSet;
            }
        }

        private void buttonItemGeneral_Click(object sender, EventArgs e)
        {
              if(_model != null)
              {
                  GeneralSetting generalSet = _model.generalSet.Clone() as GeneralSetting;
                  this.advPropertyGrid1.SelectedObject = generalSet;
              }
        }

        private void buttonXApply_Click(object sender, EventArgs e)
        {

        }

        private void buttonXCancel_Click(object sender, EventArgs e)
        {

        }

        private void buttonXQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
