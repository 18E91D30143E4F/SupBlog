using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Domain;
using SupBlog.Mappers;
using SupBlog.Services;
using SupBlog.Web.Models;
using SupBlog.Web.Models.ViewModels.Redactor;

namespace SupBlog.Web.Controllers
{
    public class RedactorController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly RedactorService _RedactorService;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _Mapper;

        public RedactorController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> manager,
            IMapper mapper)
        {
            _DbContext = dbContext;
            _RedactorService = new RedactorService(_DbContext);
            _UserManager = manager;
            _Mapper = mapper;
        }

        [Authorize(Roles = "Redactor")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> Articles()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = (await _RedactorService.GetUserArticles(userId)).ToDomain();


            return View("Articles", res);
        }

        [HttpGet]
        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> AddArticle()
        {
            var categories = await _RedactorService.GetCategories();

            var model = new CreateArticleViewModel
            {
                Categories = categories,
                AllTags = await _RedactorService.GetTags()
            };

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> AddArticle(CreateArticleViewModel viewModel)
        {
            if (viewModel.Article.Tags != null)
                viewModel.Article.Tags = _RedactorService.GetTagsByNames(viewModel.AcceptedTags.ToArray());

            var categoryEntity = await _RedactorService.GetCategoryByName(viewModel.Article.Category.Name)
                .ConfigureAwait(false);

            _DbContext.Entry(categoryEntity).State = EntityState.Detached;

            viewModel.Article.Category = categoryEntity.ToDomain();

            var currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            viewModel.Article.User = await _UserManager.FindByIdAsync(currentUserName);

            var article = viewModel.Article.ToEntity();

            await _RedactorService.AddArticle(article).ConfigureAwait(false);

            return View("Index");
        }

        /*        [HttpPost]
                [Authorize(Roles = "Redactor")]
                public async Task<IActionResult> AddArticle(Article article)
                {
                    return View();
                }*/
    }
}
