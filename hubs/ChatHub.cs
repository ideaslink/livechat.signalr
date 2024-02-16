

using System.Text.RegularExpressions;
using livechat.signalr.Data;
using livechat.signalr.models;
using Microsoft.AspNetCore.SignalR;

namespace livechat.signalr.hubs
{
    public class ChatHub : Hub
    {
        #region "Properties"
        private DataContext _datacontext {get; set;}
        #endregion

        public ChatHub(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task SendMessage(string user, string message)
        {
            SaveGroupUser(user: user);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToAll(string message)
        {

            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendToUser(string user, string message)
        {
            SaveGroupUser(user: user);
            // // save user to db (if necessary)
            // if (_datacontext.Users?.FirstOrDefault(s => s.UserName == user) == null){
            //     _datacontext.Users?.Add(new models.User() { UserName = user, Icon = "" }); 
            //     _datacontext.SaveChanges();
            // }

            await Clients.User(user).SendAsync(message);
        }

        public async Task SendToGroup(string groupname, string message, string username = "")
        {
            await AddToGroup(groupname, username);
            await Clients.Group(groupname).SendAsync("ReceiveMessage", $"Message sent to group {groupname}", $"{message}");
        }

        public async Task AddToGroup(string groupname, string username = "")
        {
            SaveGroupUser(group: groupname, user: username);

            // // save group to db (if necessary)
            // if (_datacontext.Groups?.FirstOrDefault(s => s.GroupName == groupname) == null){
            //     _datacontext.Groups?.Add(new models.Group() {  GroupName = groupname, Color = "yellow" });  // save group, color defautl to yellow
            //     _datacontext.SaveChanges();
            // }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Group(groupname).SendAsync("ReceiveMessage","Note",  $"user {Context.UserIdentifier} with connectionId {Context.ConnectionId} has joined the group {groupname}");
            
        }

        private void SaveGroupUser(string group = "", string user = "", string icon = "", string color = ""){
            // save user to db (if necessary)
            if (user.Length != 0 && _datacontext.Users?.FirstOrDefault(s => s.UserName == user) == null){
                _datacontext.Add(new models.User() { UserName = user, Icon = icon }); 
            }

            if (group.Length != 0 && _datacontext.Groups?.FirstOrDefault(s => s.GroupName == group) == null){
                _datacontext.Add(new models.Group() { GroupName = group, Color = color }); 
            }

            if (user.Length != 0 && group.Length != 0 && _datacontext.Group_Users?.FirstOrDefault(gu => gu.UserName == user && gu.GroupName == group) == null){
                _datacontext.Add(new Group_User { UserName = user, GroupName = group});
            }

            _datacontext.SaveChanges();
        }

        public async Task MessagingHub(string message, string user, string group)
        {
            if (user.Length != 0) await Clients.User(user).SendAsync("ReceiveMessage", user, message);

            if (group.Length != 0) await Clients.Group(group).SendAsync("ReceiveMessage", group, message);

            if (user.Length == 0 && group.Length == 0) await SendMessage(user, message);

        }
    }
}