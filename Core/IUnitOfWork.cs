using System.Threading.Tasks;

namespace angular_netcore.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
