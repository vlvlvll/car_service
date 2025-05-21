using EfDbCarService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace car_service.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        private readonly CarServiceDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AdminController(CarServiceDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
       
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddService(EfDbCarService.Service service, IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Пожалуйста, добавьте фото услуги.");
                return View(service); // вернём форму обратно с ошибкой
            }

            if (!ModelState.IsValid)
            {
                return View(service);
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/services");
                    Directory.CreateDirectory(uploadsFolder); // на случай, если папка не существует

                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    service.ImagePath = "/images/services/" + fileName;
                }

                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("ServiceList");
            }

            return View(service);
        }


        // Просмотр всех услуг (опционально)
        public async Task<IActionResult> ServiceList()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        [HttpGet]
        public IActionResult RemoveService()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        [HttpPost]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceID == id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }

            return RedirectToAction("RemoveService");
        }




        public ActionResult Index()
        {
            return View();
        }

        

        
    }
}
