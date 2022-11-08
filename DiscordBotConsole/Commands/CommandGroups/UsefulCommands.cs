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

namespace DiscordBotConsole.Commands.CommandGroups
{
    [Group("useful")]
    [Description("This Group includes useful commands i.e. gasoline prices")]
    public class UsefulCommands : BaseCommandModule
    {
        public IConfiguration Configuration;

        /*
         * Command GasPrices is currently not working, work in progress
         */

        //[Command("gasolineprice")]
        //[Aliases("gp")]
        public async Task GasPrices(CommandContext ctx, int radius, [RemainingText] string city)
        {
            var cityName = city.Replace(" ", "+");
            var nominatimUrl = $"https://nominatim.openstreetmap.org/search.php?q={cityName}&format=jsonv2";

            JsonApi<NominatimModel> nominatimApi = new JsonApi<NominatimModel>();
            NominatimModel[] nominatimData = await nominatimApi.GetJsonArray(nominatimUrl);

            var testUrl = "https://creativecommons.tankerkoenig.de/json/list.php?lat=52.521&lng=13.438&rad=1.5&sort=dist&type=all&apikey=00000000-0000-0000-0000-000000000002";
            
            
            var tankerKoenigUrl = $"https://creativecommons.tankerkoenig.de/json/list.php?lat={nominatimData[0].lat}&lng={nominatimData[0].lon}&rad={radius}&sort=dist&type=all&apikey={Configuration.GetRequiredSection("ApiKeys:TankerKoenig").Value}";

            Console.WriteLine(tankerKoenigUrl);

            JsonApi<TankerKoenigModel> tankerKonigApi = new JsonApi<TankerKoenigModel>();
            TankerKoenigModel tankerKoenigData = await tankerKonigApi.GetJson(testUrl);

            foreach (var station in tankerKoenigData.stations)
            {
                Console.WriteLine(station);
            }

            
        }

    }
}
