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

namespace monitor
{

    public partial class MonitorService : ServiceBase
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Monitor _monitor;

        private static Server _server = null;

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

                _monitor = Monitor.New(args[0]);
                _monitor.Start();
               
                _logger.Info("RestServer Start");

                _server = new Server(_monitor.RestServerHostName, _monitor.RestServerPort);

                SetRestHandlers();

                _server.Start();
            }
            catch (Exception ex)
            {
                _logger.Error($"<<--------{ex}-------->>");
            }
        }

        protected override void OnStop()
        {
            _logger.Info("-----------------------------Service Stop");

            _monitor.Stop();
            _server.Stop();
            
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }


        // 외부 요청 처리 
        private void SetRestHandlers()
        {
            _server.Routes.Static.Add(HttpMethod.GET, "/check", async (ctx) =>
            {
                string message = "false";

                if(_monitor.CheckIntegrity()) message = "success";
 
                await ctx.Response.Send(message);
            });
        }

    }
}
