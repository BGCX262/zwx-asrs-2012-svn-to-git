using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
namespace InstallDB
{
    [RunInstaller(true)]
    public partial class InstallDB : System.Configuration.Install.Installer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null; 
        public InstallDB()
        {
            InitializeComponent();
        }
        private void ExecuteSql(string conn,string DatabaseName,string Sql)
        {
            SqlConnection mySqlConnection = new SqlConnection(conn);
            SqlCommand Command = new SqlCommand(Sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlConnection.ChangeDatabase(DatabaseName);
            try
            {
                Command.ExecuteNonQuery();
            }
            finally
            {
                //close Connection  
                mySqlConnection.Close();
            }
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            System.Console.WriteLine("hh");
            base.Install(stateSaver);

           // MessageBox.Show("数据库students已经存在", "安装错误", MessageBoxButtons.YesNoCancel);
            // ------------------------建立数据库-------------------------------------------------
            try
            {
                string setupLogFile = AppDomain.CurrentDomain.BaseDirectory + @"dbSetupLog.txt ";
               System.IO.FileStream   myTraceLog   =   new  System.IO.FileStream( setupLogFile, System.IO.FileMode.OpenOrCreate); 
                
               TextWriterTraceListener   myListener   =   new   TextWriterTraceListener(myTraceLog); 
               Trace.Listeners.Add(myListener);
               
               // string connstr = String.Format("data source={0};user security info=false;packet size=4096", Context.Parameters["server"], Context.Parameters["user"], Context.Parameters["pwd"]);
               string connstr = String.Format("server={0};database=;uid={1};pwd={2}", Context.Parameters["server"], Context.Parameters["user"], Context.Parameters["pwd"]);
                //根据输入的数据库名称建立数据库
               myListener.WriteLine("sql server连接字符串:" + connstr);
               myListener.WriteLine("开始执行sql语句建立数据库");
               myListener.WriteLine(connstr);
               myListener.Flush();
                ExecuteSql(connstr, "master", "CREATE DATABASE " + Context.Parameters["dbname"]);
                myListener.WriteLine("建立数据库完成");
                myListener.Flush();

                Assembly Asm=Assembly.GetExecutingAssembly();
                StreamReader str;
                str = new StreamReader(Asm.GetManifestResourceStream(Asm.GetName().Name + "." +"Resources.studentDBScript.sql"));

                string strSql= string.Empty;
                string line;
                while( (line= str.ReadLine()) != null)
                {
                   
                    if(line.Trim().ToUpper().CompareTo("GO") == 0)
                    {
                        if(strSql != string.Empty)
                        {
                            ExecuteSql(connstr, Context.Parameters["dbname"], strSql);
                           
                        }
                        strSql = string.Empty;
                        continue;
                    }
                    myListener.WriteLine(line);
                    myListener.Flush();
                    strSql += line;
                    strSql += "\r\n";
                   
                }
                myListener.Flush();
               
                ////'调用osql执行脚本  
                //Process sqlprocess = new System.Diagnostics.Process();
                //sqlprocess.StartInfo.FileName = "osql.exe ";
                //sqlprocess.StartInfo.Arguments = String.Format(" -U {0} -P {1} -S{2} -i {3}studentDBScript.sql", Context.Parameters["user"], Context.Parameters["pwd"], Context.Parameters["server"], Context.Parameters["targetdir"]);

                //myListener.WriteLine("sqlprocess.StartInfo.Arguments");
                //myListener.WriteLine(sqlprocess.StartInfo.Arguments);
                //myListener.Flush();
                ////sqlprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //if(!sqlprocess.Start())
                //{
                //    myListener.WriteLine("执行sql 脚本失败");
                //    myListener.Flush();
                //}
                //else
                //{
                //    myListener.WriteLine("执行sql 脚本成功");
                //    myListener.Flush();
                //}
                //sqlprocess.WaitForExit(); // '等待执行
                //sqlprocess.Close();
                //'删除脚本文件
                FileInfo sqlfileinfo = new FileInfo(String.Format("{0}studentDBScript.sql", Context.Parameters["targetdir"]));
                if (sqlfileinfo.Exists)
                {
                    sqlfileinfo.Delete();
                }
            }
            catch (System.Exception e)
            {
            	throw e;
            }
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

        }
    }
}
