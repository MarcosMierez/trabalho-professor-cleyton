using System.Runtime.InteropServices;
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
using System.ComponentModel.DataAnnotations;
using Palestra.ViewModel;

namespace Palestra.Controllers
{
    [Authorize]
    public class SalaController : Controller
    {
        private readonly SalaAplicacao appSala;

        public SalaController()
        {
            appSala = new SalaAplicacao();
        }
        [Authorize(Roles = "sala_ver")]
        public ActionResult Index()
        {
            var listaSalas = appSala.Listar();
            return View(listaSalas);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {
            return View(new SalaViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(SalaViewModel sala)
        {
            var tempSala = new Sala
            {
                ID = sala.ID,
                Nome = sala.Nome,
                Numero = sala.Numero
            };
            if (ModelState.IsValid)
            {
                appSala.Inserir(tempSala);
                this.Flash("Salvo com sucesso");
                return RedirectToAction("Index", "Sala");
            }
            this.Flash("Favor preenha todos os campos", LoggerEnum.Error);
            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {
            var sala = appSala.ListarPorId(id);
            var tempsala = new SalaViewModel
            {
                ID = sala.ID,
                Nome = sala.Nome,
                Numero = sala.Numero

            };
            return View(tempsala);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(SalaViewModel sala)
        {
            var tempSala = new Sala()
            {
                ID = sala.ID,
                Nome = sala.Nome,
                Numero = sala.Numero
            };
            if (ModelState.IsValid)
            {
                appSala.Alterar(tempSala);
                this.Flash("Sala alterada com sucesso");
                return RedirectToAction("Index", "Sala");
            }

            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var sala = appSala.ListarPorId(id);
            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Sala sala)
        {
            //checar se pode ou nao excluir
            var salasComPalestras =(new PalestraAplicacao()).SalasComPalestras(sala.ID);
            if (salasComPalestras.Any())
            {
                 this.Flash("Esta Sala Nao pode ser removida pois ha palestras vinculadas a ela",LoggerEnum.Info);
                 return RedirectToAction("Index", "Sala");
            }
            appSala.Excluir(sala.ID);
            this.Flash("Sala removida com sucesso");

            return RedirectToAction("Index", "Sala");
        }

    }
}