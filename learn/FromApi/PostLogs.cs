using learn.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace learn.FromApi
{
    public class PostLogs
    {
        //public static async Task AddLogs(Log log)
        //{
        //    var json = JsonSerializer.Serialize(log);
        //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://recipierestapi20200828170014.azurewebsites.net/");

        //    var resp2 = await client.PatchAsync("api/recipies/AddLog/log/"+json,null);
        //    var aaa = resp2.Content;
        //    string result = await aaa.ReadAsStringAsync();
        //}

        public static Task AddLogsAsync(Log log)
        {
            //try { 

            //    using (var client = new HttpClient())
            //    {

            //        using (var request = new HttpRequestMessage(HttpMethod.Put, "https://recipierestapi20200828170014.azurewebsites.net/api/recipies/AddLog/log"))
            //        {
            //            var json = JsonConvert.SerializeObject(log);
            //            using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            //            {
            //                request.Content = stringContent;

            //                using (var response = await client
            //                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
            //                    .ConfigureAwait(false))
            //                {
            //                    response.EnsureSuccessStatusCode();
            //                }
            //            }
            //        }


            //        //var apiUrl = "https://recipierestapi20200828170014.azurewebsites.net/api/recipies/AddLog/log";
            //        //client.BaseAddress = new Uri("https://recipierestapi20200828170014.azurewebsites.net/api/recipies");
            //        //client.DefaultRequestHeaders.Accept.Clear();
            //        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        var dataAsString = JsonStringReader.SerializeObject(log);
            //        //var dataContent = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            //        //HttpResponseMessage response = await client.PutAsync(apiUrl, dataContent);

            //    }


            //    }
            //    catch(Exception ex)
            //    {

            //    }
            //}








            using (HttpClient client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(log);
                var sender = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(sender);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var url = "https://recipierestapi20200828170014.azurewebsites.net/api/recipies/AddLog/log";
                var result = client.PutAsync(url, byteContent).Result;

                return null;

            }
            



        }
    }
}