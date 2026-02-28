namespace ProudChickenWPF.Model;

public class BeskedHistorik
{
    public int BeskedId {  get; set; }
    public required string Indhold {  get; set; }
    public bool SendtSms { get; set; }
    public bool SendtEmail { get; set; }
    public DateTime BegivenhedsDato { get; set; }
    public required string KundeNavn {  get; set; }
    public required string VejNavn { get; set; }
    public required string ByNavn { get; set; }


}
