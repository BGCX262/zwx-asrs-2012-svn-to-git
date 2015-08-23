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
    public class ProductBLL
    {
        private readonly IProductStoreDAL _productStoreDAL = DALFactory.CreateProductStoreDAL();
        private readonly IProductCategoryDAL _productCategoryDAL = DALFactory.CreateProductCategoryDAL();

        #region 产品类型库相关接口
        public bool AddProductInfo(ProductCategoryME m)
        {
            if (_productCategoryDAL.Exists(m.productType))
            {
                return false;
            }
            return _productCategoryDAL.Add(m);
        }
        public bool DeleteProductInfo(string productTypeID)
        {
            return _productCategoryDAL.Delete(productTypeID);
        }
        public bool UpdateProductInfo(ProductCategoryME m)
        {
            return _productCategoryDAL.Update(m);
        }
        public ProductCategoryME GetProductInfo(string productTypeID)
        {
            return _productCategoryDAL.GetModel(productTypeID);
        }
        public DataTable GetProductInfoList()
        {
            DataSet ds = _productCategoryDAL.GetList(string.Empty);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }
        #endregion
        #region 产品存储相关接口
        public bool AddProductStore(ProductStoreME m)
        {
            if (_productStoreDAL.Exists(m.productID))
            {
                return false;
            }
            return _productStoreDAL.Add(m);
        }
        public bool DeleteProductStore(string productID)
        {
            return _productStoreDAL.Delete(productID);
        }
        public ProductStoreME GetProductStore(string productID)
        {
            return _productStoreDAL.GetModel(productID);
        }
        #endregion
    }
}
