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
    public class SideController : Controller
    {
        private readonly DataContext _context;
        public SideController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Litige(int IdFeat, int IdMembre)
        {
            Litige newLitige = new Litige()
            {
                CreationDate = DateTime.Now,
                Createur = GetMembre(IdMembre),
                Feat = GetFeat(IdFeat)
            };

            ViewBag.TypeLitige = new SelectList((from m in _context.TypeLitiges
                                                 orderby m.Libelle
                                                 select m.Libelle).ToList().Distinct());
            Membre membre = _context.Membres
            .Where(b => b.Id == IdMembre)
           .FirstOrDefault();
            ViewBag.PrenomMembre = membre.Prenom;
            ViewBag.NomMembre = membre.Nom;
            ViewBag.IdMembre = membre.Id;
            _context.Add(newLitige);



            return View(newLitige);
        }
        [HttpPost]
        public ActionResult LitigePost(int Id, [Bind("Id, CreationDate,ClotureDate,Commentaire,TypeLitige,Createur,Feat")] Litige newLitige, int IdMembre)
        {

            _context.Update(newLitige);
            _context.SaveChanges();
            

            return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
        }
        public ActionResult MajFeat(int IdFeat, int IdMembre)
        {
            Feat feat = GetFeat(IdFeat);
            if (feat.EnCoursRealisation == null)
            {
                feat.EnCoursRealisation = DateTime.Now;
            }
            else if (feat.SurPlace == null)
            {

                feat.SurPlace = DateTime.Now;

            }
            else if (feat.FinFeatHelper == null)
            {

                feat.FinFeatHelper = DateTime.Now;

            }
            else if (feat.ClotureDate == null)
            {

                feat.ClotureDate= DateTime.Now;
                _context.Update(feat);
                _context.SaveChanges();
                return RedirectToAction("MesFeats", "PAFeats", new { @id = IdMembre });
            }
            _context.Update(feat);
            _context.SaveChanges();
            return RedirectToAction("MesFeatsHelper", "PAFeats", new { @id = IdMembre });
        }

        private Membre GetMembre(int idmembre)
        {
            return (_context.Membres.Where(m => m.Id == idmembre).FirstOrDefault());
        }
        private Feat GetFeat(int idFeat)
        {
            return (_context.Feats.Where(m => m.Id == idFeat).FirstOrDefault());
        }
    }
}
