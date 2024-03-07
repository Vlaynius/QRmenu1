using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRmenu.Data;
using QRmenu.Models;

namespace QRmenu.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationContext _context;

        public RestaurantsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Restaurants!.Include(r => r.Company).Include(r => r.Status);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Company)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompanyId,StatusId,Phone,PostalCode,AddressDetail")] Restaurant restaurant)
        {
            restaurant.RegisterDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", restaurant.CompanyId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", restaurant.StatusId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", restaurant.CompanyId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", restaurant.StatusId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompanyId,StatusId,Phone,PostalCode,AddressDetail,RegisterDate")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", restaurant.CompanyId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", restaurant.StatusId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public  ActionResult Delete(int id)
        {
            //Food? food = _context.Foods!.Where(f => f.Id == id).Include(f => f.Status).FirstOrDefault();

            Restaurant? restaurant = _context.Restaurants!.Where(r => r.Id == id).Include(r => r.Company).Include(r => r.Status).FirstOrDefault();
                
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Restaurant restaurant =  _context.Restaurants!.Find(id)!;
            restaurant.StatusId = 0;
            _context.Restaurants!.Update(restaurant);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
          return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
