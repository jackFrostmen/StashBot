﻿using System.Timers;
using System.Collections.Generic;
using StashBot.BotSettings;

namespace StashBot.Module.Session
{
    internal class SessionManager : ISessionManager
    {
        private readonly Dictionary<long, IChatSession> currentChatSessions;

        internal SessionManager()
        {
            currentChatSessions = new Dictionary<long, IChatSession>();
            StartClearChatSessionsTimer();
        }

        public void CreateChatSession(long chatId)
        {
            ChatSession newChatSession = new ChatSession(chatId);
            if (IsChatSessionExist(chatId))
            {
                currentChatSessions[chatId] = newChatSession;
            }
            else
            {
                currentChatSessions.Add(chatId, newChatSession);
            }
        }

        public bool IsChatSessionExist(long chatId)
        {
            return currentChatSessions.ContainsKey(chatId);
        }

        public IChatSession GetChatSession(long chatId)
        {
            if (IsChatSessionExist(chatId))
            {
                return currentChatSessions[chatId];
            }

            return null;
        }

        public void KillChatSession(long chatId)
        {
            if (IsChatSessionExist(chatId))
            {
                currentChatSessions[chatId].Kill();
                currentChatSessions.Remove(chatId);
            }
        }

        public void UserSentMessage(long chatId, int messageId)
        {
            if (IsChatSessionExist(chatId))
            {
                IChatSession chatSession = currentChatSessions[chatId];
                chatSession.UserSentMessage(messageId);
            }
        }

        public void BotSentMessage(long chatId, int messageId)
        {
            if (IsChatSessionExist(chatId))
            {
                IChatSession chatSession = currentChatSessions[chatId];
                chatSession.BotSentMessage(messageId);
            }
        }

        private void StartClearChatSessionsTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ClearSessions;
            timer.Interval = ChatSessionSettings.ChatSessionsClearInterval * 1000;
            timer.Enabled = true;
        }

        private void ClearSessions(object sender, ElapsedEventArgs e)
        {
            foreach (var s in currentChatSessions)
            {
                IChatSession chatSession = s.Value;
                if (chatSession.NeedKill())
                {
                    KillChatSession(chatSession.ChatId);
                }
            }
        }
    }
}
