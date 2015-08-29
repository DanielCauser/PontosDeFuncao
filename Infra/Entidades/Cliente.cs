using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class Cliente
    {

        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Empresa { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string TipoDeRegistro { get; set; }
        public virtual string Registro { get; set; }

        public Cliente()
        {
            //this.Pessoas = new List<Pessoa>();
        }
    }
}