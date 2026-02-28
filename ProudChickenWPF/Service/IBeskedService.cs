using ProudChickenWPF.Model;

namespace ProudChickenWPF.Service;

public interface IBeskedService
{
    void AddBesked(Besked besked);
    List<BeskedHistorik> SearchBeskeder(BeskedHistorikFilter filter);
}
