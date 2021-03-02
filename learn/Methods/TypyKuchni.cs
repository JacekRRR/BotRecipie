using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace learn.Methods
{
    public class TypyKuchni
    {
        public static async Task KuchnieSwiata(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Kuchnia polska", Type = ActionTypes.PostBack, Value = "101Pols" },
                    new CardAction() { Title = "Kuchnia włoska", Type = ActionTypes.PostBack, Value = "101Wlos" },
                    new CardAction() { Title = "Kuchnia hiszpańsko-portygalska", Type = ActionTypes.PostBack, Value = "101His" },
                    new CardAction() { Title = "Kuchnia azjatycka", Type = ActionTypes.PostBack, Value = "101Azja" },
                    new CardAction() { Title = "Kuchnia amerykańska", Type = ActionTypes.PostBack, Value = "101Ameryk" },
                    new CardAction() { Title = "Kuchnia francuska", Type = ActionTypes.PostBack, Value = "101Franc" },
                    new CardAction() { Title = "Kuchnia czeska", Type = ActionTypes.PostBack, Value = "101Czes" },
                    new CardAction() { Title = "Kuchnia angielska", Type = ActionTypes.PostBack, Value = "101Ang" },
                    new CardAction() { Title = "Kuchnia tajska", Type = ActionTypes.PostBack, Value = "101Taj" },
                    new CardAction() { Title = "Kuchnia orientalna", Type = ActionTypes.PostBack, Value = "101Orient" },
                    new CardAction() { Title = "Kuchnia meksykańska", Type = ActionTypes.PostBack, Value = "101Meks" },
                    new CardAction() { Title = "Kuchnia grecka", Type = ActionTypes.PostBack, Value = "101Grec" },

                    new CardAction() { Title = "Wróć", Type = ActionTypes.PostBack, Value = "StartScrean" }
                }


            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}
