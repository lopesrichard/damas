using Damas.Core.Collections;
using Damas.Core.Enums;

namespace Damas.Api.Response
{
    public class Result<T> : IResult<T>
    {
        public T? Data { get; }
        public IEnumerable<Message> Messages { get; }
        public bool IsSuccess { get => Messages.None(m => m.Type == MessageType.ERROR); }

        public Result(IEnumerable<Message> messages)
        {
            Messages = messages;
        }

        public Result(Message message)
        {
            Messages = new List<Message>() { message };
        }

        public Result(T data)
        {
            Data = data;
            Messages = new List<Message>();
        }

        public Result(T data, IEnumerable<Message> messages)
        {
            Data = data;
            Messages = messages;
        }
    }
}
