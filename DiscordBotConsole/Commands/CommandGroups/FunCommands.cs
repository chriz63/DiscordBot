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
using DSharpPlus.Entities;

using System;
using System.Threading.Tasks;

namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>FunCommands</c> includes some funny commands
    /// </summary>
    [Group("fun")]
    [Description("Funny commands")]
    public class FunCommands : BaseCommandModule
    {
        public DiscordEmbedBuilder embed;

        [Command("penis")]
        public async Task Penis(CommandContext ctx, DiscordMember discordUser = null)
        {
            await ctx.TriggerTypingAsync();

            DiscordMember user = discordUser ?? ctx.Member;

            string length = new string('=', new Random(user.Id.GetHashCode()).Next(0, 20));

            embed = new DiscordEmbedBuilder()
            {
                Title = "Peepee size machine",
                Color = DiscordColor.Yellow,
                Description = $"<@{user.Id}>'s penis:\n8{length}D"
            };
            
            await ctx.RespondAsync(embed);
        }
    }
}
