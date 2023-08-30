using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ReachAroundDiscordBot
{

    public class Program : ModuleBase<SocketCommandContext>
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private DiscordSocketClient? client;
        private CommandService? commands;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            //highestVote();

            // client.Ready += OnReady;

            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            const string token = "";

            client.Log += Client_Log;
            client.MessageReceived += CommandHandler;


            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }


        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());

            return Task.CompletedTask;
        }


        private Task CommandHandler(SocketMessage message)
        {



            var command = "";
            var lengthOfCommand = -1;

            if (!message.Content.StartsWith("!"))
                return Task.CompletedTask;

            if (message.Author.IsBot)
                return Task.CompletedTask;

            if (message.Content.Contains(' '))
            {
                lengthOfCommand = message.Content.IndexOf(' ');
            }
            else
            {
                lengthOfCommand = message.Content.Length;
            }

            command = message.Content.Substring(1, lengthOfCommand - 1);

            //COMMANDS

            //COMMANDS TAB - TO DO
            if (command.Equals("commands"))
            {
                message.Channel.SendMessageAsync("Deez Nutz");
            }

            if (command.Equals("Test"))
            {
                var newEmbed = new EmbedBuilder()
               .WithTitle("Title")
               .WithDescription("Description 1")
               .WithColor(Color.Blue)
               .WithUrl("https://media.discordapp.net/attachments/1036111023884734474/1146331023781662751/randomLogo.png")    
               .AddField("Deez", "Nuts")
               .AddField("Deez", "Nuts", inline: true)
               .AddField("Deez", "Nuts", inline: true)
               .Build();
                message.Channel.SendMessageAsync(embed: newEmbed);
            }

            return Task.CompletedTask;
        }
    }
}