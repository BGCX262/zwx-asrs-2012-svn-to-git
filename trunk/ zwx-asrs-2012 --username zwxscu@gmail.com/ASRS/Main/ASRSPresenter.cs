using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASRS
{
    public class ASRSPresenter:Presenter<IASRSMainView>
    {
        public ASRSModel _Model { get; set; }
        public ASRSPresenter(IASRSMainView view)
            : base(view)
        {
            _Model = ASRSModel.GetInstance();
        }
#region view事件接口处理器
        private void LoadEventHandler(object sender, EventArgs e)
        {

        }
#endregion
        //订阅view的事件
        protected override void OnViewSet()
        {
            this.View.Load += this.LoadEventHandler;
        }
    }
}
