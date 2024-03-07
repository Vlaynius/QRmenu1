using System;
using Microsoft.AspNetCore.Mvc;
using QRmenu.Data;
using QRmenu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QRmenu.Controllers

{
	public class FoodsController : Controller
	{
		private readonly ApplicationContext _context;

		public FoodsController(ApplicationContext context)
		{
			_context = context;
		}
        
        // GET: Foods
        public ActionResult Index()
        {
            IQueryable<Food> foods = _context.Foods!;
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                foods = foods.Where(f => f.StatusId == 1);
            }
            ViewData["admin"] = userId;
            return View(foods.ToList());
        }

		public ActionResult Details(int id)
		{
            Food? food = _context.Foods!.Where(f => f.Id == id).Include(f => f.Status).FirstOrDefault();

			if(food == null)
			{
				return NotFound();
			}
			return View(food);
		}
        // GET: Foods/Create
        public ViewResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(),"Id", "Id");///
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "Id", "Name");
            return View();
        }

        // POST: Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create([Bind("id,Name,Price,Description,StatusId,CategoryId")] Food food)
		{
            if (ModelState.IsValid)
            {
                _context.Add(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", food.Category);///
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "Id", "Name", food.Status);
            return View(food);
        }

		// GET: Foods/Edit/5
        public ActionResult Edit(int id)
        {
            Food? food =  _context.Foods!.Find(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", food.Category);///
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "Id", "Name", food.Status);
            return View(food);
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Price,Description,StatusId,CategoryId")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Update(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", food.Category);///
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "Id", "Name", food.Status);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int id)
        {
            Food? food =  _context.Foods!.Where(f => f.Id == id).Include(f => f.Status).FirstOrDefault();
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food =  _context.Foods!.Find(id)!;
            food.StatusId = 0;
            _context.Foods.Update(food);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

