using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MVC.Data;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    [Route("mvc")]
    public class PlainTextController : Controller
    {

        [HttpGet("plaintext")]
        public IActionResult PlainText2()
        {
            return Ok("Hello, World!");
        }

        //[HttpGet("fortunes")]
        //public async Task<IActionResult> Fortunes()
        //{
        //    var db = HttpContext.RequestServices.GetRequiredService<RawDb>();
        //    var fortunes = await db.LoadFortunesRows();
        //    return View("Fortunes", fortunes);
        //}
    }
}
