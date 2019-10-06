﻿using System.Collections.Generic;

namespace StashBot.Module.Message
{
    internal interface IMessageManager
    {
        void HandleUserTextMessage(long chatId, int messageId, string textMessage);
        void SendTextMessage(long chatId, string textMessage);
        void SendWelcomeMessage(long chatId);
        void SendRegistrationSuccessMessage(long chatId, string authCode);
        void DeleteBotMessage(long chatId, int messageId);
        void DeleteListBotMessages(long chatId, List<int> messagesId);
    }
}
