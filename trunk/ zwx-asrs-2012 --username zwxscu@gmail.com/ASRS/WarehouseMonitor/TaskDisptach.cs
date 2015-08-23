using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ASRSDBBLL;
using ASRSDBME;
namespace ASRS
{
    /// <summary>
    /// 任务调度类，封装了出入库调度策略、算法方面的细节
    /// </summary>
    public static class TaskDisptach
    {
        /// <summary>
        /// 全局数据对象，单例模式
        /// </summary>
        private static ASRSModel _Model = ASRSModel.GetInstance();

        /// <summary>
        /// 解析任务，行成指令列表,同时分派资源
        /// </summary>
        /// <param name="task"></param>
        /// <param name ="VehicleAllocRequire">是否需要分配小车资源</param>
        /// <param name="HouseAllocRequire">是否需要分配仓位资源</param>
        /// <returns>0:成功解析任务，任务索引向前移动一条,1:执行条件不满足，任务索引停在当前，2：执行条件不满足，任务索引向前移动一条。 /// </returns>
        public static int ParseTask(bool VehicleAllocRequire, bool HouseAllocRequire,ref BaseTaskInfo task,out IList<BaseInstInfo> instList)
        {
            instList = null;
            TransVehicle m1 = _Model.transVehicleDic[1];
            TransVehicle m2 = _Model.transVehicleDic[2];
            if(task.taskCode == TaskCode.TASK_EMPTY)
            {
                //空任务
                return 0;
            }
            else if(task.taskCode == TaskCode.TASK_PRODUCT_INHOUSE)
            {
               
                TaskProductInhouse realTask= (TaskProductInhouse)task;
                //分配小车
                if(VehicleAllocRequire)
                {
                    int machineNo = ProductHouseInMachineSelect(realTask.productID);
                    if (machineNo <= 0)
                    {
                        //所有小车都在忙，
                        return 1;
                    }
                    realTask.machineAllocated = machineNo;
                }
                //分配仓位
               if(HouseAllocRequire)
               {
                   int L = 0, R = 0, C = 0;
                   if(!ChooseEmptyHouse(realTask.machineAllocated,out L,out R,out C))
                   {
                       return 2;
                   }
                   realTask.targetL = L;
                   realTask.targetR = R;
                   realTask.targetC = C;
               }
                //分解任务，生成指令列表
                instList = new List<BaseInstInfo>();
                //开始
                InstBegin inst0 = new InstBegin();
                inst0.instComment = "开始";
                instList.Add(inst0);
                //1 从当前位置到取货口
                InstMovL inst1 = new InstMovL();
                inst1.targetL = 1;
                inst1.instComment = "移动到目标层";
                instList.Add(inst1);
                //2
                InstMovC inst2 = new InstMovC();
                inst2.targetC = 1;
                inst2.instComment = "移动取货口";
                instList.Add(inst2);
                //3 取货
                InstLoad inst3 = new InstLoad();
                inst3.instComment = "取货";
                instList.Add(inst3);
                //4 到目标仓位
                InstMovC inst4 = new InstMovC();
                inst4.targetC = realTask.targetC;
                inst4.instComment = "移动到目标列";
                instList.Add(inst4);
                //5
                InstMovL inst5 = new InstMovL();
                inst5.targetL = realTask.targetL;
                inst5.instComment = "移动到目标层";
                instList.Add(inst5);
                //卸货
                InstUnload inst6 = new InstUnload();
                inst6.instComment = "仓位放货";
                instList.Add(inst6);

                //结束指令
                InstEnd inst7 = new InstEnd();
                inst7.instComment = "结束";
                instList.Add(inst7);
                return 0;
            }
            else if(task.taskCode == TaskCode.TASK_PRODUCT_OUTHOUSE)
            {
                //出库任务
                TaskProductOuthouse poutTask = task as TaskProductOuthouse;
                if(poutTask == null)
                {
                    return 2;
                }
                //自动分配仓位
                int L=0,R=0,C=0;
                if (HouseAllocRequire)
                {
                    //if (!_Model.warestoreBll.GetHouseCell(poutTask.productType, out L, out R, out C))
                    int reChooseHouse = ChooseProductoutHouse(poutTask.productType, out L, out R, out C);
                    if(reChooseHouse  != 0)
                    {
                        return 2;
                    }
                    task.targetL = L;
                    task.targetR = R;
                    task.targetC = C;
                }
                //自动分配小车
                TransVehicle selectedM = null;
                if (VehicleAllocRequire)
                {
                    if (R > 0 && R < 3)
                    {
                        if (m1.MStatus != MachineWorkStatus.MACHINE_IDLE)
                            return 2;
                        task.machineAllocated = 1;
                    }
                    else if (R >= 3 && R < 5)
                    {
                        if (m2.MStatus != MachineWorkStatus.MACHINE_IDLE)
                            return 2;
                        task.machineAllocated = 2;
                    }
                    else
                        return 2;
                }
                selectedM = _Model.transVehicleDic[task.machineAllocated];
                if(selectedM.MStatus != MachineWorkStatus.MACHINE_IDLE)
                {
                    return 2;
                }
                //指令分解
                //分解任务，生成指令列表
                instList = new List<BaseInstInfo>();
                //开始
                InstBegin inst0 = new InstBegin();
                inst0.instComment = "开始";
                instList.Add(inst0);

                //1 从当前位置到目标仓位层
                InstMovL inst1 = new InstMovL();
                inst1.targetL = task.targetL;
                inst1.instComment = "移动到出库仓位所在的层";
                instList.Add(inst1);

                //2 到目标仓位所在列
                InstMovC inst2 = new InstMovC();
                inst2.targetC = task.targetC;
                inst2.instComment = "移动到出库仓位所在的列";
                instList.Add(inst2);

                //3取货
                InstLoad inst3 = new InstLoad();
                inst3.instComment = "取货";
                instList.Add(inst3);

                //4 移动到1层
                InstMovL inst4 = new InstMovL();
                inst4.targetL = 1;
                inst4.instComment = "移动到出库口所在的层";
                instList.Add(inst4);

                //5 移动到出库口所在列
                InstMovC inst5 = new InstMovC();
                inst5.targetC = _Model.wareHouseSet.columnCount;
                inst5.instComment = "移动到出库口所在列";
                instList.Add(inst5);

                //6 卸货
                InstUnload inst6 = new InstUnload();
                inst6.instComment = "卸货";
                instList.Add(inst6);

                //7 结束
                InstEnd inst7 = new InstEnd();
                inst7.instComment = "结束";
                instList.Add(inst7);

                return 0;
            }
            else if(task.taskCode == TaskCode.TASK_PALLETE_INHOUSE)
            {
                //空板入库任务
                return 0;
            }
            else if(task.taskCode == TaskCode.TASK_PALLETE_OUTHOUSE)
            {
                //空板出库任务
                return 0;
            }
            else
            {
                //不可识别的任务码
                return 0;
            }
 
        }

