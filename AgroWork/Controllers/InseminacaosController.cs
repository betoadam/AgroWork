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
    public class InseminacaosController : Controller
    {
        private readonly AgroContext _context;

        public InseminacaosController(AgroContext context)
        {
            _context = context;
        }

        // GET: Inseminacaos
        /*public async Task<IActionResult> Index()
        {
            var agroContext = _context.Inseminacaos.Include(i => i.Inseminador).Include(i => i.Produtor).Include(i => i.Vaca);
            return View(await agroContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var inseminacaos = from s in _context.Inseminacaos.Include(i => i.Inseminador).Include(i => i.Produtor).Include(i => i.Vaca)
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                inseminacaos = inseminacaos.Where(s => s.Produtor.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    inseminacaos = inseminacaos.OrderBy(s => s.Produtor.Nome);
                    break;
                case "Date":
                    inseminacaos = inseminacaos.OrderBy(s => s.Data);
                    break;
                case "date_desc":
                    inseminacaos = inseminacaos.OrderByDescending(s => s.Data);
                    break;
                default:
                    inseminacaos = inseminacaos.OrderBy(s => s.Inseminador.Nome);
                    break;
            }
            return View(await inseminacaos.AsNoTracking().ToListAsync());
        }

        // GET: Inseminacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminacao = await _context.Inseminacaos
                .Include(i => i.Inseminador)
                .Include(i => i.Produtor)
                .Include(i => i.Vaca)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inseminacao == null)
            {
                return NotFound();
            }

            return View(inseminacao);
        }

        // GET: Inseminacaos/Create
        public IActionResult Create()
        {
            ViewData["InseminadorId"] = new SelectList(_context.Inseminadors, "Id", "Nome");
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome");
            ViewData["VacaId"] = new SelectList(_context.Vacas, "Id", "Nome");
            return View();
        }

        // POST: Inseminacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutorId,VacaId,InseminadorId,Data,Hora,Touro,Valor")] Inseminacao inseminacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inseminacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InseminadorId"] = new SelectList(_context.Inseminadors, "Id", "Nome", inseminacao.InseminadorId);
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", inseminacao.ProdutorId);
            ViewData["VacaId"] = new SelectList(_context.Vacas, "Id", "Nome",inseminacao.VacaId);
            return View(inseminacao);
        }

        // GET: Inseminacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminacao = await _context.Inseminacaos.SingleOrDefaultAsync(m => m.Id == id);
            if (inseminacao == null)
            {
                return NotFound();
            }
            ViewData["InseminadorId"] = new SelectList(_context.Inseminadors, "Id", "Nome", inseminacao.InseminadorId);
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", inseminacao.ProdutorId);
            ViewData["VacaId"] = new SelectList(_context.Vacas, "Id", "Nome", inseminacao.VacaId);
            return View(inseminacao);
        }

        // POST: Inseminacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutorId,VacaId,InseminadorId,Data,Hora,Touro,Valor")] Inseminacao inseminacao)
        {
            if (id != inseminacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inseminacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InseminacaoExists(inseminacao.Id))
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
            ViewData["InseminadorId"] = new SelectList(_context.Inseminadors, "Id", "Nome", inseminacao.InseminadorId);
            ViewData["ProdutorId"] = new SelectList(_context.Produtors, "Id", "Nome", inseminacao.ProdutorId);
            ViewData["VacaId"] = new SelectList(_context.Vacas, "Id", "Nome", inseminacao.VacaId);
            return View(inseminacao);
        }

        // GET: Inseminacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inseminacao = await _context.Inseminacaos
                .Include(i => i.Inseminador)
                .Include(i => i.Produtor)
                .Include(i => i.Vaca)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (inseminacao == null)
            {
                return NotFound();
            }

            return View(inseminacao);
        }

        // POST: Inseminacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inseminacao = await _context.Inseminacaos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Inseminacaos.Remove(inseminacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InseminacaoExists(int id)
        {
            return _context.Inseminacaos.Any(e => e.Id == id);
        }
    }
}
