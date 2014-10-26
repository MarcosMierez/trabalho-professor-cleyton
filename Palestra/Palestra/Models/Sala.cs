using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Sala : Entidade
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; set; }
    }
}