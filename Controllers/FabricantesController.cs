﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using artf_MVC.Models;

namespace artf_MVC.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly BaseartfContext _context;

        public FabricantesController(BaseartfContext context)
        {
            _context = context;
        }

        // GET: Fabricantes
        public async Task<IActionResult> Index()
        {
              return _context.Fabricantes != null ? 
                          View(await _context.Fabricantes.ToListAsync()) :
                          Problem("Entity set 'BaseartfContext.Fabricantes'  is null.");
        }

        // GET: Fabricantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fabricantes == null)
            {
                return NotFound();
            }

            var fabricante = await _context.Fabricantes
                .FirstOrDefaultAsync(m => m.Idfab == id);
            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // GET: Fabricantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idfab,Rsfab,Marfab")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fabricante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fabricante);
        }

        // GET: Fabricantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fabricantes == null)
            {
                return NotFound();
            }

            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                return NotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idfab,Rsfab,Marfab")] Fabricante fabricante)
        {
            if (id != fabricante.Idfab)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fabricante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricanteExists(fabricante.Idfab))
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
            return View(fabricante);
        }

        // GET: Fabricantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fabricantes == null)
            {
                return NotFound();
            }

            var fabricante = await _context.Fabricantes
                .FirstOrDefaultAsync(m => m.Idfab == id);
            if (fabricante == null)
            {
                return NotFound();
            }

            return View(fabricante);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fabricantes == null)
            {
                return Problem("Entity set 'BaseartfContext.Fabricantes'  is null.");
            }
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante != null)
            {
                _context.Fabricantes.Remove(fabricante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricanteExists(int id)
        {
          return (_context.Fabricantes?.Any(e => e.Idfab == id)).GetValueOrDefault();
        }
    }
}
