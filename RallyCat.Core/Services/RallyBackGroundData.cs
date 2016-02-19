using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using RallyCat.Core.Configuration;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Rally;

namespace RallyCat.Core.Services
{
    public class RallyBackgroundData
    {
        private static RallyBackgroundData _instance;
        const string jsonFile = "config.json";
        private static string _filePath = "";
        public RallyGlobalConfiguration RallyGlobalConfiguration { get ; private set; }
        public List<RallySlackMapping> RallySlackMappings { get; private set; }

        public static DateTime LastUpdatedTime { get; private set; }
        private static object _lock = new object();
        private RallyBackgroundData()
        {
            _filePath = HttpContext.Current != null ? HttpRuntime.AppDomainAppPath : Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }


        public static RallyBackgroundData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RallyBackgroundData();
                    _instance.LoadAll();
                    ThreadPool.QueueUserWorkItem(AutoRefresh);
                }
                
                return _instance;
            }
        }

        public void ForceRefresh()
        {
            lock (_lock)
            {
                _instance = new RallyBackgroundData();
                _instance.LoadAll();    
            }
        }
        private void LoadAll()
        {
            ConfigRepository repo = new ConfigRepository();
            var config = repo.GetConfig(_filePath);
            RallyGlobalConfiguration = config.GlobalConfig;
            RallySlackMappings = config.RallySlackMappings;
            LastUpdatedTime = DateTime.Now;
        }

        public static void AutoRefresh(Object stateInfo)
        {
            while (true)
            {
                lock (_lock)
                {
                    if (DateTime.Now - LastUpdatedTime >= TimeSpan.FromSeconds(20))
                    {
                        RallyBackgroundData newData = new RallyBackgroundData();
                        newData.LoadAll();

                        lock (_lock)
                        {
                            _instance = newData;
                        }

                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(20));
            }
        }


    }
}
