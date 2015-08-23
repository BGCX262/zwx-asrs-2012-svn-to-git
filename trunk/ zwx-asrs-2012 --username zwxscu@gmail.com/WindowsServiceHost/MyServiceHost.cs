using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using WCFService1;
using SvcMachineCmd;
namespace WindowsServiceHost
{
    public partial class BookServiceHost : ServiceBase
    {
        private ServiceHost _HostBookService = new ServiceHost(typeof(BookService));
        private ServiceHost _HostMachineService = new ServiceHost(typeof(MachineService));
        public BookServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _HostBookService.Open();
            _HostMachineService.Open();
        }

        protected override void OnStop()
        {
            _HostBookService.Close();
            _HostMachineService.Close();
        }
    }
}
