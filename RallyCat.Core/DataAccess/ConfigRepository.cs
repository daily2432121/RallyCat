using RallyCat.Core.Rally;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RallyCat.Core.DataAccess
{
    public class ConfigRepository
    {
        public AllConfig GetConfig(string filePath)
        {
            return JsonConvert.DeserializeObject<AllConfig>(File.ReadAllText(filePath+@"\config.json"));
        }
    }
}
