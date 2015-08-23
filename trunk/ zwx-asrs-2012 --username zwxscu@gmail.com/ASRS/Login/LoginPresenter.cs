using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ASRSDBBLL;
using ASRSDBME;
namespace ASRS
{
    public enum LoginMesIDEnum
    {
        ERROR_GETUSERLIST = 1, //获得用户列表异常
        WARNING_LOGFAILED, //登陆失败
        INFO_LOGOK,  //登陆成功

    }
    class LoginPresenter:Presenter<ILoginView>
    {
        /// <summary>
        /// 用户级名称词典
        /// </summary>
        private Dictionary<int, string> _accountRolenameDic = new Dictionary<int, string>();
        /// <summary>
        /// 后台加载线程
        /// </summary>
        private BackgroundWorker bgwLoad;
        private DataTable _mesDefTable;
        private AccountBLL _accountBll = new AccountBLL();
        public ASRSModel _Model { get; set; }
        public LoginPresenter(ILoginView view):base(view)
        {
            _Model = ASRSModel.GetInstance();
            _mesDefTable = _Model.mesBll.GetMesDefine();
            _accountRolenameDic[(int)AccountRole.OPERATOR] = "操作员";
            _accountRolenameDic[(int)AccountRole.ADMINITOR] = "管理员";
            _accountRolenameDic[(int)AccountRole.SYSTEM_MANAGER] = "系统维护员";
        }
        
        #region Login view 事件响应函数
        private void LoadEventHandler(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 改变用户级事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleChangeEventHandler(object sender,LoginEventArg e)
       {

           IList<string> strUserList = null;
           try
           {
               switch (e.CurRole)
               {
                   case AccountRole.OPERATOR:
                       {
                           strUserList = _accountBll.GetUserList(1);
                           break;
                       }
                   case AccountRole.ADMINITOR:
                       {
                           strUserList = _accountBll.GetUserList(2);
                           break;
                       }
                   case AccountRole.SYSTEM_MANAGER:
                       {
                           strUserList = _accountBll.GetUserList(3);
                           break;
                       }
                   default:
                       break;
               }
               View.RefreshUserList(strUserList);
           }
           catch (System.Exception e1)
           {
               String[] s = new string[4];
               _Model.mesBll.RecordMessage((int)LoginMesIDEnum.ERROR_GETUSERLIST, e1.Message);
               MessageDefineME m = _Model.mesBll.GetMesDef((int)LoginMesIDEnum.ERROR_GETUSERLIST);
               string strID = m.messageID.ToString();
               s[0] = System.DateTime.Now.ToString() ;
               s[1] = strID.PadLeft(3, '0');
               s[2] = m.messageContent;
               s[3] = e1.Message;
          
               View.OutputMessage(s);
           }   
        }

        /// <summary>
        /// 用户登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserLoginEventHandler(object sender,LoginEventArg e)
        {
              //先判断当前是已经否有用户登陆，若有则做出处理
              int role = _accountBll.Login(e.UserName, e.Paswd);
             
              String[] s = new string[4];
              if (role != -1)
              {
                  string dynamicContent = string.Format("当前用户：{0},用户级:{1}", e.UserName, _accountRolenameDic[role]); 
                  _Model.mesBll.RecordMessage((int)LoginMesIDEnum.INFO_LOGOK, dynamicContent);
                  MessageDefineME m = _Model.mesBll.GetMesDef((int)LoginMesIDEnum.INFO_LOGOK);
                  string strID = m.messageID.ToString();
                 
                  s[0] = System.DateTime.Now.ToString();
                  s[1] = strID.PadLeft(3, '0');
                  s[2] = m.messageContent;
                  s[3] = dynamicContent; 
                //  s = string.Format("{0}  {1} {2}。{3}。", System.DateTime.Now.ToString(),formatID, m.messageContent, dynamicContent);
                  View.LoginResult(true, e.UserName, role);
                  _Model.currentUserName = e.UserName;
                  _Model.currentUserRole = (AccountRole)role;
              }
              else
              {
                  _Model.mesBll.RecordMessage((int)LoginMesIDEnum.WARNING_LOGFAILED, "试图登陆用户：" + e.UserName);
                  MessageDefineME m = _Model.mesBll.GetMesDef((int)LoginMesIDEnum.WARNING_LOGFAILED);
                  string strID = m.messageID.ToString();
                  s[0] = System.DateTime.Now.ToString();
                  s[1] = strID.PadLeft(3, '0');
                  s[2] = m.messageContent;
                  s[3] = "试图登陆用户：" + e.UserName;
                  View.LoginResult(false, e.UserName, -1);
              }
              View.OutputMessage(s);
        }
        #endregion
        //订阅view的事件
        protected override void OnViewSet()
        {
            View.eventRoleChange += RoleChangeEventHandler;
            View.eventUserLogin += UserLoginEventHandler;
        }
    }
}
