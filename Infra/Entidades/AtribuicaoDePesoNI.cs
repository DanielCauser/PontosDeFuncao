using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infra.Entidades
{
    public class AtribuicaoDePesoNI
    {
        public virtual int Id { get; set; }
        public virtual int Avaliacao { get; set; }
        public virtual CaracteristicasGerais CaracteristicasGerais { get; set; }
        public virtual NivelDeInfluencia NivelDeInfluencia { get; set; }
    }
}