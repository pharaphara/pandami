using Microsoft.AspNetCore.Mvc;
using Pandami.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pandami.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Pandami.Controllers
{
    public class PAMembresController : Controller
    {
        private readonly DataContext _context;

        public PAMembresController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Membres.ToListAsync());
        }

        public async Task<IActionResult> Creation()
        {
            IQueryable<string> RecupGenre = from m in _context.Sexes
                                            orderby m.NomSexe
                                            select m.NomSexe;

            var newMember = new Membre.CreationMembre()
            {
                ListSexe = new SelectList(await RecupGenre.Distinct().ToListAsync())
            };

            return View(newMember);
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Nom,Prenom,Naissance,Telephone,Inscription, SexeChoisi, Mdp")] Membre.CreationMembre newMembre)
        {

            var sexeNewMembre = await (from m in _context.Sexes
                             where m.NomSexe.Contains(newMembre.SexeChoisi)
                             select m).FirstOrDefaultAsync();
           


            Membre membre = new Membre()
            {
                Email = newMembre.Email,
                Nom = newMembre.Nom,
                Prenom = newMembre.Prenom,
                Naissance = newMembre.Naissance,
                Telephone = newMembre.Telephone,
                Inscription = DateTime.Now,
                Sexe = sexeNewMembre,
                Mdp = newMembre.Mdp
            };


            if (ModelState.IsValid)
            {
                _context.Add(membre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membre);
        }
    }

    
}
