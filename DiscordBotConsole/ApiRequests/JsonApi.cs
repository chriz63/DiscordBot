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

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBotConsole.ApiRequests
{   
    /// <summary>
    /// Class <c>JsonApi</c> helps to parse Json from Web API's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonApi <T>
    {
        HttpClient client = new HttpClient();

        /// <summary>
        /// The Constructor adds Headers to the HttpClient
        /// </summary>
        public JsonApi()
        {
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6,ru;q=0.4");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate,sdch");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

        }

        /// <summary>
        /// Task <c>GetJsonArray</c> parsing a Json Request to an generic array
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T[]> GetJsonArray(string url)
        {
            var text = await client.GetStringAsync(url);

            T[] jsonArray = JsonConvert.DeserializeObject<T[]>(text);

            return jsonArray;
        }

        /// <summary>
        /// Task <c>GetJsonArray</c> parsing a generic Json Request to an generic object
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task <T> GetJson(string url)
        {
            var text = await client.GetStringAsync(url);

            T json = JsonConvert.DeserializeObject<T>(text);

            return json;
        }
    }
}
