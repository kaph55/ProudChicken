using ProudChickenWPF.Data;
using ProudChickenWPF.Model;

namespace ProudChickenWPF.Service
{
    internal class BeskedService: IBeskedService
    {
        private readonly IBeskedRepository beskedRepository;

        public BeskedService(IBeskedRepository beskedRepository) 
        {
            this.beskedRepository = beskedRepository;
        }

        public void AddBesked(Besked besked)
        {
            // Save the message to the database
            int beskedId = beskedRepository.AddBesked(besked);

            foreach (Kunde kunde in besked.SelectedkunderListe)
            {
                bool sentSms = besked.SmsSend == true && kunde.PreferSms == true; 
                bool sentEmail = besked.EmailSend && kunde.PreferEmail;

                if(sentSms == false && sentEmail == false)
                {
                    continue;
                }

                beskedRepository.AddBeskedTilKunde(
                    beskedId,
                    kunde.Id,
                    sentSms,
                    sentEmail,
                    DateTime.Now
                );
            }
        }

        public List<BeskedHistorik> SearchBeskeder(BeskedHistorikFilter filter)
        {
            return beskedRepository.SearchBeskeder(filter);
        }
    }
}
