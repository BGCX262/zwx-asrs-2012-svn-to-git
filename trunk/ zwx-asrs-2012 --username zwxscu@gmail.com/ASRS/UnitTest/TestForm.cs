using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASRSDBBLL;
using ASRSDBME;
namespace ASRS
{
    public partial class TestForm : Form
    {
        private TaskBLL _taskBll = new TaskBLL();
        private InstBLL _instBll = new InstBLL();
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StringBuilder strBuilder = new StringBuilder();
            //string strVal = XMLConfigRW.ReadLatestTaskID();
            //strBuilder.AppendFormat("测试读上次保存的任务序号,结果:{0}\r\n",strVal);
            //this.richTextBox1.AppendText(strBuilder.ToString());

            //string newID = "134";
            //XMLConfigRW.WriteLatestTaskID(newID);
            //strBuilder.Clear();
            //strBuilder.AppendFormat("测试保存当前任务序号,结果:{0}\r\n", newID);
            //this.richTextBox1.AppendText(strBuilder.ToString());
            
            //strBuilder.Clear();
            //strVal = XMLConfigRW.ReadLatestTaskID();
            //strBuilder.AppendFormat("测试读上次保存的任务序号,结果:{0}\r\n", strVal);
            //this.richTextBox1.AppendText(strBuilder.ToString());

            //strBuilder.Clear();
            //int InstID = XMLConfigRW.ReadSavedInstIndex(1);
            //strBuilder.AppendFormat("测试读上次保存的指令序号,结果:1号小车：{0}\r\n", InstID);
            //this.richTextBox1.AppendText(strBuilder.ToString());
            //strBuilder.Clear();
            //InstID = XMLConfigRW.ReadSavedInstIndex(2);
            //strBuilder.AppendFormat("测试读上次保存的指令序号,结果:2号小车：{0}\r\n", InstID);
            //this.richTextBox1.AppendText(strBuilder.ToString());

            //int NewInstID = 5;
            //XMLConfigRW.SaveInstIndex(1, NewInstID);
            //XMLConfigRW.SaveInstIndex(2, NewInstID);
            //strBuilder.Clear();
            //strBuilder.AppendFormat("测试保存当前指令序号,结果:1号小车：{0},2号小车：{1}\r\n", NewInstID,NewInstID);
            //this.richTextBox1.AppendText(strBuilder.ToString());

            //strBuilder.Clear();
            //InstID = XMLConfigRW.ReadSavedInstIndex(1);
            //strBuilder.AppendFormat("测试读上次保存的指令序号,结果:1号小车：{0}\r\n", InstID);
            //this.richTextBox1.AppendText(strBuilder.ToString());
            //strBuilder.Clear();
            //InstID = XMLConfigRW.ReadSavedInstIndex(2);
            //strBuilder.AppendFormat("测试读上次保存的指令序号,结果:2号小车：{0}\r\n", InstID);
            //this.richTextBox1.AppendText(strBuilder.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TaskProductInhouse task = new TaskProductInhouse();
            task.productID = System.DateTime.Now.ToString();
            task.targetL = 2;
            task.targetR = 1;
            task.targetC = 30;
            string strXml = TaskSerializer.Serialize(task);
            this.richTextBox1.AppendText("对象串行化到xml字节流\r\n");
            this.richTextBox1.AppendText(strXml + "\r\n");

