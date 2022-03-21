namespace Damas.Api.Response
{
    public interface IResult<T>
    {
        T? Data { get; }
        IEnumerable<Message> Messages { get; }
        bool IsSuccess { get; }
    }
}
