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

using Newtonsoft.Json;
using System.Collections.Generic;

namespace DiscordBotConsole.Commands.Models
{
    public class SongByArtistModel
    {
        public Toptracks toptracks { get; set; }
    }

    public class Toptracks
    {
        public List<TrackByArtist> track { get; set; }

        [JsonProperty("@attr")]
        public Attr Attr { get; set; }
    }

    public class TrackByArtist
    {
        public string name { get; set; }
        public string duration { get; set; }
        public string listeners { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public string streamable { get; set; }
        public ArtistByArtist artist { get; set; }
        public List<ImageByArtist> image { get; set; }

        [JsonProperty("@attr")]
        public AttrByArtist Attr { get; set; }
    }

    public class ArtistByArtist
    {
        public string name { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
    }

    public class AttrByArtist
    {
        public string rank { get; set; }
        public string country { get; set; }
        public string page { get; set; }
        public string perPage { get; set; }
        public string totalPages { get; set; }
        public string total { get; set; }
    }

    public class ImageByArtist
    {
        [JsonProperty("#text")]
        public string Text { get; set; }
        public string size { get; set; }
    }
}
