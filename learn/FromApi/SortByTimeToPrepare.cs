using learn.Objects;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace learn.FromApi
{
    public class SortByTimeToPrepare
    {
        public async static Task<bool> GetDishByTimeToPrepare(ITurnContext turnContext, CancellationToken cancellationToken,IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"time").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("timetoprepare_"))
                {
                    skipped = turnContext.Activity.Text.Replace("timetoprepare_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).ToList();
                IEnumerable<Dish> topFiveDishes = dish.OrderBy(dish => dish.timeForPrepare).Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name+"\n\n"+"(Czas przygotowania"+item.timeForPrepare.ToString()+"min)", Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "timetoprepare_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzedni", Type = ActionTypes.PostBack, Value = "timetoprepare_" + (skippedInt - 1) }));
                }
                else
                {
                    actions.Add((new CardAction() { Title = "Wróć", Type = ActionTypes.PostBack, Value = "StartScrean" }));
                }
                reply.SuggestedActions = new SuggestedActions()
                {
                    Actions = actions
                };
                await turnContext.SendActivityAsync(reply, cancellationToken);
                //var sendLog = new Log();
                //sendLog.LogDate = DateTime.UtcNow;
                //sendLog.TypeOdError = "Działa";
                //sendLog.UserId = turnContext.Activity.From.Id;
                //await PostLogs.AddLogsAsync(sendLog);

                return true;

            }
            catch (Exception ex)
            {
                var sendLog = new Log();
                sendLog.LogDate = DateTime.UtcNow;
                sendLog.TypeOdError = ex.Message;
                sendLog.UserId = turnContext.Activity.From.Id;
                await PostLogs.AddLogsAsync(sendLog);
                return false;
            }
        }
    }
}
