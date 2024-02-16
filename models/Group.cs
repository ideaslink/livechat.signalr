
using System.ComponentModel.DataAnnotations;

namespace livechat.signalr.models
{
    public class Group : IGroup
    {
        [Key]
        [MaxLength(25)]
        public string? GroupName { get; set; }
        [MaxLength(256)]
        public string? Color { get; set; }
        [MaxLength(255)]
        public string? Icon { get; set; }
        // link
        public ICollection<Group_User>? Group_Users {get; set;}
        public string? GroupAlias { get; set; }

        // public ICollection<User>? Users {get; set;}
    }
}