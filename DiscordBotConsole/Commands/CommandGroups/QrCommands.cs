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

using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronBarCode;
using Microsoft.Extensions.Configuration;
using DSharpPlus.Entities;

namespace DiscordBotConsole.Commands.CommandGroups
{
    /// <summary>
    /// Class <c>QrCommands</c> includes functions to generate Qr Codes
    /// </summary>
    [Group("qr")]
    [Description("Commands to generate Qr Codes")]
    public class QrCommands : BaseCommandModule
    {
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Task <c>Generate</c> generates a Qr Code with user specified content
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [Command("generate")]
        [Aliases("gen")]
        [Description("Generates a Qr Code with user specified content")]
        public async Task Generate(CommandContext ctx, [RemainingText] string content)
        {
            string QrCodePath = Configuration.GetRequiredSection("QrCode:Path").Value + "QrCode.png";

            await ctx.Channel.TriggerTypingAsync();

            QRCodeWriter.CreateQrCode(content, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium)
                .SaveAsPng(QrCodePath);

            using (var fileStream = new FileStream(QrCodePath, FileMode.Open, FileAccess.Read))
            {
                var message = await new DiscordMessageBuilder()
                    .WithFiles(new Dictionary<string, Stream>() { { QrCodePath, fileStream } })
                    .SendAsync(ctx.Channel);
            }
        }

    }
}
