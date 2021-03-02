using learn.Objects;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace learn.FromApi
{
    public class CountryKitchen
    {
        public async static Task<bool> Polish(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"p").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Pols_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Pols_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "p").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Pols_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Pols_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
        public async static Task<bool> Italian(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {

            IEnumerable<Dish> dish = new List<Dish>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var dania = client.GetAsync($"w").Result.Content.ReadAsStringAsync();

            string skipped = "";
            int skippedInt = 0;
            if (turnContext.Activity.Text.Contains("101Wlos_"))
            {
                skipped = turnContext.Activity.Text.Replace("101Wlos_", "");
                skippedInt = Convert.ToInt32(skipped);
            }


            dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "w").ToList();
            IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

            var reply = MessageFactory.Text("");
            List<CardAction> actions = new List<CardAction>();

            foreach (var item in topFiveDishes)
            {
                actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
            }
            actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Wlos_" + (skippedInt + 1) }));
            if (skippedInt > 0)
            {
                actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Wlos_" + (skippedInt - 1) }));
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
            return true;

        }

        public async static Task<bool> Spanish(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {

            IEnumerable<Dish> dish = new List<Dish>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var dania = client.GetAsync($"s").Result.Content.ReadAsStringAsync();

            string skipped = "";
            int skippedInt = 0;
            if (turnContext.Activity.Text.Contains("101His_"))
            {
                skipped = turnContext.Activity.Text.Replace("101His_", "");
                skippedInt = Convert.ToInt32(skipped);
            }


            dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "s").ToList();
            IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

            var reply = MessageFactory.Text("");
            List<CardAction> actions = new List<CardAction>();

            foreach (var item in topFiveDishes)
            {
                actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
            }
            actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = (skippedInt + 1) }));
            if (skippedInt > 0)
            {
                actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101His_" + (skippedInt - 1) }));
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
            return true;
        }

        public async static Task<bool> Orient(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"o").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Orient_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Orient_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "o").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Orient_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Orient_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> Mex(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"m").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Meks_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Meks_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "m").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Meks_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Meks_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> Greek(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"g").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Grec_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Grec_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "g").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Grec_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Grec_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> American(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"a").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Ameryk_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Ameryk_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "a").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Ameryk_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Ameryk_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> France(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"f").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Franc_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Franc_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "f").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Franc" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Franc" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> Thai(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"t").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Taj_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Taj_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "t").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Taj_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Taj_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> Czech(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"c").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Czes_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Czes_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "c").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Czes_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Czes_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public async static Task<bool> English(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"e").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Ang_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Ang_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "e").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Ang_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Ang_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }

        }

        public async static Task<bool> Asia(ITurnContext turnContext, CancellationToken cancellationToken, IConfiguration config)
        {
            try
            {
                IEnumerable<Dish> dish = new List<Dish>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(config.GetValue<string>("ConnectionStrings"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var dania = client.GetAsync($"z").Result.Content.ReadAsStringAsync();

                string skipped = "";
                int skippedInt = 0;
                if (turnContext.Activity.Text.Contains("101Azja_"))
                {
                    skipped = turnContext.Activity.Text.Replace("101Azja_", "");
                    skippedInt = Convert.ToInt32(skipped);
                }


                dish = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Dish>>(dania.Result).Where(x => x.kitchenFrom == "z").ToList();
                IEnumerable<Dish> topFiveDishes = dish.Skip(5 * skippedInt).Take(5);

                var reply = MessageFactory.Text("");
                List<CardAction> actions = new List<CardAction>();

                foreach (var item in topFiveDishes)
                {
                    actions.Add(new CardAction() { Title = item.name, Type = ActionTypes.PostBack, Value = item.name });
                }
                actions.Add((new CardAction() { Title = "Następne", Type = ActionTypes.PostBack, Value = "101Azja_" + (skippedInt + 1) }));
                if (skippedInt > 0)
                {
                    actions.Add((new CardAction() { Title = "Poprzednie", Type = ActionTypes.PostBack, Value = "101Azja_" + (skippedInt - 1) }));
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
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }

        }

    }

}