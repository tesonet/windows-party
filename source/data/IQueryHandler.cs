using System.Threading.Tasks;

namespace tesonet.windowsparty.data
{
    public interface IQueryHandler<TPayload, TResult>
    {
        Task<TResult> Get(IQuery<TPayload> query);
    }
}
