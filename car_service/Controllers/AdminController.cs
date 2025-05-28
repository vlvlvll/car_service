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
                return View(service); 
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
                    Directory.CreateDirectory(uploadsFolder); 

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


        
        public async Task<IActionResult> ServiceList()
        {
            var services = await _context.Services.ToListAsync();
            Console.WriteLine($"Type of services: {services.FirstOrDefault()?.GetType().FullName}");

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


        // GET: Admin/EditService/5
        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceID == id);
            Console.WriteLine($"ID: {id}");

            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Admin/EditService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(EfDbCarService.Service service, IFormFile imageFile)
        {
                if (!ModelState.IsValid)
                return View(service);

            var existingService = await _context.Services.FindAsync(service.ServiceID);
            if (existingService == null)
                return NotFound();

            existingService.ServiceName = service.ServiceName;
            existingService.Price = service.Price;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/services");
                Directory.CreateDirectory(uploadsFolder); 

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingService.ImagePath = "/images/services/" + uniqueFileName;
            }

            _context.Services.Update(existingService);
            await _context.SaveChangesAsync();

            return RedirectToAction("RemoveService");
        }





        public ActionResult Index()
        {
            return View();
        }

        

        
    }
}
