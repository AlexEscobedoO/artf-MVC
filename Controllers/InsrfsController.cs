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
    public class InsrfsController : Controller
    {
        private readonly BaseartfContext _context;

        public InsrfsController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Insrfs
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Insrves.Include(i => i.IdempinsNavigation).Include(i => i.IdsolinsNavigation).Include(i => i.IduserinsNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Insrfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insrves == null)
            {
                return NotFound();
            }

            var insrf = await _context.Insrves
                .Include(i => i.IdempinsNavigation)
                .Include(i => i.IdsolinsNavigation)
                .Include(i => i.IduserinsNavigation)
                .FirstOrDefaultAsync(m => m.Idins == id);
            if (insrf == null)
            {
                return NotFound();
            }

            return View(insrf);
        }

        // GET: Insrfs/Create
        public IActionResult Create()
        {
            ViewData["Idempins"] = new SelectList(_context.Empresas, "Idempre", "Rsempre");
            ViewData["Idsolins"] = new SelectList(_context.Solrves, "Idsol", "Idsol");
            ViewData["Iduserins"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Insrfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idins,Idempins,Idsolins,Iduserins,Numacuofins,Obsins,Fecapins,Docins")] Insrf insrf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insrf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idempins"] = new SelectList(_context.Empresas, "Idempre", "Idempre", insrf.Idempins);
            ViewData["Idsolins"] = new SelectList(_context.Solrves, "Idsol", "Idsol", insrf.Idsolins);
            ViewData["Iduserins"] = new SelectList(_context.Users, "Iduser", "Iduser", insrf.Iduserins);
            return View(insrf);
        }

        // GET: Insrfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insrves == null)
            {
                return NotFound();
            }

            var insrf = await _context.Insrves.FindAsync(id);
            if (insrf == null)
            {
                return NotFound();
            }
            ViewData["Idempins"] = new SelectList(_context.Empresas, "Idempre", "Rsempre", insrf.Idempins);
            ViewData["Idsolins"] = new SelectList(_context.Solrves, "Idsol", "Idsol", insrf.Idsolins);
            ViewData["Iduserins"] = new SelectList(_context.Users, "Iduser", "Iduser", insrf.Iduserins);
            return View(insrf);
        }

        // POST: Insrfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idins,Idempins,Idsolins,Iduserins,Numacuofins,Obsins,Fecapins,Docins")] Insrf insrf)
        {
            if (id != insrf.Idins)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insrf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsrfExists(insrf.Idins))
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
            ViewData["Idempins"] = new SelectList(_context.Empresas, "Idempre", "Idempre", insrf.Idempins);
            ViewData["Idsolins"] = new SelectList(_context.Solrves, "Idsol", "Idsol", insrf.Idsolins);
            ViewData["Iduserins"] = new SelectList(_context.Users, "Iduser", "Iduser", insrf.Iduserins);
            return View(insrf);
        }

        // GET: Insrfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insrves == null)
            {
                return NotFound();
            }

            var insrf = await _context.Insrves
                .Include(i => i.IdempinsNavigation)
                .Include(i => i.IdsolinsNavigation)
                .Include(i => i.IduserinsNavigation)
                .FirstOrDefaultAsync(m => m.Idins == id);
            if (insrf == null)
            {
                return NotFound();
            }

            return View(insrf);
        }

        // POST: Insrfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insrves == null)
            {
                return Problem("Entity set 'BaseartfContext.Insrves'  is null.");
            }
            var insrf = await _context.Insrves.FindAsync(id);
            if (insrf != null)
            {
                _context.Insrves.Remove(insrf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> GenerateConsistency(int? id)
        //{
        //    if (id == null || _context.Insrves == null)
        //    {
        //        return NotFound();
        //    }

        //    var insrf = await _context.Insrves
        //        .Include(i => i.IdempinsNavigation)
        //        .Include(i => i.IdsolinsNavigation)
        //        .Include(i => i.IduserinsNavigation)
        //        .FirstOrDefaultAsync(m => m.Idins == id);

        //    if (insrf == null)
        //    {
        //        return NotFound();
        //    }

        //    ConstanciaInscripcion constanciaInscripcion = new ConstanciaInscripcion();
        //    var result = constanciaInscripcion.Generar(insrf);

        //    // Verificar si el resultado es de tipo FileContentResult o FileStreamResult
        //    if (result is FileContentResult fileContentResult)
        //    {
        //        // Puedes realizar acciones adicionales si es necesario
        //    }
        //    else if (result is FileStreamResult fileStreamResult)
        //    {
        //        // Puedes realizar acciones adicionales si es necesario
        //    }
        //    return result;
        //}




        private bool InsrfExists(int id)
        {
          return (_context.Insrves?.Any(e => e.Idins == id)).GetValueOrDefault();
        }
    }
}
