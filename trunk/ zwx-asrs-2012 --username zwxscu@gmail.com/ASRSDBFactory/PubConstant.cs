using System;
using System.Configuration;
using System.Text;

namespace ASRSDBFactory
{
    
    public static class PubConstant
    {
        public static string dbServerIP = "127.0.0.1"; //默认本机配置
        public static string dbUser = "sa";
        public static string dbPwd = "zwx2006";
      
        public static string ConnectionStringAccountDB
        {           
            get 
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("server={0};database=ASRSAccountDB;uid={1};pwd={2}",dbServerIP,dbUser,dbPwd);
                return strBuilder.ToString(); 
            }
        }
        public static string ConnectionStringProductDB
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("server={0};database=ASRSProductDB;uid={1};pwd={2}", dbServerIP, dbUser, dbPwd);
                return strBuilder.ToString(); 
            }
        }
    
    }
}
