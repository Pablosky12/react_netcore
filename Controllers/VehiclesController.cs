using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular_netcore.Models;
using angular_netcore.Persistance;
using angular_netcore.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angular_netcore.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly VegaDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IMapper mapper, VegaDbContext context, IVehicleRepository vehicleRepository)
        {
            _mapper = mapper;
            _context = context;
            _vehicleRepository = vehicleRepository;
        }
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            _vehicleRepository.Add(vehicle);
            await _context.SaveChangesAsync();


            vehicle = await _vehicleRepository.GetVehicle(vehicle.ID);
            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = await _context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.ID == id);

            if (vehicle == null)
                return NotFound();

            _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();


            vehicle = await _vehicleRepository.GetVehicle(id);

            var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id, false);
            if (vehicle == null)
                return NotFound();
            _vehicleRepository.Remove(vehicle);
            await _context.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                NotFound();
            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }
    }
}
