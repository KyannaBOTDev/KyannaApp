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
    public class KyannaLove : ModuleBase<SocketCommandContext>
    {
        [Command("Love"), Alias("I love you", "love you", "Do you love me?"), Summary("LoveKyanna command")]
        public async Task kyannaApp()
        {
            string Greeting = File.ReadAllLines("LoveListA.txt").Skip(1).Take(1).First();
            await Context.Channel.SendMessageAsync(Greeting);
        }
    }
}
