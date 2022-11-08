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

using DSharpPlus;
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
    [Group("info")]
    public class InfoCommands : BaseCommandModule
    {
        public IConfiguration Configuration { get; set; }
        
        [Command("ip")]
        public async Task Ip(CommandContext ctx, string ip)
        {
            //await ctx.TriggerTypingAsync();
            var ipInfoApiUrl = $"https://ipinfo.io/{ip}?token={Configuration.GetRequiredSection("ApiKeys:IpInfoIo").Value}";

            JsonApi<IpInfoModel> ipInfoApi = new JsonApi<IpInfoModel>();
            IpInfoModel ipInfoData = await ipInfoApi.GetJson(ipInfoApiUrl);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = $"IP Details for {ip}"
            };

            try
            {
                embed.AddField("IP", ipInfoData.ip);
                embed.AddField("Hostname", ipInfoData.hostname);
                embed.AddField("Geolocation", ipInfoData.loc);
                embed.AddField("City", ipInfoData.city);
                embed.AddField("Region", ipInfoData.region);
                embed.AddField("Country", ipInfoData.country);
                embed.AddField("Organisation", ipInfoData.org);
            }
            catch (Exception ex)
            {
                embed.AddField("Error", "No details found for this IP Address\n"
                    + "Check the IP Address and try again");
            }

            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
