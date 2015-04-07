using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RallyCat.Core.Interfaces;
using RallyCat.Core.Rally;
using IDataReader = FluentData.IDataReader;

namespace RallyCat.Core.DataAccess
{
    public class RallySlackMappingRepository : IRallySlackMappingRepository
    {
        public Result<List<RallySlackMapping>> GetAll()
        {
            Result<List<RallySlackMapping>> result = new Result<List<RallySlackMapping>>();
            using (var context = RallyCatDbContext.QueryDb())
            {
                var item = context.Sql(@"select * from RallyConfigurations").QueryMany<RallySlackMapping>((RallySlackMapping o, IDataReader r) =>
                {
                    o.Id = (int) r["Id"];
                    o.TeamName = (string) r["TeamName"];
                    o.UserName = (string) r["UserName"];
                    o.Password = (string) r["Password"];
                    o.ProjectId = (long) r["ProjectId"];
                    o.WorkspaceId = (long) r["WorkspaceId"];
                    o.KanbanSortColumn = (string) r["KanbanSortColumn"];
                    o.EnableKanban = (bool)r["EnableKanban"];
                    o.Channels = ((string) r["Channels"]).Split(',').ToList();
                });

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
