using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgroWork.Dados;
using AgroWork.Models;

namespace AgroWork.Controllers
{
    public class VacasController : Controller
    {
        private readonly AgroContext _context;

        public VacasController(AgroContext context)
        {
            _context = context;
        }

        // GET: Vacas
        public async Task<IActionResult> Index()
        {
            var agroContext = _context.Vacas.Include(v => v.Produtor);
            return View(await agroContext.ToListAsync());
        }

        // GET: Vacas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaca = await _context.Vacas
                .Include(v => v.Produtor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vaca == null)
            {
                return NotFound();
            }

            return View(vaca);
        }

        // GET: Vacas/Create
        public IActionResult Create()
        {
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome");/*(_context.Produtors, "Id", "Id");*/
            return View();
        }

        // POST: Vacas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Raca,Numero,Nome,Idade,Tipo,ProdutorId")] Vaca vaca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", vaca.ProdutorId);
            return View(vaca);
        }

        // GET: Vacas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaca = await _context.Vacas.SingleOrDefaultAsync(m => m.Id == id);
            if (vaca == null)
            {
                return NotFound();
            }
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", vaca.ProdutorId);
            return View(vaca);
        }

        // POST: Vacas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Raca,Numero,Nome,Idade,Tipo,ProdutorId")] Vaca vaca)
        {
            if (id != vaca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacaExists(vaca.Id))
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
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", vaca.ProdutorId);
            return View(vaca);
        }

        // GET: Vacas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaca = await _context.Vacas
                .Include(v => v.Produtor)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vaca == null)
            {
                return NotFound();
            }

            return View(vaca);
        }

        // POST: Vacas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaca = await _context.Vacas.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vacas.Remove(vaca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacaExists(int id)
        {
            return _context.Vacas.Any(e => e.Id == id);
        }
    }
}
