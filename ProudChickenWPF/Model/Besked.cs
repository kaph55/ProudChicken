namespace ProudChickenWPF.Model
{
    public class Besked
    {
        public int Id { get; set; }
        public string Indhold { get; set; }
        public bool SmsSend { get; set; }
        public bool EmailSend { get; set; }
        public DateTime BegivenhedsDato { get; set; }
        public List<Kunde> SelectedkunderListe { get; set; } = new List<Kunde>();


    }
}
