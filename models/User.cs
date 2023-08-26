// User model

using System.ComponentModel.DataAnnotations;

namespace livechat.signalr.models
{
    public class User : IUser
    {
        [Key]
        [MaxLength(25)]
        public string? UserName { get; set; }
        [MaxLength(256)]
        public string? Icon { get; set;}
        [MaxLength(25)]
        public string? DisplayName { get; set; }

        public ICollection<Group_User>? Group_Users {get; set;}

        // public string? GroupName {get; set;}
        // public Group? Group {get; set;}
    }
}