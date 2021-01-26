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
                return RedirectToAction("Login");
            }
            return View(membre);
        }

        public  IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id, Email, Mdp")] Membre membreLogin)
        {

            var membreLogged = await (from m in _context.Membres
                                      where m.Email.Equals(membreLogin.Email)
                                      select m).FirstOrDefaultAsync();
            ViewBag.Id = null;

            if (membreLogged != null && membreLogged.Mdp.Equals(membreLogin.Mdp))
            {
                ViewBag.Id = membreLogged.Id;

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
            ViewBag.id = membre.Id;

            return View(membre);

        }

        public async Task<IActionResult> ModifProfil(int? Id)
        {


            if (Id == null)
            {
                return NotFound();
            }



            var membre = _context.Membres
                       .Where(b => b.Id == Id)
                       .Include(b => b.Sexe)
                       .Include(b => b.Adresse)
                       .FirstOrDefault();

            if (membre == null)
            {

                return NotFound();
            }
            IQueryable<string> RecupGenre = from m in _context.Sexes
                                            orderby m.NomSexe
                                            select m.NomSexe;


            IQueryable<string> RecupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;



            ViewBag.ListSexe = new SelectList(await RecupGenre.Distinct().ToListAsync());
            ViewBag.ListAdresse = new SelectList(await RecupAdresse.Distinct().ToListAsync());



            return View(membre);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifProfil(int id, [Bind("Id,Email,Nom,Prenom,Naissance,Telephone,Mdp")] Membre membre,string Sexe, string Adresse)
        {
            


            if (id != membre.Id)
            {
                return NotFound();
            }
            var sexeMembre = await (from m in _context.Sexes
                                       where m.NomSexe.Contains(Sexe)
                                       select m).FirstOrDefaultAsync();

            var adresseMembre = await (from m in _context.Adresses
                                          where m.NomDeVoie.Contains(Adresse)
                                          select m).FirstOrDefaultAsync();
            membre.Sexe = sexeMembre;
            membre.Adresse = adresseMembre;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreExists(membre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(membre);
        }


        public async Task<IActionResult> Dispo(int Id)
        {
            ViewBag.Id = Id;
            List<Disponibilite> listDispo = _context.Disponibilites
                        .Where(b=>b.membre.Id ==Id)
                        //.Where(b => b.ValiditeFinDate >= DateTime.Now)
                       .Include(b => b.Jour)
                       .Include(b => b.membre)
                       .ToList();

            return View(listDispo);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dispo(int dispoId, int membreId)
        {
            ViewBag.Id = membreId;
            var dispo = await _context.Disponibilites.FindAsync(dispoId);
           
            _context.Disponibilites.Remove(dispo);
            await _context.SaveChangesAsync();

            return View();

        }

        public async Task<IActionResult> DispoAjout(int? Id)
        {
            ViewBag.IdMembre = Id;
            IQueryable<string> RecupJours = from m in _context.JourDeLaSemaines
                                              orderby m.Id
                                              select m.NomDuJour;
            ViewBag.Cree = false;

            Disponibilite newDispo = new Disponibilite
            {
                ValiditeDebutDate = DateTime.Now
            };
           


            ViewBag.ListJour = new SelectList(await RecupJours.Distinct().ToListAsync());

            return View(newDispo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DispoAjout(int? Id, [Bind("ValiditeDebutDate, ValiditeFinDate")] Disponibilite newDispo, bool matin, bool apresMidi, string Jour, int IdMembre)
        {
            ViewBag.Cree = false;
            newDispo.Jour = _context.JourDeLaSemaines.Where((m => m.NomDuJour == Jour)).FirstOrDefault();

            newDispo.membre = _context.Membres.Where(m => m.Id == IdMembre).FirstOrDefault();
            if (matin & apresMidi)
            {
                newDispo.DebutHeure= new System.DateTime(1,1,1,8,0,0);
                newDispo.FinHeure = new System.DateTime(1, 1, 1, 21, 0, 0);
            }else if (matin)
            {
                newDispo.DebutHeure = new System.DateTime(1, 1, 1, 8, 0, 0);
                newDispo.FinHeure = new System.DateTime(1, 1, 1, 14, 0, 0);
            }
            else if (apresMidi)
            {
                newDispo.DebutHeure = new System.DateTime(1, 1, 1, 14, 0, 0);
                newDispo.FinHeure = new System.DateTime(1, 1, 1, 21, 0, 0);
            }
            if (ModelState.IsValid)
            {

                _context.Add(newDispo);
                await _context.SaveChangesAsync();

                ViewBag.Cree = true;



                return View(newDispo);
            }
            

           

            

            return View();

        }

        private bool MembreExists(int id)
        {
            return _context.Membres.Any(e => e.Id == id);
        }

    }
}
    

    

