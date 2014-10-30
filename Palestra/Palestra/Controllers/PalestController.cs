using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Management;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    public class PalestController : Controller
    {
        public ActionResult Index()
        {
            var lista = new List<Palestras>
            {
                new Palestras
                {
                    Nome = "Palestra Fuck Yeah!",
                    Titulo = "C# na Tora",
                    Codigo = "666",
                    Descricao = "Muito bom",
                    Horario = DateTime.Now,
                    Nivel = "100",
                    Palestrante = new Palestrante {Bio = "nao tem", Nome = "Marcos Vinicius"},
                    Sala = new Sala {Nome = "Sala 2", Numero = "2"},
                    Trilha = new Trilha {Nome = "Desenvolvimento"}
                }
            };
            return View(lista);
        }
        public ActionResult Cadastrar()
        {
            var trilhas = new List<Trilha>()
            {
                new Trilha() {Nome = "minha trilha"},
                new Trilha() {Nome = "minha outratrilha"}
            };
            ViewBag.Trilhas = new SelectList(trilhas,"ID","Nome");

            var salas = new List<Sala>()
            {
                new Sala() {Nome = "Sala 01"},
                new Sala() {Nome = "Sala02"}
            };
            ViewBag.Salas = new SelectList(salas, "ID", "Nome");

            var palestrantes = new List<Palestrante>()
            {
                new Palestrante() {Nome = "Marcos"},
                new Palestrante() {Nome = "Vinicius"}
            };
            ViewBag.Palestrantes = new SelectList(palestrantes, "ID", "Nome");



            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Palestras palestra)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index", "Palest");
            }
            return View();
        }//cadastrar sala
        public ActionResult Editar(Palestras palestras)
        {
          var palestra =   new Palestras
            {
                Nome = "Palestra Fuck Yeah!",
                Titulo = "C# na Tora",
                Codigo = "666",
                Descricao = "Muito bom",
                Horario = DateTime.Parse("10/10/2109"),
                Nivel = "100",
                Palestrante = new Palestrante {Bio = "nao tem", Nome = "Marcos Vinicius"},
                Sala = new Sala {Nome = "Sala 2", Numero = "2"},
                Trilha = new Trilha {Nome = "Desenvolvimento"}
            };
            return View(palestra);
        }
        public ActionResult Detalhe(string id)
        {
            var palestra = new Palestras
            {
                Nome = "Palestra Fuck Yeah!",
                Titulo = "C# na Tora",
                Codigo = "666",
                Descricao = "Muito bom",
                Horario = DateTime.Parse("10/10/2109"),
                Nivel = "100",
                Palestrante = new Palestrante { Bio = "nao tem", Nome = "Marcos Vinicius" },
                Sala = new Sala { Nome = "Sala 2", Numero = "2" },
                Trilha = new Trilha { Nome = "Desenvolvimento" }
            };
            return View(palestra);
        }
        public ActionResult Delete(string id)
        {
            var palestra = new Palestras
            {
                Nome = "Palestra Fuck Yeah!",
                Titulo = "C# na Tora",
                Codigo = "666",
                Descricao = "Muito bom",
                Horario = DateTime.Parse("10/10/2109"),
                Nivel = "100",
                Palestrante = new Palestrante { Bio = "nao tem", Nome = "Marcos Vinicius" },
                Sala = new Sala { Nome = "Sala 2", Numero = "2" },
                Trilha = new Trilha { Nome = "Desenvolvimento" }
            };
            return View(palestra);
        }

    }
}