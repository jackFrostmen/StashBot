﻿using System.Collections.Generic;
using StashBot.Module.Database;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorizedStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal AuthorizedStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/stash", GetStash);
            commands.Add("/logout", Logout);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string loginMessage = "Input message to save it in stash.\nGet messages in stash: /stash\nLogout: /logout";
            messageManager.SendMessage(chatId, loginMessage);
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (IsCommand(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    SaveTextMessageToStash(message);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void SaveTextMessageToStash(ITelegramUserMessage message)
        {
            IDatabaseManager databaseManager =
                    ModulesManager.GetModulesManager().GetDatabaseManager();

            databaseManager.SaveMessageToStash(message.ChatId, message.Message);
        }

        private void GetStash(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            List<string> messagesFromStash = databaseManager.GetMessagesFromStash(chatId);
            foreach (string textMessage in messagesFromStash)
            {
                messageManager.SendMessage(chatId, textMessage);
            }
        }

        private void Logout(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            userManager.LogoutUser(chatId);
            const string logoutMessage = "You're logged out";
            messageManager.SendMessage(chatId, logoutMessage);
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
