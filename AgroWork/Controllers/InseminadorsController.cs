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
    public class InseminadorsController : Controller
    {
        private readonly AgroContext _context;

        public InseminadorsController(AgroContext context)
        {
            _context = context;
        }

        // GET: Inseminadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inseminadors.ToListAsync());
        }

        // GET: Inseminadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminador = await _context.Inseminadors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inseminador == null)
            {
                return NotFound();
            }

            return View(inseminador);
        }

        // GET: Inseminadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inseminadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone,Email,Ativo,Carro")] Inseminador inseminador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inseminador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inseminador);
        }

        // GET: Inseminadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminador = await _context.Inseminadors.SingleOrDefaultAsync(m => m.Id == id);
            if (inseminador == null)
            {
                return NotFound();
            }
            return View(inseminador);
        }

        // POST: Inseminadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone,Email,Ativo,Carro")] Inseminador inseminador)
        {
            if (id != inseminador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inseminador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InseminadorExists(inseminador.Id))
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
            return View(inseminador);
        }

        // GET: Inseminadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminador = await _context.Inseminadors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inseminador == null)
            {
                return NotFound();
            }

            return View(inseminador);
        }

        // POST: Inseminadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inseminador = await _context.Inseminadors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Inseminadors.Remove(inseminador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InseminadorExists(int id)
        {
            return _context.Inseminadors.Any(e => e.Id == id);
        }
    }
}
