using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;
using RallyCat.Core.Configuration;
using RallyCat.Core.Interfaces;

namespace RallyCat.Core.DataAccess
{
    public class RallyGlobalConfigurationRepository : IRallyGlobalConfigurationRepository
    {
        public Result<RallyGlobalConfiguration> GetItem()
        {
            Result<RallyGlobalConfiguration> result = new Result<RallyGlobalConfiguration>();
            using (var context = RallyCatDbContext.QueryDb())
            {
                var item = context.Sql(@"GlobalVariables_FetchAll_AsPivot").CommandType(DbCommandTypes.StoredProcedure).QuerySingle<RallyGlobalConfiguration>();
                if (item == null)
                {
                    result.Success = false;
                    result.Object = null;
                    return result;
                }
                result.Object = item;
                result.Success = true;
            }
            return result;
        }
    }
}
