using MediatR;

namespace Student.BLL.Mediator
{
    public interface IMediatorHandler
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> message);
    }
}
