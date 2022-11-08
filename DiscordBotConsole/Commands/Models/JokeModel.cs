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
    public class JokeApiModel
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string delivery { get; set; }
        public Flags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }
    public class Flags
    {
        public bool nsfw { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool @explicit { get; set; }
    }
}

    
