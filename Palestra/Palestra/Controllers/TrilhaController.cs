using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;

namespace Palestra.Controllers
{
    public class TrilhaController : Controller
    {
        // GET: Trilha
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Trilha trilha)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index", "Trilha");
            }
            return View();
        }//cadastrar sala
        public ActionResult Editar(Trilha  trilha)
        {
            return View();
        }
        public ActionResult Detalhe(int id)
        {
            return View();
        }//mostrar detalhe
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}