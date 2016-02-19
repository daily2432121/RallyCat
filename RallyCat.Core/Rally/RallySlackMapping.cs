using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RallyCat.Core.DataAccess;

namespace RallyCat.Core.Rally
{
    public class RallySlackMapping
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long ProjectId { get; set; }
        public long WorkspaceId { get; set; }
        public string KanbanSortColumn { get; set; }
        public bool EnableKanban { get; set; }
        public List<string> Channels { get; set; }
    }
}
