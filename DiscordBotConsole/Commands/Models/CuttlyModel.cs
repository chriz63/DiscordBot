using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotConsole.Commands.Models
{
    public class CuttlyModel
    {
        public Url url { get; set; }
    }

    public class Url
    {
        public int status { get; set; }
        public string fullLink { get; set; }
        public string date { get; set; }
        public string shortLink { get; set; }
        public string title { get; set; }
    }


}
