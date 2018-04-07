using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleChatBot.Service;
// using NLog.Web;

namespace SimpleChatBot
{
    public class Program
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            // NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            LoadLog4netConfig();
            _log.Info("Application Start");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilder, configurationBinder) =>
                {
                    configurationBinder.AddJsonFile("settings.json", optional: true);
                })
                .ConfigureLogging(logging => {
                    logging.AddProvider(new Log4netProvider("log4net.config"));
                })
                .UseStartup<Startup>()
                // .UseNLog()
                .Build();

        private static void LoadLog4netConfig()
        {
            var repository = LogManager.CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy)
                );
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }
    }
}
