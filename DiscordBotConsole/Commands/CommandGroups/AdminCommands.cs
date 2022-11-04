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
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
        [Description("Clears the chat, if a number is given after command it clears only the range from the given number.")]
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
        [Description("Sends a amount of messages to a channel.")]
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
    }
}
