using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Usuario :Entidade
    {
        public Usuario(){
            Permissao = new List<string>();
        }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public List<string> Permissao { get; set; }
    }
}