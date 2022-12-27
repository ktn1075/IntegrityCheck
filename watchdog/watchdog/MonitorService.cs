using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WatsonWebserver;

namespace watchdog
{

    public partial class MonitorService : ServiceBase
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Monitor _monitor;

        private static Server _Server = null;

        public MonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("-----------------------------Service Start");
            
            try
            { 
                _logger.Info("Monitoring Start");

                _monitor = Monitor.New("server");
                _monitor.Start();

                _logger.Info("RestServer Start");

                _Server = new Server(_monitor.RestServerHostName, _monitor.RestServerPort, false, DefaultRoute);
                _Server.Start();
            }
            catch (Exception ex)
            {
                _logger.Error($"<<--------{ex}-------->>");
            }
        }

        protected override void OnStop()
        {
            _logger.Info("-----------------------------Service Stop");

            _Server.Stop();
  
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        static async Task DefaultRoute(HttpContext ctx)
        {

        }

    }
}
