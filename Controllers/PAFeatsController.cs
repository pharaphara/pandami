using Microsoft.AspNetCore.Mvc;
using Pandami.Data;
using Pandami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Pandami.Controllers
{
    public class PAFeatsController : Controller
    {
        private readonly DataContext _context;

        public PAFeatsController(DataContext context)
        {
            _context = context;
        }
        //dessin de bite
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Creation([Bind("Id, Email, Mdp")] int? Id)
        {

           var membreLogged = await (from m in _context.Membres
                                    where m.Id.Equals(Id)
                                    select m).FirstOrDefaultAsync();


            ViewBag.Id = Id;
            ViewBag.Nom = membreLogged.Nom;

            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;
            var newFeat = new Feat.CreationFeat()
            {
                TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync())
            };

            return View(newFeat);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id, CreationDate, RealisatinDate, HeureDebut, HeureFin" +
            "AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate"+
            "AnnulationDate, EchangeMonetaire, AideChoisie")] Feat.CreationFeat newFeat, int Createur, string test)
        {
            var aideChoisieNewFeat = await (from m in _context.TypeAides
                                            where m.NomAide.Equals(newFeat.AideChoisie)
                                            select m).FirstOrDefaultAsync();

            
            
            var membreLogged = await (from m in _context.Membres
                                      where m.Id.Equals(Createur)
                                      select m).FirstOrDefaultAsync();

            ViewBag.Id = Createur;

            Feat feat = new Feat()
            {
                CreationDate = DateTime.Now,
                RealisationDate = newFeat.RealisationDate,
                HeureDebut = newFeat.HeureDebut,
                HeureFin = newFeat.HeureFin,
                AcceptationDate = null,
                EnCoursRealisation = null,
                SurPlace = null,
                FinFeatHelper = null,
                ClotureDate = null,
                SommePrevoir = newFeat.SommePrevoir,
                SommeAvancee = null,
                SommeRembourseeDate = null,
                AnnulationDate = null,
                EchangeMonetaire = newFeat.EchangeMonetaire,
                Type = aideChoisieNewFeat,
                Createur = membreLogged
                
            };

            if (ModelState.IsValid)
            {
                _context.Add(feat);
                await _context.SaveChangesAsync();


                return RedirectToAction("HomeFeatsHome", "PAFeats");
            }
            return RedirectToAction("Profil", "PAMembres");
        }



        public async Task<IActionResult> HomeFeatsHome(int? Id)
        {
            var membre = await (from m in _context.Membres
                                where m.Id.Equals(Id)
                                select m).FirstOrDefaultAsync();
            ViewBag.Id = Id;

            var listeFeats = _context.Feats
                       .Include(b => b.Type).ToListAsync();
            var total = _context.Feats.Count();
            for (int i = 0 ; i == total; i++ )
            {
                var feat = _context.Feats
                            .Where(b => b.Id == i)
                            .Include(b => b.Type)
                            .Include(b => b.Createur).FirstOrDefault();
            }

            return View(listeFeats);
        }
    }
}
