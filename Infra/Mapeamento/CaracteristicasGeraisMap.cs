using FluentNHibernate.Mapping;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Mapeamento
{
    public class CaracteristicasGeraisMap : ClassMap<CaracteristicasGerais>
    {
        public CaracteristicasGeraisMap()
        {
            Table("CaracteristicasGerais");
            Id(x => x.Id, "idCaracteristicasGerais");
            Map(x => x.Nome, "nome");
        }
    }
}