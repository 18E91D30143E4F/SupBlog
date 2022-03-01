using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Mappers;
using SupBlog.Services;
using SupBlog.Web.Models;
using SupBlog.Web.Models.ViewModels.Redactor;

namespace SupBlog.Web.Controllers
{
    public class RedactorController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IMapper _Mapper;
        private readonly RedactorService _RedactorService;
        private readonly UserManager<ApplicationUser> _UserManager;

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

            return View("CreateArticle", model);
        }

        [HttpPost]
        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> AddArticle(CreateArticleViewModel viewModel)
        {
            try
            {
                var articleDomain = viewModel.Article;
                if (viewModel.AcceptedTags != null)
                    articleDomain.Tags = _RedactorService.GetTagsByNames(viewModel.AcceptedTags.ToArray());
                articleDomain.User = new ApplicationUser { Id = await GetCurrentUserId() };
                articleDomain.Category.Id = (await _RedactorService.GetCategoryByName(articleDomain.Category.Name)
                    .ConfigureAwait(false)).Id;

                await _RedactorService.AddArticle(articleDomain.ToEntity()).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel());
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = (await _RedactorService.GetUserArticles(userId)).ToDomain();

            return View("Articles", res);
        }

        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> DetailArticle(int id)
        {
            try
            {
                var article = await _RedactorService.GetArticleById(id);

                return View("DetailArticle", article);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        [HttpGet]
        [Authorize(Roles = "Redactor")]
        public IActionResult CreateCategory()
        {
            return View("Category/Add");
        }

        [HttpPost]
        [Authorize(Roles = "Redactor")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            return View("Category/Add");
        }

        private async Task<Guid> GetCurrentUserId()
        {
            var currentUser = User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return (await _UserManager.FindByIdAsync(currentUserName).ConfigureAwait(false)).Id;
        }
    }
}