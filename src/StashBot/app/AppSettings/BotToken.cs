﻿using System.IO;
using Newtonsoft.Json;

namespace StashBot.AppSetting
{
    internal class BotToken
    {
        private const string BOT_TOKEN_FILE_NAME = "AppSettings.json";
        private static string botToken;

        internal string Get()
        {
            if (string.IsNullOrEmpty(botToken))
            {
                string text = File.ReadAllText(BOT_TOKEN_FILE_NAME);
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(text);
                botToken = (string)jsonObject.botToken;
            }

            return botToken;
        }

        internal void Set(string newBotToken)
        {
            if (string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(newBotToken))
            {
                botToken = newBotToken;
            }
        }
    }
}
