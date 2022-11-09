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
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Monitor _monitor;

        private Server _server;

        public MonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("-----------------------------Service Start");

            Utils.WithExceptionHandled("", true, () =>
              {
                  _monitor = Monitor.New("server");

                  _server = new Server(_monitor.RestServerHostName, _monitor.RestServerPort);






              });
        }

        protected override void OnStop()
        {
            _logger.Info("-----------------------------Service Stop");
        }
    }
}
