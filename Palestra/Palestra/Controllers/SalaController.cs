﻿using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Management;
using Palestra.Aplicacao;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    public class SalaController : Controller
    {
        private readonly SalaAplicacao appSala;

        public SalaController()
        {
            appSala = new SalaAplicacao();
        }
        public ActionResult Index()
        {
            var listaSalas = appSala.Listar();
            return View(listaSalas);
        }
        public ActionResult Cadastrar()
        {
            return View(new Sala());
        }
        [HttpPost]
        public ActionResult Cadastrar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Inserir(sala);
               return RedirectToAction("Index", "Sala");
            }
            return View(sala);
        }
        public ActionResult Editar(string id)
        {
            var sala = appSala.ListarPorId(id);
            return View(sala);
        }
        [HttpPost]
        public ActionResult Editar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Alterar(sala);
                return RedirectToAction("Index", "Sala");
            }
            return View(sala);
        }
        public ActionResult Delete(string id)
        {
            var sala = appSala.ListarPorId(id);
            return View(sala);
        }
        [HttpPost]
        public ActionResult Delete(Sala sala)
        {
            appSala.Excluir(sala.ID);
            return RedirectToAction("Index","Sala");
        }

    }
}