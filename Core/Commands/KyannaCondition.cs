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
    public class KyannaCondition : ModuleBase<SocketCommandContext>
    {
        [Command("Nothing"), Alias("Messing with Lounger", "Working", "Workin", "Eating", "Thinking about you", "Planning improvments"), Summary("activity response command")]
        public async Task kyannaApp()
        {
            string Greeting = File.ReadAllLines("ResponseListA.txt").Skip(3).Take(1).First();
            await Context.Channel.SendMessageAsync(Greeting);
        }
    }
}
