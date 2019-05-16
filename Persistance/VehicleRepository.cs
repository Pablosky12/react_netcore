using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using angular_netcore.Core;
using angular_netcore.Core.Models;
using Microsoft.EntityFrameworkCore;
using react_netcore.Core.Models;
using react_netcore.Extensions;

namespace angular_netcore.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;

        public VehicleRepository(VegaDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Vehicles.FindAsync(id);

            return _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(vm => vm.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .FirstOrDefault(v => v.ID == id);
        }

        public async Task Add(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(ListFilter filter)
        {
            var queryResult = new QueryResult<Vehicle>();

            var vehicleQuery = _context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(v => v.Make)
                .Include(v => v.Features)
                .ThenInclude(f => f.Feature)
                .AsQueryable();
            if (filter.MakeId.HasValue)
                vehicleQuery = vehicleQuery.Where(vehicle => vehicle.Model.MakeId == filter.MakeId);
            if (filter.ModelId.HasValue)
                vehicleQuery = vehicleQuery.Where(vehicle => vehicle.ModelId == filter.ModelId);

            var map = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v => v.Model.Make,
                ["model"] = v => v.ModelId,
                ["contactName"] = v => v.ContactName,
            };

            vehicleQuery = vehicleQuery.ApplySorting(filter, map);
            queryResult.TotalItems = await vehicleQuery.CountAsync();

            vehicleQuery = vehicleQuery.ApplyPaging(filter);

            queryResult.Items = await vehicleQuery.ToListAsync();
            return queryResult;
        }

        private IQueryable<Vehicle> ApplySorting(ListFilter filter, IQueryable<Vehicle> vehicleQuery, Dictionary<string, Expression<Func<Vehicle, object>>> map)
        {
            if (filter.IsSortAscending)
                return vehicleQuery.OrderBy(map[filter.SortBy]);
            else
                return vehicleQuery.OrderByDescending(map[filter.SortBy]);
        }
    }
}
