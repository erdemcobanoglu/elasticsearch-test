using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleESTest.Api.Data;
using SimpleESTest.Api.Helpers;
using SimpleESTest.Api.Models;

namespace SimpleESTest.Api.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IRepository _repository;
        public CandidatesController(IRepository repository)
        {
            _repository = repository;

        }


        [HttpGet]
        public async Task<IActionResult> GetCandidates()
        {
            var list =await _repository.GetCandidates();

            return Ok(list);
        }
    }
}