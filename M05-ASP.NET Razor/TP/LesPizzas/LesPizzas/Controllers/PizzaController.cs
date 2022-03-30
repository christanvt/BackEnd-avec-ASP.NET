using BO;
using LesPizzas.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LesPizzas.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Pizza> pizzas;
        private static List<Pate> patesDisponibles;
        private static List<Ingredient> ingredientsDisponibles;

        public PizzaController()
        {
            // Lors de l'appel à la toute première action du controleur, les propriétés statiques seront chargées une et une seule fois pour toute la durée d'exécution du site       
            if (pizzas == null)
            {
                pizzas = new List<Pizza>();
            }

            if (patesDisponibles == null)
            {
                patesDisponibles = Pizza.PatesDisponibles;
            }

            if (ingredientsDisponibles == null)
            {
                ingredientsDisponibles = Pizza.IngredientsDisponibles;
            }
        }


        private PizzaEdition GetPizzaEdition(int id)
        {
            var vm = new PizzaEdition();
            vm.Pizza = pizzas.FirstOrDefault(p => p.Id == id);
            vm.Ingredients = ingredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
            vm.Pates = patesDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

            if (vm.Pizza.Pate != null)
            {
                vm.IdSelectedPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdSelectedIngredients = vm.Pizza.Ingredients.Select(i => i.Id).ToList();
            }
            return vm;
        }

        private bool ValidRegles(PizzaEdition pe)
        {
            var errorScore = 0;

            if (pizzas.Any(p => p.Nom.ToUpper() == pe.Pizza.Nom.ToUpper() && pe.Pizza.Id != p.Id))
            {
                ModelState.AddModelError("", "Il existe déjà une pizza portant ce nom ");
                errorScore++;
            }

            if (pe.Pizza.Nom.Length < 5 || pe.Pizza.Nom.Length > 20)
            {
                ModelState.AddModelError("", "Une pizza doitavoir un nom compris entre 5 et 20 caractères ");
                errorScore++;
            }

            if (pe.IdSelectedPate == 0)
            {
                ModelState.AddModelError("", "La pizza doit avoir une pate ");
                errorScore++;
            }

            if (pe.IdSelectedIngredients.Count < 2 || pe.IdSelectedIngredients.Count > 5)
            {
                ModelState.AddModelError("", "La pizza doit avoir entre 2 et 5 ingrédients ");
                errorScore++;
            }

            var pizzasVerifNombreIngredients = pizzas.Where(p => p.Ingredients.Count == pe.IdSelectedIngredients.Count && pe.Pizza.Id != p.Id);

            foreach (var pizza in pizzasVerifNombreIngredients)
            {
                var verifIngredients = true;
                for (int i = 0; i < pizza.Ingredients.Count; i++)
                {
                    if (!pe.IdSelectedIngredients.Contains(pizza.Ingredients[i].Id))
                    {
                        verifIngredients = false;
                        break;
                    }
                }
                if (verifIngredients)
                {
                    ModelState.AddModelError("", "Il existe une pizza pocédant les mêmes ingrédients");
                    errorScore++;
                    break;
                }

            }
            return errorScore == 0;
        }

        // GET: Pizza
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            var pizza = pizzas.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            var vm = new PizzaEdition();
            vm.Ingredients = ingredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
            vm.Pates = patesDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaEdition pizzaEdition)
        {
            try
            {
                if (!ValidRegles(pizzaEdition))
                {
                    throw new Exception("Une erreure de saisie s'est produite");
                }

                
                var pizza = pizzaEdition.Pizza;
                pizza.Ingredients = ingredientsDisponibles.Where(i => pizzaEdition.IdSelectedIngredients.Contains(i.Id)).ToList();
                pizza.Pate = patesDisponibles.FirstOrDefault(p => p.Id == pizzaEdition.IdSelectedPate);
                pizza.Id = pizzas.Any() ? pizzas.Max(p => p.Id) + 1 : 1;
                pizzas.Add(pizza);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                pizzaEdition.Ingredients = ingredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
                pizzaEdition.Pates = patesDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();
                return View(pizzaEdition);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetPizzaEdition(id));
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaEdition pizzaEdition)
        {
            try
            {
                if (!ValidRegles(pizzaEdition))
                {
                    throw new Exception("Une erreure de saisie s'est produite");
                }

                var pizza = pizzas.FirstOrDefault(p => p.Id == pizzaEdition.Pizza.Id);
                pizza.Nom = pizzaEdition.Pizza.Nom;
                pizza.Ingredients = ingredientsDisponibles.Where(i => pizzaEdition.IdSelectedIngredients.Contains(i.Id)).ToList();
                pizza.Pate = patesDisponibles.FirstOrDefault(p => p.Id == pizzaEdition.IdSelectedPate);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View(GetPizzaEdition(pizzaEdition.Pizza.Id));
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            var pizza = pizzas.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var pizza = pizzas.FirstOrDefault(p => p.Id == id);
                pizzas.Remove(pizza);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
