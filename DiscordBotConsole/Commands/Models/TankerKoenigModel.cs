using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotConsole.Commands.Models
{
    public class TankerKoenigModel
    {
        public bool ok { get; set; }
        public string license { get; set; }
        public string data { get; set; }
        public string status { get; set; }
        public List<Station> stations { get; set; }
    }

    public class Station
    {
        public string id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string street { get; set; }
        public string place { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double dist { get; set; }
        public double diesel { get; set; }
        public double e5 { get; set; }
        public double e10 { get; set; }
        public bool isOpen { get; set; }
        public string houseNumber { get; set; }
        public int postCode { get; set; }
    }
}
