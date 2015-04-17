using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Rectangle GetKanbanItemSize(Point startPoint, int recWidth, int headerHeight, int textMargin, KanbanItem item)
        {
            Image img =new Bitmap(1,1);
            using (var g = Graphics.FromImage(img))
            {
                float currentHeight = 0;
                var halfMargin = textMargin / 2;
                Rectangle headerRec = new Rectangle(new Point(startPoint.X, startPoint.Y), new Size(recWidth + 1, headerHeight));
                Font font = new Font("Segoe UI", 20, FontStyle.Regular);
                
                currentHeight += headerRec.Bottom + halfMargin;
                var formattedIdSize = g.MeasureString(item.FormattedId, font);
                var assignedToSize = g.MeasureString(item.AssignedTo, font);

                var descriptionSize = g.MeasureString(item.StoryDescription, font, headerRec.Width - 2 * textMargin);
                RectangleF recDescription = new RectangleF(new PointF(headerRec.Left + textMargin, currentHeight + textMargin), descriptionSize);

                currentHeight += formattedIdSize.Height + textMargin;
                currentHeight += assignedToSize.Height + textMargin;
                currentHeight += recDescription.Height + textMargin;
                
                if (item.IsBlocked)
                {
                    Image blockedIcon = Resources.blocked;
                    var blockedReasonSize = g.MeasureString(item.BlockedReason, font, headerRec.Width - 2 * textMargin - blockedIcon.Width - halfMargin);
                    SizeF blockedReasonBackgroundSize = new SizeF(headerRec.Width - 2 * textMargin, blockedReasonSize.Height);
                    
                    currentHeight += blockedReasonBackgroundSize.Height +textMargin;
                }
                currentHeight += halfMargin;
                //currentBottom += halfMargin;
                //Frame, wire frame rectangle
                Rectangle recFrame = new Rectangle(startPoint, new Size(recWidth, (int)currentHeight));
                return recFrame;
            }
        }

        public Image DrawWholeKanban(int rectWidth, int headerHeight, int stackItemMargin,
            int textMargin, int categoryHeaderHeight, Dictionary<string, List<KanbanItem>> kanbanItems)
        {
            Point next = new Point(0, 0);
            int maxHeight = 0;
            int width = 0;
            foreach (var key in kanbanItems.Keys)
            {
                var oneCol = GetOneKanbanColumnSize(next, rectWidth, headerHeight, stackItemMargin, textMargin, categoryHeaderHeight, kanbanItems[key]);
                maxHeight = Math.Max(maxHeight, oneCol.Height);
                width += oneCol.Width;
            }

            Image img=new Bitmap(width,maxHeight);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.White);
                next = new Point(0, 0);
                foreach (var key in kanbanItems.Keys)
                {
                    var oneCol = GetOneKanbanColumnSize(next, rectWidth, headerHeight, stackItemMargin, textMargin, categoryHeaderHeight, kanbanItems[key]);
                    DrawOneKanbanColumn(g, next, rectWidth, headerHeight, stackItemMargin, textMargin, key, categoryHeaderHeight, kanbanItems[key]);
                    next.X += oneCol.Width;
                }
                return img;
            }
            

        }


        public Rectangle DrawOneKanbanColumn(Graphics g, Point startPoint, int rectWidth, int headerHeight,int stackItemMargin, 
            int textMargin,string category, int categoryHeight, List<KanbanItem> items)
        {
            Point next = startPoint;
            next.X += stackItemMargin;
            next.Y += stackItemMargin;

            Font font = new Font("Segoe UI", 25, FontStyle.Bold);
            StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            Brush brush =new SolidBrush(Color.Black);
            g.DrawString(category,font,brush, new RectangleF(next.X,next.Y,rectWidth,categoryHeight),sf);

            next.Y += categoryHeight;

            foreach (KanbanItem item in items)
            {
                var rec = DrawOneKanbanItem(g, next, rectWidth, headerHeight, textMargin, item);
                next.Y = rec.Bottom + stackItemMargin ;
            }
            return new Rectangle(startPoint, new Size(stackItemMargin + rectWidth + stackItemMargin, next.Y));
        }

        public Rectangle GetOneKanbanColumnSize(Point startPoint, int rectWidth, int headerHeight, int stackItemMargin,
            int textMargin, int categoryHeight, List<KanbanItem> items)
        {
            
            Point next = startPoint;
            next.X += stackItemMargin;
            next.Y += categoryHeight;
            next.Y += stackItemMargin;
            int lastHeight = 0;
            foreach (KanbanItem item in items)
            {
                var rec = GetKanbanItemSize(next, rectWidth, headerHeight, textMargin, item);
                next.Y = rec.Height +  stackItemMargin ;
            }

            return new Rectangle(startPoint, new Size(stackItemMargin + rectWidth + stackItemMargin, next.Y ));
        }


        public Rectangle DrawOneKanbanItem(Graphics g, Point startPoint, int recWidth, int headerHeight, int textMargin, KanbanItem item)
        {
            float currentHeight = 0;

            var halfMargin = textMargin/2;

            //Header, solid rectangle
            Brush headerBrush=new SolidBrush(Color.DarkOrange);
            Rectangle headerRec = new Rectangle(new Point(startPoint.X, startPoint.Y), new Size(recWidth+1, headerHeight));
            
            Font font = new Font("Segoe UI", 20, FontStyle.Regular);
            Debug.WriteLine("Draw:currentHeight0:"+currentHeight);
            currentHeight = headerRec.Bottom + halfMargin;
            Debug.WriteLine("Draw:currentHeight1:" + currentHeight);
            //FormattedId
            Brush formattedIdBrush = new SolidBrush(Color.DeepSkyBlue);
            var formattedIdSize = g.MeasureString(item.FormattedId, font);
            g.DrawString(item.FormattedId, font, formattedIdBrush, headerRec.Left + textMargin, currentHeight );

            currentHeight += formattedIdSize.Height + textMargin;
            Debug.WriteLine("Draw:currentHeight2:" + currentHeight);
            //AssignedToId
            Brush assignedToBrush = new SolidBrush(Color.Black);
            var assignedToSize = g.MeasureString(item.AssignedTo, font);
            g.DrawString(item.AssignedTo, font, assignedToBrush, headerRec.Left + textMargin, currentHeight);

            currentHeight += assignedToSize.Height + textMargin;
            Debug.WriteLine("Draw:currentHeight3:" + currentHeight);
            //Description
            Brush descriptionBrush = new SolidBrush(Color.Black);
            var descriptionSize = g.MeasureString(item.StoryDescription, font, headerRec.Width - 2*textMargin);
            RectangleF recDescription = new RectangleF(new PointF(headerRec.Left + textMargin, currentHeight), descriptionSize);

            
            g.DrawString(item.StoryDescription, font, descriptionBrush, recDescription);
            currentHeight += recDescription.Height + textMargin;
            Debug.WriteLine("Draw:currentHeight4:" + currentHeight);
            if (item.IsBlocked)
            {
                Image blockedIcon = Resources.blocked;

                var blockedReasonSize = g.MeasureString(item.BlockedReason, font, headerRec.Width - 2 * textMargin - blockedIcon.Width - halfMargin);
                SizeF blockedReasonBackgroundSize = new SizeF(headerRec.Width -2 * textMargin,blockedReasonSize.Height);
                Brush blockedReasonBackgroundBrush = new SolidBrush(Color.LightGray);
                g.FillRectangle(blockedReasonBackgroundBrush, new RectangleF(new PointF(headerRec.Left + textMargin, currentHeight), blockedReasonBackgroundSize));

                Brush blockedReasonBrush = new SolidBrush(Color.Black);
                RectangleF recBlockedReason = new RectangleF(new PointF(headerRec.Left + blockedIcon.Width + textMargin + halfMargin, currentHeight), blockedReasonSize);
                g.DrawString(item.BlockedReason,font,blockedReasonBrush,recBlockedReason );

                RectangleF recBlockedIconSmall = new RectangleF(headerRec.Left + textMargin + halfMargin, currentHeight  + recBlockedReason.Height / 2 - blockedIcon.Height / 2, blockedIcon.Width, blockedIcon.Height);
                g.DrawImage(blockedIcon, recBlockedIconSmall);

                currentHeight += blockedReasonBackgroundSize.Height + textMargin;
            }
            
            currentHeight += halfMargin;
            //Frame, wire frame rectangle

            Pen framePen = item.IsBlocked ? new Pen(Color.DarkRed, 2) : new Pen(Color.Gray, 2);
            //g.Clear(Color.White);
            Rectangle recFrame = new Rectangle(startPoint,new Size(recWidth,(int)currentHeight -startPoint.Y));
            g.DrawRectangle(framePen, recFrame);
            g.FillRectangle(headerBrush, headerRec);

            return recFrame;
        }
    }
}
