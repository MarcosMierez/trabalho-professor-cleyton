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
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string  Nivel { get; set; }
        [Required]
        public DateTime Horario { get; set; }
        [Required]
        [Display(Name="Palestrante")]
        public string PalestranteID { get; set; }
        [Required]
        [Display(Name = "Trilha")]
        public string TrilhaID { get; set; }
        [Required]
        [Display(Name = "Sala")]
        public string SalaID { get; set; }
        public Palestrante Palestrante { get; set; }
        public Trilha Trilha { get; set; }
        public  Sala Sala { get; set; }
    }
}