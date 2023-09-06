using Discord;
using Discord.Commands;

namespace ReachAroundDiscordBot.Modules;

public class PublicCommands : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync() => await ReplyAsync("pong!");

    [Command("me")]
    public async Task MeAsync(IUser user = null)
    {
        user ??= Context.User;
        await ReplyAsync(user.ToString());
    }
}