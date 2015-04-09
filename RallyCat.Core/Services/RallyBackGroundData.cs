using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentData;
using RallyCat.Core.Configuration;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Rally;

namespace RallyCat.Core.Services
{
    public class RallyBackgroundData
    {
        private static RallyBackgroundData _instance;
        private static IDbContext _dbContext;
        public RallyGlobalConfiguration RallyGlobalConfiguration { get ; private set; }
        public List<RallySlackMapping> RallySlackMappings { get; private set; }

        public static DateTime LastUpdatedTime { get; private set; }
        private static object _lock = new object();
        private RallyBackgroundData(IDbContext context)
        {
            SetDbContext(context);
        }

        public static void SetDbContext(IDbContext context)
        {
            _dbContext = context;
        }

        public static RallyBackgroundData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RallyBackgroundData(_dbContext);
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
                _instance = new RallyBackgroundData(_dbContext);
                _instance.LoadAll();    
            }
        }
        private void LoadAll()
        {
            RallyGlobalConfiguration = GetRallyGlobalConfiguration();
            RallySlackMappings = GetRallySlackMappings();
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
                        RallyBackgroundData newData = new RallyBackgroundData(_dbContext);
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

        private RallyGlobalConfiguration GetRallyGlobalConfiguration()
        {
            if (_dbContext == null)
            {
                throw new ArgumentNullException("DBContext is null.");
            }
            RallyGlobalConfigurationRepository repo = new RallyGlobalConfigurationRepository(_dbContext);
            var result = repo.GetItem();
            return result.Object;
        }

        private List<RallySlackMapping> GetRallySlackMappings()
        {
            if (_dbContext == null)
            {
                throw new ArgumentNullException("DBContext is null.");
            }
            RallySlackMappingRepository repo = new RallySlackMappingRepository(_dbContext);
            var result = repo.GetAll();
            return result.Object;
        }
    }
}
