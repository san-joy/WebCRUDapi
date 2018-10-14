using System;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(user model) { 
            if (!ModelState.IsValid) return View(model);
            model.Created = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) { 
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(user model) { 
            if (!ModelState.IsValid) return View(model);
            model.Created = DateTime.Now;
            _context.Users.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id){ 
           var user = await _context.Users.FindAsync(id);
           _context.Users.Remove(user);
           await _context.SaveChangesAsync();
           return RedirectToAction("index");
        }
    }
}