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

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;

namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>AdminCommands</c> includes all commands available for admins
    /// </summary>
    [Group("admin")]
    [Description("Administrative commands")]
    [Hidden]
    [RequirePermissions(Permissions.ManageGuild)]
    public class AdminCommands : BaseCommandModule
    {
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Task <c>Clear</c> clears all or the given number of the messages in current channel
        /// and send's back a simple string
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        [Command("clear")]
        [Description("Clears the chat, if a number is given after command it clears only the range from the given number.\n\n" +
            "Usage: !admin clear or !admin clear <range>")]
        public async Task Clear(CommandContext ctx, params int[] range)
        {
            int rangeToDelete = 0;

            if (range.Length != 0)
            {
                rangeToDelete = range[0];
            }
            else
            {
                rangeToDelete = 100000;
            }
            var messages = ctx.Channel.GetMessagesAsync(rangeToDelete);
            foreach (var message in await messages)
            {
                await ctx.Channel.DeleteMessageAsync(message);
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// Task <c>Spammer</c> sends a specific ammount of messages to a channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="amount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Command("spammer")]
        [Description("Sends a amount of messages to a channel.\n\n" +
            "Usage: !admin spammer <amount> <message>")]
        public async Task Spammer(CommandContext ctx, int amount, [RemainingText] string message)
        {
            await ctx.TriggerTypingAsync();
            for (int i = 0; i < amount; i++)
            {
                //await ctx.RespondAsync(message);
                await ctx.Channel.SendMessageAsync(message);
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// Task <c>UserList</c> gets all users from the guild with their statuses
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("userstatuslist")]
        [Aliases("usl")]
        [Description("Gets all users from the guild with their statuses.\n\n" +
            "Usage: !admin userstatuslist or !admin usl")]
        public async Task UserStatusList(CommandContext ctx)
        {
            var onlineEmoji = DiscordEmoji.FromName(ctx.Client, ":green_circle:");
            var offlineEmoji = DiscordEmoji.FromName(ctx.Client, ":red_circle:");

            DiscordEmbedBuilder embedOfflineUsers = new DiscordEmbedBuilder()
            {
                Title = $"{offlineEmoji} User Status Offline List {offlineEmoji}"
            };

            DiscordEmbedBuilder embedOnlineUsers = new DiscordEmbedBuilder()
            {
                Title = $"{onlineEmoji} User Status Online List {onlineEmoji}"
            };

            foreach (var member in await ctx.Guild.GetAllMembersAsync())
            {
                if (member.Presence == null)
                {
                    embedOfflineUsers.AddField(member.DisplayName, "Offline");
                }
                else
                {
                    embedOnlineUsers.AddField(member.DisplayName, member.Presence.Status.ToString());
                }
            }
            await ctx.Channel.SendMessageAsync(embedOnlineUsers);
            await ctx.Channel.SendMessageAsync(embedOfflineUsers);
        }

        /// <summary>
        /// Task <c>GrantRole</c> Grants a role to a specific user
        /// </summary>
        /// <param name="ctx">CommandContext</param>
        /// <param name="discordUser">DiscordUser</param>
        /// <param name="discordRole">DiscordRole</param>
        /// <returns></returns>
        [Command("grantrole")]
        public async Task GrantRole(CommandContext ctx, DiscordMember discordUser, DiscordRole discordRole)
        {
            var embed = new DiscordEmbedBuilder();

            try
            {
                await discordUser.GrantRoleAsync(discordRole);
                embed.AddField("Success", $"{discordUser.Username} successfully granted to {discordRole.Name}");
            }
            catch
            {
                embed.AddField("Error", $"{discordUser.Username} could not granted to {discordRole.Name}");
            }
            await ctx.Channel.SendMessageAsync(embed);
        }

        /// <summary>
        /// Task <c>RevokeRole</c> Revokes a role from a specific user
        /// </summary>
        /// <param name="ctx" > CommandContext </ param >
        /// <param name="discordUser">DiscordUser</param>
        /// <param name="discordRole">DiscordRole</param>
        /// <returns></returns>
        [Command("revokerole")]
        public async Task RevokeRole(CommandContext ctx, DiscordMember discordUser, DiscordRole discordRole)
        {
            var embed = new DiscordEmbedBuilder();

            try
            {
                await discordUser.RevokeRoleAsync(discordRole);
                embed.AddField("Success", $"{discordUser.Username} successfully revoked from {discordRole.Name}");
            }
            catch
            {
                embed.AddField("Error", $"{discordUser.Username} could not revoked from {discordRole.Name}");
            }
            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
