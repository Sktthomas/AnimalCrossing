using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimalCrossing.Data;
using AnimalCrossing.Models;

namespace AnimalCrossing.Controllers
{
    public class SpeciesController : Controller
    {        
        private readonly ISpeciesRepository repo;

        public SpeciesController(ISpeciesRepository repo)
        {
            this.repo = repo;
        }

        // GET: Species
        public IActionResult Index()
        {
            return View(repo.Get());
        }

        // GET: Species/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = repo.Get((int)id);
            if (species == null)
            {
                return NotFound();
            }

            return View(species);
        }

        // GET: Species/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SpeciesId,Name,Description")] Species species)
        {
            if (ModelState.IsValid)
            {
                repo.Save(species);

                return RedirectToAction(nameof(Index));
            }
            return View(species);
        }

        // GET: Species/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = repo.Get((int)id);
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SpeciesId,Name,Description")] Species species)
        {
            if (id != species.SpeciesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Save(species);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeciesExists(species.SpeciesId))
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
            return View(species);
        }

        // GET: Species/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var species = repo.Get((int)id);
            if (species == null)
            {
                return NotFound();
            }

            return View(species);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool SpeciesExists(int id)
        {
            // I did this so we could unit test. TODO: Fix this..
            return false;
        }
    }
}
