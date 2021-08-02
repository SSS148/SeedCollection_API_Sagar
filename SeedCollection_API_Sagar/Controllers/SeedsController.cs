using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeedCollection_API_Sagar.Data;
using SeedCollection_API_Sagar.Models;

namespace SeedCollection_API_Sagar.Controllers
{
    public class SeedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seeds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seeds.ToListAsync());
        }

        // GET: Seeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seed = await _context.Seeds
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seed == null)
            {
                return NotFound();
            }

            return View(seed);
        }

        // GET: Seeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SeedName")] Seed seed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seed);
        }

        // GET: Seeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seed = await _context.Seeds.FindAsync(id);
            if (seed == null)
            {
                return NotFound();
            }
            return View(seed);
        }

        // POST: Seeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeedName")] Seed seed)
        {
            if (id != seed.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeedExists(seed.ID))
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
            return View(seed);
        }

        // GET: Seeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seed = await _context.Seeds
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seed == null)
            {
                return NotFound();
            }

            return View(seed);
        }

        // POST: Seeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seed = await _context.Seeds.FindAsync(id);
            _context.Seeds.Remove(seed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeedExists(int id)
        {
            return _context.Seeds.Any(e => e.ID == id);
        }
    }
}
