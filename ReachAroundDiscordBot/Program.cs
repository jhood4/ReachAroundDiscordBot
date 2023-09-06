using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using ReachAroundDiscordBot.Services;

namespace ReachAroundDiscordBot
{

    public class Program : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        public static void Main(string[] args) 
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);
            
            var services = ConfigureServices();
            await services.GetRequiredService<CommandHandlerService>().InitializeAsync();

            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_KEY"));
            await _client.StartAsync();
            

            await Task.Delay(-1);
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .AddSingleton<LoggingService>()
                .BuildServiceProvider();
        }

    }
}