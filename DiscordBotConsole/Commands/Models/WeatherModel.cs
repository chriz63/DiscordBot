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
    public class WeatherModel
    {
        public Coord coord;
        public List<Weather> weather;
        public string @base;
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public int dt;
        public Sys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }

    public class Clouds
    {
        public int all;
    }

    public class Coord
    {
        public double lon;
        public double lat;
    }

    public class Main
    {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public int pressure;
        public int humidity;
    }

    public class Sys
    {
        public int type;
        public int id;
        public string country;
        public int sunrise;
        public int sunset;
    }

    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    public class Wind
    {
        public double speed;
        public int deg;
    }
}
