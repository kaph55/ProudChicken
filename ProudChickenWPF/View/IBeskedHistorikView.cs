using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.View
{
    public interface IBeskedHistorikView
    {
        public BeskedHistorikFilter GetBeskedHistorikFilter();
        public void VisBeskedHistorik(List<BeskedHistorik> beskedHistorikListe);
    }
}
