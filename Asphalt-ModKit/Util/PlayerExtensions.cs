using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
 

namespace Asphalt.Util
{
    public static class PlayerExtensions
    {
        public static void SendMessage(this Player pPlayer, string message, MessageCategory category = MessageCategory.Info)
        {
            pPlayer.Msg(new LocString(message), category);
        }

        public static void SendTemporaryError(this Player pPlayer, string message, MessageCategory category = MessageCategory.Error)
        {
            pPlayer.Msg(new LocString(message), category);
        }

        public static void SendTemporaryMessage(this User pUser, string pMessage, bool temporary = true, DefaultChatTags tag = DefaultChatTags.Notifications, MessageCategory category = MessageCategory.Info)
        {
            ChatManager.ServerMessageToPlayer(new LocString(pMessage), pUser, tag, category, temporary);
        }
    }
}
