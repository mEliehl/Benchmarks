using Microsoft.AspNetCore.Mvc;
using Mvc.Data;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    [Route("mvc")]
    public class FortunesController : Controller
    {
        readonly DapperDb db;

        public FortunesController(DapperDb db)
        {
            this.db = db;
        }

        [HttpGet("fortunes")]
        public async Task<IActionResult> Dapper()
        {
            return View("Fortunes", await db.LoadFortunesRows());
        }
    }
}