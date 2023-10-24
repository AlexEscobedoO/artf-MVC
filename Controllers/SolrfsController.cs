using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using artf_MVC.Models;

namespace artf_MVC.Controllers
{
    public class SolrfsController : Controller
    {
        private readonly BaseartfContext _context;

        public SolrfsController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Solrfs
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Solrves.Include(s => s.IdempsolNavigation).Include(s => s.IdusersolNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Solrfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Solrves == null)
            {
                return NotFound();
            }

            var solrf = await _context.Solrves
                .Include(s => s.IdempsolNavigation)
                .Include(s => s.IdusersolNavigation)
                .FirstOrDefaultAsync(m => m.Idsol == id);
            if (solrf == null)
            {
                return NotFound();
            }

            return View(solrf);
        }

        // GET: Solrfs/Create
        public IActionResult Create()
        {
            ViewData["Idempsol"] = new SelectList(_context.Empresas, "Idempre", "Idempre");
            ViewData["Idusersol"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Solrfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsol,Idempsol,Idusersol,Numacuofsol,Obssol,Fecapsol,Docsol")] Solrf solrf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solrf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idempsol"] = new SelectList(_context.Empresas, "Idempre", "Idempre", solrf.Idempsol);
            ViewData["Idusersol"] = new SelectList(_context.Users, "Iduser", "Iduser", solrf.Idusersol);
            return View(solrf);
        }

        // GET: Solrfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Solrves == null)
            {
                return NotFound();
            }

            var solrf = await _context.Solrves.FindAsync(id);
            if (solrf == null)
            {
                return NotFound();
            }
            ViewData["Idempsol"] = new SelectList(_context.Empresas, "Idempre", "Idempre", solrf.Idempsol);
            ViewData["Idusersol"] = new SelectList(_context.Users, "Iduser", "Iduser", solrf.Idusersol);
            return View(solrf);
        }

        // POST: Solrfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idsol,Idempsol,Idusersol,Numacuofsol,Obssol,Fecapsol,Docsol")] Solrf solrf)
        {
            if (id != solrf.Idsol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solrf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolrfExists(solrf.Idsol))
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
            ViewData["Idempsol"] = new SelectList(_context.Empresas, "Idempre", "Idempre", solrf.Idempsol);
            ViewData["Idusersol"] = new SelectList(_context.Users, "Iduser", "Iduser", solrf.Idusersol);
            return View(solrf);
        }

        // GET: Solrfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Solrves == null)
            {
                return NotFound();
            }

            var solrf = await _context.Solrves
                .Include(s => s.IdempsolNavigation)
                .Include(s => s.IdusersolNavigation)
                .FirstOrDefaultAsync(m => m.Idsol == id);
            if (solrf == null)
            {
                return NotFound();
            }

            return View(solrf);
        }

        // POST: Solrfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Solrves == null)
            {
                return Problem("Entity set 'BaseartfContext.Solrves'  is null.");
            }
            var solrf = await _context.Solrves.FindAsync(id);
            if (solrf != null)
            {
                _context.Solrves.Remove(solrf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolrfExists(int id)
        {
          return (_context.Solrves?.Any(e => e.Idsol == id)).GetValueOrDefault();
        }
    }
}
