namespace Damas.Api.Response
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Content { get; set; }

        public Message(MessageType type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
