using learn.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace learn
{
    public class IngrSelection
    {
        public async static Task<List<Ingridient>> GetIngridientsFirst(IConfiguration config)
        {
            
            List<Ingridient> ingridient = new List<Ingridient>();
            List <Ingridient> ingridientClear = new List<Ingridient>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

           string skladnik = await client.GetAsync($"api/recipies/GetIngridientList").Result.Content.ReadAsStringAsync();

            ingridient =  System.Text.Json.JsonSerializer.Deserialize<List<Ingridient>>(skladnik);

            foreach (var i in ingridient)
            {
                i.name.Replace("<h2>", "").Replace("</h2>", "");
                ingridientClear.Add(i);
            }

            return ingridientClear;
        }











    }

}
