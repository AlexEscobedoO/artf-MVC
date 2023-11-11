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
    public class ModrfsController : Controller
    {
        private readonly BaseartfContext _context;

        public ModrfsController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Modrfs
        public async Task<IActionResult> Index()
        {
            var baseartfContext = _context.Modrves.Include(m => m.IdrectmodNavigation).Include(m => m.IdusermodNavigation);
            return View(await baseartfContext.ToListAsync());
        }

        // GET: Modrfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modrves == null)
            {
                return NotFound();
            }

            var modrf = await _context.Modrves
                .Include(m => m.IdrectmodNavigation)
                .Include(m => m.IdusermodNavigation)
                .FirstOrDefaultAsync(m => m.Idmod == id);
            if (modrf == null)
            {
                return NotFound();
            }

            return View(modrf);
        }

        // GET: Modrfs/Create
        public IActionResult Create()
        {
            ViewData["Idrectmod"] = new SelectList(_context.Rectrves, "Idrect", "Idrect");
            ViewData["Idusermod"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Modrfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmod,Idrectmod,Idusermod,Numacuofmod,Acumod,Fechamod,Desmod,Obsmod,Fichatecmod,Clavemod")] Modrf modrf ,IFormFile fichatecmod)
        {
             //if (fichatecmod != null && fichatecmod.Length > 0)
             //{
             //   using (var memoryStream = new MemoryStream())
             //   {
             //       await fichatecmod.CopyToAsync(memoryStream);
             //       modrf.Fichatecmod = memoryStream.ToArray();
             //   }
             //}            
            
            if (ModelState.IsValid)
            {
                _context.Add(modrf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idrectmod"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", modrf.Idrectmod);
            ViewData["Idusermod"] = new SelectList(_context.Users, "Iduser", "Iduser", modrf.Idusermod);
            return View(modrf);
        }

        // GET: Modrfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modrves == null)
            {
                return NotFound();
            }

            var modrf = await _context.Modrves.FindAsync(id);
            if (modrf == null)
            {
                return NotFound();
            }
            ViewData["Idrectmod"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", modrf.Idrectmod);
            ViewData["Idusermod"] = new SelectList(_context.Users, "Iduser", "Iduser", modrf.Idusermod);
            return View(modrf);
        }

        // POST: Modrfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmod,Idrectmod,Idusermod,Numacuofmod,Acumod,Fechamod,Desmod,Obsmod,Fichatecmod,Clavemod")] Modrf modrf)
        {
            if (id != modrf.Idmod)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modrf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModrfExists(modrf.Idmod))
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
            ViewData["Idrectmod"] = new SelectList(_context.Rectrves, "Idrect", "Idrect", modrf.Idrectmod);
            ViewData["Idusermod"] = new SelectList(_context.Users, "Iduser", "Iduser", modrf.Idusermod);
            return View(modrf);
        }

        // GET: Modrfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modrves == null)
            {
                return NotFound();
            }

            var modrf = await _context.Modrves
                .Include(m => m.IdrectmodNavigation)
                .Include(m => m.IdusermodNavigation)
                .FirstOrDefaultAsync(m => m.Idmod == id);
            if (modrf == null)
            {
                return NotFound();
            }

            return View(modrf);
        }

        // POST: Modrfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modrves == null)
            {
                return Problem("Entity set 'BaseartfContext.Modrves'  is null.");
            }
            var modrf = await _context.Modrves.FindAsync(id);
            if (modrf != null)
            {
                _context.Modrves.Remove(modrf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModrfExists(int id)
        {
          return (_context.Modrves?.Any(e => e.Idmod == id)).GetValueOrDefault();
        }
    }
}
