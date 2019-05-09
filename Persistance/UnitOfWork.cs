using System.Threading.Tasks;
using angular_netcore.Core;

namespace angular_netcore.Persistance
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}