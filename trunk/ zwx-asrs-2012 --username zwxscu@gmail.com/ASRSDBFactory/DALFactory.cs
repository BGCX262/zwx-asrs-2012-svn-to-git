using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASRSDBIDAL;
using ASRSDBSQLServerDAL;
namespace ASRSDBFactory
{
    public enum DBType
    {
        DB_SQLSERVER = 1,
        DB_ACCESS2007,
        DB_MySql
    }
   
    /// <summary>
    /// 抽象工厂模式创建DAL
    /// </summary>
    public sealed class DALFactory
    {
        private const DBType dbProduct = DBType.DB_SQLSERVER;
        
        /// <summary>
        /// 创建账户信息类接口
        /// </summary>
        /// <returns></returns>
        public static IAccountDAL CreateAccountDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.AccountDAL(PubConstant.ConnectionStringAccountDB);
            }
            else
                return null;
        }

        /// <summary>
        /// 创建产品信息类接口
        /// </summary>
        /// <returns></returns>
        public static IProductStoreDAL CreateProductStoreDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.ProductStoreDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }

        public static IProductCategoryDAL CreateProductCategoryDAL()
        {
           if (dbProduct == DBType.DB_SQLSERVER)
           {
                return new ASRSDBSQLServerDAL.ProductCategoryDAL(PubConstant.ConnectionStringProductDB);
           }
           else
               return null;
        }
        /// <summary>
        /// 创建仓储信息类接口
        /// </summary>
        /// <returns></returns>
        public static IWarehouseStoreDAL CreateWarehouseStoreDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.WarehouseStoreDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }

        /// <summary>
        /// 创建仓储-产品数据表视图类接口
        /// </summary>
        /// <returns></returns>
        public static IWareProductViewDAL CreateWareProductViewDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                 return new ASRSDBSQLServerDAL.WareProductViewDAL(PubConstant.ConnectionStringProductDB);
             }
            else
                return null;
        }

        /// <summary>
        /// 创建任务表类接口
        /// </summary>
        /// <returns></returns>
        public static ITaskDAL CreateTaskDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                  return new ASRSDBSQLServerDAL.TaskDAL(PubConstant.ConnectionStringProductDB);
             }
            else
                return null;
        }

        /// <summary>
        /// 创建指令表类接口
        /// </summary>
        /// <returns></returns>
        public static IInstDAL CreateInstDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                 return new ASRSDBSQLServerDAL.InstDAL(PubConstant.ConnectionStringProductDB);
             }
            else
                return null;
        }

        public static IMessageDefineDAL CreateMessageDefineDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                 return new ASRSDBSQLServerDAL.MessageDefineDAL(PubConstant.ConnectionStringProductDB);
             }
            else
                return null;
        }

        public static IMessageRecordDAL CreateMessageRecordDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.MessageRecordDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }

        public static IMessageViewDAL CreateMessageViewDAL()
        {
           if (dbProduct == DBType.DB_SQLSERVER)
           {
               return new ASRSDBSQLServerDAL.MessageViewDAL(PubConstant.ConnectionStringProductDB);
           }
           else
               return null;
        }

        public static IInHouseRecordDAL CreateInHouseDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.InHouseRecordDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }
        public static IOutHouseRecordDAL CreateOutHouseDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.OutHouseRecordDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }

        public static IHouseInOutViewDAL CreateHouseInOutViewDAL()
        {
            if (dbProduct == DBType.DB_SQLSERVER)
            {
                return new ASRSDBSQLServerDAL.HouseInOutViewDAL(PubConstant.ConnectionStringProductDB);
            }
            else
                return null;
        }
    }
}
