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
        [AllowAnonymous]
        public ActionResult Index()
        {

            var listaPalestrante = appPalestrante.Listar();
            return View(listaPalestrante);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {
            return View(new Palestrante());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(Palestrante palestrante, HttpPostedFileBase arquivo)
        {
            if (ModelState.IsValid)
            {
                var palestranteBanco = appPalestrante.ListarPorId(palestrante.ID);
                if (palestranteBanco==null)
                {
                    this.Flash("Este usuario nao pode ser alterado", LoggerEnum.Warning);
                    return RedirectToAction("Index");
                }

                if (arquivo != null)
                {
                    if (ImagemHelper.validate(arquivo)==false)
                    {
                        this.Flash("A imagem dever ser os formatos jpg,jpeg,png e ter menos de 1 MB", LoggerEnum.Warning);
                        return View(palestrante);
                    }
                   palestrante.Foto= ImagemHelper.Upload(arquivo, "");
                }
                else
                {
                    palestrante.Foto = palestrante.Foto;
                }



                appPalestrante.Inserir(palestrante);
                return RedirectToAction("Index", "Palestrante");
            }
            return View(palestrante);
        }//cadastrar sala
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {
            var palestrante = appPalestrante.ListarPorId(id);
            return View(palestrante);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Palestrante palestrante ,HttpPostedFileBase arquivo)
        {
            if (ModelState.IsValid)
            {
                var palestranteBanco = appPalestrante.ListarPorId(palestrante.ID);
                if (palestranteBanco==null)
                {
                    this.Flash("Este usuario nao pode ser alterado", LoggerEnum.Warning);
                    return RedirectToAction("Index");
                }

                if (arquivo != null)
                {
                    if (ImagemHelper.validate(arquivo) == false)
                    {
                        this.Flash("A imagem dever ser os formatos jpg,jpeg,png e ter menos de 1 MB", LoggerEnum.Warning);
                        return View(palestrante);
                    }
                    palestrante.Foto = ImagemHelper.Upload(arquivo, "");
                    ImagemHelper.ExcluirArquivo(palestranteBanco.Foto,"");
                }
                else
                {
                    palestrante.Foto = palestranteBanco.Foto;
                }
                appPalestrante.Alterar(palestrante);
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
    }

}