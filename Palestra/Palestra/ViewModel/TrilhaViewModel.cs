using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.ViewModel
{
    public class TrilhaViewModel
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Preencha o campo nome")]
        public string Nome { get; set; }
    }
}