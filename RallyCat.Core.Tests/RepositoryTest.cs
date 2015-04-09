using System;
using System.Diagnostics;
using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.DataAccess;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        public string _conn ="RallyCatConnection";
        public IDbContext _context;
        [TestInitialize]
        public void Init()
        {
            RallyCatDbContext.SetConnectionString(_conn);
            _context = RallyCatDbContext.QueryDb();
        }

        [TestMethod]
        public void RallyGlobalConfigurationRepositoryLoadTest()
        {

            RallyGlobalConfigurationRepository repo = new RallyGlobalConfigurationRepository(_context);
            var result = repo.GetItem();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            

        }

        [TestMethod]
        public void RallySlackMappingRepositoryLoadTest()
        {
            RallySlackMappingRepository repo = new RallySlackMappingRepository(_context);
            var result = repo.GetAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Object.Count>0);


        }
    }
}
