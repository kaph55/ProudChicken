using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Service
{
    public interface IKundeService
    {
        public List<Kunde> HentFilterKunder(string postNummer);
        
        public void TilføjeOgOpdateKunde(Kunde kunde);
        public void SletKunde(int kundeId);
    }
}
