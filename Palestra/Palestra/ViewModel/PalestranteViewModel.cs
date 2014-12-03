using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.ViewModel
{
    public class PalestranteViewModel
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string Nome { get; set; }
        [StringLength(300,MinimumLength=30,ErrorMessage="sua biografia dever ter de 30 a 300 caracteres")]
        public string Bio { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio")]
        public string Twitter { get; set; }
        
        public HttpPostedFileBase Foto { get; set; }
        public string FotoPath { get; set; }  
    }
}