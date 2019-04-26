using System.Threading.Tasks;
using angular_netcore.Models;

namespace angular_netcore.Persistance
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}