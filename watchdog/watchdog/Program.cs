using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace watchdog
{
    static class Program
    {
  
        static void Main(string[] args)
        {
            //DEBUG 용 콘솔처리
            if (Environment.UserInteractive)
            {
                LogSetting(args[0]);
                MonitorService monitorService = new MonitorService();
                monitorService.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MonitorService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        static void LogSetting(string targetName)
        {
             // 로깅 부분 추가
        }
    }
}
