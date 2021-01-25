using Microsoft.AspNetCore.Mvc;
using Pandami.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pandami.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Web;

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


            IQueryable<string> RecupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;


            var newMember = new Membre.CreationMembre()
            {
                ListSexe = new SelectList(await RecupGenre.Distinct().ToListAsync()),
                ListAdresse = new SelectList(await RecupAdresse.Distinct().ToListAsync()),

            };

            return View(newMember);
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Nom,Prenom,Naissance,Telephone,Inscription, SexeChoisi, adresseChoisie, Mdp")] Membre.CreationMembre newMembre)
        {

            var sexeNewMembre = await (from m in _context.Sexes
                                       where m.NomSexe.Contains(newMembre.SexeChoisi)
                                       select m).FirstOrDefaultAsync();

            var adresseNewMembre = await (from m in _context.Adresses
                                          where m.NomDeVoie.Contains(newMembre.adresseChoisie)
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
                Mdp = newMembre.Mdp,
                Adresse = adresseNewMembre
            };


            if (ModelState.IsValid)
            {

                _context.Add(membre);
                await _context.SaveChangesAsync();
                int Id = 0;
                _context.Adresses.TakeLast(Id);

                String Idmembre = membre.Id.ToString();
                return RedirectToAction("HomeFeatsHome", "Feats", Idmembre);
            }
            return View(membre);
        }

        public async Task<IActionResult> Login([Bind("Id, Email, Mdp")] Membre membreLogin)
        {

            var membreLogged = await(from m in _context.Membres
                                     where m.Email.Equals(membreLogin.Email)
                                     select m).FirstOrDefaultAsync();
            ViewBag.Id = null;

            if (membreLogged != null && membreLogged.Mdp.Equals(membreLogin.Mdp))
            {
                ViewBag.Id = membreLogged.Id;
                ViewBag.Nom = membreLogged.Nom;
                ViewBag.Prenom = membreLogged.Prenom;
                
            }
            
            return View();
        }

        //public async Task<IActionResult> VerifPwd([Bind("Id, Email, Mdp")] Membre membreLogin)
        //{
        //
        //
        //    var membreLogged = await (from m in _context.Membres
        //                              where m.Email.Equals(membreLogin.Email)
        //                              select m).FirstOrDefaultAsync();
        //
        //    if (membreLogged != null && membreLogged.Mdp.Equals(membreLogin.Mdp))
        //    {
        //        
        //        
        //        return RedirectToAction(nameof(Create));
        //    }
        //
        //    
        //    return RedirectToAction(nameof(Login));
        //}

        public async Task<IActionResult> profil(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
                }

           // var membre = _context.Membres.Find(Id);

            var membre = _context.Membres
                       .Where(b => b.Id == Id)
                       .Include(b => b.Sexe)
                       .Include(b => b.Adresse)
                       .FirstOrDefault();

            if (membre == null)
            {

                return NotFound();
            }
            ViewBag.sexe = membre.Sexe.NomSexe;

            return View(membre);

        }


    } 
}
    

    

