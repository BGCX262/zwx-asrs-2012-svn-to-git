using System;
using System.ComponentModel;  
using System.Windows.Forms;
namespace ASRS
{
    public class viewBase : Form
    {
        private object _presenter;
        public viewBase()
        {
            _presenter = this.CreatePresenter();
        }
        protected virtual object CreatePresenter()
        {
            if (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Designtime)
            {
                return null;
            }
            else
            {
                throw new NotImplementedException(string.Format("{0} must override the CreatePresenter method.", this.GetType().FullName));
            }
        }

    }

    //基presenter
    public class Presenter<IView>
    {
        public IView View { get; private set; }
        public Presenter(IView view)
        {
            this.View = view;           
            this.OnViewSet();
        }
        protected virtual void OnViewSet()
        {
        }
    }

    //view 基接口
    public interface IViewBase
    {
        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="mes"></param>
        void OutputMessage(string[] mes);
        event EventHandler Load;  
        event EventHandler Closed;
    }
}