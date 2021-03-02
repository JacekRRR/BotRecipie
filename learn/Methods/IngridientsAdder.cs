using learn.Objects;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace learn.Methods
{
    public class IngridientsAdder
    {
        public async static Task<bool> GetAllAdders(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<string> ingridients = turnContext.Activity.Text.Split('+').ToList();


                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"Ing").Result.Content.ReadAsStringAsync();

                dish = System.Text.Json.JsonSerializer.Deserialize<List<Dish>>(dania.Result).ToList();

                List<AddingItem> firstList = new List<AddingItem>();

               





                foreach (var item in dish)
                {
                    foreach (var item2 in item.ingridients)
                    {
                        foreach (var x in ingridients)
                        {
                            if (x != "")
                            {


                                if (item2.name.StartsWith(x))
                                {
                                    AddingItem iA = new AddingItem();
                                    iA.DishName = item.name;
                                    iA.index++;
                                    if (firstList.IndexOf(iA) == -1)
                                    {
                                        firstList.Add(iA);
                                    }
                                }
                            }
                        }
                    }

                }
                IEnumerable<AddingItem> topFiveItems = firstList.OrderByDescending(x => x.index).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();
                int skippedInt = 0;

                foreach (var item in topFiveItems)
                {
                    actions.Add(new CardAction() { Title = item.DishName, Type = ActionTypes.PostBack, Value = "1qq1_" + item.DishName + "_" + turnContext.Activity.Text });
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

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async static Task<bool> GetMatchedAndNotmachet(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {


            turnContext.Activity.Text = turnContext.Activity.Text.Replace(("1qq1_"), "");
            string dishName = turnContext.Activity.Text.Substring(0, turnContext.Activity.Text.LastIndexOf("_"));
            turnContext.Activity.Text = turnContext.Activity.Text.Remove(0, turnContext.Activity.Text.LastIndexOf('_') + 1);
            IEnumerable<string> ingridients = turnContext.Activity.Text.Split(';').ToList();

            List<Matched> MachedList = new List<Matched>();
            string Message = "";



            Dish dish = new Dish();
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var danie = client.GetAsync($"/api/recipies/GetDishByName/{dishName}").Result.Content.ReadAsStringAsync();

            dish = System.Text.Json.JsonSerializer.Deserialize<Dish>(danie.Result);
            var nonRepeat = dish.ingridients.Select(x => x.name).Distinct();
            List<string> noH2 = new List<string>();
            string j = "";

            foreach (var i in nonRepeat)
            {
                if ((i.Contains("<h2>")) || (i.Contains("</h2>")))
                {
                    j = i.Replace("<h2>", ("\n\n**")).Replace("</h2>", ("**\n\n")); ;
                    noH2.Add(j);
                }
                else
                {
                    j = i;
                    noH2.Add(j);
                }
                
            }

            foreach (var item2 in noH2)
            {
                foreach (var item3 in ingridients)
                {
                    if (item3 != "")
                    {
                        Matched matched = new Matched();

                        if (item2.StartsWith(item3))
                        {
                            matched.Name = item2;
                            matched.isMatched = true;
                            MachedList.Add(matched);

                        }
                        else
                        {
                            matched.Name = item2;
                            matched.isMatched = false;
                            MachedList.Add(matched);

                        }
                    }
                    
                }
               

            }

            foreach (var i in MachedList)
            {
                if (i.isMatched)
                {

                    Message +="**"+ i.Name+ "**\n\n";


                }
                else
                {
                    Message += i.Name + "\n\n";

                }
            }


            Attachment imp = new Attachment();
            imp.ContentType = "image/png";
            imp.ContentUrl = (dish.pictUrl);
            imp.Name = dish.name;
            var rep = MessageFactory.Attachment(imp);

            string listOfIng = string.Join("\n\n", dish.ingridients.Select(p => p.name).Distinct().ToList());
            listOfIng = listOfIng.Replace("<h2>", ("\n\n**")).Replace("</h2>", ("**\n\n"));

            
            await turnContext.SendActivityAsync(rep);
            await turnContext.SendActivityAsync((dish.name.ToString() + "\n\n " + "Czas przygotowania: " + dish.timeForPrepare.ToString() + "minut" + "\n" + "\n" + dish.description.ToString().Replace("  ", "") + "\n\n" + Message + "\n \n" + dish.directions1.ToString().Replace("<p style=\"text - align: justify;\">", " ")
                                                    + "\n\n" + dish.directions2.ToString().Replace("<p style=\"text - align: justify;\">"," ") + "\n\n" + dish.directions3.ToString().Replace("<p style=\"text - align: justify;\">", " ") + "\n\n" + dish.directions4.ToString().Replace("<p style=\"text - align: justify;\">", " ")));
            return true;
        }
        
    }

}


    

