namespace Damas.Api.Response
{
    public class ListResult<T> : Result<IEnumerable<T>>, IListResult<T>
    {
        public ListResult(IEnumerable<Message> messages) : base(messages)
        {
        }

        public ListResult(Message message) : base(message)
        {
        }

        public ListResult(IEnumerable<T> data) : base(data)
        {
        }

        public ListResult(IEnumerable<T> data, IEnumerable<Message> messages) : base(data, messages)
        {
        }
    }
}
