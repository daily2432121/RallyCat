using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallyCat.Core.Configuration
{
    public class RallyGlobalConfiguration
    {
        public bool EnableGoogleSearch { get; set; }
        public string ErrorSlackResponse { get; set; }
        public string NoResultSlackResponse { get; set; }
        public string AzureBlobName { get; set; }
        public string AzureBlobContainerRef { get; set; }
        public string AzureToken { get; set; }
        public string KanbanImageFormat { get; set; }
    }
}
