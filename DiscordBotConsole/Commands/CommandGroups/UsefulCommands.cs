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

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DiscordBotConsole.ApiRequests;
using DiscordBotConsole.Commands.Models;
using DSharpPlus.Entities;

namespace DiscordBotConsole.Commands.CommandGroups
{
    [Group("useful")]
    [Description("This Group includes useful commands i.e. gasoline prices")]
    public class UsefulCommands : BaseCommandModule
    {
        public IConfiguration Configuration;


        /// <summary>
        /// Task <c>GasolinePrices</c> send the Gas Stations with current prices in 3 km radius to a channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        [Command("gasolineprice")]
        [Aliases("gp")]
        [Description("Send the Gas Stations with current prices in 3 km radius to a channel")]
        public async Task GasolinePrices(CommandContext ctx, [RemainingText] string city)
        {
            // add a + between spaces in string for the api
            var cityName = city.Replace(" ", "+");
            var nominatimUrl = $"https://nominatim.openstreetmap.org/search.php?q={cityName}&format=jsonv2";

            JsonApi<NominatimModel> nominatimApi = new JsonApi<NominatimModel>();
            NominatimModel[] nominatimData = await nominatimApi.GetJsonArray(nominatimUrl);

            var tankerKoenigUrl = $"https://creativecommons.tankerkoenig.de/json/list.php?lat={nominatimData[0].lat}&lng={nominatimData[0].lon}&rad=3&sort=dist&type=all&apikey={Configuration.GetRequiredSection("ApiKeys:TankerKoenig").Value}";

            JsonApi<TankerKoenigModel> tankerKonigApi = new JsonApi<TankerKoenigModel>();
            TankerKoenigModel tankerKoenigData = await tankerKonigApi.GetJson(tankerKoenigUrl);

            foreach (var station in tankerKoenigData.stations)
            {
                Console.WriteLine(station.street);

                // Build a own embed for every gas station
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                {
                    Title = station.brand,
                    Description = $"{station.street} {station.houseNumber}, {station.postCode} {station.place}"
                };

                embed.AddField("Super", $"{station.e5.ToString()}€");
                embed.AddField("E10", $"{station.e10.ToString()}€");
                embed.AddField("Diesel", $"{station.diesel.ToString()}€");

                await ctx.Channel.SendMessageAsync(embed);
                await Task.Delay(1000);
            }
        }

    }
}
