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
                RealisationDate = DateTime.Now,


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


            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

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



        public async Task<IActionResult> HomeFeatsHome(int Id, string recherche, DateTime date)
        {
            //Eagerly Loading pour charger toutes les entités liées
            IList<Feat> listFeats = _context.Feats
                       .Where(b => b.AnnulationDate == null && b.Createur.Id != Id && b.ClotureDate == null && b.AcceptationDate == null)
                      .Include(b => b.Adresse)
                      .Include(b => b.Type)
                      .Include(b => b.Createur)
                      .Include("Reponses.Helper")
                      .OrderBy(b => b.RealisationDate)
                      .ToList();

            if (!String.IsNullOrEmpty(recherche))
            {
                listFeats = listFeats.Where(s => s.Type.NomAide.Contains(recherche)).ToList();
            }

            if (!(date.Date<DateTime.Now.Date))
            {
                listFeats = listFeats.Where(s => s.RealisationDate.Date==date.Date).ToList();
            }


            ViewBag.Type = new SelectList( (from m in _context.TypeAides select m.NomAide).Distinct().ToList());
            ViewBag.IdMembre = Id;

            Membre membre = _context.Membres
             .Where(b => b.Id == Id)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;


            return View(listFeats);
        }
        public async Task<IActionResult> HomeFeatsHome2(int Id)
        {
            //Eagerly Loading pour charger toutes les entités liées
            List<Feat> listFeats = _context.Feats
                       .Where(b => b.AnnulationDate == null && b.Createur.Id != Id && b.ClotureDate == null && b.AcceptationDate == null)
                      .Include(b => b.Adresse)
                      .Include(b => b.Type)
                      .Include(b => b.Createur)
                      .Include("Reponses.Helper")
                      .OrderBy(b => b.RealisationDate)
                      .ToList();


            List<PreferenceAide> listPrefs = _context.PreferenceAides
                                               .Where(p => p.Membre.Id == Id)
                                               .Where(p => p.ValiditeFin == null)
                                               .Where(p => p.ValiditeDebut < DateTime.Now)
                                               .ToList();

            List<Disponibilite> listDispos = _context.Disponibilites
                                                .Where(p => p.membre.Id == Id)
                                               .Where(p => p.ValiditeFinDate > DateTime.Now || p.ValiditeFinDate == null)
                                               .Where(p => p.ValiditeDebutDate < DateTime.Now)
                                               .Include(p => p.Jour)
                                               .ToList();




            List<Feat> feats = (from p in listPrefs
                                join f in listFeats on p.TypeAide equals f.Type
                                select f).ToList();

            feats = (from f in feats
                     join d in listDispos on (int)f.RealisationDate.DayOfWeek equals (d.Jour.Id - 1) //-1 pour matcher le système de jour anglais
                     where d.ValiditeDebutDate < f.RealisationDate && (d.ValiditeFinDate == null || d.ValiditeFinDate > f.RealisationDate)
                     where f.HeureDebut.TimeOfDay >= d.DebutHeure.TimeOfDay
                     where f.HeureFin.TimeOfDay <= d.FinHeure.TimeOfDay
                     select f).ToList();


            ViewBag.NoResult = false;

            if (feats.Count == 0)
            {
                feats = listFeats;
                ViewBag.NoResult = true;
            }


            Membre membre = _context.Membres
             .Where(b => b.Id == Id)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            ViewBag.IdMembre = Id;



            return View(feats);
        }
        public async Task<IActionResult> MesFeats(int Id)
        {

            //Eagerly Loading pour charger toutes les entités liées
            List<Feat> listFeats = _context.Feats
                      .Where(b => b.Createur.Id == Id && b.ClotureDate == null)
                      .Include(b => b.Adresse)
                      .Include(b => b.Type)
                      .Include(b => b.Createur)
                      .Include(b => b.Materiel)
                      .Include("Reponses.Helper")
                      .OrderBy(b => b.RealisationDate)
                      .ToList();


            var negociationsEnCours = _context.Negociations
                                                    .Where(n => n.IsAccepted == false && n.DateClotureNegociation == null)
                                                    .Select(n => n.feat.Id)
                                                    .ToList();




            ViewBag.ListIdNegociationsEnCours = negociationsEnCours;

            ViewBag.IdMembre = Id;

            Membre membre = _context.Membres
                 .Where(b => b.Id == Id)
                 .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            var reponsesActives = _context.ReponseHelpers
                            .Where(b => b.Feat.Createur.Id == Id)
                            .Where(b => b.DesistementDate == null)
                            .Select(b => b.Feat.Id)
                            .ToList();

            ViewBag.IdMembre = Id;
            ViewBag.ReponsesActives = reponsesActives;
 


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
                        orderby h.Feat.RealisationDate
                        select h.Feat;

            //feats = feats.Distinct();

            ViewBag.IdMembre = Id;
            ViewBag.ListIdNegociationsEnCours = negociationsEnCours;

            Membre membre = _context.Membres
              .Where(b => b.Id == Id)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;


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


            ViewBag.PrenomMembre = membreLogged.Prenom;
            ViewBag.NomMembre = membreLogged.Nom;

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

            Membre membre = _context.Membres
             .Where(b => b.Id == featToCancel.Createur.Id)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

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


        public ActionResult DesistementFeat(int IdMembre, int IdFeat)
        {


            ReponseHelper reponseFeatDesistement = _context.ReponseHelpers
                                                .Where(b => b.Feat.Id == IdFeat)
                                                .Where(b => b.DesistementDate == null)
                                                .FirstOrDefault();
            Feat feat = GetFeat(IdFeat);
            feat.AcceptationDate = null;

            reponseFeatDesistement.DesistementDate = DateTime.Now;


            _context.UpdateRange(reponseFeatDesistement, feat);
            _context.SaveChanges();

            Membre membre = _context.Membres
             .Where(b => b.Id == IdMembre)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            return RedirectToAction("MesFeatsHelper", "PAFeats", new { @id = IdMembre });
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

            Feat feat = _context.Feats
                                .Where(b => b.Id == IdFeat)
                                .Include(b => b.Createur)
                                .Include("Reponses.Helper")
                                .Include(b => b.Adresse)
                                .Include(b => b.Materiel)
                                .Include(b => b.Type)
                                .FirstOrDefault();

            var Reponses = _context.ReponseHelpers
                                .Where(b => b.Feat.Id == IdFeat)
                                .Where(b => b.DesistementDate == null)
                                .ToList();

            ViewBag.IdMembre = IdMembre;
            ViewBag.Reponses = Reponses;
            if(Reponses.Count() != 0)
            {
                ViewBag.PrenomHelper = Reponses.FirstOrDefault().Helper.Prenom;
                ViewBag.NomHelper = Reponses.FirstOrDefault().Helper.Nom;
            }



            Membre membre = _context.Membres
             .Where(b => b.Id == IdMembre)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            if (feat.Createur.Id == IdMembre)
            {
                ViewBag.Affichage = 1;
            }
            else
            {
                ViewBag.Affichage = 2;
            }

            return View(feat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepterFeat([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] int IdMembre, int IdFeat)
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

            Membre membre = _context.Membres
             .Where(b => b.Id == IdMembre)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            return View(featToModify);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ModifierMonFeatHelper([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int IdMembre, string Type, string Materiel, string Adresse, int Id)
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

        public async Task<IActionResult> ModifFeatGiftee(int IdFeat, int IdMembre, int JeSuis)              //IdFeat
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
                                .Include("Reponses.Helper")
                                .FirstOrDefault();


            ViewBag.IdMembre = IdMembre;
            ViewBag.JeSuis = JeSuis;

            ViewBag.Materiels = new SelectList(await recupMateriel.Distinct().ToListAsync());

            ViewBag.TypesAide = new SelectList(await recupTypeAide.Distinct().ToListAsync());

            ViewBag.Adresse = new SelectList(await recupAdresse.Distinct().ToListAsync());

            Membre membre = _context.Membres
             .Where(b => b.Id == IdMembre)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            return View(featToModify);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ModifierMonFeatGiftee([Bind("Id, CreationDate, RealisationDate, HeureDebut, HeureFin, AcceptationDate, EnCoursRealisation, SurPlace, FinFeatHelper, ClotureDate, SommePrevoir, SommeAvancee, SommeRembourseeDate, AnnulationDate, EchangeMonetaire, AideChoisie, Materiel")] Feat featToModify, int JeSuis, int IdMembre)
        {



            Feat oldFeat = GetFeat(featToModify.Id);

            var ReponseValide = _context.ReponseHelpers
                                        .Where(b => b.Feat.Id == featToModify.Id)
                                        .Where(b => b.DesistementDate == null)
                                        .Include(b => b.Helper)
                                        .FirstOrDefault();



            Negociation newNegociation = new Negociation();

            if (oldFeat.HeureDebut != featToModify.HeureDebut || oldFeat.HeureFin != featToModify.HeureFin || oldFeat.RealisationDate != featToModify.RealisationDate)
            {

                var newDate = featToModify.RealisationDate;
                var newHeureDeb = featToModify.HeureDebut;
                var newHeureFin = featToModify.HeureFin;
                featToModify = new Feat();

                newNegociation = new Negociation()
                {
                    DateCreationNegociation = DateTime.Now,
                    NewDateProposee = newDate,
                    HeureDbtProposee = newHeureDeb,
                    HeureFinProposee = newHeureFin,
                    feat = oldFeat,
                    IsAccepted = false

                };

                if (JeSuis==1)
                {
                    newNegociation.DemandeurId = oldFeat.Createur.Id;
                    newNegociation.RepondeurId = ReponseValide.Helper.Id;
                }
                else
                {
                    newNegociation.DemandeurId = ReponseValide.Helper.Id;
                    newNegociation.RepondeurId = oldFeat.Createur.Id;
                }
            }

            // _context.Entry(featToModify).Reference(p => p.Createur).Load();

            if (ModelState.IsValid)
            {
                _context.Add(newNegociation);
                await _context.SaveChangesAsync();


                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre  });
            }
            return RedirectToAction("HomeFeatsHome");

        }






        public async Task<IActionResult> VisualiserNegociation(int IdFeat, int IdMembre)
        {


            Negociation negocProposee = _context.Negociations
                .Where(b => b.feat.Id == IdFeat)
                .Where(b => b.DateClotureNegociation==null)
                .Include(b => b.feat)
                .Include(b => b.Demandeur)
                .Include(b => b.Repondeur)
                .Include(b => b.feat.Type)
                .Include(b => b.feat.Createur)
                .Include(b => b.feat.Materiel)
                .FirstOrDefault();

            ViewBag.IdMembre = IdMembre;
            Membre membre = _context.Membres
              .Where(b => b.Id == IdMembre)
             .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;

            return View(negocProposee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepterNegociation([Bind("Id, DateClotureNegociation, NewDateProposee, HeureDbtProposee, HeurFinProposee, IsAccepted, DateCreationNegociation")] Negociation negocProposee, int Id, int IdMembre, int IdFeat)
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
                _context.UpdateRange(negocProposee,featToModify);
                _context.SaveChanges();


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
        private Membre GetMembre(int idmembre)
        {
            return (_context.Membres.Where(m => m.Id == idmembre).FirstOrDefault());
        }
        private Feat GetFeat(int idFeat)
        {
            return (_context.Feats.Where(m => m.Id == idFeat)
                .Include(p => p.Createur)

                .FirstOrDefault());
        }
    }
}
