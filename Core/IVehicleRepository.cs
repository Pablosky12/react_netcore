using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using angular_netcore.Core.Models;
using react_netcore.Core.Models;

namespace angular_netcore.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = false);
        Task Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetVehicles(ListFilter filter);
    }
}