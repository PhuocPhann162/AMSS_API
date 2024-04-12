using AMSS.Enums;
using AMSS.Models;
using AMSS.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMSS.Controllers
{
    [Route("api/farm")]
    [ApiController]
    [Authorize(Roles = nameof(Role.ADMIN))]
    public class FarmController : ControllerBase
    {
        private readonly IFarmRepository _farmRepository;
        protected APIResponse _apiResponse;
        public FarmController(IFarmRepository farmRepository)
        {
            _farmRepository = farmRepository;
            _apiResponse = new APIResponse();

        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllFarms()
        {

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetFarmById(int id)
        {

        }
    }
}
