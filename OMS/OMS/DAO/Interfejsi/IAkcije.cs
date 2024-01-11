using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Klase;
namespace OMS.DAO.Interfejsi
{
    interface IAkcije
    {
        List<Akcija> FindAkcije();
        List<Akcija> FindAkcijeByKvar(string id);
    }
}
