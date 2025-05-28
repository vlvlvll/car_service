using EfDbCarService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace car_service.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesRepository servicesDbRepository;

        public ServicesController(IServicesRepository serviceDbRepository)
        {
            this.servicesDbRepository = serviceDbRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        
    } 
}
