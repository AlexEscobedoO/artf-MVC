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
    public class EquiunisController : Controller
    {
        private readonly BaseartfContext _context;

        public EquiunisController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Equiunis
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Equiunis.Include(e => e.IdcanequiNavigation).Include(e => e.IdempreequiNavigation).Include(e => e.IdfabequiNavigation).Include(e => e.IdinsequiNavigation).Include(e => e.IdmodeequiNavigation).Include(e => e.IdmodequiNavigation).Include(e => e.IdrectequiNavigation).Include(e => e.IdsolequiNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Equiunis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equiunis == null)
            {
                return NotFound();
            }

            var equiuni = await _context.Equiunis
                .Include(e => e.IdcanequiNavigation)
                .Include(e => e.IdempreequiNavigation)
                .Include(e => e.IdfabequiNavigation)
                .Include(e => e.IdinsequiNavigation)
                .Include(e => e.IdmodeequiNavigation)
                .Include(e => e.IdmodequiNavigation)
                .Include(e => e.IdrectequiNavigation)
                .Include(e => e.IdsolequiNavigation)
                .FirstOrDefaultAsync(m => m.Idequi == id);
            if (equiuni == null)
            {
                return NotFound();
            }

            return View(equiuni);
        }

        // GET: Equiunis/Create
        public IActionResult Create()
        {
            ViewData["Idcanequi"] = new SelectList(_context.Canrves, "Idcan", "Idcan");
            ViewData["Idempreequi"] = new SelectList(_context.Empresas, "Idempre", "Idempre");
            ViewData["Idfabequi"] = new SelectList(_context.Fabricantes, "Idfab", "Idfab");
            ViewData["Idinsequi"] = new SelectList(_context.Insrves, "Idins", "Idins");
            ViewData["Idmodeequi"] = new SelectList(_context.Modelos, "Idmod", "Idmod");
            ViewData["Idmodequi"] = new SelectList(_context.Modrves, "Idmod", "Idmod");
            ViewData["Idrectequi"] = new SelectList(_context.Rectrves, "Idrect", "Idrect");
            ViewData["Idsolequi"] = new SelectList(_context.Solrves, "Idsol", "Idsol");
            return View();
        }

        // POST: Equiunis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idequi,Idempreequi,Idfabequi,Idmodeequi,Idsolequi,Idinsequi,Idrectequi,Idmodequi,Idcanequi,Modaequi,Tipequi,Combuequi,Pequi,Nserie,Regiequi,Graequi,Usoequi,Fcons,Nofact,Tcontra,Fcontra,Vcontra,Mrent,Monrent,Obsarre,Obsgra,Obsequi,Fichaequi,Fecharequi")] Equiuni equiuni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equiuni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcanequi"] = new SelectList(_context.Canrves, "Idcan", "Idcan", equiuni.Idcanequi);
            ViewData["Idempreequi"] = new SelectList(_context.Empresas, "Idempre", "Idempre", equiuni.Idempreequi);
            ViewData["Idfabequi"] = new SelectList(_context.Fabricantes, "Idfab", "Idfab", equiuni.Idfabequi);
            ViewData["Idinsequi"] = new SelectList(_context.Insrves, "Idins", "Idins", equiuni.Idinsequi);
            ViewData["Idmodeequi"] = new SelectList(_context.Modelos, "Idmod", "Idmod", equiuni.Idmodeequi);
            ViewData["Idmodequi"] = new SelectList(_context.Modrves, "Idmod", "Idmod", equiuni.Idmodequi);
            ViewData["Idrectequi"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", equiuni.Idrectequi);
            ViewData["Idsolequi"] = new SelectList(_context.Solrves, "Idsol", "Idsol", equiuni.Idsolequi);
            return View(equiuni);
        }

        // GET: Equiunis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equiunis == null)
            {
                return NotFound();
            }

            var equiuni = await _context.Equiunis.FindAsync(id);
            if (equiuni == null)
            {
                return NotFound();
            }
            ViewData["Idcanequi"] = new SelectList(_context.Canrves, "Idcan", "Idcan", equiuni.Idcanequi);
            ViewData["Idempreequi"] = new SelectList(_context.Empresas, "Idempre", "Idempre", equiuni.Idempreequi);
            ViewData["Idfabequi"] = new SelectList(_context.Fabricantes, "Idfab", "Idfab", equiuni.Idfabequi);
            ViewData["Idinsequi"] = new SelectList(_context.Insrves, "Idins", "Idins", equiuni.Idinsequi);
            ViewData["Idmodeequi"] = new SelectList(_context.Modelos, "Idmod", "Idmod", equiuni.Idmodeequi);
            ViewData["Idmodequi"] = new SelectList(_context.Modrves, "Idmod", "Idmod", equiuni.Idmodequi);
            ViewData["Idrectequi"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", equiuni.Idrectequi);
            ViewData["Idsolequi"] = new SelectList(_context.Solrves, "Idsol", "Idsol", equiuni.Idsolequi);
            return View(equiuni);
        }

        // POST: Equiunis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idequi,Idempreequi,Idfabequi,Idmodeequi,Idsolequi,Idinsequi,Idrectequi,Idmodequi,Idcanequi,Modaequi,Tipequi,Combuequi,Pequi,Nserie,Regiequi,Graequi,Usoequi,Fcons,Nofact,Tcontra,Fcontra,Vcontra,Mrent,Monrent,Obsarre,Obsgra,Obsequi,Fichaequi,Fecharequi")] Equiuni equiuni)
        {
            if (id != equiuni.Idequi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equiuni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquiuniExists(equiuni.Idequi))
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
            ViewData["Idcanequi"] = new SelectList(_context.Canrves, "Idcan", "Idcan", equiuni.Idcanequi);
            ViewData["Idempreequi"] = new SelectList(_context.Empresas, "Idempre", "Idempre", equiuni.Idempreequi);
            ViewData["Idfabequi"] = new SelectList(_context.Fabricantes, "Idfab", "Idfab", equiuni.Idfabequi);
            ViewData["Idinsequi"] = new SelectList(_context.Insrves, "Idins", "Idins", equiuni.Idinsequi);
            ViewData["Idmodeequi"] = new SelectList(_context.Modelos, "Idmod", "Idmod", equiuni.Idmodeequi);
            ViewData["Idmodequi"] = new SelectList(_context.Modrves, "Idmod", "Idmod", equiuni.Idmodequi);
            ViewData["Idrectequi"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", equiuni.Idrectequi);
            ViewData["Idsolequi"] = new SelectList(_context.Solrves, "Idsol", "Idsol", equiuni.Idsolequi);
            return View(equiuni);
        }

        // GET: Equiunis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equiunis == null)
            {
                return NotFound();
            }

            var equiuni = await _context.Equiunis
                .Include(e => e.IdcanequiNavigation)
                .Include(e => e.IdempreequiNavigation)
                .Include(e => e.IdfabequiNavigation)
                .Include(e => e.IdinsequiNavigation)
                .Include(e => e.IdmodeequiNavigation)
                .Include(e => e.IdmodequiNavigation)
                .Include(e => e.IdrectequiNavigation)
                .Include(e => e.IdsolequiNavigation)
                .FirstOrDefaultAsync(m => m.Idequi == id);
            if (equiuni == null)
            {
                return NotFound();
            }

            return View(equiuni);
        }

        // POST: Equiunis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equiunis == null)
            {
                return Problem("Entity set 'BaseartfContext.Equiunis'  is null.");
            }
            var equiuni = await _context.Equiunis.FindAsync(id);
            if (equiuni != null)
            {
                _context.Equiunis.Remove(equiuni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquiuniExists(int id)
        {
          return (_context.Equiunis?.Any(e => e.Idequi == id)).GetValueOrDefault();
        }
    }
}
