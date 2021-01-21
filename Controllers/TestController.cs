using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandami.Data;
using Pandami.Models;

namespace Pandami.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _context;
        public TestController(DataContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index()
        {
            return View(await _context.Membres.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
           var listsexes = await _context.Sexes.ToListAsync();
            listsexes = listsexes.ToList();
            ViewBag.sexes = listsexes;
           
            return View();
        }


    }
}
