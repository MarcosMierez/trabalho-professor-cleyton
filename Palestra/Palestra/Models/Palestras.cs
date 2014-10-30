using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Palestra.Models
{
    public class Palestras : Entidade
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string  Nome { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string  Nivel { get; set; }
        public Palestrante Palestrante { get; set; }
        public Trilha Trilha { get; set; }
        public Sala Sala { get; set; }
        public DateTime Horario { get; set; }
    }
}