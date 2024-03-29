﻿
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
    public class ForecastModel
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<ListForecast> list { get; set; }
        public CityForecast city { get; set; }
    }

    public class ListForecast
    {
        public int dt { get; set; }
        public MainForecast main { get; set; }
        public List<WeatherForecast> weather { get; set; }
        public CloudsForecast clouds { get; set; }
        public WindForecast wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public SysForecast sys { get; set; }
        public string dt_txt { get; set; }
        public RainForecast rain { get; set; }
    }

    public class CityForecast
    {
        public int id { get; set; }
        public string name { get; set; }
        public CoordForecast coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RainForecast
    {
        public double _3h { get; set; }
    }

    public class CoordForecast
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class CloudsForecast
    {
        public int all { get; set; }
    }

    public class MainForecast
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class SysForecast
    {
        public string pod { get; set; }
    }

    public class WeatherForecast
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class WindForecast
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }
}
