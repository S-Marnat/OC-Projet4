using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressVoitures.Controllers
{
    public class VoituresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoituresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voitures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Voitures.Include(v => v.Finition).Include(v => v.Marque).Include(v => v.Modele);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Voitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voitures
                .Include(v => v.Finition)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // GET: Voitures/Create
        public IActionResult Create(int? idMarque, int? idModele)
        {
            ListeAnnees();
            ListesMarquesModelesFinitions(idMarque, idModele);
            return View();
        }

        // POST: Voitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodeVin,Annee,Image,Description,DateAchat,PrixAchat,VoitureReparee,DateMiseEnVente,PrixMiseEnVente,AnnoncePubliee,VoitureVendue,IdMarque,IdModele,IdFinition")] Voiture voiture)
        {
            bool voitureExiste;
            voitureExiste = _context.Voitures.Any(v => v.CodeVin == voiture.CodeVin);
            if (voitureExiste)
            {
                ModelState.AddModelError("", "Une voiture portant ce code VIN existe déjà.");
            }

            if (voiture.DateMiseEnVente < voiture.DateAchat)
            {
                ModelState.AddModelError("", "La date de mise en vente ne peut pas être antérieure à la date d'achat de la voiture.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(voiture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ListeAnnees();
            ListesMarquesModelesFinitions(voiture.IdMarque, voiture.IdModele);
            return View(voiture);
        }

        // GET: Voitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voitures.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }

            ListeAnnees();
            ListesMarquesModelesFinitions(voiture.IdMarque, voiture.IdModele);
            return View(voiture);
        }

        // POST: Voitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodeVin,Annee,Image,Description,DateAchat,PrixAchat,VoitureReparee,DateMiseEnVente,PrixMiseEnVente,AnnoncePubliee,VoitureVendue,IdMarque,IdModele,IdFinition")] Voiture voiture)
        {
            if (id != voiture.Id)
            {
                return NotFound();
            }

            bool voitureExiste;
            voitureExiste = _context.Voitures.Any(v => v.CodeVin == voiture.CodeVin && v.Id != voiture.Id);
            if (voitureExiste)
            {
                ModelState.AddModelError("", "Une voiture portant ce code VIN existe déjà.");
            }

            if (voiture.DateMiseEnVente < voiture.DateAchat)
            {
                ModelState.AddModelError("", "La date de mise en vente ne peut pas être antérieure à la date d'achat de la voiture.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voiture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoitureExists(voiture.Id))
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

            ListeAnnees();
            ListesMarquesModelesFinitions(voiture.IdMarque, voiture.IdModele);
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voitures
                .Include(v => v.Finition)
                .Include(v => v.Marque)
                .Include(v => v.Modele)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voiture = await _context.Voitures.FindAsync(id);
            if (voiture != null)
            {
                _context.Voitures.Remove(voiture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoitureExists(int id)
        {
            return _context.Voitures.Any(e => e.Id == id);
        }

        private void ListeAnnees()
        {
            var anneeLaPlusAncienne = 1990;
            var anneeActuelle = DateTime.Now.Year;

            var annees = Enumerable
                .Range(anneeLaPlusAncienne, anneeActuelle - anneeLaPlusAncienne + 1)
                .Reverse()
                .ToList();

            ViewData["Annees"] = new SelectList(annees);
        }

        private void ListesMarquesModelesFinitions(int? idMarque, int? idModele)
        {
            // Marques
            ViewData["IdMarque"] = new SelectList(_context.Marques, "Id", "Nom", idMarque);

            // Modèles
            if (idMarque == null)
            {
                ViewData["IdModele"] = new SelectList(Enumerable.Empty<Modele>(), "Id", "Nom");
            }
            else
            {
                var modeles = _context.Modeles
                    .Where(m => m.IdMarque == idMarque)
                    .ToList();

                ViewData["IdModele"] = new SelectList(modeles, "Id", "Nom", idModele);
            }

            // Finitions
            if (idModele == null)
            {
                ViewData["IdFinition"] = new SelectList(Enumerable.Empty<Finition>(), "Id", "Nom");
            }
            else
            {
                var finitions = _context.Finitions
                    .Where(f => f.IdModele == idModele)
                    .ToList();

                ViewData["IdFinition"] = new SelectList(finitions, "Id", "Nom");
            }

            // Solution à revoir : les pages se rechargent en effaçant les données rentrées dans les autres champs...
        }
    }
}
