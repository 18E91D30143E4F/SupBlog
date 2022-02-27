using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupBlog.Data;
using SupBlog.Web.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Services;

namespace SupBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _DbContext;
        private readonly ArticleService _ArticleService;
        private readonly GuestService _GuestService;
        private readonly CategoryService _CategoryService;
        private readonly TagService _TagService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _DbContext = dbContext;
            _ArticleService = new ArticleService(dbContext);
            _GuestService = new GuestService(dbContext);
            _CategoryService = new CategoryService(dbContext);
            _TagService = new TagService(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var ar = await _ArticleService._ArticleRepository.GetAll();

            //var guestService = new GuestService(_DbContext);

            return View(await _GuestService.GetArticles().ConfigureAwait(false));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
