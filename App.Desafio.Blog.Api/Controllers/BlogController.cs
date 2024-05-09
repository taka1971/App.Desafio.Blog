using Microsoft.AspNetCore.Mvc;

namespace App.Desafio.Blog.Api.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
