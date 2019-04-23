using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular_netcore.Models;
using angular_netcore.Persistance;
using angular_netcore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace angular_netcore.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly VegaDbContext _context;

        public VehiclesController(IMapper mapper, VegaDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleViewModel vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = _mapper.Map<VehicleViewModel, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Vehicle, VehicleViewModel>(vehicle);
            return Ok(result);
        }
    }
}
