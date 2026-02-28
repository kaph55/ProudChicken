using ProudChickenWPF.Data;
using ProudChickenWPF.Model;
using ProudChickenWPF.Service;
using ProudChickenWPF.View;

namespace ProudChickenWPF.Controller;

class BeskedController
{
    private IKundeRepository kundeRepository;        
    private IBeskedService BeskedService;
    private IBeskedView beskedView;
    public BeskedController(IKundeRepository kundeRepository,  IBeskedService beskedService, IBeskedView beskedView)
    {
        this.kundeRepository = kundeRepository;
        this.beskedView = beskedView;
        this.BeskedService = beskedService;
        
    }
    public void VisKunder()
    {
        string PostNummer = beskedView.GetPostNummerInBesked();        
        List<Kunde> KundeList = kundeRepository.GetAlleKunder(PostNummer);
        beskedView.DisplayKunderInBesked(KundeList);

    }
    public void SendBesked() 
    {
       Besked besked = beskedView.OpretteBesked();

        // Check for empty message or empty customer list
        if (string.IsNullOrWhiteSpace(besked.Indhold) || besked.SelectedkunderListe == null || !besked.SelectedkunderListe.Any())
        {
            beskedView.ShowErrorBesked("Indhold or Kunder liste er tom.");
            return;
        }
        // Save the message to the database
        BeskedService.AddBesked(besked);
        beskedView.VisSendtBeskedBekræftelse();

       
    }
    
}
