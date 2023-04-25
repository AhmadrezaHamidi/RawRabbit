using System.Threading.Tasks;
using Betisa.Framework.Core.Types;

namespace Betisa.Framework.Core.Handlers
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    public interface IQuery
    {
    }
    public interface IQuery<T> : IQuery
    {
    }
}