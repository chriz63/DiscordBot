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

using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using DiscordBotConsole.ApiRequests;
using DiscordBotConsole.Commands.Models;


namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>WeatherCommands</c> includes commands for weather informations
    /// </summary>
    [Group("weather")]
    [Description("Commands for informations about weather")]
    public class WeatherCommands : BaseCommandModule
    {
        public IConfiguration Configuration { get; set; }
        private DiscordEmbedBuilder embed;

        /// <summary>
        /// Task <c>City</c> sends an DiscordEmbed with current informations about weather from given city
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        [Command("city")]
        [Description("Displays weather informations for the given city.\n\n" + 
            "Usage: !weather city <city>")]
        public async Task City(CommandContext ctx, [RemainingText] string city)
        {
            await ctx.Channel.TriggerTypingAsync();

            var cityName = city.Replace(" ", "+");
            var nominatimUrl = $"https://nominatim.openstreetmap.org/search.php?q={cityName}&format=jsonv2";

            JsonApi<NominatimModel> jsonApi = new JsonApi<NominatimModel>();
            NominatimModel[] nominatimData = await jsonApi.GetJsonArray(nominatimUrl);

            var owmUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={nominatimData[0].lat}&lon={nominatimData[0].lon}&units=metric&appid={Configuration.GetRequiredSection("ApiKeys:OpenWeatherMap").Value}";

            JsonApi<WeatherModel> jsonApi1 = new JsonApi<WeatherModel>();
            WeatherModel weatherData = await jsonApi1.GetJson(owmUrl);

            embed = new DiscordEmbedBuilder()
            {
                Color = new DiscordColor(DiscordColor.Aquamarine.ToString()),
                Title = $"Weather in {weatherData.name} {weatherData.sys.country}"
            };
            
            embed.AddField("Current temperature", weatherData.main.temp.ToString() + " °C");
            embed.AddField("Minimum temperature", weatherData.main.temp_min.ToString() + " °C");
            embed.AddField("Maxmimum temperature", weatherData.main.temp_max.ToString() + " °C");
            embed.AddField("Weather status", weatherData.weather[0].main);
            embed.AddField("Weather description", weatherData.weather[0].description);
            embed.AddField("Air pressure", weatherData.main.pressure.ToString() + " hPa");
            embed.AddField("Air humidity", weatherData.main.humidity.ToString() + " %");
            embed.AddField("Wind speed", weatherData.wind.speed.ToString() + " m/s");
            embed.AddField("Wind destination", weatherData.wind.deg.ToString() + " °");

            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
