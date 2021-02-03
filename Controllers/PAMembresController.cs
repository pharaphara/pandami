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

        public IActionResult Login()
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
            ViewBag.IdMembre = null;

            if (membreLogged != null && membreLogged.Mdp.Equals(membreLogin.Mdp))
            {
                ViewBag.IdMembre = membreLogged.Id;
                return RedirectToAction("HomeFeatsHome", "PAFeats", new { @id = ViewBag.IdMembre });
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
            ViewBag.idMembre = membre.Id;
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

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

            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;
            ViewBag.IdMembre = membre.Id;

            return View(membre);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifProfil(int id, [Bind("Id,Email,Nom,Prenom,Naissance,Telephone,Inscription,Mdp")] Membre membre, string Sexe, string Adresse)
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
            ViewBag.IdMembre = membre.Id;

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
                return RedirectToAction("profil", "PAMembres", new { @id = ViewBag.IdMembre });
            }
            return View(membre);
        }


        public async Task<IActionResult> Dispo(int Id)
        {
            ViewBag.IdMembre = Id;
            List<Disponibilite> listDispo = _context.Disponibilites
                        .Where(b => b.membre.Id == Id)
                       //.Where(b => b.ValiditeFinDate >= DateTime.Now) 
                       .Include(b => b.Jour)
                       .Include(b => b.membre)
                       .ToList();

            Membre membre = _context.Membres
                                .Where(b => b.Id == Id).FirstOrDefault();
            ViewBag.IdMembre = membre.Id;
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            return View(listDispo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dispo(int dispoId, int membreId)
        {
            ViewBag.IdMembre = membreId;
            var dispo = await _context.Disponibilites.FindAsync(dispoId);

            _context.Disponibilites.Remove(dispo);
            await _context.SaveChangesAsync();

            List<Disponibilite> listDispo = _context.Disponibilites
                        .Where(b => b.membre.Id == membreId)
                       //.Where(b => b.ValiditeFinDate >= DateTime.Now)
                       .Include(b => b.Jour)
                       .Include(b => b.membre)
                       .ToList();

            return View(listDispo);

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

            Membre membre = _context.Membres
                    .Where(b => b.Id == Id).FirstOrDefault();
            ViewBag.IdMembre = membre.Id;
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;


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
                newDispo.DebutHeure = new System.DateTime(1, 1, 1, 8, 0, 0);
                newDispo.FinHeure = new System.DateTime(1, 1, 1, 21, 0, 0);
            }
            else if (matin)
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

        public async Task<IActionResult> Pref(int Id)
        {
            ViewBag.IdMembre = Id;

            List<SelectListItem> ListCheckType = await GetTypeAide();
            List<string> ListSelectedType = GetTypeAideChoisi(Id);

            ViewBag.CheckType = ListCheckType;
            ViewBag.SelectType = ListSelectedType;


            var oldRayon = GetTRayonAction(Id);
            if (oldRayon.Count == 0)
            {
                ViewBag.rayon = 0;
            }
            else
            {
                ViewBag.rayon = oldRayon.FirstOrDefault().Rayon;
            }



            ViewBag.Verif = 0;
            if (ListSelectedType.Count > 0)
            {
                ViewBag.Verif = 1;
            }
            Membre membre = _context.Membres
                    .Where(b => b.Id == Id).FirstOrDefault();
            ViewBag.IdMembre = membre.Id;
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Pref(int membreId, string[] CheckBoxes, float rayon)
        {
            ViewBag.IdMembre = membreId;
            Membre membre = GetMembre(membreId);
            IList<PreferenceAide> NewPref = new List<PreferenceAide>();

            IList<PreferenceAide> OldPref = await (from m in _context.PreferenceAides
                                                   where m.Membre.Id.Equals(membreId)
                                                   where m.ValiditeFin == null
                                                   orderby m.Id
                                                   select m)
                                                   .Include(p => p.TypeAide)
                                                   .ToListAsync();

            foreach (string item in CheckBoxes)
            {
                var typeChoisi = await (from m in _context.TypeAides
                                        where m.NomAide.Contains(item)
                                        select m).FirstOrDefaultAsync();

                PreferenceAide preferenceAide = new PreferenceAide
                {
                    Membre = membre,
                    TypeAide = typeChoisi,
                    ValiditeDebut = DateTime.Now
                };

                NewPref.Add(preferenceAide);
            }




            for (int i = 0; i < NewPref.Count; i++)
            {
                for (int j = 0; j < OldPref.Count; j++)
                {

                    if (NewPref[i].TypeAide.Id == OldPref[j].TypeAide.Id)
                    {
                        NewPref.Remove(NewPref[i]);
                        OldPref.Remove(OldPref[j]);
                    }
                }
            }
            if (OldPref.Count > 0)
            {
                foreach (var item in OldPref)
                {
                    item.ValiditeFin = DateTime.Now;

                    _context.Update(item);
                    await _context.SaveChangesAsync();

                }
            }

            if (NewPref.Count > 0)
            {
                foreach (var item in NewPref)
                {


                    _context.Add(item);
                    await _context.SaveChangesAsync();

                }
            }
            RayonAction newRayon = new RayonAction
            {
                Membre = GetMembre(membreId),
                ValiditeDebut = DateTime.Now,
                Rayon = rayon

            };
            var oldRayon = GetTRayonAction(membreId);
            if (rayon != 0)
            {


                if (oldRayon.Count == 0 && rayon != 0)
                {
                    _context.Add(newRayon);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (oldRayon.FirstOrDefault().Rayon != newRayon.Rayon)
                    {
                        oldRayon.FirstOrDefault().ValiditeFin = DateTime.Now;
                        _context.Update(oldRayon.FirstOrDefault());
                        _context.Add(newRayon);
                        await _context.SaveChangesAsync();
                    }
                }
            }



            return RedirectToAction("Profil", "PaMembres", new { @id = membreId }); ;

        }

        private Membre GetMembre(int idmembre)
        {
            return (_context.Membres.Where(m => m.Id == idmembre).FirstOrDefault());
        }

        private List<string> GetTypeAideChoisi(int Id)
        {


            List<string> ListTypeAideChoisie = _context.PreferenceAides
                                              .Where(p => p.Membre.Id == Id)
                                              .Where(p => p.ValiditeFin == null)
                                              .Include(p => p.TypeAide)
                                              .Select(p => p.TypeAide.NomAide)

                                              .ToList();





            return ListTypeAideChoisie;
        }
        private List<RayonAction> GetTRayonAction(int Id)
        {
            List<RayonAction> rayonAction = new List<RayonAction>();

            if (_context.RayonActions.Any())
            {
                rayonAction = _context.RayonActions
                                                 .Where(p => p.Membre.Id == Id)
                                                 .Where(p => p.ValiditeFin == null)

                                                 .ToList();

            }



            return rayonAction;
        }
        private async Task<List<SelectListItem>> GetTypeAide()
        {
            IQueryable<string> RecupGenre = from m in _context.TypeAides
                                            orderby m.NomAide
                                            select m.NomAide;

            List<string> ListTypeAide = new List<string>(await RecupGenre.Distinct().ToListAsync());

            List<SelectListItem> ListCheckType = new List<SelectListItem>();

            foreach (var item in ListTypeAide)
            {

                ListCheckType.Add(new SelectListItem { Text = item, Value = item });
            }




            return ListCheckType;
        }






        private bool MembreExists(int id)
        {
            return _context.Membres.Any(e => e.Id == id);
        }



    }
}




