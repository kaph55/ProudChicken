using ProudChickenWPF.Model;

namespace ProudChickenWPF.View
{
    public interface IKundeView

    {
        public string GetPostNummer();
        public void DisplayKunder(List<Kunde> kunder);
        public Kunde GetKunde();
        public void ShowErrorKunde(string message);
        public void VisBekræftelse(string message);
       
        public void ClosePopUpKunde();
       

    }
}
