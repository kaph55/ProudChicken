using ProudChickenWPF.Model;
using ProudChickenWPF.Service;
using ProudChickenWPF.View;

namespace ProudChickenWPF.Controller;

public class BeskedHistorikController 
{
    private IBeskedService beskedService;
    private IBeskedHistorikView beskedHistorikView;
    public BeskedHistorikController(IBeskedService beskedService, IBeskedHistorikView beskedHistorikView)
    {
       
        this.beskedService = beskedService;
        this.beskedHistorikView = beskedHistorikView;
    }


    public void VisBeskedHistorik()
    {
        BeskedHistorikFilter filter = beskedHistorikView.GetBeskedHistorikFilter();
        List<BeskedHistorik> beskedHistorikListe = beskedService.SearchBeskeder(filter);
        beskedHistorikView.VisBeskedHistorik(beskedHistorikListe);
    }
}
