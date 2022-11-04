using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
