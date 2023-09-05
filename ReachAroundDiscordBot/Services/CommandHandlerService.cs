using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ReachAroundDiscordBot.Services;

public class CommandHandlerService
{
    private readonly CommandService _commands;
    private readonly DiscordSocketClient _discord;
    private readonly IServiceProvider _provider;

    public CommandHandlerService(IServiceProvider provider,
        DiscordSocketClient discord,
        CommandService commands)
    {
        _commands = commands;
        _discord = discord;
        _provider = provider;

        _discord.MessageReceived += MessageReceivedAsync;
    }
    public async Task InitializeAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
    }

    public async Task MessageReceivedAsync(SocketMessage rawMsg)
    {
        if (rawMsg is not SocketUserMessage message)
            return;

        if (message.Source != MessageSource.User)
            return;

        var argPos = 0;
        // if (!(message.HasCharPrefix('!', ref argPos)))
        //     return;

        var context = new SocketCommandContext(_discord, message);
        var result = await _commands.ExecuteAsync(context, argPos, _provider);
        
        if (result.Error.HasValue &&
            result.Error.Value != CommandError.UnknownCommand)
            await context.Channel.SendMessageAsync(result.ToString());
        
    }
}