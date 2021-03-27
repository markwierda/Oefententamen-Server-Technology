using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oefententamen.Data;
using Oefententamen.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Oefententamen.Controllers
{
    public class ReserveringController : Controller
    {
        private readonly TheaterDbContext _context;

        public ReserveringController(TheaterDbContext context)
        {
            _context = context;
        }

        // GET: Reservering
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservering.ToListAsync());
        }

        // GET: Reservering/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // GET: Reservering/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservering/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,KlantId,Bezet")] Reservering reservering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservering);
        }

        // GET: Reservering/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering.FindAsync(id);
            if (reservering == null)
            {
                return NotFound();
            }
            return View(reservering);
        }

        // POST: Reservering/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,KlantId,Bezet")] Reservering reservering)
        {
            if (id != reservering.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveringExists(reservering.Id))
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
            return View(reservering);
        }

        // GET: Reservering/EditWithKlantId/5
        public async Task<IActionResult> EditWithKlantId(int? id)
        {
            if (id == null) return NotFound();

            var klant = await _context.Klant.FirstOrDefaultAsync(k => k.Id == id);
            if (klant == null) return NotFound();

            var reserveringen = _context.Reservering.Include(r => r.Klant).OrderBy(r => r.Naam);
            ViewData["Klant"] = $"{klant.Naam}, {klant.Email}, {klant.Woonplaats}";
            ViewData["KlantId"] = klant.Id;

            return View(await reserveringen.ToListAsync());
        }

        // POST: Reservering/EditWithKlantId/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWithKlantId(int id, int[] reserveringsIds)
        {
            if (ModelState.IsValid)
            {
                var reserveringen = await _context.Reservering.Where(r => r.KlantId == id).ToListAsync();
                foreach (var reservering in reserveringen)
                {
                    reservering.KlantId = null;
                    reservering.Bezet = false;
                    _context.Update(reservering);
                    await _context.SaveChangesAsync();
                }

                foreach (int rid in reserveringsIds)
                {
                    var reservering = await _context.Reservering.FindAsync(rid);
                    if (reservering != null)
                    {
                        reservering.KlantId = id;
                        reservering.Bezet = true;
                        _context.Update(reservering);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index), "Klant");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservering/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // POST: Reservering/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservering = await _context.Reservering.FindAsync(id);
            _context.Reservering.Remove(reservering);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveringExists(int id)
        {
            return _context.Reservering.Any(e => e.Id == id);
        }
    }
}