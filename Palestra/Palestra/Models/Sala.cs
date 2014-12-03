using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Sala : Entidade
    {
        public string Nome { get; set; }
        public string Numero { get; set; }
    }
}