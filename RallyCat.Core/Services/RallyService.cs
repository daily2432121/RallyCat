using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using FluentData;
using Rally.RestApi;
using Rally.RestApi.Response;
using RallyCat.Core.Rally;
using RallyApi = Rally.RestApi;
namespace RallyCat.Core.Services
{
    public class RallyService
    {
        private RallyApiConnectionPool _pool;

        public RallyService()
        {
            _pool = new RallyApiConnectionPool();
        }

        

        public QueryResult GetRallyItem(RallySlackMapping map, string formattedId)
        {
            
            var api = _pool.GetApi(map.UserName, map.Password);
            if (api == null)
            {
                throw new AuthenticationException("Cannot verify rally login");
            }
            Request request = new Request("hierarchicalrequirement");
            request.Project = "/project/" + map.ProjectId;
            request.Workspace = "/workspace/" + map.WorkspaceId;
            request.Fetch = new List<string>() { "Name", "Description", "FormattedID" };
            request.Query = new Query("FormattedID", Query.Operator.Equals, formattedId);
            QueryResult queryResult = api.Query(request);
            return queryResult;

        }

    }
}
