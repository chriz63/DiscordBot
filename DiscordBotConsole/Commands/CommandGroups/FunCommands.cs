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

using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DiscordBotConsole.ApiRequests;
using DiscordBotConsole.Commands.Models;
using DSharpPlus.CommandsNext.Attributes;

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Reflection.Metadata;

/// TODO: Better argument handling in GIF command

namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>FunCommands</c> includes some funny commands
    /// </summary>
    [Group("fun")]
    [Description("Funny commands")]
    public class FunCommands : BaseCommandModule
    {
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Task <c>Penis</c> sends a users penis size from random length
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="discordUser"></param>
        /// <returns></returns>
        [Command("penis")]
        [Description("Sends a users penis size from random length.\n\n" + 
            "Usage: !fun penis or !fun penis <username>")]
        public async Task Penis(CommandContext ctx, DiscordMember discordUser = null)
        {
            await ctx.TriggerTypingAsync();

            DiscordMember user = discordUser ?? ctx.Member;

            string length = new string('=', new Random(user.Id.GetHashCode()).Next(0, 20));

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = "Peepee size machine",
                Color = DiscordColor.Yellow,
                Description = $"<@{user.Id}>'s penis:\n8{length}D"
            };
            
            await ctx.RespondAsync(embed);
        }

        /// <summary>
        /// Task <c>Joke</c> sends a joke in german or english to the channel
        /// </summary> 
        /// <param name="ctx"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [Command("joke")]
        [Description("Sends a joke in german or english to the channel.\n" + "Available languages are: <de> or <en>\n\n" + 
            "Usage: !fun joke or !fun joke <language>")]
        public async Task Joke(CommandContext ctx, string language = null)
        {
            string jokeApiUrl = null;

            await ctx.TriggerTypingAsync();

            // german jokes
            if (language == null || language == "de" || language == "deutsch" || language == "german")
            {
                jokeApiUrl = "https://v2.jokeapi.dev/joke/Any?lang=de&type=twopart";
            }
            // english jokes
            else if (language == "en" || language == "english" || language == "englisch")
            {
                jokeApiUrl = "https://v2.jokeapi.dev/joke/Any?lang=en&type=twopart";
            }

            JsonApi<JokeApiModel> jokeJson = new JsonApi<JokeApiModel>();
            JokeApiModel jokeData = await jokeJson.GetJson(jokeApiUrl);

            var laughingEmoji = DiscordEmoji.FromName(ctx.Client, ":joy:");

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = "Your joke",
                Description = $"{jokeData.setup} \n\n{jokeData.delivery} {laughingEmoji}"
            };

            await ctx.Channel.SendMessageAsync(embed);
        }

        /// <summary>
        /// Task <c>Gif</c> sends a random GIF by category to a channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [Command("gif")]
        [Description("Sends a random GIF by category to a channel.\n" + " Available sizes are: <small> or <medium>\n\n" +
            "Usage: !fun gif <size> <category>")]
        public async Task Gif(CommandContext ctx, string size,[RemainingText] string category)
        {
            var tenorApiUrl = $"https://g.tenor.com/v1/random?q={category}&key={Configuration.GetRequiredSection("ApiKeys:Tenor").Value}&limit=1";

            Console.WriteLine(tenorApiUrl);

            JsonApi<TenorModel> tenorApi = new JsonApi<TenorModel>();
            TenorModel tenorData = await tenorApi.GetJson(tenorApiUrl);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            if (size == "small")
            {
                embed.WithImageUrl(tenorData.results[0].media[0].tinygif.url);
            }
            else if (size == "medium")
            {
                embed.WithImageUrl(tenorData.results[0].media[0].mediumgif.url);
            }

            embed.WithFooter("Via Tenor", "https://www.gstatic.com/tenor/web/attribution/via_tenor_logo_white.png");

            await ctx.Channel.SendMessageAsync(embed);
        }

        /// <summary>
        /// Task <c>Moviel</c> sends a random movie from IMDB Top 250 to a channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("movie")]
        [Description("Sends a random movie from IMDB Top 250 to a channel.\n\n" + 
            "Usage: !fun movie")]
        public async Task Movie(CommandContext ctx)
        {
            var imdbApiUrl = $"https://imdb-api.com/de/API/Top250Movies/{Configuration.GetRequiredSection("ApiKeys:IMDB").Value}";

            JsonApi<IMDBMovieModel> imdbApi = new JsonApi<IMDBMovieModel>();
            IMDBMovieModel imdbData = await imdbApi.GetJson(imdbApiUrl);

            Random random = new Random();
            int index = random.Next(imdbData.items.Count);

            var movie = imdbData.items[index];

            string[] crew = movie.crew.Split(",");

            foreach (var crewMember in crew)
            {
                crewMember.Replace(" ", "");
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = movie.fullTitle,
                Description = $"IMDB Rating: {movie.imDbRating}",
                ImageUrl = movie.image,
            };

            foreach (var crewMember in crew)
            {
                embed.AddField("Crew", crewMember);
            }

            await ctx.Channel.SendMessageAsync(embed);
        }

        /// <summary>
        /// Task <c>Song</c> sends a random song from LastFM to a channel
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("song")]
        [Description("Sends a random song from LastFM to a channel")]
        public async Task Song(CommandContext ctx)
        {
            var lastFmApiUrl = $"http://ws.audioscrobbler.com/2.0/?method=geo.gettoptracks&country=germany&api_key={Configuration.GetRequiredSection("ApiKeys:LastFM").Value}&format=json";
            
            JsonApi<SongModel> lastFmApi = new JsonApi<SongModel>();
            SongModel lastFmData = await lastFmApi.GetJson(lastFmApiUrl);

            Random random = new Random();
            int index = random.Next(lastFmData.tracks.track.Count);

            var song = lastFmData.tracks.track[index];
            var imageList = song.image;
            Image image = new Image();

            foreach (var img in imageList)
            {
                if (img.size == "large")
                {
                    image = img;
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = song.name,
                Description = song.artist.name,
                ImageUrl = image.Text
            };

            embed.AddField("Listeners", song.listeners);
            embed.AddField("LastFM URL", song.url);

            await ctx.Channel.SendMessageAsync(embed);
        }
    }
}
