using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using learn.FromApi;
using learn.Methods;
using learn.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.Bot.Connector;
using RecipieBot.FromApi;

namespace learn.Bots
{
    public class BotBox : ActivityHandler
    {
        private readonly IConfiguration _config;

        public BotBox(IConfiguration config)
        {
            _config = config;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {

            if (turnContext.Activity.Type is ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded != null && turnContext.Activity.MembersAdded.Any())
                {
                    foreach (var newMember in turnContext.Activity.MembersAdded)
                    {
                        if (newMember.Id != turnContext.Activity.Recipient.Id)
                        {
                            await turnContext.SendActivityAsync("Cześć: " + turnContext.Activity.From.Name + " " + "\n witaj w krainie pyszności");
                            await turnContext.SendActivityAsync("Wpisz nazwę potrawy, lub skorzystaj z podpowiedzi poniżej.");
                            await SendSuggestedActionsAsync(turnContext, cancellationToken);
                        }
                    }
                }

            }
            else if (turnContext.Activity.Type is ActivityTypes.Message)
            { 
            //{
            //    var reply = turnContext.Activity.Text;
            //    var typing = turnContext.Activity.CreateReply();
            //    typing.Type = ActivityTypes.Typing;

            //    do
            //    {
            //        turnContext.SendActivityAsync(typing);
            //    } while (reply.Wait(2000));

                //Activity reply = turnContext.Activity.CreateReply();
                //reply.Type = ActivityTypes.Typing;
                //reply.Text = null;
                //reply.

                //await turnContext.SendActivityAsync(reply, cancellationToken);

                switch (turnContext.Activity.Text)
                {
                    case "KL":
                        await SortByTimeToPrepare.GetDishByTimeToPrepare(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("timetoprepare_")):
                        await SortByTimeToPrepare.GetDishByTimeToPrepare(turnContext, cancellationToken, _config);
                        break;
                    case "K88S":
                        await TypyKuchni.KuchnieSwiata(turnContext, cancellationToken);
                        
                        break;
                    case "PD":
                        await turnContext.SendActivityAsync("Wpisz posiadane przez Ciebie składniki po każdym z nich dodając +");
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("1qq1_")):
                        await Methods.IngridientsAdder.GetMatchedAndNotmachet(turnContext, cancellationToken, _config);
                        await SendSuggestedActionsAsync(turnContext, default);
                        break;

                    case var expresion when (turnContext.Activity.Text.Contains("+"))&& !(turnContext.Activity.Text.Contains("1qq1_")):
                        await FastAdding.GetAddingFast(turnContext, cancellationToken, _config);
    
                        break;
                    
                    case "StartScrean":
                        await turnContext.SendActivityAsync("Wpisz nazwę potrawy, lub skorzystaj z podpowiedzi poniżej.");
                        await SendSuggestedActionsAsync(turnContext, cancellationToken);
                        break;
                   case var expresion when (turnContext.Activity.Text.Contains("101Pols")):
                        await CountryKitchen.Polish(turnContext, cancellationToken, _config);
                        break;

                    case var expresion when (turnContext.Activity.Text.Contains("101Wlos")):
                        await CountryKitchen.Italian(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101His")):
                        await CountryKitchen.Spanish(turnContext, cancellationToken, _config);
                        break;
                       
                    case var expresion when (turnContext.Activity.Text.Contains("101Azja")):
                        await CountryKitchen.Asia(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Ameryk")):
                        await CountryKitchen.American(turnContext, cancellationToken,_config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Franc")):
                        await CountryKitchen.France(turnContext, cancellationToken, _config);
                        break;

                    case var expresion when (turnContext.Activity.Text.Contains("101Czes")):
                        await CountryKitchen.Czech(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Ang")):
                        await CountryKitchen.English(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Taj")):
                        await CountryKitchen.Thai(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Orient")):
                        await CountryKitchen.Orient(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Meks")):
                        await CountryKitchen.Mex(turnContext, cancellationToken, _config);
                        break;
                    case var expresion when (turnContext.Activity.Text.Contains("101Grec")):
                        await CountryKitchen.Greek(turnContext, cancellationToken, _config);
                        break;


                    default:
                        await DataFromApi.GetDishByName(turnContext.Activity.Text, turnContext,cancellationToken, _config);
                        await SendSuggestedActionsAsync(turnContext, default);
                        break;
                }
            }
        }

        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Kuchnie świata", Type= ActionTypes.PostBack,Value="K88S" },
                    new CardAction() { Title = "Sortuj według czasu przygotowania", Type= ActionTypes.PostBack,Value="KL" },
                    new CardAction() { Title = "Wpisz twoje produkty", Type= ActionTypes.PostBack,Value="PD" },
                    new CardAction() { Title = "Nasza gazetka", Type= ActionTypes.OpenUrl,Value="https://www.lidl.pl/informacje-dla-klienta/nasze-gazetki" },


                },
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

    }
}