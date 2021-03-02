using learn.Objects;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace learn.FromApi
{
    public class DataFromApi
    {
        public async static Task <bool> GetDishByName(string nazwa, ITurnContext turncontext, CancellationToken cancellationToken, IConfiguration config)
        {
            nazwa = nazwa.Replace("calories_", "");
            string ErrorAnswer = "Niestety tekst, który wpisałeś nie jest nazwą potrawy znajdyjącą się w naszej bazie. Wpisz jeszcze raz nazwę potrawy lub skorzystaj z podpowiedzi poniżej";
            var dish = new Dish();
            var client = new HttpClient();
            client.BaseAddress= new Uri(config.GetValue<string>("ConnectionStrings"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var danie = client.GetAsync($"/api/recipies/GetDishByName/{nazwa}").Result.Content.ReadAsStringAsync();
                if (danie.Result != "")
                {
                    dish = System.Text.Json.JsonSerializer.Deserialize<Dish>(danie.Result);

                    string listOfIng = string.Join("\n\n", dish.ingridients.Select(p => p.name).Distinct().ToList());
                    listOfIng = listOfIng.Replace("<h2>", ("\n\n**")).Replace("</h2>", ("**\n\n"));

                    Attachment imp = new Attachment();
                    imp.ContentType = "image/png";
                    imp.ContentUrl = (dish.pictUrl);
                    imp.Name = dish.name;
                    var rep = MessageFactory.Attachment(imp);
                    await turncontext.SendActivityAsync(rep);
                    await turncontext.SendActivityAsync(dish.name.ToString() + "\n\n " + "Czas przygotowania: " + dish.timeForPrepare.ToString() + " " + "minut" + "\n\n" + dish.description.ToString().Replace("  ", "") + "\n\n" + listOfIng + "\n\n" + dish.directions1.ToString().Replace("<p style=\"text-align: justify;\">", " ")
                                                        + "\n\n" + dish.directions2.ToString().Replace("<p style=\"text-align: justify;\">", " ") + "\n\n" + dish.directions3.ToString().Replace("<p style=\"text-align: justify;\">", " ") + "\n\n" + dish.directions4.ToString().Replace("<p style=\"text-align: justify;\">", " "));

                   
                    var reply = MessageFactory.Text("");


                    List<CardAction> actions = new List<CardAction>();
                    {
                        actions.Add((new CardAction() { Title = "Video", Type = ActionTypes.PostBack, Value = dish.video }));

                    }
                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = actions
                    };
                    await turncontext.SendActivityAsync(reply, cancellationToken);
                }
                else
                {
                    if (!await Methods.IngridientsAdder.GetAllAdders(turncontext, cancellationToken, config))
                    {
                        await turncontext.SendActivityAsync(ErrorAnswer);
                    }

                }


                return true;
            }
            catch(Exception ex)
            {
                return true;
            }
        }
    }
}
