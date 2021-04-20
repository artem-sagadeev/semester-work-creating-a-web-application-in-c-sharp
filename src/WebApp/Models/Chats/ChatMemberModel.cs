namespace WebApp.Models.Chats
{
    public class ChatMemberModel
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public bool IsAuthor { get; set; }
    }
}
