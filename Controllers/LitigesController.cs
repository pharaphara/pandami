using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandami.Data;
using Pandami.Models;

namespace Pandami.Controllers
{
    public class LitigesController : Controller
    {
        private readonly DataContext _context;

        public LitigesController(DataContext context)
        {
            _context = context;
        }

        // GET: Litiges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Litiges.ToListAsync());
        }

        // GET: Litiges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var litige = await _context.Litiges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (litige == null)
            {
                return NotFound();
            }

            return View(litige);
        }

        // GET: Litiges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Litiges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,ClotureDate,Commentaire")] Litige litige)
        {
            if (ModelState.IsValid)
            {
                _context.Add(litige);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(litige);
        }

        // GET: Litiges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var litige = await _context.Litiges.FindAsync(id);
            if (litige == null)
            {
                return NotFound();
            }
            return View(litige);
        }

        // POST: Litiges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,ClotureDate,Commentaire")] Litige litige)
        {
            if (id != litige.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(litige);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LitigeExists(litige.Id))
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
            return View(litige);
        }

        // GET: Litiges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var litige = await _context.Litiges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (litige == null)
            {
                return NotFound();
            }

            return View(litige);
        }

        // POST: Litiges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var litige = await _context.Litiges.FindAsync(id);
            _context.Litiges.Remove(litige);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LitigeExists(int id)
        {
            return _context.Litiges.Any(e => e.Id == id);
        }
    }
}
