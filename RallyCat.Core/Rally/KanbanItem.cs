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
        public string KanbanState { get; set; }
        public KanbanItem(string kanbanState, string formattedId, string assignedTo, string storyDescription)
        {
            KanbanState = kanbanState;
            FormattedId = formattedId;
            AssignedTo = assignedTo;
            StoryDescription = storyDescription;
            IsBlocked = false;
            
        }

        public KanbanItem(string kanbanState, string formattedId, string assignedTo, string storyDescription, string blockedReason)
        {
            KanbanState = kanbanState;
            FormattedId = formattedId;
            AssignedTo = assignedTo;
            StoryDescription = storyDescription;
            IsBlocked = true;
            BlockedReason = blockedReason;
        }

        public static KanbanItem ConvertFrom(dynamic queryResult, string kanbanStateKeyWord)
        {
            var s = queryResult;
            if (s["Blocked"])
            {
                return new KanbanItem(s[kanbanStateKeyWord] ?? "None", s["FormattedID"], s["Owner"] == null ? "(None)" : s["Owner"]["_refObjectName"], s["Name"], s["BlockedReason"]);
            }
            else
            {
                return new KanbanItem(s[kanbanStateKeyWord] ?? "None", s["FormattedID"], s["Owner"] == null ? "(None)" : s["Owner"]["_refObjectName"], s["Name"]);
            }
        }
    }
}
