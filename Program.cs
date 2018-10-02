using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace KyannaApp
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private static readonly string DataDirectory = string.Format("..{0}..{0}..{0}Core{0}Data", Path.DirectorySeparatorChar);
        private static readonly string ResponseFile = Path.Combine(DataDirectory, "response.json");

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            await Client.LoginAsync(TokenType.Bot, File.ReadAllText("Token.txt").Trim());
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("IN DEVELOPMENT");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var message = MessageParam as SocketUserMessage;
            var context = new SocketCommandContext(Client, message);

            if (context.Message == null || context.Message.Content == "") return;
            if (context.User.IsBot) return;

            int argPos = 0;
            if (!message.HasStringPrefix("Hey", ref argPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argPos)) return;

            var result = await Commands.ExecuteAsync(context, argPos);
            if (!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {context.Message.Content} | Error: {result.ErrorReason}");
            }
        }
    }
}
