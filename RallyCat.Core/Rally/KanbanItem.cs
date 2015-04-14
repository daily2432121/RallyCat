using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallyCat.Core.Rally
{
    public class KanbanItem
    {
        public string FormattedId { get; set; }
        public string AssignedTo { get; set; }
        public string StoryDescription { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockedReason { get; set; }

        public KanbanItem(string formattedId, string assignedTo, string storyDescription)
        {
            FormattedId = formattedId;
            AssignedTo = assignedTo;
            StoryDescription = storyDescription;
            IsBlocked = false;
            
        }

        public KanbanItem(string formattedId, string assignedTo, string storyDescription, string blockedReason)
        {
            FormattedId = formattedId;
            AssignedTo = assignedTo;
            StoryDescription = storyDescription;
            IsBlocked = true;
            BlockedReason = blockedReason;
        }
    }
}
