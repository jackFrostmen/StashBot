﻿using System.Collections.Generic;

namespace StashBot.Module.Database.Stash
{
    internal interface IDatabaseStash
    {
        void SaveMessageToStash(IStashMessage stashMessage);
        List<IStashMessage> GetMessagesFromStash(long chatId);
        void ClearStash(long chatId);
    }
}
