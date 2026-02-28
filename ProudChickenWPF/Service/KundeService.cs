using ProudChickenWPF.Controller;
using ProudChickenWPF.Data;
using ProudChickenWPF.Model;
using ProudChickenWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Service
{
    class KundeService : IKundeService
    {

        private IKundeRepository kundeRepository;

        public KundeService(IKundeRepository kundeRepository)
        {
            this.kundeRepository = kundeRepository;
        }

        public List<Kunde> HentFilterKunder(string postNummer)
        {
            return kundeRepository.GetAlleKunder(postNummer);
        }

        
        public void TilføjeOgOpdateKunde(Kunde kunde)
        {
            if(kunde.Id > 0)
            {
                kundeRepository.OpdateKunde(kunde);                

            }
            else
            {
                kundeRepository.AddKunde(kunde);
            }
           

        }

        public void SletKunde(int kundeId)
        {
            kundeRepository.SletKunde(kundeId);
        }

    }
}
