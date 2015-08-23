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
    /// <summary>
    /// 指令表业务层
    /// </summary>
    public class InstBLL
    {
        private readonly IInstDAL _instDAL = DALFactory.CreateInstDAL();

        /// <summary>
        /// 根据当前任务数据库生成新的指令ID流水号
        /// </summary>
        /// <returns></returns>
        public string GenerateNewInstID()
        {
            string nowMaxID = _instDAL.GetMaxInstID();
            string nowDt = System.DateTime.Now.ToString("yyyyMMddHHmm");
            string newInstID = string.Empty;
            if (nowMaxID.Substring(0, 12).Equals(nowDt.Substring(0, 12)))
            {
                int idPlus = Convert.ToInt32(nowMaxID.Substring(12)) + 1;
                newInstID = nowMaxID.Substring(0, 12) + Convert.ToString(idPlus).PadLeft(4, '0');
            }
            else
            {
                newInstID = nowDt + "0001";
            }
            return newInstID;
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instModel"></param>
        /// <returns></returns>
        public bool AddInst(InstME instModel)
        {
            return _instDAL.Add(instModel);
        }

        /// <summary>
        /// 查询指令
        /// </summary>
        /// <param name="instID"></param>
        /// <returns></returns>
        public InstME GetInst(string instID)
        {
            return _instDAL.GetModel(instID);
        }

        /// <summary>
        /// 得到分配给指定小车的所有指令
        /// </summary>
        /// <param name="vehicleNo">小车编号</param>
        /// <returns></returns>
        public IList<InstME> GetInstList(int vehicleNo)
        {
            List<InstME> instList = new List<InstME>();
            StringBuilder  strSql = new StringBuilder();
            strSql.AppendFormat(@"vehicleAlloc = {0} ",vehicleNo);
            DataSet ds = _instDAL.GetList(strSql.ToString());
            if(ds.Tables.Count>0)
            {
                foreach(DataRow rw in ds.Tables[0].Rows)
                {
                    InstME inst = new InstME();
                    inst.instID = rw["instID"].ToString();
                    inst.instCode = int.Parse(rw["instCode"].ToString());
                    inst.vehicleAlloc = int.Parse(rw["vehicleAlloc"].ToString());
                    inst.instObj = rw["instObj"].ToString();
                    instList.Add(inst);
                }
            }
            return instList;
        }
        /// <summary>
        /// 删除指令
        /// </summary>
        /// <param name="instID"></param>
        /// <returns></returns>
        public bool DeleteInst(string instID)
        {
            return _instDAL.Delete(instID);
        }

        /// <summary>
        /// 清掉某小车的所有指令
        /// </summary>
        /// <param name="vehicleNo">小车编号，从1开始编号</param>
        /// <returns></returns>
        public bool ClearInstList(int vehicleNo)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" vehicleAlloc= {0}", vehicleNo);
            return _instDAL.ConditionedDelete(strWhere.ToString());
        }

        /// <summary>
        /// 获取小车的指令列表
        /// </summary>
        /// <param name="vehicleIndex"></param>
        /// <returns></returns>
        public DataTable GetListByVehicleNo(int vehicleIndex)
        {
            string strWhere = string.Format(" vehicleAlloc ={0}",vehicleIndex);
            DataSet ds = _instDAL.GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }
    }
}
