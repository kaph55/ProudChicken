using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.View
{
    public interface IBeskedView
    {
         public string GetPostNummerInBesked();        
        public Besked OpretteBesked();
        public void DisplayKunderInBesked(List<Kunde> kunder);         
        public void ShowErrorBesked(string message);
        public void VisSendtBeskedBekræftelse();

    }
}
