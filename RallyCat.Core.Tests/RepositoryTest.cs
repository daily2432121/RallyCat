using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Rally;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestInitialize]
        public void Init()
        {
        }

        //[TestMethod]
        //public void RallyGlobalConfigurationRepositoryLoadTest()
        //{

        //    RallyGlobalConfigurationRepository repo = new RallyGlobalConfigurationRepository(_context);
        //    var result = repo.GetItem();
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Success);
        //    var r = JsonConvert.SerializeObject(result.Object);
        //    Console.Write(r);

        //}

        //[TestMethod]
        //public void RallySlackMappingRepositoryLoadTest()
        //{
        //    RallySlackMappingRepository repo = new RallySlackMappingRepository(_context);
        //    var result = repo.GetAll();
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Success);
        //    Assert.IsTrue(result.Object.Count>0);


        //}

        //[TestMethod]
        //public void Get()
        //{
        //    RallySlackMappingRepository repo = new RallySlackMappingRepository(_context);
        //    var r = repo.GetAll();
        //    AllConfig config = new AllConfig();
        //    config.RallySlackMappings = r.Object;
        //    RallyGlobalConfigurationRepository repo2 = new RallyGlobalConfigurationRepository(_context);
        //    config.GlobalConfig = repo2.GetItem().Object;
        //    var o = JsonConvert.SerializeObject(config);
        //    Console.Write(o);
        //}

    }
}
