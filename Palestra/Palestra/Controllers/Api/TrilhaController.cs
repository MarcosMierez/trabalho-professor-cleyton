using Palestra.Aplicacao;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Palestra.Controllers.Api
{
    public class TrilhaController : ApiController
    {
        private readonly TrilhaAplicacao appTrilha;

        public TrilhaController()
        {
            appTrilha = new TrilhaAplicacao();
        }


        //Composer :GET http://localhost:2592/Api/Sala

        public IEnumerable<Trilha> Get()
        {
            return appTrilha.Listar();
        }
        //Composer :GET http://localhost:2592/Api/Sala/49f9862ec462457d8dc0849cf166c7e1
        public Trilha Get(string id)
        {
            return appTrilha.ListarPorId(id);
        }

        /*
         POST http://localhost:2592/Api/Sala
         User-Agent: Fiddler
        Host: localhost:2592
        Content-Length: 40
        Content-Type : Application/Json

         {
            Nome : 'Sala 01',
            Numero : '01'
            }

         
         */

        public HttpResponseMessage Post(Trilha trilha)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            appTrilha.Inserir(trilha);

            return Request.CreateResponse(HttpStatusCode.Created, trilha);
        }

        /*
        PUT: http://localhost:2592/Api/Sala/c90c9d68e9d944ac8412c96ebd415d83
         * 
         User-Agent: Fiddler
Host: localhost:2592
Content-Length: 86
Content-Type : Application/Json

            {
            ID:'c90c9d68e9d944ac8412c96ebd415d83',
            Nome : 'Sala Alterada',
            Numero : '01'
            }

         */
        public HttpResponseMessage Put(string id, Trilha trilha)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != trilha.ID)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var salaBanco = appTrilha.ListarPorId(id);
            if (salaBanco == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            appTrilha.Alterar(trilha);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /*
         
       DELETE:  http://localhost:2592/Api/Sala/c90c9d68e9d944ac8412c96ebd415d83
         */
        public HttpResponseMessage Delete(string id)
        {

            var trilhaBanco = appTrilha.ListarPorId(id);
            if (string.IsNullOrEmpty(trilhaBanco.Nome))
                return Request.CreateResponse(HttpStatusCode.NotFound,"Trilha nao encontrada");

            appTrilha.Excluir(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
