using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DAO.Interfejsi
{
    interface IKvar
    {
        List<Kvar> FindKvarovi();

        int brKvarZaDatum(DateTime dt);

        void UnesiKvar();

        List<Kvar> KvaroviUOpsegu();

        Kvar PrikazPoId(string id);

        void AzurirajKvarove(string id);

        void SaveExcel();

        double IzracunajPrioritetZaDane(string id);
    }
}
