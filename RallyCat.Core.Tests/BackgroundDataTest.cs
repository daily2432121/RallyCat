using System;
using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Services;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class BackgroundDataTest
    {
        public string _conn = "RallyCatConnection";
        public IDbContext _context;
        [TestInitialize]
        public void Init()
        {
            RallyCatDbContext.SetConnectionString(_conn);
            _context = RallyCatDbContext.QueryDb();
        }
        [TestMethod]
        public void BackgroundDataLoadTest()
        {
            RallyBackgroundData.SetDbContext(_context);
            var globalSetting = RallyBackgroundData.Instance.RallyGlobalConfiguration;
            var mappings = RallyBackgroundData.Instance.RallySlackMappings;
            Assert.IsNotNull(globalSetting);
            Assert.IsTrue(mappings.Count>1);
        }
    }
}
