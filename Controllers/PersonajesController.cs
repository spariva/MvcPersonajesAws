using Microsoft.AspNetCore.Mvc;
using MvcPersonajesAws.Models;
using MvcPersonajesAws.Services;

namespace MvcPersonajesAws.Controllers
{
    public class PersonajesController: Controller
    {
        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = this.service.GetPersonajesAsync().Result;
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje p)
        {
            await this.service.CreatePersonaje(p.Nombre, p.Imagen);
            return RedirectToAction("Index");
        }
    }
}
