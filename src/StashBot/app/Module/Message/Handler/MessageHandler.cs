﻿using StashBot.Module.Session;
using StashBot.Module.Message.Handler.ChatStateHandler;

namespace StashBot.Module.Message.Handler
{
    internal class MessageHandler : IMessageHandler, IChatStateHandlerContext
    {
        private readonly IChatStateHandlerFactory chatStateHandlerFactory;

        internal MessageHandler()
        {
            chatStateHandlerFactory = new ChatStateHandlerFactory();
        }

        public void HandleUserMessage(ITelegramUserMessage message)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            if (message.IsEmpty())
            {
                return;
            }

            IChatSession chatSession = sessionManager.GetChatSession(message.ChatId);
            if (chatSession == null)
            {
                sessionManager.CreateChatSession(message.ChatId);
                chatSession = sessionManager.GetChatSession(message.ChatId);
            }

            sessionManager.UserSentMessage(message.ChatId, message.MessageId);
            if (string.Equals(message.Message, "/e") || string.Equals(message.Message, "/exit"))
            {
                sessionManager.KillChatSession(message.ChatId);
            }
            else
            {
                chatStateHandlerFactory.GetChatStateHandler(chatSession.State).HandleUserMessage(message, this);
            }
        }

        public void ChangeChatState(long chatId, ChatSessionState newChatSessionState)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            IChatSession chatSession = sessionManager.GetChatSession(chatId);
            if (chatSession != null)
            {
                chatSession.State = newChatSessionState;
                chatStateHandlerFactory.GetChatStateHandler(chatSession.State).StartStateMessage(chatId);
            }
        }
    }
}
