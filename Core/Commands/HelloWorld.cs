using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;
using Discord.Commands;

namespace KyannaApp.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("Hello"), Alias("Helloworld", "world", "hello", "Hey Kyanna", "Hey", "Kyanna", " Helloworld", " world", " hello", " Hey Kyanna", " Hey", " Kyanna"), Summary("Hello world command")]
        public async Task Greet()
        {
            string Greeting = File.ReadAllLines("GreetingList.txt").Skip(3).Take(1).First();
            await Context.Channel.SendMessageAsync(Greeting);
        }
    }
}
