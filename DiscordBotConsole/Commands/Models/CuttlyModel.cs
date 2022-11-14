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
