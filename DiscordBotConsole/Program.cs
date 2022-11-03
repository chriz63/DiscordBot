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
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using DiscordBot2Console.Formatters;

namespace DiscordBotConsole
{
    /// <summary>
    /// The main class of DiscordBot
    /// </summary>
    internal class Program
    {

        public IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
            .Build();

        public readonly EventId BotEventId = new EventId(1, "DiscordBot");
        
        public DiscordClient Client { get; set; }

        public ServiceCollection Services { get; set; }

        public CommandsNextExtension CommandsNext { get; set; }

        static void Main(string[] args)
        {
            var prog = new Program();
            prog.RunBotAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Task <c>RunBotAsync</c> runs the bot in asynchronous way
        /// </summary>
        public async Task RunBotAsync()
        {
            // Configure the Discord Client
            this.Client = new DiscordClient(new DiscordConfiguration()
            {
                Token = Configuration.GetRequiredSection("Settings:Token").Value,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All
            });

            // Add dependency injection
            this.Services = new ServiceCollection();

            // Configure the Commands
            this.CommandsNext = this.Client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { Configuration.GetRequiredSection("Settings:CommandPrefix").Value },
            });

            // Hook some events to see whats going on
            this.Client.Ready += this.Client_Ready;
            this.Client.GuildAvailable += this.Client_GuildAvailable;
            this.Client.ClientErrored += this.Client_ClientError;
            this.CommandsNext.CommandExecuted += this.Commands_CommandExecuted;
            this.CommandsNext.CommandErrored += this.Commands_CommandErrored;

            // set the help formatter
            this.CommandsNext.SetHelpFormatter<HelpFormatter>();

            // register the available commands

            await this.Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, "Client is ready to process events.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task <c>Client_GuildAvailable</c> logs if the guild is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>The successfully completed Task</returns>
        private Task Client_GuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task <c>Client_ClientError</c> logs if there are errors in client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>The successfully completed Task</returns>
        private Task Client_ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            sender.Logger.LogError(BotEventId, e.Exception, "Exeption occured");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task <c>Commands_CommandExecuted</c> logs if a command is successful executed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>The successfully completed Task</returns>
        private Task Commands_CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            e.Context.Client.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}' command");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task <c>Commands_CommandErrored</c> logs if a command is errored
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            // let's check if the error is a result of lack
            // of required permissions
            if (e.Exception is ChecksFailedException ex)
            {
                // yes, the user lacks required permissions, 
                // let them know

                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                // let's wrap the response into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} You do not have the permissions required to execute this command.",
                    Color = new DiscordColor(0xFF0000) // red
                };
                await e.Context.RespondAsync(embed);
            }
        }
    }
}
