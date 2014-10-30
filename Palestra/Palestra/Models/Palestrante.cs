using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Palestrante :Entidade
    {
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string Nome { get; set; }      
        public string  Bio { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string  Twitter { get; set; }  
        public string Foto { get; set; }  
    }
}