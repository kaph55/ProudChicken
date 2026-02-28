using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.View
{
    interface IBrugerView
    {
        public Bruger GetBrugerDetails();
       
        public void ShowErrorLogin(string message);
        public void ClosePopUpLogin();
        public void GoToAdminPage();
    }
}
