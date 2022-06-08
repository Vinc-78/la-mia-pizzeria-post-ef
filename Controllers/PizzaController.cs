using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Diagnostics;
using la_mia_pizzeria_static.Models;



namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
       

        public static PizzaContext db = new PizzaContext();

        

        public IActionResult Index()
        {
            //inizializzato il db con 3 pizze e caricate nel db
            //parte da eseguire una prima volta
            /*
            Pizza pizza1 = new Pizza() 
            {
                
                Nome = "Margherita",
                Descrizione = "Pizza con mozzarella e pomodoro",
                ImgPath = "/img/pizza1.jpg",
                Prezzo = "5.00",

            };
            Pizza pizza2 = new Pizza()
            {
                
                Nome = "Filetto",
                Descrizione = "Pizza con pomodorini tagliatti a fette",
                ImgPath = "/img/pizza2.jpg",
                Prezzo = "7.00"

            };
            Pizza pizza3 = new Pizza()
            {
                
                Nome = "Al salame",
                Descrizione = "Pizza con fette di salame",
                ImgPath = "/img/pizza3.jpg",
                Prezzo = "6.50"

            };

            db.Add(pizza1);
            db.Add(pizza2);
            db.Add(pizza3);
            db.SaveChanges();

           */


            return View(db);

        }


        
        public IActionResult Show(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
               
                Pizza pizzaTrovata = db.pizzas
                             .Where(pizza => pizza.Id == id)
                             .First();
                if (pizzaTrovata != null) { return View("Show", pizzaTrovata); }
                else return NotFound("ID non trovato" + id);
            }

        }
        
        
        public IActionResult Modifica(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? PizzaDaModificare = db.pizzas.Where(p => p.Id == id).First();

                if (PizzaDaModificare != null)
                {
                    return View("Modifica", PizzaDaModificare);
                }
                else { return NotFound(" La pizza con l'id " + id + " non è stato trovato "); }
            }
        }
        
        
        public IActionResult ModificaSecondaVersione(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? PizzaDaModificare = db.pizzas.Where(p => p.Id == id).First();
                if (PizzaDaModificare != null)
                {
                    return View("ModificaSecondaVersione", PizzaDaModificare);
                }
                else { return NotFound(" La pizza con l'id " + id + " non è stato trovato "); }
            }
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pizza Modificata)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifica", Modificata);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileInfo fileInfo = new FileInfo(Modificata.File.FileName);

            string fileName = Modificata.Nome.Trim().ToLower() + fileInfo.Extension.Trim().ToLower();


            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                Modificata.File.CopyTo(stream);
            }



            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaModificare = null;

                pizzaDaModificare = db.pizzas
                         .Where(pizza => pizza.Id == Modificata.Id).FirstOrDefault(); 

                if (pizzaDaModificare != null)
                {

                    pizzaDaModificare.Nome = Modificata.Nome;
                    pizzaDaModificare.Descrizione = Modificata.Descrizione;
                    pizzaDaModificare.ImgPath = "/File/" + fileName;
                    pizzaDaModificare.Prezzo = Modificata.Prezzo.ToString();

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
        

        
        public IActionResult Elimina(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaDaRimuovere = db.pizzas
                    .Where(pizza => pizza.Id == id)
                    .First();

                if (pizzaDaRimuovere != null)
                {
                    db.Remove(pizzaDaRimuovere);

                    db.SaveChanges();
                    return RedirectToAction("Index", "Pizza");
                }
                else
                {
                    return NotFound(" La pizza con l'id " + id + " non è stato trovato ");
                }


            }
        }


       

    }
}
