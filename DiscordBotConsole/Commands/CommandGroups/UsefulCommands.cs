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

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DiscordBotConsole.ApiRequests;
using DiscordBotConsole.Commands.Models;
using DSharpPlus.Entities;
using System.Reflection.Metadata;

namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>UsfulCommands</c> includes useful commands
    /// </summary>
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
        [Description("Send the Gas Stations with current prices in 3 km radius to a channel.\n\n" + 
            "Usage: !useful gasolineprice <city> or !useful gp <city>")]
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

        /// <summary>
        /// Task <c>Url</c> sends a via cutt.ly shortenend URL to a Channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [Command("url")]
        [Description("Sends a via cutt.ly shortenend URL to a Channel.\n\n" + 
            "Usage: !useful url <url>")]
        public async Task Url(CommandContext ctx, string url)
        {
            var cuttlyApiUrl = $"https://cutt.ly/api/api.php?key={Configuration.GetRequiredSection("ApiKeys:Cuttly").Value}&short={url}";

            JsonApi<CuttlyModel> cuttlyApi = new JsonApi<CuttlyModel>();
            CuttlyModel cuttlyData = await cuttlyApi.GetJson(cuttlyApiUrl);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = $"Your shortenend URL to {cuttlyData.url.title}",
                Description = $"Created at: {cuttlyData.url.date}",
            };

            embed.AddField("Short URL", cuttlyData.url.shortLink);
            embed.AddField("Full URL", cuttlyData.url.fullLink);

            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
