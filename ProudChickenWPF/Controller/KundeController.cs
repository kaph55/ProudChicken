using ProudChickenWPF.Data;
using ProudChickenWPF.Model;
using ProudChickenWPF.Service;
using ProudChickenWPF.View;

namespace ProudChickenWPF.Controller
{
    class KundeController
    {
        private IKundeService kundeService;
        private IKundeView kundeView;

        public KundeController(IKundeService kundeService, IKundeView kundeView)
        {
            this.kundeService = kundeService;
            this.kundeView = kundeView;
            
        }
        public void ShowKunder()
        {
            string postNummer = kundeView.GetPostNummer();
             List<Kunde> KundeList = kundeService.HentFilterKunder(postNummer);  
            kundeView.DisplayKunder(KundeList);
        }
       
        public void TilføjeOgOpdateKunde()
        {
            Kunde kunde = kundeView.GetKunde();
            

            if(string.IsNullOrWhiteSpace(kunde.KundeNavn)
                || string.IsNullOrWhiteSpace(kunde.VejNavn)
                || string.IsNullOrWhiteSpace(kunde.ByNavn)
                || string.IsNullOrWhiteSpace(kunde.PostNummer)
                || string.IsNullOrWhiteSpace(kunde.TelefonNummer)
                || string.IsNullOrWhiteSpace(kunde.Email))
            {
                kundeView.ShowErrorKunde("Ingen kundedata angivet.");
                return;
            }

            bool isOpdate = kunde.Id > 0;

            kundeService.TilføjeOgOpdateKunde(kunde);

            if (isOpdate)
            {
                kundeView.VisBekræftelse("Kunden er opdateret.");
            }
            else
            {
                kundeView.VisBekræftelse("Kunden er tilføjet.");
            }



            kundeView.ClosePopUpKunde();   
           
            ShowKunder();
            
        }
        public void SletKunde(int kundeId)
        {
            kundeService.SletKunde(kundeId);
            kundeView.VisBekræftelse("Kunden er slet");
            ShowKunder();
        }


    }
}
