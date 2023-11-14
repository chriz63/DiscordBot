/*
 * THIS FILE IS A PART OF CHRIZ63'S DISCORD BOT
 * 
 * Copyright 2022 Chriz63
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, 
 * distributed under the License is distributed on an "AS IS" BASIS
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied
 * See the License for the specific language governing permissions 
 * limitations under the License.
 */

using System.Collections.Generic;

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
        public double? diesel { get; set; }
        public double? e5 { get; set; }
        public double? e10 { get; set; }
        public bool isOpen { get; set; }
        public string houseNumber { get; set; }
        public int postCode { get; set; }
    }
}
