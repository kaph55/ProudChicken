using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Data
{
    interface IKundeRepository
    {
        public List<Kunde> GetAlleKunder(string postNummer);
        public void AddKunde(Kunde kunde);
        public void OpdateKunde(Kunde kunde);
        public void SletKunde(int kundeId);
    }
}
