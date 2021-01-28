using Microsoft.AspNetCore.Mvc;
using Pandami.Data;
using Pandami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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


        public async Task<IActionResult> Creation(int Id)
        {


            var membre = _context.Membres.Where(m => m.Id == Id)
                                            .Include(m => m.Adresse)
                                            .FirstOrDefault();

            Feat newFeat = new Feat()
            {
                Createur = membre,
               
            };

            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;

            IQueryable<string> recupMateriel = from m in _context.Materiels
                                               orderby m.NomMateriel
                                               select m.NomMateriel;

            IQueryable<string> recupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;
            

            ViewBag.Materiels = new SelectList(await recupMateriel.Distinct().ToListAsync());

            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());
            
            ViewBag.Adresse = new SelectList(await recupAdresse.Distinct().ToListAsync());

            ViewBag.AdresseCreateur = membre.Adresse.NomDeVoie;

            return View(newFeat);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat newFeat, int Createur, string Type, string Materiel, string Adresse)
        {
            var aideChoisieNewFeat = await (from m in _context.TypeAides
                                            where m.NomAide.Equals(Type)
                                            select m).FirstOrDefaultAsync();           
            
            var membreLogged = await (from m in _context.Membres
                                      where m.Id.Equals(Createur)
                                      select m).FirstOrDefaultAsync();

            var materielChoisi = await (from m in _context.Materiels
                                        where m.NomMateriel.Equals(Materiel)
                                        select m).FirstOrDefaultAsync();

            var adresseChoisie = await (from m in _context.Adresses
                                        where m.NomDeVoie.Equals(Adresse)
                                        select m).FirstOrDefaultAsync();

            ViewBag.IdMembre = Createur;
           

            Feat feat = new Feat()
            {
                CreationDate = DateTime.Now,
                RealisationDate = newFeat.RealisationDate,
                HeureDebut = newFeat.HeureDebut,
                HeureFin = newFeat.HeureFin,
                SommePrevoir = newFeat.SommePrevoir,
                EchangeMonetaire = newFeat.EchangeMonetaire,
                Type = aideChoisieNewFeat,
                Createur = membreLogged,
                Materiel = materielChoisi,
                Adresse = adresseChoisie
            };

            if (ModelState.IsValid)
            {
                _context.Add(feat);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = ViewBag.IdMembre });
            }
            return RedirectToAction("Profil", "PAMembres");
        }



        public async Task<IActionResult> HomeFeatsHome(int Id)
        {
            //Eagerly Loading pour charger toutes les entités liées
             List<Feat> listFeats = _context.Feats
                        .Where(b => b.AnnulationDate == null && b.Createur.Id != Id && b.ClotureDate == null && b.AcceptationDate == null)
                       .Include(b => b.Adresse)
                       .Include(b => b.Type)
                       .Include(b => b.Createur)
                       .ToList();

            ViewBag.IdMembre = Id;



            return View(listFeats);
        }

        public async Task<IActionResult> MesFeats(int Id)
        {
            
            //Eagerly Loading pour charger toutes les entités liées
             List<Feat> listFeats = _context.Feats
                       .Where(b => b.Createur.Id == Id)
                       .Include(b => b.Adresse)
                       .Include(b => b.Type)
                       .Include(b => b.Createur)
                       .Include(b => b.Materiel)
                       .Include("Reponses.Helper")
                       .ToList();

            ViewBag.IdMembre = Id;

            return View(listFeats);
        }

        public async Task<IActionResult> MesFeatsHelper(int Id)
        {
            List<ReponseHelper> reponsesHelper = _context.ReponseHelpers
                                                .Where(p => p.Helper.Id == Id && p.DesistementDate == null)
                                                .Include(p => p.Feat)
                                                .Include(p => p.Feat.Type)                                                
                                                .Include(p => p.Feat.Materiel)
                                                .Include(p => p.Feat.Createur)
                                                .Include(p => p.Feat.Adresse)
                                                .Include(p => p.Feat.Negociations)
                                                .ToList();

            var negociationsEnCours = _context.Negociations
                                                    .Where(n => n.IsAccepted == false && n.DateClotureNegociation == null)
                                                    .Select(n => n.feat.Id)
                                                    .ToList();


            var feats = from h in reponsesHelper
                        join m in _context.Membres on h.Helper.Id equals m.Id
                        where m.Id == Id
                        select h.Feat;

            //feats = feats.Distinct();

            ViewBag.IdMembre = Id;
            ViewBag.ListIdNegociationsEnCours = negociationsEnCours;


            return View(feats);
        }


        public async Task<IActionResult> ModifFeat(int Id)              //IdFeat
        {
            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;

            IQueryable<string> recupMateriel = from m in _context.Materiels
                                               orderby m.NomMateriel
                                               select m.NomMateriel;

            IQueryable<string> recupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;


 

            Feat featToModify = _context.Feats
                                .Where(b => b.Id == Id)
                                .Include(b => b.Createur)
                                .Include(b => b.Adresse)
                                .Include(b => b.Materiel)
                                .Include(b => b.Type)
                                .FirstOrDefault();

           Membre membreLogged = _context.Membres
                                .Where(b => b.Id == featToModify.Createur.Id).FirstOrDefault();
            ViewBag.IdMembre = membreLogged.Id;

            ViewBag.IdFeat = Id;

            ViewBag.Materiels = new SelectList(await recupMateriel.Distinct().ToListAsync());

            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());

            ViewBag.Adresse = new SelectList(await recupAdresse.Distinct().ToListAsync());

            return View(featToModify);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierMonFeat([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int IdMembre, string Type, string Materiel, string Adresse)
        {
            var aideChoisie = await (from m in _context.TypeAides
                                            where m.NomAide.Equals(Type)
                                            select m).FirstOrDefaultAsync();


            var materielChoisi = await (from m in _context.Materiels
                                        where m.NomMateriel.Equals(Materiel)
                                        select m).FirstOrDefaultAsync();

            var adresseChoisie = await (from m in _context.Adresses
                                        where m.NomDeVoie.Equals(Adresse)
                                        select m).FirstOrDefaultAsync();
            featToModify.Materiel = materielChoisi;
            featToModify.Adresse = adresseChoisie;
            featToModify.Type = aideChoisie;

            ViewBag.IdMembre = IdMembre;




            if (ModelState.IsValid)
            {
                _context.Update(featToModify);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }

        public async Task<IActionResult> AnnulationFeat(int Id)  //ID Feat
        {
            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;

            Feat featToCancel = _context.Feats
                                .Where(b => b.Id == Id)
                                .Include(b => b.Createur)
                                .Include(b => b.Type)
                                .FirstOrDefault();


            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());

            return View(featToCancel);
        } 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnnulerMonFeat([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int IdMembre)

        {
            featToModify.AnnulationDate = DateTime.Now;
            // featToModify.ClotureDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Update(featToModify);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }

        public async Task<IActionResult> DesistementFeat(int Id)
        {
            ReponseHelper reponseFeatDesistement = _context.ReponseHelpers
                                                .Where(b => b.Id == Id)
                                                .Include(b => b.Helper)
                                                .Include(b => b.Feat)
                                                .Include(b => b.Feat.Type)
                                                .Include(b => b.Feat.Createur)
                                                .FirstOrDefault();

            return View(reponseFeatDesistement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeDesisterDuFeat([Bind("Id, AcceptationDate, DesistementDate")] ReponseHelper reponseFeatDesistement, int IdFeat, int IdMembre)
        {

            Feat featDesistement = _context.Feats
                                .Where(b => b.Id == IdFeat)
                                .Include(b => b.Createur)
                                .Include(b => b.Type)
                                .Include(b => b.Materiel)
                                .FirstOrDefault();

            featDesistement.AcceptationDate = null;            
            reponseFeatDesistement.DesistementDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Update(featDesistement);
                _context.Update(reponseFeatDesistement);
                await _context.SaveChangesAsync();
                return RedirectToAction("MesFeatsHelper", "PAFeats", new { @id = IdMembre });
            }
            return View("HomeFeatsHome");
            
        }




            public async Task<IActionResult> Details(int IdFeat, int IdMembre)  //ID Feat
        {
           
            Feat featEnAttente = _context.Feats
                                .Where(b => b.Id == IdFeat)
                                .Include(b => b.Createur)
                                .Include(b => b.Type)
                                .FirstOrDefault();

            ViewBag.IdMembre = IdMembre;
      
            return View(featEnAttente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepterFeat([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")]int IdMembre, int IdFeat)
        {

            Membre membreLogged = _context.Membres
                                .Where(b => b.Id == IdMembre)
                                .FirstOrDefault();

            Feat featEnAttente = _context.Feats
                                .Where(b => b.Id == IdFeat)
                                .FirstOrDefault();

            featEnAttente.AcceptationDate = DateTime.Now;

            ReponseHelper reponseHelper = new ReponseHelper()
            {
                AcceptationDate = DateTime.Now,
                Helper = membreLogged,
                Feat = featEnAttente
            };

            
            if (ModelState.IsValid)
            {   
                _context.Add(reponseHelper);
                _context.Update(featEnAttente);
                await _context.SaveChangesAsync();

                return RedirectToAction("MesFeatsHelper", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }
        public async Task<IActionResult> ModifFeatHelper(int IdFeat, int IdMembre, int IdReponse)              //IdFeat
        {
            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;

            IQueryable<string> recupMateriel = from m in _context.Materiels
                                               orderby m.NomMateriel
                                               select m.NomMateriel;

            IQueryable<string> recupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;




            Feat featToModify = _context.Feats
                                .Where(b => b.Id == IdFeat)
                                .Include(b => b.Createur)
                                .Include(b => b.Adresse)
                                .Include(b => b.Materiel)
                                .Include(b => b.Type)
                                .FirstOrDefault();

            ViewBag.IdMembre = IdMembre;

            ViewBag.IdFeat = IdFeat;

            ViewBag.Materiels = new SelectList(await recupMateriel.Distinct().ToListAsync());

            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());

            ViewBag.Adresse = new SelectList(await recupAdresse.Distinct().ToListAsync());

            return View(featToModify);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> ModifierMonFeatHelper ([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int IdMembre, string Type, string Materiel, string Adresse, int Id)
        {
            var aideChoisie = await (from m in _context.TypeAides
                                     where m.NomAide.Equals(Type)
                                     select m).FirstOrDefaultAsync();


            var materielChoisi = await (from m in _context.Materiels
                                        where m.NomMateriel.Equals(Materiel)
                                        select m).FirstOrDefaultAsync();

            var adresseChoisie = await (from m in _context.Adresses
                                        where m.NomDeVoie.Equals(Adresse)
                                        select m).FirstOrDefaultAsync();
            featToModify.Materiel = materielChoisi;
            featToModify.Adresse = adresseChoisie;
            featToModify.Type = aideChoisie;

            ViewBag.IdMembre = IdMembre;


            if (ModelState.IsValid)
            {
                _context.Update(featToModify);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeatsHelper", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }
        public async Task<IActionResult> ModifFeatGiftee(int Id , int IdMembre)              //IdFeat
        {
            IQueryable<string> recupTypeAide = from m in _context.TypeAides
                                               orderby m.NomAide
                                               select m.NomAide;

            IQueryable<string> recupMateriel = from m in _context.Materiels
                                               orderby m.NomMateriel
                                               select m.NomMateriel;

            IQueryable<string> recupAdresse = from m in _context.Adresses
                                              orderby m.NomDeVoie
                                              select m.NomDeVoie;




            Feat featToModify = _context.Feats
                                .Where(b => b.Id == Id)
                                .Include(b => b.Createur)
                                .Include(b => b.Adresse)
                                .Include(b => b.Materiel)
                                .Include(b => b.Type)
                                .FirstOrDefault();


            

            ViewBag.Materiels = new SelectList(await recupMateriel.Distinct().ToListAsync());

            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());

            ViewBag.Adresse = new SelectList(await recupAdresse.Distinct().ToListAsync());

            return View(featToModify);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ModifierMonFeatGiftee([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int IdMembre, string Type, string Materiel, string Adresse, int Id)
        {
            featToModify = _context.Feats
                       .Where(b => b.Id == Id)
                       .Include(b => b.Createur)
                       .Include(b => b.Adresse)
                       .Include(b => b.Materiel)
                       .Include(b => b.Type)
                       .FirstOrDefault();


            ReponseHelper reponseDuFeat = _context.ReponseHelpers
                                        .Where(b => b.Feat.Id == Id)
                                        .Include(b => b.Helper)
                                        .FirstOrDefault();

            Negociation negocProposee = new Negociation()
            {
                DateCreationNegociation = DateTime.Now,
                NewDateProposee = featToModify.RealisationDate,
                HeureDbtProposee = featToModify.HeureDebut,
                HeureFinProposee = featToModify.HeureFin,
                DemandeurId = IdMembre,
                RepondeurId = reponseDuFeat.Helper.Id,
                feat = featToModify
                //IsAccepted = false
            };

            if (ModelState.IsValid)
            {
                _context.Add(negocProposee);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");

        }






        public async Task<IActionResult> VisualiserNegociation (int IdFeat, int IdMembre)
        {


            Negociation negocProposee = _context.Negociations
                .Where(b => b.feat.Id == IdFeat)
                .Include(b => b.feat)
                .Include(b => b.Demandeur)
                .Include(b => b.Repondeur)
                .Include(b => b.feat.Type)
                .Include(b => b.feat.Createur)
                .Include(b => b.feat.Materiel)
                .FirstOrDefault();

            return View(negocProposee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepterNegociation([Bind("Id, DateClotureNegociation, NewDateProposee, HeureDbtProposee, HeurFinProposee, IsAccepted, DateCreationNegociation")]Negociation negocProposee, int Id, int IdMembre, int IdFeat)
        {
            negocProposee = _context.Negociations
                .Where(b => b.Id == Id)
                .Include(b => b.feat)
                .Include(b => b.Demandeur)
                .Include(b => b.Repondeur)
                .Include(b => b.feat.Type)
                .Include(b => b.feat.Createur)
                .Include(b => b.feat.Materiel)
                .FirstOrDefault();

          Feat featToModify = _context.Feats
                       .Where(b => b.Id == IdFeat)
                       .Include(b => b.Createur)
                       .Include(b => b.Adresse)
                       .Include(b => b.Materiel)
                       .Include(b => b.Type)
                       .FirstOrDefault();

            negocProposee.IsAccepted = true;
            negocProposee.DateClotureNegociation = DateTime.Now;

            featToModify.RealisationDate = negocProposee.NewDateProposee;
            featToModify.HeureDebut = negocProposee.HeureDbtProposee;
            featToModify.HeureFin = negocProposee.HeureFinProposee;
            

            if (ModelState.IsValid)
            {
                _context.Update(negocProposee);
                _context.Update(featToModify);

                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefuserNegociation([Bind("Id, DateClotureNegociation, NewDateProposee, HeureDbtProposee, HeurFinProposee, IsAccepted, DateCreationNegociation")] Negociation negocProposee, int Id, int IdMembre, int IdFeat)
        {
            negocProposee = _context.Negociations
                .Where(b => b.Id == Id)
                .Include(b => b.feat)
                .Include(b => b.Demandeur)
                .Include(b => b.Repondeur)
                .Include(b => b.feat.Type)
                .Include(b => b.feat.Createur)
                .Include(b => b.feat.Materiel)
                .FirstOrDefault();

            negocProposee.IsAccepted = false;
            negocProposee.DateClotureNegociation = DateTime.Now;



            if (ModelState.IsValid)
            {
                _context.Update(negocProposee);

                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            return RedirectToAction("HomeFeatsHome");
        }
    }
}
