using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForcastApp.Web.Data;

namespace WeatherForcastApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public UserController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Query()
        {
            var q = await _dbcontext
                .Post
                .Include(p => p.Comments)
                .ToListAsync();

            //Console.WriteLine(q);

            return Ok(q);
        }
    }
}
