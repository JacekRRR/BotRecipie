using learn;
using learn.Objects;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace RecipieBot.FromApi
{
    public class FastAdding
    {
        public async static Task<bool> GetAddingFast(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            List<string> ingridients = turnContext.Activity.Text.Split('+').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            List <Ingridient> ListFromApi = new List<Ingridient>();
            ListFromApi = await IngrSelection.GetIngridientsFirst(config);
            var test = ListFromApi.Where(p => ingridients.Any(p2 => p.name.StartsWith(p2.ToString()))).Distinct().ToList();
            var ListaId = test.Select(x => x.id);

            IEnumerable<Dish> dishes = new List<Dish>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

             string dania = await client.GetAsync($"bping").Result.Content.ReadAsStringAsync();
             dishes = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania);

            List<string> listaDopasowań = new List<string>();
            var dish = dishes.Where(p => test.Any(p2 => p2.dishesId == p.id));
            IEnumerable<Dish> topFiveItems = dish.OrderByDescending(x => x.id).Take(5);

            var reply = MessageFactory.Text("");
            List<CardAction> actions = new List<CardAction>();
            int skippedInt = 0;

            foreach (var item in topFiveItems)
            {
                actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = "1qq1_" + item.name + "_" + turnContext.Activity.Text });
            }
            actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = +(skippedInt + 1) }));
            actions.Add((new CardAction() { Title = "Wróć", Type = ActionTypes.PostBack, Value = +(skippedInt + 1) }));
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = actions
            };
            if (topFiveItems.Count() > 0)
            {

                await turnContext.SendActivityAsync(reply, cancellationToken);
                return true;
            }
            else
            {
                return false;
            }


            //foreach(var i in dishes)
            //{
            //    foreach (var j in ListaId)
            //    {
            //        if (i.id == j)
            //            listaDopasowań.Add(i.name);
            //    }
            //}


        }
    }
}
