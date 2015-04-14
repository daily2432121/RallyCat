using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.Rally;
using RallyCat.Core.Services;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class GraphicServiceTest
    {
        [TestMethod]
        public void DrawKanbanItemFrameTest()
        {
            GraphicService service= new GraphicService();
            Image image = new Bitmap(500, 500);
            using (var g = Graphics.FromImage(image))
            {
                KanbanItem item=new KanbanItem("US1234","Cheng Huang","aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
                service.DrawKanbanItemFrame(g, new Rectangle(50, 50, 400, 400),20,20,item);
                image.Save(@"c:\temp\pngTemp.png", ImageFormat.Png);
            }
            
        }
    }
}
