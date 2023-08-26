
using System.ComponentModel.DataAnnotations;

namespace livechat.signalr.models
{
    public class Group_User : IGroup_User
    {
        [MaxLength(25)]
        public string? UserName { get; set; }
        [MaxLength(25)]
        public string? GroupName { get; set; }

        // link
        public User? User {get; set;}
        public Group? Group {get; set;}
    }
}