using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBME;
using ASRSDBFactory;
using ASRSDBIDAL;
using System.Data;
using System.Threading;
namespace ASRSDBBLL
{
    public class MessageBLL
    {
        private readonly IMessageDefineDAL _mesDefDAL = DALFactory.CreateMessageDefineDAL();
        private readonly IMessageRecordDAL _mesRecordDAL = DALFactory.CreateMessageRecordDAL();
        private readonly IMessageViewDAL _mesViewDAL = DALFactory.CreateMessageViewDAL();
        #region message define
        /// <summary>
        /// 得到消息定义表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMesDefine()
        {
            DataSet ds = _mesDefDAL.GetList(" ");
            if(ds != null && ds.Tables.Count>0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 根据消息ID获得消息定义
        /// </summary>
        /// <param name="mesID"></param>
        /// <returns></returns>
        public MessageDefineME GetMesDef(int mesID)
        {
            return _mesDefDAL.GetModel(mesID);
        }
        #endregion
        #region message record
        /// <summary>
        /// 增加一条消息记录
        /// </summary>
        /// <param name="mesID"></param>
        /// <param name="dynamicContent"></param>
        /// <returns></returns>
        public bool RecordMessage(int mesID,string dynamicContent)
        {
            if(_mesDefDAL.Exists(mesID))
            {
                MessageRecordME m = new MessageRecordME();
                m.messageID = mesID;
                m.happenTime = System.DateTime.Now;
                m.dynamicContent = dynamicContent;
                if(_mesRecordDAL.Exists(m.happenTime))
                {
                    Thread.Sleep(100);
                    m.happenTime = System.DateTime.Now;
                    return _mesRecordDAL.Add(m);
                }
                return _mesRecordDAL.Add(m);
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region message view
        #endregion
    }
}
