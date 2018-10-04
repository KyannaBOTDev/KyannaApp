using System;
using System.Linq;
using Discord;

namespace KyannaApp.Core
{
    public static class MessageExtensions
    {
        public static bool HasMention(this IUserMessage message, IUser user)
        {
            return message != null && message.MentionedUserIds.Any(i => i == user.Id);
        }
    }
}
