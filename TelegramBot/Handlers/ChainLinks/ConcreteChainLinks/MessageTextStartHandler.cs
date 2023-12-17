﻿using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MessageTextStartHandler : AsyncChainLinkBase
{
    public override async Task HandleAsync(Request request)
    {
        if (request.Update.Message?.Text is null) return;

        if (request.Update.Message.Text.Equals(MessageTextConstants.StartCommandMessage, StringComparison.Ordinal)
            || request.Update.Message.Text.Equals(MessageTextConstants.BackToMenuMessage, StringComparison.Ordinal))
        {
            request.Logger.LogInformation("Receive message text: {MessageText}", request.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.FactMenuMessage),
                        new KeyboardButton(MessageTextConstants.CheckKnowledgeMenuMessage),
                    },
                }) { ResizeKeyboard = true, };

            await request.BotClient.SendTextMessageAsync(
                request.Update.Message.Chat.Id,
                DescriptionConstants.StartDescription,
                replyMarkup: replyKeyboard,
                cancellationToken: request.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(request);
        }
    }
}