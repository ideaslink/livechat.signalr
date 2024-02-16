
namespace livechat.signalr.models
{
    public interface IGroup
    {
        string? GroupName {get; set;}
        /// <summary>
        /// Group alias - represents a connection for the group
        /// </summary>
        string? GroupAlias {get; set;}
        string? Color {get; set;}
        public string? Icon { get; set;}
    }
}