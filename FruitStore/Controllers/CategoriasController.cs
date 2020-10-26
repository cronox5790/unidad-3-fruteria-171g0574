using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FruitStore.Models;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Controllers
{
    public class CategoriasController : Controller
    {
        [Route("Categorias")]
        public IActionResult Index()
        {
            fruteriashopContext context = new fruteriashopContext();
            Repository<Categorias> repos = new Repository<Categorias>(context);
           

            return View(repos.GetAll().Where(x=>x.Eliminado==false).OrderBy(x => x.Nombre));
        }

        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            //c.Eliminado = false;
            try
            {
                fruteriashopContext context = new fruteriashopContext();
                CategoriasRepository repos = new CategoriasRepository(context);

                repos.Insert(c);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }

        }


        public IActionResult Editar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);
                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
        }
        [HttpPost]
        public IActionResult Editar(Categorias vm)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);

                    var original = repos.Get(vm.Id);

                    if (original != null)
                    {
                        original.Nombre = vm.Nombre;

                        repos.Update(original);
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }

        }

        public IActionResult Eliminar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);

                if (categoria == null)
                    return RedirectToAction("Index");
                else
                    return View(categoria);
            }
        }
        [HttpPost]
        public IActionResult Eliminar(Categorias c)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoria = repos.Get(c.Id);

                    repos.Delete(categoria);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);

            }
           
        }
    }
}