            BaseTaskInfo newTask = TaskSerializer.Deserialize((int)TaskCode.TASK_PRODUCT_INHOUSE, strXml);
            task = newTask as TaskProductInhouse;
            this.richTextBox1.AppendText("xnl字节流反串行化到对象\r\n");
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("创建时间:{0},修改时间:{1},产品id:{2},目标:层{3},行{4},列{5}\r\n",
                task.createTime, task.modifyTime, task.productID, task.targetL, task.targetR, task.targetC);
            this.richTextBox1.AppendText(strBuilder.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TaskProductInhouse task = new TaskProductInhouse();
            task.productID = "124314";
            task.targetL = 1;
            task.targetR = 2;
            task.targetC = 40;
            TaskME taskM = new TaskME();
            taskM.taskCode = (int)TaskCode.TASK_PRODUCT_INHOUSE;
            taskM.taskID = DateTime.Now.ToString();
            taskM.taskObj = TaskSerializer.Serialize(task);
            try
            {
                this.richTextBox1.AppendText(taskM.taskObj + "\r\n");
                _taskBll.AddTask(taskM);
            }
            catch (System.Exception e1)
            {
                this.richTextBox1.AppendText(e1.Message);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder strBuilder = new StringBuilder();
            //TaskExeStatus s = TaskExeStatus.TASK_NEW;
            //string TaskID = string.Empty;
            //XMLConfigRW.ReadMachineCurrentTask(1,out s,out TaskID);
            //strBuilder.AppendFormat("1号小车当前任务ID：{0}，任务状态:{1}\r\n", TaskID, ((int)s).ToString());
            //this.richTextBox1.AppendText(strBuilder.ToString());
            //strBuilder.Clear();
            //XMLConfigRW.SaveMachineCurrentTask(1, TaskExeStatus.TASK_COMPLETED, System.DateTime.Now.ToString("yyyyMMddHHmm"));
            //s = TaskExeStatus.TASK_RUN;
            //TaskID = string.Empty;
            //XMLConfigRW.ReadMachineCurrentTask(1, out s, out TaskID);
            //strBuilder.AppendFormat("1号小车当前任务ID：{0}，任务状态:{1}\r\n", TaskID, ((int)s).ToString());
            //this.richTextBox1.AppendText(strBuilder.ToString());

            string taskSerialNo = _taskBll.GenerateNewTaskSerialNo();
            BaseTaskInfo taskObj= new TaskProductInhouse();
            taskObj.taskID = taskSerialNo;
            TaskME newTask = new TaskME();
            newTask.taskCode = (int)taskObj.taskCode;
            newTask.taskID = taskSerialNo;
            newTask.taskObj = TaskSerializer.Serialize(taskObj);
            newTask.taskExeStatus = 0;
            _taskBll.AddTask(newTask);
            this.richTextBox1.AppendText("生成新的流水号:" + taskSerialNo+"\r\n");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TaskME taskM = _taskBll.GetFirstUnexeAutoTask();
            BaseTaskInfo taskObj = TaskSerializer.Deserialize(taskM.taskCode, taskM.taskObj);
            if(taskObj != null)
            {
                string strXML = TaskSerializer.Serialize(taskObj);
                this.richTextBox1.AppendText(strXML + "\r\n");
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("提取任务:");
                strBuilder.AppendLine(taskM.taskObj);
                this.richTextBox1.AppendText(strBuilder.ToString());
                InstMovL instMovl = new InstMovL();
                instMovl.targetL = 2;
                instMovl.instComment = "移动到2层";
                strXML = InstSerializer.Serialize(instMovl);
                this.richTextBox1.AppendText(strXML + "\r\n");
                IList<BaseInstInfo> instList = null;
                if(0 == TaskDisptach.ParseTask(true, true, ref taskObj, out instList))
                {
                    strBuilder.Clear();
                    strBuilder.AppendLine("任务分解成功");
                    this.richTextBox1.AppendText(strBuilder.ToString()+"\r\n");
                    strBuilder.Clear();
                    ASRSModel.GetInstance().transVehicleDic[taskObj.machineAllocated].FillInstList(instList, taskObj, taskObj.taskID);
                    foreach(BaseInstInfo inst in instList)
                    {
                        if(inst != null)
                        {
                            InstME instM = new InstME();
                            instM.instID = _instBll.GenerateNewInstID();
                            instM.instCode = (int)inst.instCode;
                            instM.instObj = InstSerializer.Serialize(inst);
                            try
                            {
                                BaseInstInfo newInst = InstSerializer.Deserialize(instM.instCode, instM.instObj);
                                richTextBox1.AppendText("指令反序列化：" + newInst.instComment + "\r\n");
                            }
                            catch (System.Exception e1)
                            {
                                richTextBox1.AppendText("反序列化失败:" + e1.Message);
                            }
  
                            instM.vehicleAlloc = taskObj.machineAllocated;
                            _instBll.AddInst(instM);
                            strBuilder.AppendLine("插入指令:");
                            strBuilder.AppendLine(instM.instObj);
                            this.richTextBox1.AppendText(strBuilder.ToString());
                            strBuilder.Clear();
                        }
                    }
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _instBll.ClearInstList(1);
            _instBll.ClearInstList(2);
        }
    }
}
