using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("Cliente");
            Id(x => x.Id, "idCliente");
            Map(x => x.Nome, "nome");
            Map(x => x.Empresa, "empresa");
            Map(x => x.Email, "email");
            Map(x => x.Telefone, "telefone");
            Map(x => x.TipoDeRegistro, "tipoDeRegistro");
            Map(x => x.Registro, "registro");

        }
    }
}

