using System.Collections.Generic;
using RallyCat.Core.Configuration;

namespace RallyCat.Core.Rally
{
    public class AllConfig
    {
        public List<RallySlackMapping> RallySlackMappings { get; set; }
        public RallyGlobalConfiguration GlobalConfig { get; set; }
    }
}