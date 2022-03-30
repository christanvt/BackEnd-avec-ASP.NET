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
