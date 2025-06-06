﻿using car_service.Models;
using EfDbCarService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;


namespace car_service.Controllers
{
    public class ServicesController : Controller
    {
        
        private readonly CarServiceDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ServicesController(CarServiceDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult SignUpToService(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceID == id);
            if (service == null) return NotFound();

            var model = new SignUpViewModel
            {
                ServiceId = service.ServiceID,
                ServiceName = service.ServiceName,
                ServicePrice = service.Price
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SignUpToService(SignUpViewModel model)
        {


            var service = _context.Services.FirstOrDefault(s => s.ServiceID == model.ServiceId);
            if (service != null)
            {
                model.ServiceName = service.ServiceName;
                model.ServicePrice = service.Price;
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Ошибки валидации:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            try
            {
                var client = new EfDbCarService.Client
                {
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber
                };
                _context.Clients.Add(client);
                _context.SaveChanges();
                Console.WriteLine($"Добавлен клиент ID: {client.ClientId}");

                var car = new EfDbCarService.Car
                {
                    Brand = model.Brand,
                    Version = model.Version,
                    VIN = model.VIN,
                    ClientId = client.ClientId
                };
                _context.Cars.Add(car);
                _context.SaveChanges();
                Console.WriteLine($"Добавлен автомобиль ID: {car.CarId}");

                var order = new EfDbCarService.Order
                {
                    ClientId = client.ClientId,
                    CarId = car.CarId,
                    TotalPrice = model.ServicePrice
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                Console.WriteLine($"Добавлен заказ ID: {order.Id}");

                return View("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.ToString()}");
                ModelState.AddModelError("", "Произошла ошибка при сохранении данных");
                return View(model);
            }
        }



    }
}
