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
            ListesMarquesModelesFinitions();
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
            ListesMarquesModelesFinitions();
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
            ListesMarquesModelesFinitions();
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
            ListesMarquesModelesFinitions();
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

        private void ListesMarquesModelesFinitions()
        {
            // Charger toutes les marques
            var marques = _context.Marques.ToList();
            ViewBag.Marques = new SelectList(marques, "Id", "Nom");

            // Construire le dictionnaire : Modèles par marque
            var modelesParMarque = new Dictionary<int, List<SelectListItem>>();

            foreach (var marque in marques)
            {
                var modeles = _context.Modeles
                    .Where(m => m.IdMarque == marque.Id)
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Nom
                    })
                    .ToList();

                modelesParMarque[marque.Id] = modeles;
            }

            ViewBag.ModelesParMarque = modelesParMarque;

            // Construire le dictionnaire : Finitions par modèle
            var finitionsParModele = new Dictionary<int, List<SelectListItem>>();

            foreach (var modele in _context.Modeles.ToList())
            {
                var finitions = _context.Finitions
                    .Where(f => f.IdModele == modele.Id)
                    .Select(f => new SelectListItem
                    {
                        Value = f.Id.ToString(),
                        Text = f.Nom
                    })
                    .ToList();

                finitionsParModele[modele.Id] = finitions;
            }

            ViewBag.FinitionsParModele = finitionsParModele;
        }
    }
}
