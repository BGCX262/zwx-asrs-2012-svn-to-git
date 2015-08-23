using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using ASRSDBFactory;
using ASRSDBIDAL;
using System.Data;
namespace ASRSDBBLL
{
    public class HouseInOutBll
    {
        private readonly IInHouseRecordDAL _inhouseRecordDAL = DALFactory.CreateInHouseDAL();
        private readonly IOutHouseRecordDAL _outhouseRecordDAL = DALFactory.CreateOutHouseDAL();
        private readonly IHouseInOutViewDAL _houseInOutViewDAL = DALFactory.CreateHouseInOutViewDAL();
#region 出入库记录view
        public DataTable GetAllRecord()
        {
            DataSet ds = _houseInOutViewDAL.GetList(" ");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }
        public bool AddProductInRecord(InHouseRecordME m)
        {
            return _inhouseRecordDAL.Add(m);
        }

        public bool AddProductOutRecord(OutHouseRecordME m)
        {
            return _outhouseRecordDAL.Add(m);
        }
#endregion
    }
}
