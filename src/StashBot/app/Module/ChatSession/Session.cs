﻿using System;
using System.Collections.Generic;

namespace StashBot.Module.ChatSession
{
    internal class Session : ISession
    {
        private readonly long chatId;
        private DateTime lastUserMessage;
        private DateTime lastBotMessage;
        private readonly List<int> userMessagesId;
        private readonly List<int> botMessagesId;

        internal Session(long chatId)
        {
            this.chatId = chatId;
            lastUserMessage = DateTime.UtcNow;
            lastBotMessage = DateTime.UtcNow;
            userMessagesId = new List<int>();
            botMessagesId = new List<int>();
        }

        public void UserSentMessage(int messageId)
        {
            lastUserMessage = DateTime.UtcNow;
            userMessagesId.Add(messageId);
        }

        public void BotSentMessage(int messageId)
        {
            lastBotMessage = DateTime.UtcNow;
            botMessagesId.Add(messageId);
        }

        public long ChatId()
        {
            return chatId;
        }

        public DateTime LastUserMessage()
        {
            return lastUserMessage;
        }

        public List<int> BotMessagesId()
        {
            return botMessagesId;
        }
    }
}
