using AspNetCore.Utilities.Bootstrap5TagHelpers.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCore.Utilities.Bootstrap5TagHelpers.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SampleModel());
        }

        [HttpGet]
        public IActionResult StandardForm()
        {
            return View(new SampleModel());
        }

        [HttpGet]
        public IActionResult StandardFormStyled()
        {
            return View(new SampleModel());
        }

        [HttpPost]
        public IActionResult StandardForm(SampleModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult StandardFormStyled(SampleModel model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}