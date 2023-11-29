using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using artf_MVC.Models;
using artf_MVC.Helper.Constancias;

namespace artf_MVC.Controllers
{
    public class CanrfsController : Controller
    {
        private readonly BaseartfContext _context;

        public CanrfsController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Canrfs
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Canrves.Include(c => c.IdmodcanNavigation).Include(c => c.IdusercanNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Canrfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Canrves == null)
            {
                return NotFound();
            }

            var canrf = await _context.Canrves
                .Include(c => c.IdmodcanNavigation)
                .Include(c => c.IdusercanNavigation)
                .FirstOrDefaultAsync(m => m.Idcan == id);
            if (canrf == null)
            {
                return NotFound();
            }

            return View(canrf);
        }

        // GET: Canrfs/Create
        public IActionResult Create()
        {
            ViewData["Idmodcan"] = new SelectList(_context.Modrves, "Idmod", "Idmod");
            ViewData["Idusercan"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Canrfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcan,Idmodcan,Idusercan,Numacuofcan,Fechaofcan,Juscan,Obscan,Fichacan,Clavecan,Fechacan")] Canrf canrf, IFormFile Fichacan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canrf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmodcan"] = new SelectList(_context.Modrves, "Idmod", "Idmod", canrf.Idmodcan);
            ViewData["Idusercan"] = new SelectList(_context.Users, "Iduser", "Iduser", canrf.Idusercan);
            return View(canrf);
        }

        // GET: Canrfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Canrves == null)
            {
                return NotFound();
            }

            var canrf = await _context.Canrves.FindAsync(id);
            if (canrf == null)
            {
                return NotFound();
            }
            ViewData["Idmodcan"] = new SelectList(_context.Modrves, "Idmod", "Idmod", canrf.Idmodcan);
            ViewData["Idusercan"] = new SelectList(_context.Users, "Iduser", "Iduser", canrf.Idusercan);
            return View(canrf);
        }

        // POST: Canrfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcan,Idmodcan,Idusercan,Numacuofcan,Fechaofcan,Juscan,Obscan,Fichacan,Clavecan,Fechacan")] Canrf canrf)
        {
            if (id != canrf.Idcan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canrf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanrfExists(canrf.Idcan))
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
            ViewData["Idmodcan"] = new SelectList(_context.Modrves, "Idmod", "Idmod", canrf.Idmodcan);
            ViewData["Idusercan"] = new SelectList(_context.Users, "Iduser", "Iduser", canrf.Idusercan);
            return View(canrf);
        }

        // GET: Canrfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Canrves == null)
            {
                return NotFound();
            }

            var canrf = await _context.Canrves
                .Include(c => c.IdmodcanNavigation)
                .Include(c => c.IdusercanNavigation)
                .FirstOrDefaultAsync(m => m.Idcan == id);
            if (canrf == null)
            {
                return NotFound();
            }

            return View(canrf);
        }

        // POST: Canrfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Canrves == null)
            {
                return Problem("Entity set 'BaseartfContext.Canrves'  is null.");
            }
            var canrf = await _context.Canrves.FindAsync(id);
            if (canrf != null)
            {
                _context.Canrves.Remove(canrf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanrfExists(int id)
        {
          return (_context.Canrves?.Any(e => e.Idcan == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GenerateConsistencyCancelation(int Id)
        {
            if (Id == 0 || _context.Canrves == null)
            {
                return NotFound();
            }


            var equipo = await _context.Equiunis
               .Include(i => i.IdcanequiNavigation)
               .Include(i => i.IdmodequiNavigation)
               .Include(i => i.IdrectequiNavigation)
               .Include(i => i.IdinsequiNavigation)
               .Include(i => i.IdsolequiNavigation)
               .Include(i => i.IdempreequiNavigation)
               .Include(i => i.IdfabequiNavigation)
               .Include(i => i.IdmodeequiNavigation)
               .FirstOrDefaultAsync(m => m.IdcanequiNavigation.Idcan == Id);




            if (equipo == null)
            {
                return NotFound();
            }

            ConstanciaCancelacion constanciaInscripcion = new ConstanciaCancelacion();
            var result = constanciaInscripcion.Generar(equipo);

            // Verificar si el resultado es de tipo FileContentResult o FileStreamResult
            if (result is FileContentResult fileContentResult)
            {
                // Puedes realizar acciones adicionales si es necesario
            }
            else if (result is FileStreamResult fileStreamResult)
            {
                // Puedes realizar acciones adicionales si es necesario
            }
            return result;
        }

    }
}
