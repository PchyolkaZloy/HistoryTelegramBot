﻿using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MessageTextStartHandler : AsyncChainLinkBase
{
    public override async Task HandleAsync(UpdateHandlerContext context)
    {
        if (context.Update.Message?.Text is null) return;

        if (context.Update.Message.Text.Equals(MessageTextConstants.StartCommandMessage, StringComparison.Ordinal)
            || context.Update.Message.Text.Equals(MessageTextConstants.BackToMenuMessage, StringComparison.Ordinal))
        {
            context.Logger.LogInformation("Receive message text: {MessageText}", context.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.FactMenuMessage),
                        new KeyboardButton(MessageTextConstants.CheckKnowledgeMenuMessage),
                    },
                }) { ResizeKeyboard = true, };

            await context.BotClient.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                DescriptionConstants.StartDescription,
                replyMarkup: replyKeyboard,
                cancellationToken: context.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(context);
        }
    }
}