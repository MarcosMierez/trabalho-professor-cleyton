using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palestra.Models
{
    public class Usuario :Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string Permissao { get; set; }
    }
}