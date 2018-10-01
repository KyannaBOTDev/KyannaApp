using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;

using Discord;
using Discord.Commands;

namespace KyannaApp.Core.Commands
{
    public class BotKill : ModuleBase<SocketCommandContext>
    {
        [Command("Bye"), Alias("Goodnight", "Goodbye", "See you later"), Summary("ByeKyanna command")]
        public async Task kyannaApp()
        {
            string Greeting = File.ReadAllLines("BotKill.txt").Skip(1).Take(1).First();
            await Context.Channel.SendMessageAsync(Greeting);
            Environment.Exit(0);
        }
    }
}
