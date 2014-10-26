using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;

namespace Palestra.Controllers
{
    public class PalestranteController : Controller
    {
        // GET: Trilha
        public ActionResult Index()
        {
            var listaPalestrante = new List<Palestrante>();
            var palestrante = new Palestrante();
            palestrante.Nome = "Marcos";
            palestrante.Twitter = "@Vindicator.com";
            palestrante.Bio = "Tem nao Véi";
            listaPalestrante.Add(palestrante);

            var palestrante01 = new Palestrante();
            palestrante01.Nome = "Vinicius";
            palestrante01.Twitter = "@Vindicator.com";
            palestrante01.Bio = "Tem nao Véi";
            listaPalestrante.Add(palestrante01);

            var palestrante03 = new Palestrante();
            palestrante03.Nome = "Maria";
            palestrante03.Twitter = "@Maria.com";
            palestrante03.Bio = "Tem nao Véi";
            listaPalestrante.Add(palestrante);
            return View(listaPalestrante);
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Palestrante palestrante)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index", "Palestrante");
            }
            return View();
        }//cadastrar sala

        public ActionResult Editar(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Editar(Palestrante  palestrante)
        {
            return View();
        }
        public ActionResult Detalhe(string id)
        {
            return View();
        }
        public ActionResult Delete(string id)
        {
            return View();
        }
    }
}