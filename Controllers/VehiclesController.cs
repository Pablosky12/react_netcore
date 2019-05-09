using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using angular_netcore.Core;
using angular_netcore.Core.Models;
using angular_netcore.Persistance;
using angular_netcore.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using react_netcore.Core.Models;

namespace angular_netcore.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly VegaDbContext dbContext;

        public VehiclesController(IMapper mapper,  IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, VegaDbContext dbContext)
        {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            await vehicleRepository.Add(vehicle);
            await unitOfWork.CompleteAsync();
            vehicle = await vehicleRepository.GetVehicle(vehicle.ID, true);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = await vehicleRepository.GetVehicle(id, true);

            if (vehicle == null)
                return NotFound();

            mapper.Map(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();


            vehicle = await vehicleRepository.GetVehicle(id, true);

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            vehicleRepository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id, true);
            if (vehicle == null)
                NotFound();
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<QueryResultResource<Vehicle>> GetVehicles(ListFilterResource filterResource)
        {
            var filter = mapper.Map<ListFilterResource, ListFilter>(filterResource);
            var queryResult = await vehicleRepository.GetVehicles(filter);
            var mappedResult = mapper.Map<QueryResult<Vehicle>, QueryResultResource<Vehicle>>(queryResult);   
            return mappedResult;
        }
    }
}
