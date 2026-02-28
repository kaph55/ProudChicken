using ProudChickenWPF.Model;
using ProudChickenWPF.Data;
using ProudChickenWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Controller
{
    class BrugerController
    {
        private BrugerRepository brugerRepository;
        private IBrugerView brugerView;
        public BrugerController(IBrugerView brugerView)
        {
            brugerRepository = new BrugerRepository();
            this.brugerView = brugerView;
            
        }
        public void doLogin()
        {
            Bruger bruger = brugerView.GetBrugerDetails();

            if (string.IsNullOrWhiteSpace(bruger.BrugerNavn) || string.IsNullOrWhiteSpace(bruger.AdgangsKode))
            {
                brugerView.ShowErrorLogin("Marked field are required");
                 return;
            }
           
             bool isValidBruger = brugerRepository.CheckBruger(bruger);
            if(!isValidBruger)
            {
                brugerView.ShowErrorLogin("Incorrect BrugerNavn or AdgangsKode");
                return;
            }
            brugerView.ClosePopUpLogin();
            brugerView.GoToAdminPage();
        }

       
    }
}
