using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupBlog.Data;
using SupBlog.Mappers;
using SupBlog.Services;
using SupBlog.Web.Models;

namespace SupBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleService _ArticleService;
        private readonly CategoryService _CategoryService;
        private readonly ApplicationDbContext _DbContext;
        private readonly GuestService _GuestService;
        private readonly ILogger<HomeController> _logger;
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

            return View(await _GuestService.GetArticles().ConfigureAwait(false));
        }

        public async Task<IActionResult> DetailArticle(int id)
        {
            return View((await _GuestService.GetArticleById(id)).ToDomain());
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