using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAO;
using OMS.DTO;

namespace OMS.Services
{

    public class KvarAllInfo
    {

        public KvarDAO kvarDAO = new KvarDAO();
        public AkcijeDAO akcijeDAO = new AkcijeDAO();
        public ElektricniElementiDAO elDAO = new ElektricniElementiDAO();
        public List<KvarAkcijaDTO> KvarElAkcije()
        {
            List<KvarAkcijaDTO> dtos = new List<KvarAkcijaDTO>();//DTO klasa sadrzi kvar sa njegovim elementom i listom akcija koje su izvrsene nad istim
            foreach (Kvar k in kvarDAO.FindKvarovi())
            {
                KvarAkcijaDTO dto = new KvarAkcijaDTO();
                dto.k = k;
                dto.akcije = akcijeDAO.FindAkcijeByKvar(k.IdKv);
                dto.el = elDAO.FindByIdEl(k.IdEl);
                dtos.Add(dto);
            }
            return dtos;
        }

        public List<KvarPrioritetDTO>  KvarPrioritet()
        {
            List<KvarPrioritetDTO> dtos= new List<KvarPrioritetDTO>();
            foreach(Kvar k in kvarDAO.FindKvarovi())
            {
                KvarPrioritetDTO dto = new KvarPrioritetDTO();
                dto.k = k;
                dto.akcije = akcijeDAO.FindAkcijeByKvar(k.IdKv);
                dto.el=elDAO.FindByIdEl(k.IdEl);
                dto.k.prioritet = Convert.ToDouble(kvarDAO.PrikaziPrioritet(k.IdKv));
                dtos.Add(dto);
            }
            return dtos;
        }
        public void AzurirajKvar()
        {
            Console.WriteLine("Unesite id kvara koji hocete da azurirate:");
            string id = Console.ReadLine();
            kvarDAO.AzurirajKvarove(id);
        }


       
    

        

    }
}
