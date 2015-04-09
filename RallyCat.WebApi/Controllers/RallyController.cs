using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using FluentData;
using RallyCat.Core;
using RallyCat.Core.DataAccess;
using RallyCat.Core.Services;
using RallyCat.WebApi.Models.Slack;
using RallyCat.WebApi.ViewModels;

namespace RallyCat.WebApi.Controllers
{
    public class RallyController : ApiController
    {
        //
        // GET: /Rally/
        public IDbContext _dbContext;
        public RallyController()
        {
            RallyCatDbContext.SetConnectionString("RallyCatConnection");
            _dbContext = RallyCatDbContext.QueryDb();
            RallyBackgroundData.SetDbContext(_dbContext);

        }
        [Route("api/Rally/Details")]
        [HttpPost]
        public async Task<SlackResponseVM> Details()
        {
            string input = await Request.Content.ReadAsStringAsync();

            string str = input;

            SlackMessage msg = SlackMessage.FromString(str);
            msg.MessageType = SlackMessageType.OutgoingWebhooks;
            if (str.ToLower().Contains("kanban"))
            {
                //return GetKanban2(msg.ChannelName);
                return new SlackResponseVM("Not supported yet.");
            }
            Regex regex = new Regex(@"((US|Us|uS|us)\d{1,9})|(((dE|de|De|DE)\d{1,9}))");
            Match m = regex.Match(msg.Text);

            if (!m.Success)
            {
                return new SlackResponseVM("_Whuaaat?_" );
            }

            string formattedId = m.Groups[0].Value;
            string result = GetItem(formattedId, msg.ChannelName);
            return new SlackResponseVM (result);
        }

        private string GetItem(string formattedId, string channelName)
        {
            var mappings = RallyBackgroundData.Instance.RallySlackMappings;
            var map = mappings.Find(o => o.Channels.Contains(channelName.ToLower()));
            if (map == null)
            {
                throw new ObjectNotFoundException("Cannot found channel name mapping for " + channelName);
            }
            RallyService service = new RallyService();
            var result = service.GetRallyItem(map, formattedId);
            var item = result.Results.FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            string itemName = (string)item["Name"];
            string itemDescription = ((string)item["Description"]).HtmlToPlainText();
            return "_" + GetWelcomeMsg() + "_" + "\r\n\r\n" + "*" + itemName.ToUpper() + "*\r\n" + "*" + itemName + "*" + "\r\n" + itemDescription;

        }

        private string GetWelcomeMsg()
        {
            List<string> welcomes = new List<string> { "how can I help all of you slackers?", "you called?", "Wassup?", "I think I heard my name", "Yes?", "At your service" };
            Random r = new Random((int)DateTime.Now.Ticks);
            return welcomes[r.Next(0, welcomes.Count - 1)];
        }
    }
}
