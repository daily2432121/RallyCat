using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RallyCat.Core.Rally;

namespace RallyCat.Core.Services
{
    public class GraphicService
    {
        
        public Graphics DrawKanbanItemFrame(Graphics g, Rectangle rec,int headerHeight,int textMargin, KanbanItem item)
        {
            //Frame, wire frame rectangle
            Pen bluePen = new Pen(Color.DeepSkyBlue,4);
            g.Clear(Color.White);
            g.DrawRectangle(bluePen,rec);

            //Header, solid rectangle
            Brush headerBrush=new SolidBrush(Color.DarkOrange);
            Rectangle headerRec = new Rectangle(new Point(rec.Left, rec.Top), new Size(rec.Width + 1, headerHeight));
            g.FillRectangle(headerBrush, headerRec);
            Font font = new Font("Segoe UI", 20, FontStyle.Bold);

            Brush formattedIdBrush = new SolidBrush(Color.DeepSkyBlue);

            g.DrawString(item.FormattedId, font, formattedIdBrush, headerRec.Left + textMargin, headerRec.Bottom + textMargin);

            var formattedIdSize = g.MeasureString(item.FormattedId, font);

            Brush assignedToBrush = new SolidBrush(Color.Black);

            var assignedToSize = g.MeasureString(item.AssignedTo, font);

            g.DrawString(item.AssignedTo,font,assignedToBrush, headerRec.Left+textMargin, headerRec.Bottom + formattedIdSize.Height + 2 * textMargin);

            Brush descriptionBrush = new SolidBrush(Color.Black);

            var descriptionSize = g.MeasureString(item.StoryDescription, font, headerRec.Width - 2*textMargin);

            RectangleF recDescription = new RectangleF(new PointF(headerRec.Left+textMargin, headerRec.Bottom + formattedIdSize.Height+ assignedToSize.Height + 3 * textMargin), descriptionSize);

            g.DrawString(item.StoryDescription, font, descriptionBrush, recDescription);

            


            if (item.IsBlocked)
            {
                
            }


            //FormattedId

            //Username

            //Description

            //Blocked Rectangle

            //Blocked Reason

            //Blocked Icon
            return g;
        }
    }
}