        /// <summary>
        /// 入库任务选择小车
        /// </summary>
        /// <returns>若该产品ID不存在，返回 -1，若无小车可用，返回-2</returns>
        private static int ProductHouseInMachineSelect(string ProductID)
        {
            TransVehicle m1 = _Model.transVehicleDic[1];
            TransVehicle m2 = _Model.transVehicleDic[2];
         
            int machineNo = -1;
            ProductStoreME ProductM = _Model.productBll.GetProductStore(ProductID);
            if (ProductM == null)
            {
                //产品不存在
                return -1;
            }
            int StoreM1 = _Model.warestoreBll.GetProductNum(1, ProductM.productType) + _Model.warestoreBll.GetProductNum(2, ProductM.productType);
            int StoreM2 = _Model.warestoreBll.GetProductNum(3, ProductM.productType) + _Model.warestoreBll.GetProductNum(4, ProductM.productType);

            if (StoreM1 <= StoreM2)
            {
                if (m1.NewTaskAccepted())
                {
                    machineNo = 1;
                }
                else if (m2.NewTaskAccepted())
                {
                    machineNo = 2;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                if (m2.NewTaskAccepted())
                {
                    machineNo = 2;
                }
                else if (m1.NewTaskAccepted())
                {
                    machineNo = 1;
                }
                else
                {
                    return -2;
                }
            }
            return machineNo;
        }

       /// <summary>
       /// 选择可用仓位
       /// </summary>
       /// <param name="machineNo">小车编号</param>
       /// <param name="layerNo">out:层号</param>
       /// <param name="rowNo">out:行号</param>
       /// <param name="colNo">out:列号</param>
       /// <returns></returns>
       private static bool ChooseEmptyHouse(int machineNo,out int  layerNo,out int rowNo,out int colNo)
        {
            layerNo = 0;
            rowNo = 0;
            colNo = 0;
            if (machineNo == 1)
            {
                for (int L = 1; L <= _Model.wareHouseSet.layerCount; L++)
                {
                    for (int C = 1; C <= _Model.wareHouseSet.columnCount; C++)
                    {
                        for (int R = 1; R <= 2; R++)
                        {
                            WarehouseStatus s = _Model.warestoreBll.GetHousecellStatus(L,R,C);
                            if (s == WarehouseStatus.HOUSE_EMPTY)
                            {
                                // Console.WriteLine();
                                layerNo = L;
                                rowNo = R;
                                colNo = C;
                                return true;
                            }
                        }
                    }
                }
                //无空位
                return false;
            }
            else if (machineNo == 2)
            {

                for (int L = 1; L <= _Model.wareHouseSet.layerCount; L++)
                {
                    for (int C = 1; C <= _Model.wareHouseSet.columnCount; C++)
                    {
                        for (int R = 3; R <= 4; R++)
                        {
                            WarehouseStatus s = _Model.warestoreBll.GetHousecellStatus(L, R, C);
                            if (s == WarehouseStatus.HOUSE_EMPTY)
                            {
                                // Console.WriteLine();
                                layerNo = L;
                                rowNo = R;
                                colNo = C;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 选择产品出库的可用仓位
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="L"></param>
        /// <param name="R"></param>
        /// <param name="C"></param>
        /// <returns>0：成功选择仓位，1：该型号产品无存储，2：无空闲小车，3：无满足工艺要求的仓位</returns>
        private static int ChooseProductoutHouse(string productType,out int L,out int R,out int C)
        {
            L = 0;
            R = 0;
            C = 0;
            DataTable dt =_Model.warestoreBll.GetHouseListByProducttype(productType);
            if(dt == null)
            {
                return 1;
            }
            try
            {
                 foreach(DataRow dr in dt.Rows)
                {
                    int r = int.Parse(dr["houseRowID"].ToString());
                    if(r>0 && r<3)
                    {
                        if(_Model.transVehicleDic[1].MStatus == MachineWorkStatus.MACHINE_IDLE)
                        {
                            int l = int.Parse(dr["houseLayerID"].ToString());
                            int c = int.Parse(dr["houseColumnID"].ToString());
                            L = l;
                            R = r;
                            C = c;
                            return 0;
                        }
                    }
                    else if(r>=3 && r<5)
                    {
                        if (_Model.transVehicleDic[2].MStatus == MachineWorkStatus.MACHINE_IDLE)
                        {
                            int l = int.Parse(dr["houseLayerID"].ToString());
                            int c = int.Parse(dr["houseColumnID"].ToString());
                            L = l;
                            R = r;
                            C = c;
                            return 0;
                        }
                    }
                }
                 return 2;
            }
            catch (System.Exception e)
            {
                return 1;
            }
           
        }
    }
}
