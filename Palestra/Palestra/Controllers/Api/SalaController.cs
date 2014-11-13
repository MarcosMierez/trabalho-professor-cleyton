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
    public class SalaController : ApiController
    {
        private readonly SalaAplicacao appSala;

        public SalaController()
        {
            appSala = new SalaAplicacao();
        }


        //Composer :GET http://localhost:2592/Api/Sala

        public IEnumerable<Sala> Get()
        {
            return appSala.Listar();
        }
        //Composer :GET http://localhost:2592/Api/Sala/49f9862ec462457d8dc0849cf166c7e1
        public Sala Get(string id)
        {
            return appSala.ListarPorId(id);
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

        public HttpResponseMessage Post(Sala sala)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            appSala.Inserir(sala);

            return Request.CreateResponse(HttpStatusCode.Created, sala);
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
        public HttpResponseMessage Put(string id, Sala sala)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != sala.ID)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var salaBanco = appSala.ListarPorId(id);
            if (salaBanco == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            appSala.Alterar(sala);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /*
         
       DELETE:  http://localhost:2592/Api/Sala/c90c9d68e9d944ac8412c96ebd415d83
         */
        public HttpResponseMessage Delete(string id)
        {
            
            var salaBanco = appSala.ListarPorId(id);
            if (string.IsNullOrEmpty(salaBanco.Nome))
                return Request.CreateResponse(HttpStatusCode.NotFound,"Sala nao encontrada");

            appSala.Excluir(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
