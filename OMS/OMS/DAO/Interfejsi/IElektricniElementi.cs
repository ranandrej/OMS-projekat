using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DAO.Interfejsi
{
    public interface IElektricniElementi
    {
        List<ElektricniElementi> PronadjiElemente();
        ElektricniElementi FindByIdEl(int id);
        void UnesiElektricniElement();

    }
}
