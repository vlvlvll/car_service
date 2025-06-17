using car_service.Models;
using EfDbCarService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace car_service.Controllers
{
    [Authorize(Roles = Constants.AdminRoleName)]
    public class AdminController : Controller
    {
        
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

        
        [HttpPost]
       
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

        public async Task<IActionResult> EditClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Cars)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null) return NotFound();

            var viewModel = new EditClientViewModel
            {
                ClientId = client.ClientId,
                FullName = client.FullName,
                PhoneNumber = client.PhoneNumber,
                CarId = client.Cars.FirstOrDefault()?.CarId ?? 0,
                Brand = client.Cars.FirstOrDefault()?.Brand,
                Version = client.Cars.FirstOrDefault()?.Version,
                VIN = client.Cars.FirstOrDefault()?.VIN,
                OrderId = client.Orders.FirstOrDefault()?.Id ?? 0,
                TotalPrice = client.Orders.FirstOrDefault()?.TotalPrice ?? 0
            };

            return View(viewModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> EditClient(EditClientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = await _context.Clients
                .Include(c => c.Cars)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.ClientId == model.ClientId);

            if (client == null) return NotFound();

            client.FullName = model.FullName;
            client.PhoneNumber = model.PhoneNumber;

            var car = client.Cars.FirstOrDefault(c => c.CarId == model.CarId);
            if (car != null)
            {
                car.Brand = model.Brand;
                car.Version = model.Version;
                car.VIN = model.VIN;
            }

            var order = client.Orders.FirstOrDefault(o => o.Id == model.OrderId);
            if (order != null)
            {
                order.TotalPrice = model.TotalPrice;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Данные клиента успешно обновлены!";
            return RedirectToAction("AllClients");
        }

        public async Task<IActionResult> AllClients()
        {
            var clients = await _context.Clients
                .Include(c => c.Cars)
                .Include(c => c.Orders)
                .ToListAsync();

            var viewModel = clients.Select(c => new ClientInfoViewModel
            {
                ClientId = c.ClientId,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                Cars = c.Cars.Select(car => new CarInfo
                {
                    Brand = car.Brand,
                    Version = car.Version,
                    VIN = car.VIN
                }).ToList(),
                Orders = c.Orders.Select(order => new OrderInfo
                {
                    TotalPrice = order.TotalPrice
                }).ToList()
            }).ToList();

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult RemoveClient()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }

        [HttpPost]
        
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Cars)
                    .ThenInclude(car => car.Orders)
                        .ThenInclude(o => o.Payments)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null)
                return NotFound();

            foreach (var order in client.Orders)
            {
                _context.Payments.RemoveRange(order.Payments);
            }

            _context.Orders.RemoveRange(client.Orders);

            _context.Cars.RemoveRange(client.Cars);

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Клиент и все связанные данные были удалены.";
            return RedirectToAction("AllClients");
        }



        public ActionResult Index()
        {
            return View();
        }

        

        
    }
}
