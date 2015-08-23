using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.Windows.Forms;
namespace ASRS
{
    #region 枚举定义

    /// <summary>
    /// 任务执行状态
    /// </summary>
     [Serializable]
    public enum TaskExeStatus
    {
        TASK_NEW = 0, //新建，未指派资源
        TASK_RUN, //已指派资源，正在运行
        TASK_COMPLETED //任务已完成
    }
    /// <summary>
    /// 任务码枚举
    /// </summary>
     [Serializable]
    public enum TaskCode
    {
        TASK_EMPTY = 0,
        TASK_PRODUCT_INHOUSE,
        TASK_PRODUCT_OUTHOUSE,
        TASK_PALLETE_INHOUSE,
        TASK_PALLETE_OUTHOUSE
    }

    /// <summary>
    /// 指令码枚举
    /// </summary>
    public enum InstCode
    {
        INST_EMPTY = 0, //无效的空指令
        INST_BEGIN, //开始
        INST_MOVL, //上升/下降，到指定层
        INST_MOVC, //运动到目标列
        INST_LOAD, //装载
        INST_UNLOAD, //卸载

        INST_END //结束
    }
    #endregion
    #region 任务定义
    /// <summary>
    /// 基类任务信息
    /// </summary>
     [Serializable]
    public abstract class BaseTaskInfo
    {
         protected string timeFormat = "yyyyMMddHHmmss";

         /// <summary>
         /// 是否手动分配小车
         /// </summary>
         protected bool _manualAllocedTM = false;
         public bool manualAllocedTM
         {
             get
             {
                 return _manualAllocedTM;
             }
             set
             {
                 _manualAllocedTM = value;
             }
         }

         /// <summary>
         /// 是否手动分配仓位
         /// </summary>
         protected bool _manualAllocedHouse = false;
         public bool manualAllocedHouse
         {
             get
             {
                 return _manualAllocedHouse;
             }
             set
             {
                 _manualAllocedHouse = value;
             }
         }
        /// <summary>
        /// 任务码
        /// </summary>
        protected TaskCode _taskCode = TaskCode.TASK_EMPTY;
         [XmlIgnore] 
        public TaskCode taskCode
        {
            get
            {
                return _taskCode;
            }
        }

        /// <summary>
        /// 任务流水号
        /// </summary>
        protected string _taskID = string.Empty;
        public string taskID
        {
            get
            {
                return _taskID;
            }
            set
            {
                _taskID = value;
            }
        }
        
         /// <summary>
        /// 创建时间
        /// </summary>
        protected string _createTime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        public string createTime
        {
            get
            {
                return _createTime;
            }
        }

        /// <summary>
        /// 最新修改时间
        /// </summary>
        protected string _modifyTime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        public string modifyTime
        {
            get
            {
                return _modifyTime;
            }
        }

