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
    public class RectrfsController : Controller
    {
        private readonly BaseartfContext _context;

        public RectrfsController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Rectrfs
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Rectrves.Include(r => r.IdinsrectNavigation).Include(r => r.IduserrectNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Rectrfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rectrves == null)
            {
                return NotFound();
            }

            var rectrf = await _context.Rectrves
                .Include(r => r.IdinsrectNavigation)
                .Include(r => r.IduserrectNavigation)
                .FirstOrDefaultAsync(m => m.Idrect == id);
            if (rectrf == null)
            {
                return NotFound();
            }

            return View(rectrf);
        }

        // GET: Rectrfs/Create
        public IActionResult Create()
        {
            ViewData["Idinsrect"] = new SelectList(_context.Insrves, "Idins", "Idins");
            ViewData["Iduserrect"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Rectrfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idrect,Idinsrect,Iduserrect,Numacuofrect,Fichatecrect,Numdocemp,Fechadocsol,Fecharect,Desrect,Obsrect,Acurect,Claverect,Fechamodrect")] Rectrf rectrf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rectrf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idinsrect"] = new SelectList(_context.Insrves, "Idins", "Idins", rectrf.Idinsrect);
            ViewData["Iduserrect"] = new SelectList(_context.Users, "Iduser", "Iduser", rectrf.Iduserrect);
            return View(rectrf);
        }

        // GET: Rectrfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rectrves == null)
            {
                return NotFound();
            }

            var rectrf = await _context.Rectrves.FindAsync(id);
            if (rectrf == null)
            {
                return NotFound();
            }
            ViewData["Idinsrect"] = new SelectList(_context.Insrves, "Idins", "Idins", rectrf.Idinsrect);
            ViewData["Iduserrect"] = new SelectList(_context.Users, "Iduser", "Iduser", rectrf.Iduserrect);
            return View(rectrf);
        }

        // POST: Rectrfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idrect,Idinsrect,Iduserrect,Numacuofrect,Fichatecrect,Numdocemp,Fechadocsol,Fecharect,Desrect,Obsrect,Acurect,Claverect,Fechamodrect")] Rectrf rectrf)
        {
            if (id != rectrf.Idrect)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rectrf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RectrfExists(rectrf.Idrect))
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
            ViewData["Idinsrect"] = new SelectList(_context.Insrves, "Idins", "Idins", rectrf.Idinsrect);
            ViewData["Iduserrect"] = new SelectList(_context.Users, "Iduser", "Iduser", rectrf.Iduserrect);
            return View(rectrf);
        }

        // GET: Rectrfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rectrves == null)
            {
                return NotFound();
            }

            var rectrf = await _context.Rectrves
                .Include(r => r.IdinsrectNavigation)
                .Include(r => r.IduserrectNavigation)
                .FirstOrDefaultAsync(m => m.Idrect == id);
            if (rectrf == null)
            {
                return NotFound();
            }

            return View(rectrf);
        }

        // POST: Rectrfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rectrves == null)
            {
                return Problem("Entity set 'BaseartfContext.Rectrves'  is null.");
            }
            var rectrf = await _context.Rectrves.FindAsync(id);
            if (rectrf != null)
            {
                _context.Rectrves.Remove(rectrf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RectrfExists(int id)
        {
          return (_context.Rectrves?.Any(e => e.Idrect == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> GenerateConsistencyRectificacion(int Id, string Text)
        {
            if (Id == 0 || _context.Equiunis == null)
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
                .FirstOrDefaultAsync(m => m.IdrectequiNavigation.Idrect == Id);

            if (equipo == null)
            {
                return NotFound();
            }

            CostanciaModificacion_o_Rectificacion constanciaInscripcion = new CostanciaModificacion_o_Rectificacion();
            var result = constanciaInscripcion.Generar(equipo, Text);

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
