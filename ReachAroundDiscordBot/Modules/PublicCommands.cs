using Discord.Commands;

namespace ReachAroundDiscordBot.Modules;

public class PublicCommands : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync() => await ReplyAsync("pong!");
}