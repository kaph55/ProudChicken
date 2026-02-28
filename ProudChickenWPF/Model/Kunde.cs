namespace ProudChickenWPF.Model
{
    public class Kunde
    {
        public int Id {  get; set; }
        public required string KundeNavn { get; set; }
        public required string VejNavn { get; set; }
        public required string PostNummer { get; set; }
        public required string ByNavn { get; set;}
        public required string TelefonNummer {  get; set; }
        public required string Email {  get; set; }
        public bool PreferEmail { get; set; }
        public bool PreferSms { get; set; }
        public bool Kold { get; set; }
        public bool Frozen { get; set; }
        public bool Varm { get; set; }
        
    }
}
