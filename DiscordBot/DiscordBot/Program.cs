using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += HandleCommand;
            _client.Log += Log;

            string token = "";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task HandleCommand(SocketMessage message)
        {
            string command = "";
            int lengthOfCommand = message.ToString().Length;
            Console.WriteLine(lengthOfCommand);

            if (!message.Content.StartsWith("!"))
            {
                return Task.CompletedTask;
            }

            if (message.Author.IsBot)
            {
                return Task.CompletedTask;
            }

            command = message.Content.Substring(1, lengthOfCommand - 1);

            // Commands

            if (command.ToLower().Equals("hello"))
            {
                message.Channel.SendMessageAsync($"Hello {message.Author.Mention}");
            }
            else if (command.ToLower().StartsWith("status"))
            {
                Status(message);
            }
            else if (command.ToLower().StartsWith("run"))
            {

            }

            return Task.CompletedTask;
        }

        private static void Status(SocketMessage message)
        {
            if (message.Content.EndsWith("valheim"))
            {
                message.Channel.SendMessageAsync($"valheim");
            }
            else if (message.Content.EndsWith("minecraft"))
            {
                message.Channel.SendMessageAsync($"minecraft");
            }
        }
    }
}
