using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Sala : Entidade
    {
        [Required(ErrorMessage="Favor digite o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor digite o Numero")]
        public string Numero { get; set; }
    }
}