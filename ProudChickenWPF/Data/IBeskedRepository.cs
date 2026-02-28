using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Data
{
    interface IBeskedRepository
    {
        public int AddBesked(Besked besked);
        public void AddBeskedTilKunde(int beskedId, int kundeId, bool smsSent, bool emailSent, DateTime sentTime);
        public List<BeskedHistorik> SearchBeskeder(BeskedHistorikFilter filter);
    }
}
