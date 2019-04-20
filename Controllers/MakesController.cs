using System.Collections.Generic;
using System.Threading.Tasks;
using angular_netcore.Models;
using angular_netcore.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using angular_netcore.ViewModels;

namespace angular_netcore.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet("api/makes")]
        public async Task<IEnumerable<ViewModels.Make>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Models.Make>, List<ViewModels.Make>>(makes);
        }
    }
}