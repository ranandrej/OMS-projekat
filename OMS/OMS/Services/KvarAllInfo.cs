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
        
        public KvarDAO kvarDAO= new KvarDAO();
        public AkcijeDAO akcijeDAO = new AkcijeDAO();
        public ElektricniElementiDAO elDAO = new ElektricniElementiDAO();
        public List<KvarAkcijaDTO>KvarElAkcije()
        {
            List<KvarAkcijaDTO> dtos = new List<KvarAkcijaDTO>();//DTO klasa sadrzi kvar sa njegovim elementom i listom akcija koje su izvrsene nad istim
            foreach(Kvar k in kvarDAO.FindKvarovi())
            {
                KvarAkcijaDTO dto = new KvarAkcijaDTO();
                dto.k = k;
                dto.akcije = akcijeDAO.FindAkcijeByKvar(k.IdKv);
                dto.el = elDAO.FindByIdEl(k.IdEl);
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
        public KvarPrioritetDTO kvarSaPrioritetom(string id)
        {
            KvarPrioritetDTO dto = new KvarPrioritetDTO();
            Kvar k = kvarDAO.PrikazPoId(id);
            dto.k = k;
            dto.akcije = akcijeDAO.FindAkcijeByKvar(k.IdKv);
            dto.el = elDAO.FindByIdEl(k.IdEl);
            if (String.Compare(k.statusKv, "U popravci")==0)
            {
                double p = kvarDAO.IzracunajPrioritetZaDane(k.IdKv) + (dto.akcije.Count() * 0.5);
                dto.prioritet = p;
            }
            else
            {
                dto.prioritet = 0;
            }
            return dto;


        }
    }
}
