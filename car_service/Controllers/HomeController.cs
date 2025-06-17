using System.Diagnostics;
using car_service.Models;
using EfDbCarService;
using Microsoft.AspNetCore.Mvc;

namespace car_service.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesRepository servicesDbRepository;

        public HomeController(IServicesRepository serviceDbRepository)
        {
            this.servicesDbRepository = serviceDbRepository;
        }


        public IActionResult Index()
        {
            var services = servicesDbRepository.GetAll();
            ViewBag.Count = services.Count;
            return View(services);
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
