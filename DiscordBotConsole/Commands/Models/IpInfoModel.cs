using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotConsole.Commands.Models
{
    public class IpInfoModel
    {
        public string ip { get; set; }
        public string hostname { get; set; }
        public bool anycast { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
    }

}
