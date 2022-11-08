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
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;

using System.Collections.Generic;


namespace DiscordBotConsole.Formatters
{   
    /// <summary>
    /// Class <c>HelpFormatter</c> is the class including a HelpFormatter
    /// </summary>
    public class HelpFormatter : BaseHelpFormatter
    {

        protected DiscordEmbedBuilder Embed;

        public HelpFormatter(CommandContext ctx) : base(ctx)
        {
            Embed = new DiscordEmbedBuilder
            {
                Title = "Help",
                Description = "Here are listed all available commands and command groups.\n\n" +
                "You can get more informations to a command or a command group by typing\n\n" +
                "!help [Command] | i.e. !help help" +
                "\n\nor\n\n" +
                "!help [CommandGroup] | i.e. !help fun",
                Color = new DiscordColor(DiscordColor.Aquamarine.ToString()),
            };
            
        }

        /// <summary>
        /// BaseHelpFormatter <c>WithCommand</c> returns a HelpFormatter to a specific command or command group
        /// </summary>
        /// <param name="command"></param>
        /// <returns><c>HelpFormatter</c></returns>
        public override BaseHelpFormatter WithCommand(Command command)
        {
            if (command is CommandGroup)
            {
                Embed.WithDescription($"This is the help for all commands from !{command.Name}");
            }
            else
            {
                Embed.WithDescription($"This is the help for !{command.Name}");
            }
            Embed.AddField("Command: !" + command.Name, "Description: " + command.Description);
            return this;
        }

        /// <summary>
        /// BaseHelpFormatter <c>WithSubCommands</c> returns a HelpFormatter to all commands and command groups
        /// </summary>
        /// <param name="commands"></param>
        /// <returns><c>HelpFormatter</c></returns>
        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                // add ungrouped commands
                if (command is CommandGroup)
                {
                    
                    Embed.AddField($"CommandGroup: !{command.Name}", $"Description: {command.Description}");

                }
                // add grouped commands
                else
                {
                    Embed.AddField("Command: !" + command.Name, "Description: " + command.Description);
                }
            }
            return this;
        }

        /// <summary>
        /// CommandHelpMessage <c>Build</c> returns a CommandHelpMessage with the embed created before
        /// </summary>
        /// <returns><c>CommandHelpMessage</c></returns>
        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: Embed);
        }
    }
}
