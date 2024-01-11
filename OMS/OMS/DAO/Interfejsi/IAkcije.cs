using OMS.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DAO.Interfejsi
{
    public interface IAkcije
    {
        List<Akcija> FindAkcije();
        List<Akcija> FindAkcijeByKvar(string id);
    }
}
