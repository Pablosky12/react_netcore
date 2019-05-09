using System.Collections.Generic;
using System.Threading.Tasks;
using angular_netcore.Core.Models;
using angular_netcore.Persistance;
using angular_netcore.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angular_netcore.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _dbContext;
        private readonly IMapper _mapper;
        public FeaturesController(VegaDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet("api/features")]
        public async Task<List<KeyValuePairResource>> GetFeatures() {
            var featureList = await _dbContext.Features.ToListAsync();
            return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(featureList);
        }
    }
}