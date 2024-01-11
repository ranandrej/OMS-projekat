using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Klase;
namespace OMS.DTO
{
    public class KvarPrioritetDTO
    {
        public Kvar k { get; set; }
        public double prioritet;
        public List<Akcija> akcije { get; set; }

        public ElektricniElementi el { get; set; }
    }
}
