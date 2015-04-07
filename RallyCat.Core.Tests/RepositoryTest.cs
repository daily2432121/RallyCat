using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.DataAccess;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        public string _conn ="RallyCatConnection";
        [TestInitialize]
        public void Init()
        {
            RallyCatDbContext.SetConnectionString(_conn);
        }

        [TestMethod]
        public void RallyGlobalConfigurationRepositoryLoadTest()
        {
            RallyGlobalConfigurationRepository repo=new RallyGlobalConfigurationRepository();
            var result = repo.GetItem();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            

        }

        [TestMethod]
        public void RallySlackMappingRepositoryLoadTest()
        {
            RallySlackMappingRepository repo = new RallySlackMappingRepository();
            var result = repo.GetAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Object.Count>0);


        }
    }
}
