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
    public class FeatsController : Controller
    {
        private readonly DataContext _context;

        public FeatsController(DataContext context)
        {
            _context = context;
        }

        // GET: Feats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feats.ToListAsync());
        }

        public async Task<IActionResult> HomeFeatsHome(int? Id)
        {
            var membre = await (from m in _context.Membres
                                where m.Id.Equals(Id)
                                select m).FirstOrDefaultAsync();
            ViewBag.Id = Id;



            return View(membre);
        }


    }
}
