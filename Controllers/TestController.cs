using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandami.Data;
using Pandami.Models;
using Microsoft.Extensions.DependencyInjection;

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

        /* public async Task<IActionResult> Create()
        {
            IQueryable<string> sexeQuery = from m in _context.Sexes
                                            orderby m.NomSexe
                                            select m.NomSexe;

            var sexes = from m in _context.Sexes
                         select m;

              var CreaMembre = new Membre.CreationMembre()
               {
                   Sexe = new SelectList (await sexeQuery.Distinct().ToListAsync())
               };








               return View(CreaMembre);*/
    }


}

