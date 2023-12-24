using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Klase;
namespace OMS.DTO
{
    public class KvarAkcijaDTO
    {
        public Kvar k { get; set; }
        public List<Akcija> akcije { get; set; }

        public ElektricniElementi el { get; set; }
    }
}
