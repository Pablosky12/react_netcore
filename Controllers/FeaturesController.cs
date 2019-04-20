using System.Collections.Generic;
using System.Threading.Tasks;
using angular_netcore.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angular_netcore.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext dbContext;
        private readonly IMapper mapper;
        public FeaturesController(VegaDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        [HttpGet("api/features")]
        public async Task<List<ViewModels.Feature>> GetFeatures() {
            var featureList = await dbContext.Features.ToListAsync();
            return mapper.Map<List<Models.Feature>, List<ViewModels.Feature>>(featureList);
        }
    }
}