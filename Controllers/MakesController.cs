using System.Collections.Generic;
using System.Threading.Tasks;
using angular_netcore.Models;
using angular_netcore.Persistance;
using angular_netcore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angular_netcore.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeViewModel>> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeViewModel>>(makes);
        }
    }
}