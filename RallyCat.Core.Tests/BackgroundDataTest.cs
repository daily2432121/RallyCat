using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Services;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class BackgroundDataTest
    {
        public string _conn = "RallyCatConnection";
        [TestInitialize]
        public void Init()
        {
        }
        [TestMethod]
        public void BackgroundDataLoadTest()
        {
            var globalSetting = RallyBackgroundData.Instance.RallyGlobalConfiguration;
            var mappings = RallyBackgroundData.Instance.RallySlackMappings;
            Assert.IsNotNull(globalSetting);
            Assert.IsTrue(mappings.Count>1);
        }
    }
}
