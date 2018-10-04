using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace KyannaApp.Core.Features
{
    class Chatterbot
    {
        private Dictionary<string, string> chatTriggers;
        private Dictionary<string, string[]> chatResponses;

        public Chatterbot(string dialogFile)
        {
            if (string.IsNullOrWhiteSpace(dialogFile) || !File.Exists(dialogFile))
            {
                return;
            }
            InitDialog(dialogFile);
        }

        private void InitDialog(string file)
        {
            string text;
            chatTriggers = new Dictionary<string, string>();
            chatResponses = new Dictionary<string, string[]>();

            try
            {
                text = File.ReadAllText(file);
                JObject obj = JObject.Parse(text);
                List<JToken> dialog = ((JArray)obj["dialog"]).ToList();

                dialog.ForEach(item =>
                {
                    string category = (string)item["category"];
                    List<string> triggers = item["triggers"].Select(t => ((string)t).ToLower()).ToList();
                    var responses = item["responses"].Select(t => (string)t).ToArray();

                    triggers.ForEach(trigger =>
                    {
                        if (!chatTriggers.ContainsKey(trigger))
                            chatTriggers.Add(trigger, category);
                        if (!chatResponses.ContainsKey(category))
                            chatResponses.Add(category, responses);
                    });
                });
            }
            catch { }
        }

        public async Task Mentioned(SocketCommandContext context)
        {
            if (context == null) return;

            foreach (string word in context.Message.Content.Split(' '))
            {
                string text = word.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(text) && chatTriggers.ContainsKey(text))
                {
                    Random r = new Random();
                    string category = chatTriggers[text];
                    string response = chatResponses[category][r.Next(chatResponses[category].Length)];
                    await context.Channel.SendMessageAsync(response);
                }
            }
        }
    }
}
