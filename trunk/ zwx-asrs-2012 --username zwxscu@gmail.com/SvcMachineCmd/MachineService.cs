using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SvcMachineCmd
{
    public class MachineService:IMachineService
    {
        public String GetMachineStatus()
        {
            String strStatus = "Machine is running";
            return strStatus;
        }
        public void ExetuteCmd(String str)
        {
            Console.WriteLine("收到设备指令:" + str);
        }

    }
}
