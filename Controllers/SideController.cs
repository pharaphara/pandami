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

             ViewBag.TypeLitige= new SelectList((from m in _context.TypeLitiges
                                                         orderby m.Libelle
                                                         select m.Libelle).ToList().Distinct());

            return View(newLitige);
        }
        [HttpPost]
        public ActionResult Litige(int Id, [Bind("Id, CreationDate,ClotureDate,Commentaire,TypeLitige,Createur,Feat")] Litige newLitige)
        {
           

            return View(newLitige);
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
