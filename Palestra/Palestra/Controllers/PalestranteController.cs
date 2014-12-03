using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;
using Palestra.Aplicacao;
using System.IO;
using System.Web.Helpers;
using Palestra.Helper;
using Palestra.ViewModel;

namespace Palestra.Controllers
{
    [Authorize]
    public class PalestranteController : Controller
    {

        private readonly PalestranteAplicacao appPalestrante;
        public PalestranteController()
        {
            appPalestrante = new PalestranteAplicacao();
        }
        [Authorize(Roles="palestrante_ver")]
        public ActionResult Index()
        {

            var listaPalestrante = appPalestrante.Listar();
            return View(listaPalestrante);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {
            return View(new PalestranteViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(PalestranteViewModel palestrante)
        {

            if (ModelState.IsValid)
            {
                var palestranteBanco = appPalestrante.ListarPorId(palestrante.ID);
                if (palestranteBanco == null)
                {
                    this.Flash("Este usuario nao pode ser alterado", LoggerEnum.Warning);
                    return RedirectToAction("Index");
                }
                if (palestrante.Foto != null)
                {
                    if (ImagemHelper.validate(palestrante.Foto) == false)
                    {
                        this.Flash("A imagem dever ser os formatos jpg,jpeg,png e ter menos de 1 MB", LoggerEnum.Warning);
                        return View(palestrante);
                    }
                    palestrante.FotoPath = ImagemHelper.Upload(palestrante.Foto, "");
                }
                var tempPalestrante = ConverteEmPalestrante(palestrante);
                appPalestrante.Inserir(tempPalestrante);
                return RedirectToAction("Index", "Palestrante");
            }
            return View(palestrante);
        }//cadastrar sala
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {
            var palestrante = appPalestrante.ListarPorId(id);
            var tempPalestrante = ConverteEmPalestranteViewModel(palestrante);
            return View(tempPalestrante);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(PalestranteViewModel palestrante)
        {
            var palestranteBanco = appPalestrante.ListarPorId(palestrante.ID);
            palestrante.FotoPath = palestranteBanco.Foto;
            if (palestranteBanco == null)
            {
                this.Flash("Este usuario nao pode ser alterado", LoggerEnum.Warning);
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {

                if (palestrante.Foto != null)
                {
                    if (ImagemHelper.validate(palestrante.Foto) == false)
                    {
                        this.Flash("A imagem dever ser os formatos jpg,jpeg,png e ter menos de 1 MB", LoggerEnum.Warning);
                        return View(palestrante);
                    }
                    palestrante.FotoPath = ImagemHelper.Upload(palestrante.Foto, "");
                    ImagemHelper.ExcluirArquivo(palestranteBanco.Foto, "");
                }
                var tempPalestrante = ConverteEmPalestrante(palestrante);

                appPalestrante.Alterar(tempPalestrante);
                return RedirectToAction("Index", "Palestrante");
            }
            return View(palestrante);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var palestrante = appPalestrante.ListarPorId(id);
            return View(palestrante);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Palestrante palestrante)
        {
            appPalestrante.Excluir(palestrante.ID);
            return RedirectToAction("Index", "Palestrante");
        }
        public ActionResult Detalhe(string id)
        {
            var usuario = appPalestrante.ListarPorId(id);
            return View(usuario);
        }
        private Palestrante ConverteEmPalestrante(PalestranteViewModel palestrante)
        {
            var tempPalestrante = new Palestrante
            {
                ID = palestrante.ID,
                Nome = palestrante.Nome,
                Twitter = palestrante.Twitter,
                Bio = palestrante.Bio,
                Foto = palestrante.FotoPath
            };
            return tempPalestrante;
        }
        private PalestranteViewModel ConverteEmPalestranteViewModel(Palestrante palestrante)
        {
            var tempPalestrante = new PalestranteViewModel
            {
                ID = palestrante.ID,
                Nome = palestrante.Nome,
                Twitter = palestrante.Twitter,
                Bio = palestrante.Bio,
                FotoPath = palestrante.Foto
            };
            return tempPalestrante;
        }
    }

}