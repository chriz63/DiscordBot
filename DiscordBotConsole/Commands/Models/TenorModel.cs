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
    public class TenorModel
    {
        public List<Result> results { get; set; }
        public string next { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content_description { get; set; }
        public string content_rating { get; set; }
        public string h1_title { get; set; }
        public List<Media> media { get; set; }
        public string bg_color { get; set; }
        public double created { get; set; }
        public string itemurl { get; set; }
        public string url { get; set; }
        public List<object> tags { get; set; }
        public List<object> flags { get; set; }
        public int shares { get; set; }
        public bool hasaudio { get; set; }
        public bool hascaption { get; set; }
        public string source_id { get; set; }
        public object composite { get; set; }
    }

    public class Media
    {
        public Mp4 mp4 { get; set; }
        public Tinymp4 tinymp4 { get; set; }
        public Loopedmp4 loopedmp4 { get; set; }
        public Nanogif nanogif { get; set; }
        public Nanomp4 nanomp4 { get; set; }
        public Gif gif { get; set; }
        public Nanowebm nanowebm { get; set; }
        public Tinywebm tinywebm { get; set; }
        public Tinygif tinygif { get; set; }
        public Webm webm { get; set; }
        public Mediumgif mediumgif { get; set; }
    }

    public class Gif
    {
        public string preview { get; set; }
        public List<int> dims { get; set; }
        public int size { get; set; }
        public string url { get; set; }
    }

    public class Loopedmp4
    {
        public int size { get; set; }
        public double duration { get; set; }
        public List<int> dims { get; set; }
        public string url { get; set; }
        public string preview { get; set; }
    }

    public class Mediumgif
    {
        public List<int> dims { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string preview { get; set; }
    }

    public class Mp4
    {
        public int size { get; set; }
        public string preview { get; set; }
        public double duration { get; set; }
        public List<int> dims { get; set; }
        public string url { get; set; }
    }

    public class Nanogif
    {
        public int size { get; set; }
        public string url { get; set; }
        public List<int> dims { get; set; }
        public string preview { get; set; }
    }

    public class Nanomp4
    {
        public string url { get; set; }
        public List<int> dims { get; set; }
        public double duration { get; set; }
        public int size { get; set; }
        public string preview { get; set; }
    }

    public class Nanowebm
    {
        public int size { get; set; }
        public string url { get; set; }
        public string preview { get; set; }
        public List<int> dims { get; set; }
    }

    

    

    public class Tinygif
    {
        public List<int> dims { get; set; }
        public string url { get; set; }
        public string preview { get; set; }
        public int size { get; set; }
    }

    public class Tinymp4
    {
        public string preview { get; set; }
        public double duration { get; set; }
        public List<int> dims { get; set; }
        public string url { get; set; }
        public int size { get; set; }
    }

    public class Tinywebm
    {
        public int size { get; set; }
        public List<int> dims { get; set; }
        public string url { get; set; }
        public string preview { get; set; }
    }

    public class Webm
    {
        public string preview { get; set; }
        public List<int> dims { get; set; }
        public int size { get; set; }
        public string url { get; set; }
    }



}
