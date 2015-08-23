using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace SvcMachineCmd
{
    [ServiceContract]
    public interface IMachineService
    {
        [OperationContract]
        String GetMachineStatus();

        [OperationContract]
        void ExetuteCmd(String strCmd);
    }
}