         /// <summary>
         /// 该任务分配的小车编号
         /// </summary>
        protected int _machineAllocated = 0;
        public int machineAllocated
        {
            get
            {
                return _machineAllocated;
            }
            set
            {
                if(value != _machineAllocated)
                {
                    _machineAllocated = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
            }
        }

        /// <summary>
        /// 目标仓位层号
        /// </summary>
        private int _targetL = 1;
        public int targetL
        {
            get
            {
                return _targetL;
            }
            set
            {
                if (value != _targetL)
                {
                    _targetL = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
            }
        }
        /// <summary>
        /// 目标仓位行号
        /// </summary>
        private int _targetR = 1;
        public int targetR
        {
            get
            {
                return _targetR;
            }
            set
            {
                if (value != _targetR)
                {
                    _targetR = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
            }
        }

        /// <summary>
        /// 目标仓位列号
        /// </summary>
        private int _targetC = 1;
        public int targetC
        {
            get
            {
                return _targetC;
            }
            set
            {
                if (value != _targetC)
                {
                    _targetC = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
            }
        }

        public BaseTaskInfo()
        {
            _createTime = System.DateTime.Now.ToString(timeFormat);
            _modifyTime = _createTime;
        }
    }

    /// <summary>
    /// 产品入库任务
    /// </summary>
    [Serializable]
    public class TaskProductInhouse : BaseTaskInfo
    {
        /// <summary>
        /// 入库任务的产品ID
        /// </summary>
        private string _productID = string.Empty;
        public string productID
        {
            get
            {
                return _productID;
            }
            set
            {
                if (_productID != value)
                {
                    _productID = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }

            }
        }

        public TaskProductInhouse()
        {
            _taskCode = TaskCode.TASK_PRODUCT_INHOUSE;
        }
    }

    /// <summary>
    /// 产品出库任务
    /// </summary>
    [Serializable]
    public class TaskProductOuthouse : BaseTaskInfo
    {
        private string timeFormat = "yyyyMMddHHmmss";
        /// <summary>
        /// 出库任务的产品ID
        /// </summary>
        private string _productID=string.Empty;
        public string productID
        {
            get
            {
                return _productID;
            }
            set
            {
                if (_productID != value)
                {
                    _productID = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }

            }
        }
        private string _productType = string.Empty;
        public string productType
        {
            get
            {
                return _productType;
            }
            set
            {
                _productType = value;
            }
        }
        public TaskProductOuthouse()
        {
            _taskCode = TaskCode.TASK_PRODUCT_OUTHOUSE;
        }
    }

    /// <summary>
    /// 空板入库任务
    /// </summary>
    /// 
    [Serializable]
    public class TaskPalleteInhouse : BaseTaskInfo
    {
        public TaskPalleteInhouse()
        {
            _taskCode = TaskCode.TASK_PALLETE_INHOUSE;
        }
    }

    /// <summary>
    /// 空板出库任务
    /// </summary>
    /// 
    [Serializable]
    public class TaskPalleteOuthouse : BaseTaskInfo
    {
        public TaskPalleteOuthouse()
        {
            _taskCode = TaskCode.TASK_PALLETE_OUTHOUSE;
        }
    }
    #endregion
    #region 指令定义
    /// <summary>
    /// 基类指令信息
    /// </summary>
    /// 
  
    public abstract class BaseInstInfo
    {
        protected string timeFormat = "G";
        /// <summary>
        /// 指令码
        /// </summary>
        protected InstCode _instCode;
         [XmlIgnore] 
        public InstCode instCode
        {
            get
            {
                return _instCode;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        protected string _createTime;
        public string createTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        /// <summary>
        /// 最新修改时间
        /// </summary>
        protected string _modifyTime;
        public string modifyTime
        {
            get
            {
                return _modifyTime;
            }
            set
            {
                _modifyTime = value;
            }
        }

        /// <summary>
        /// 指令注释
        /// </summary>
        protected string _instComment = string.Empty;
        public string instComment
        {
            get
            {
                return _instComment;
            }
            set
            { 
                _instComment = value;
            }
        }

        /// <summary>
        /// 指令抽象类
        /// </summary>
        public BaseInstInfo()
        {
            _createTime = System.DateTime.Now.ToString(timeFormat);
            _modifyTime = _createTime;
        }
    }

    /// <summary>
    /// 层方向移动指令
    /// </summary>
    [Serializable]
    public class InstMovL : BaseInstInfo
    {
        private int _targetL = 0;
        public int targetL
        {
            get
            {
                return _targetL;
            }
            set
            {
                if (_targetL != value)
                {
                    _targetL = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
              
                
            }
        }
        public InstMovL()
        {
            _instCode = InstCode.INST_MOVL;
        }

    }

    /// <summary>
    /// 列方向移动指令
    /// </summary>
    [Serializable]
    public class InstMovC : BaseInstInfo
    {
        /// <summary>
        /// 目标列
        /// </summary>
        private int _targetC = 0;
        public int targetC
        {
            get
            {
                return _targetC;
            }
            set
            {
                if (_targetC != value)
                {
                    _targetC = value;
                    _modifyTime = System.DateTime.Now.ToString(timeFormat);
                }
            }
        }
        public InstMovC()
        {
            _instCode = InstCode.INST_MOVC;
        }
    }

    /// <summary>
    /// 装载指令
    /// </summary>
    [Serializable]
    public class InstLoad : BaseInstInfo
    {
        public InstLoad()
        {
            _instCode = InstCode.INST_LOAD;
        }
    }
    [Serializable]
    public class InstUnload : BaseInstInfo
    {
        public InstUnload()
        {
            _instCode = InstCode.INST_UNLOAD;
        }
    }
    [Serializable]
    public class InstBegin : BaseInstInfo
    {
        public InstBegin()
        {
            _instCode = InstCode.INST_BEGIN;
        }
    }
    [Serializable]
    public class InstEnd : BaseInstInfo
    {
        public InstEnd()
        {
            _instCode = InstCode.INST_END;
        }
    }
    #endregion

    #region 任务、指令的序列化功能
        /// <summary>
    /// 任务对象的序列化器
    /// </summary>
    /// 
    public static class TaskSerializer
    {
        /// <summary>
        /// 序列化任务到xml字符串
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string Serialize(BaseTaskInfo task)
        {
            string xmlStr = string.Empty;
            XmlSerializer serializer = new XmlSerializer(task.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, task);
            byte[] buffer = stream.GetBuffer();
            xmlStr = Encoding.UTF8.GetString(buffer);
            //switch(task.taskCode)
            //{
            //    case TaskCode.TASK_EMPTY:
            //        {
            //            return string.Empty;
            //        }
            //    case TaskCode.TASK_PRODUCT_INHOUSE:
            //        {
                        
            //            break;
            //        }
            //    case TaskCode.TASK_PRODUCT_OUTHOUSE:
            //        {
                       
            //            break;
            //        }
            //    case TaskCode.TASK_PALLETE_INHOUSE:
            //        {
            //            break;
            //        }
            //    case TaskCode.TASK_PALLETE_OUTHOUSE:
            //        {
            //            break;
            //        }
            //    default:
            //        break;
            //}
            return xmlStr;
        }

        /// <summary>
        /// 反序列化xml字符串到任务对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
         public static BaseTaskInfo Deserialize(int taskCode,string xml)
         {
             //将XML反序列化为ClientInfo对象
             BaseTaskInfo task = null;
             switch (taskCode)
             {
                 case (int)TaskCode.TASK_EMPTY:
                     {
                         task = null;
                         break;
                     }
                 case (int)TaskCode.TASK_PRODUCT_INHOUSE:
                     {
                         byte[] buffer = Encoding.UTF8.GetBytes(xml);
                         MemoryStream stream = new MemoryStream(buffer);
                         XmlSerializer serializer = new XmlSerializer(typeof(TaskProductInhouse));
                         TaskProductInhouse realTask =serializer.Deserialize(stream) as  TaskProductInhouse;
                         stream.Close();
                         task = realTask; 
                         break;
                     }
                 case (int)TaskCode.TASK_PRODUCT_OUTHOUSE:
                     {
                         byte[] buffer = Encoding.UTF8.GetBytes(xml);
                         MemoryStream stream = new MemoryStream(buffer);
                         XmlSerializer serializer = new XmlSerializer(typeof(TaskProductOuthouse));
                         TaskProductOuthouse realTask = serializer.Deserialize(stream) as  TaskProductOuthouse;
                         stream.Close();
                         task = realTask; 
                         break;
                     }
                 case (int)TaskCode.TASK_PALLETE_INHOUSE:
                     {
                         byte[] buffer = Encoding.UTF8.GetBytes(xml);
                         MemoryStream stream = new MemoryStream(buffer);
                         XmlSerializer serializer = new XmlSerializer(typeof(TaskPalleteInhouse));
                         TaskPalleteInhouse realTask = serializer.Deserialize(stream) as TaskPalleteInhouse;
                         stream.Close();
                         task = realTask; 
                         break;
                     }
                 case (int)TaskCode.TASK_PALLETE_OUTHOUSE:
                     {
                         byte[] buffer = Encoding.UTF8.GetBytes(xml);
                         MemoryStream stream = new MemoryStream(buffer);
                         XmlSerializer serializer = new XmlSerializer(typeof(TaskPalleteOuthouse));
                         TaskPalleteOuthouse realTask = serializer.Deserialize(stream) as TaskPalleteOuthouse;
                         stream.Close();
                         task = realTask; 
                         break;
                     }
                 default:
                     break;
             }
             return task;
         }
    }

    public static class InstSerializer
    {
         public static string Serialize(BaseInstInfo inst)
         {
             string xmlStr = string.Empty;
             Type objType = inst.GetType();
             string strType = objType.ToString();
             

             XmlSerializer serializer = new XmlSerializer(objType);
             MemoryStream stream = new MemoryStream();
           //  StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);  
             serializer.Serialize(stream,inst);
             byte[] buffer = stream.GetBuffer();
             xmlStr = Encoding.UTF8.GetString(buffer);
             return xmlStr;
         }
         public static BaseInstInfo Deserialize(int instCode,string xml)
         {
             BaseInstInfo inst = null;
              switch (instCode)
              {
                  case (int)InstCode.INST_BEGIN:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstBegin));
                          InstBegin realInst = serializer.Deserialize(stream) as InstBegin;
                          stream.Close();
                          inst = realInst;
                          break;
                      }
                  case (int)InstCode.INST_END:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstEnd));
                          InstEnd realInst = serializer.Deserialize(stream) as InstEnd;
                          stream.Close();
                          inst = realInst; 
                          break;
                      }
                  case (int)InstCode.INST_EMPTY:
                      {
                          inst = null;
                          break;
                      }
                  case (int)InstCode.INST_LOAD:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstLoad));
                          InstLoad realInst = serializer.Deserialize(stream) as InstLoad;
                          stream.Close();
                          inst = realInst; 
                          break;
                      }
                  case (int)InstCode.INST_UNLOAD:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstUnload));
                          InstUnload realInst = serializer.Deserialize(stream) as InstUnload;
                          stream.Close();
                          inst = realInst; 
                          break;
                      }
                  case (int)InstCode.INST_MOVL:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstMovL));
                          InstMovL realInst = serializer.Deserialize(stream) as InstMovL;
                          stream.Close();
                          inst = realInst; 
                          break;
                      }
                  case (int)InstCode.INST_MOVC:
                      {
                          byte[] buffer = Encoding.UTF8.GetBytes(xml);
                          MemoryStream stream = new MemoryStream(buffer);
                          XmlSerializer serializer = new XmlSerializer(typeof(InstMovC));
                          InstMovC realInst = serializer.Deserialize(stream) as InstMovC;
                          stream.Close();
                          inst = realInst; 
                          break;
                      }
                  default:
                      break;
              }
              return inst;
         }
    }
    #endregion
}
