using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyCat.Core.Helpers;
using RallyCat.Core.Rally;
using RallyCat.Core.Services;

namespace RallyCat.Core.Tests
{
    [TestClass]
    public class GraphicServiceTest
    {
        [TestMethod]
        public void DrawKanbanItemFrameBlockedTest()
        {
            GraphicService service= new GraphicService();
            KanbanItem item = new KanbanItem("C1","US1234", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas", "blockedblockedblockedblockedblockedblockedblockedblockedblockedblockedblocked");
            var rec = service.GetKanbanItemSize(new Point(0, 0), 400, 20, 20, item);
            Bitmap image = new Bitmap(rec.Width, rec.Height);
            using (var g = Graphics.FromImage(image))
            {
                service.DrawOneKanbanItem(g, new Point(0, 0), 400, 20, 20, item);
                image.Save(@"c:\temp\pngTempBlocked.png", ImageFormat.Png);
                image.Dispose();
            }
           
            
            
            
        }

        [TestMethod]
        public void DrawKanbanItemFrameNonBlockedTest()
        {
            GraphicService service = new GraphicService();
            KanbanItem item = new KanbanItem("C1","US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            var rec = service.GetKanbanItemSize(new Point(0, 0), 400, 20, 20, item);
            Image image = new Bitmap(rec.Width, rec.Height);
            using (var g = Graphics.FromImage(image))
            {
                service.DrawOneKanbanItem(g, new Point(0, 0), 400, 20, 20, item);
                image.Save(@"c:\temp\pngTempNonBlocked.png", ImageFormat.Png);
            }

        }

        [TestMethod]
        public void DrawOneKanbanColumnTest()
        {
            GraphicService service = new GraphicService();
            KanbanItem item0 = new KanbanItem("C1","US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            KanbanItem item1 = new KanbanItem("C1","US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas","BLOCKED BLOCKED BLOCKED BBBBBBBB");
            //KanbanItem item2 = new KanbanItem("US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            //KanbanItem item3 = new KanbanItem("US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            //KanbanItem item4 = new KanbanItem("US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            //KanbanItem item5 = new KanbanItem("US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            //KanbanItem item6 = new KanbanItem("US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            List<KanbanItem> items =new List<KanbanItem>(){item0,item1};
            var rec = service.GetOneKanbanColumnSize(new Point(0, 0), 400, 20, 50, 20,100, items);
            Image image=new Bitmap(rec.Width,rec.Height);
            using (var g = Graphics.FromImage(image))
            {
                service.DrawOneKanbanColumn(g, new Point(0, 0), 400, 20, 50, 20,"Development",100, items);
                image.Save(@"c:\temp\pngTempOneColumn.png",ImageFormat.Png);
            }
            

        }

        [TestMethod]
        public void DrawWholeKanbanTest()
        {
            GraphicService service = new GraphicService();
            KanbanItem item0 = new KanbanItem("C1", "US1234", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            KanbanItem item1 = new KanbanItem("C1", "DE4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas", "BLOCKED BLOCKED BLOCKED BBBBBBBB");
            KanbanItem item2 = new KanbanItem("C2", "US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxddsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            KanbanItem item3 = new KanbanItem("C2", "US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxefadsfasefaefasdfsdfsafas");
            KanbanItem item4 = new KanbanItem("C3", "US22221", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxdsdfgsdfgsfgsdsssseeqqzxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            KanbanItem item5 = new KanbanItem("C4", "US4321", "Cheng Huang", "aabbccddxxxxxxxxxxxxxxxxxxxxxxxxxbbbbdeevsdfaefefadsfasefaefasdfsdfsafas");
            KanbanItem item6 = new KanbanItem("C4", "US4321", "Cheng Huang", "aabbccfaefasdfsdfsafas", "abeedde");
            List<KanbanItem> itemg1 = new List<KanbanItem>(){item0,item1};
            List<KanbanItem> itemg2 = new List<KanbanItem>(){item2, item3, item0, item5};
            List<KanbanItem> itemg3 = new List<KanbanItem>(){item4};
            List<KanbanItem> itemg4 = new List<KanbanItem>(){item5, item6};
            Dictionary<string, List<KanbanItem>> dict =new Dictionary<string, List<KanbanItem>>();
            dict.Add("C1", itemg1);
            dict.Add("C2", itemg2);
            dict.Add("C3", itemg3);
            dict.Add("C4", itemg4);
            Image img = service.DrawWholeKanban(400, 20, 50, 20, 100, dict);
            img.Save(@"c:\temp\pngTempWholeKanban.png",ImageFormat.Png);

        }
    }
}
