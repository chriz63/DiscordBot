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
    public class IMDBMovieModel
    {
        public List<Item> items { get; set; }
        public string errorMessage { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string year { get; set; }
        public string image { get; set; }
        public string crew { get; set; }
        public string imDbRating { get; set; }
        public string imDbRatingCount { get; set; }
    }
}
