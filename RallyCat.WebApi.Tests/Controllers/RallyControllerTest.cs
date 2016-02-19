using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.WebApi.Controllers;

namespace RallyCat.WebApi.Tests.Controllers
{
    [TestClass]
    public class RallyControllerTest
    {
        [TestMethod]
        public void GetRallyItemTest()
        {
            RallyController c =new RallyController();
            var item = c.GetItem("DE1877", "de-support");
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetRallyKanbanTest()
        {
            RallyController c = new RallyController();
            string imgUrl =c.GetKanban("de-support");
            Assert.IsNull(imgUrl);

        }
    }
}
