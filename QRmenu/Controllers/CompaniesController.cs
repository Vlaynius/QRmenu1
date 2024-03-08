using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRmenu.Data;
using QRmenu.Models;

namespace QRmenu
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationContext _context;

        public CompaniesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return _context.Companies != null? 
                          View(await _context.Companies.ToListAsync()) :
                          Problem("Entity set 'AppContext.Company'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "Id", "Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email,TaxNumber,PostalCode,AddressDetail,Web,StatusId")] Company company)
        {
            company.RegisterDate = DateTime.Now;//Anlık zaman ataması
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email,TaxNumber,PostalCode,AddressDetail,Web,RegisterDate,StatusId")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int id)
        {
            Company? company = _context.Companies!.Where(c => c.Id == id).Include(f => f.Status).FirstOrDefault();
            
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company? company = _context.Companies!.Where(c => c.Id == id).Include(c=>c.Users).Include(c => c.Restaurants)!.ThenInclude(c=> c.Categories).FirstOrDefault();
            if(company != null)
            {
                company.StatusId = 0;
                foreach(User user in company.Users!)
                {
                    user.StatusId = 0;
                }
                foreach(Restaurant rest in company.Restaurants!)
                {
                    rest.StatusId = 0;
                    foreach(Category cat in rest.Categories!)
                    {
                        cat.StatusId = 0;
                        foreach(Food food in cat.Foods!)
                        {
                            food.StatusId = 0;
                        }

                    }
                }
                _context.Companies!.Update(company);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
