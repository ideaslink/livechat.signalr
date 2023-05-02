

using Microsoft.AspNetCore.SignalR;

namespace livechat.signalr.hubs
{
    public class ChatHub : Hub
    {
        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendToUser(string user, string message)
        {
            await Clients.User(user).SendAsync(message);
        }

        public async Task SendToGroup(string groupname, string message)
        {
            await AddToGroup(groupname);
            await Clients.Group(groupname).SendAsync("ReceiveMessage", $"Message sent to group {groupname}", $"{message}");
        }

        public async Task AddToGroup(string groupname)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Group(groupname).SendAsync("ReceiveMessage","Note",  $"user {Context.UserIdentifier} with connectionId {Context.ConnectionId} has joined the group {groupname}");
        }

        public async Task MessagingHub(string message, string user, string group)
        {
            if (user.Length != 0) await Clients.User(user).SendAsync("ReceiveMessage", user, message);

            if (group.Length != 0) await Clients.Group(group).SendAsync("ReceiveMessage", group, message);

            if (user.Length == 0 && group.Length == 0) await SendMessage(user, message);

        }
    }
}
