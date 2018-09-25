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
    public class ProdutorsController : Controller
    {
        private readonly AgroContext _context;

        public ProdutorsController(AgroContext context)
        {
            _context = context;
        }

        // GET: Produtors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtors.ToListAsync());
        }

        // GET: Produtors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtors
                .Include(i=>i.Inseminacaos)
                    .ThenInclude(w=>w.Inseminador)
                .Include(i => i.Inseminacaos)
                    .ThenInclude(w => w.Vaca)
                .Include(v=>v.Vacas)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (produtor == null)
            {
                return NotFound();
            }

            return View(produtor);
        }

        // GET: Produtors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone,Email,Ativo")] Produtor produtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtor);
        }

        // GET: Produtors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtors.SingleOrDefaultAsync(m => m.Id == id);
            if (produtor == null)
            {
                return NotFound();
            }
            return View(produtor);
        }

        // POST: Produtors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone,Email,Ativo")] Produtor produtor)
        {
            if (id != produtor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutorExists(produtor.Id))
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
            return View(produtor);
        }

        // GET: Produtors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtor = await _context.Produtors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produtor == null)
            {
                return NotFound();
            }

            return View(produtor);
        }

        // POST: Produtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtor = await _context.Produtors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Produtors.Remove(produtor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutorExists(int id)
        {
            return _context.Produtors.Any(e => e.Id == id);
        }
    }
}
