using ProudChickenWPF.Controller;
using ProudChickenWPF.Data;
using ProudChickenWPF.Model;
using ProudChickenWPF.Service;
using ProudChickenWPF.View;
using System.Windows;
using System.Windows.Documents;
namespace ProudChickenWPF
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window, IKundeView, IBeskedView, IBeskedHistorikView
    {
        private KundeController kundeController;
        private BeskedController beskedController;
        private BeskedHistorikController beskedReportController;
        private int selectedKundeId = 0;

        public Admin()
        {
            InitializeComponent();

            IBeskedRepository beskedRepository = new BeskedRepository();
            IBeskedService beskedService = new BeskedService(beskedRepository);
            IKundeRepository kundeRepository = new KundeRepository();
            kundeController = new KundeController(new KundeService(kundeRepository), this);
            beskedController = new BeskedController(new KundeRepository(), beskedService, this);
            beskedReportController = new BeskedHistorikController(beskedService, this);
            DatePickerBesked.SelectedDate = DateTime.Now;
            StartDatePicker.SelectedDate = DateTime.Now.AddDays(-1);
            EndDatePicker.SelectedDate = DateTime.Now;
        }

        // ShowKunder
        public void DisplayKunder(List<Kunde> kunder)
        {
            KundeList.ItemsSource = kunder;
        }

        public string GetPostNummer()
        {
            return TextBoxPostnummer.Text;
        }
        private void ShowKunderButton_Click(object sender, RoutedEventArgs e)
        {
            kundeController.ShowKunder();
        }

        private void GemKundeButton_Click(object sender, RoutedEventArgs e)
        {
            kundeController.TilføjeOgOpdateKunde();

        }

        // AddNyKunde 
        public Kunde GetKunde()
        {
            Kunde kunde = new Kunde
            {
                KundeNavn = TextBoxKundeNavn.Text,
                VejNavn = TextBoxVejNavn.Text,
                ByNavn = TextBoxByNavn.Text,
                Email = TextBoxEmail.Text,
                PostNummer = TextBoxPostNummer.Text,
                TelefonNummer = TextBoxTelefonNummer.Text,
                Id = selectedKundeId
            };
            if (CheckBoxPreferEmail.IsChecked.HasValue)
            {
                kunde.PreferEmail = CheckBoxPreferEmail.IsChecked.Value;
            }
            if (CheckBoxPreferSms.IsChecked.HasValue)
            {
                kunde.PreferSms = CheckBoxPreferSms.IsChecked.Value;
            }
            if (CheckBoxVarm.IsChecked.HasValue)
            {
                kunde.Varm = CheckBoxVarm.IsChecked.Value;
            }
            if (CheckBoxKold.IsChecked.HasValue)
            {
                kunde.Kold = CheckBoxKold.IsChecked.Value;
            }
            if (CheckBoxFrozen.IsChecked.HasValue)
            {
                kunde.Frozen = CheckBoxFrozen.IsChecked.Value;
            }


            return kunde;
        }

        private void AddNewKundeButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
            TextBoxKundeNavn.Focus();

            TextBoxKundeNavn.Text = string.Empty;
            TextBoxVejNavn.Text = string.Empty;
            TextBoxByNavn.Text = string.Empty;
            TextBoxPostNummer.Text = string.Empty;
            TextBoxTelefonNummer.Text = string.Empty;
            TextBoxEmail.Text = string.Empty;           
            CheckBoxPreferEmail.IsChecked = true;
            CheckBoxPreferSms.IsChecked = true;
            CheckBoxVarm.IsChecked = true;
            CheckBoxFrozen.IsChecked = true;
            CheckBoxKold.IsChecked = true;
            selectedKundeId = 0;
        }
        public void ShowErrorKunde(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }
        public void ClosePopUpKunde()
        {
            popup.IsOpen = false;
        }

        // Kunde TilFøje/Opdate bekræftelse message
        public void VisBekræftelse(string message)
        {
            MessageBox.Show(message);
        }




        // Oprette NyBesked
        public Besked OpretteBesked()
        {
            TextRange textBoxIndholdRange = new TextRange(TextBoxIndhold.Document.ContentStart, TextBoxIndhold.Document.ContentEnd);
            DateTime? selectedDate = DatePickerBesked.SelectedDate;

            DateTime now = DateTime.Now;

            if (selectedDate is null)
            {
                selectedDate = now;
            }

            string hour = HoursComboBoxBesked.Text;
            string minutes = MinutesComboBoxBesked.Text;

            if(!string.IsNullOrWhiteSpace(hour) && !string.IsNullOrWhiteSpace(minutes))
            {
                selectedDate = selectedDate.Value.Date.Add(new TimeSpan(Convert.ToInt32(hour), Convert.ToInt32(minutes), 0));
            }
            else
            {
                selectedDate = selectedDate.Value.Date.Add(now.TimeOfDay);
            }


            //if selected date is in past, set it to current
            if(selectedDate < now)
            {
                DatePickerBesked.SelectedDate = now;
                selectedDate = now;
            }           

            List<Kunde> kunder = KundeGridBesked.SelectedItems.Cast<Kunde>().ToList();

            if (kunder == null || kunder.Count == 0)
            {
                kunder = KundeGridBesked.ItemsSource as List<Kunde>;
            }

            Besked besked = new Besked
            {
                Indhold = textBoxIndholdRange.Text,
                SmsSend = CheckBoxSms.IsChecked == true,
                EmailSend = CheckBoxEmail.IsChecked == true,
                BegivenhedsDato = selectedDate.Value,
                SelectedkunderListe = kunder
            };
            return besked;
        }
        public string GetPostNummerInBesked()
        {
            return TextPostNummerInBesked.Text;
        }
       
        public void DisplayKunderInBesked(List<Kunde> kunder)
        {
            KundeGridBesked.ItemsSource = kunder;
        }
        private void KunderInBeskedButton_Click(object sender, RoutedEventArgs e)
        {
            beskedController.VisKunder();
        }

        public void ShowErrorBesked(string message)
        {
            MessageBox.Show(message);
            //ErrorMessageBeskedTextBlock.Text = message;
            //ErrorMessageBeskedTextBlock.Visibility = Visibility.Visible;
        }


        private void SendBeskedButton_Click(object sender, RoutedEventArgs e)
        {
           if( MessageBox.Show("Er du sikker på, du vil sende beskeden ? ", "Bekræft Afsendelse", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                beskedController.SendBesked();
            }
        }
        public void VisSendtBeskedBekræftelse()
        {
            MessageBox.Show("Tillykke! Besked sendt.");
            KundeGridBesked.ItemsSource = null;
        }


        // lave BeskedHistorik method
        public BeskedHistorikFilter GetBeskedHistorikFilter()
        {
            BeskedHistorikFilter filter = new BeskedHistorikFilter
            {
                StartAt = StartDatePicker.SelectedDate ?? DateTime.Now.AddYears(-1),
                EndAt = EndDatePicker.SelectedDate?.AddDays(1) ?? DateTime.Now,
                PostNummer = PostNummerTextBox.Text,
                KundeNavn = KundeNavnTextBox.Text,                
                ByNavn = ByNavnTextBox.Text,
            };
            return filter;
        }


        private void SøgBeskedButton_Click(object sender, RoutedEventArgs e)
        {
            beskedReportController.VisBeskedHistorik();
        }
        public void VisBeskedHistorik(List<BeskedHistorik> beskedReportListe)
        {
            ReportGrid.ItemsSource = beskedReportListe;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();

            // Close current (Admin) window
            Close();
        }

        private void OpdateKundeButton_Click(object sender, RoutedEventArgs e)
        {
            Kunde kunde = KundeList.SelectedItem as Kunde;
            if (kunde == null)
            {
                MessageBox.Show("Vælg kunde for at opdatere");
                return;
            }
            popup.IsOpen = true;
            TextBoxKundeNavn.Text = kunde.KundeNavn;
            TextBoxVejNavn.Text = kunde.VejNavn;
            TextBoxByNavn.Text = kunde.ByNavn;
            TextBoxPostNummer.Text = kunde.PostNummer;
            TextBoxTelefonNummer.Text = kunde.TelefonNummer;
            TextBoxEmail.Text = kunde.Email;

            CheckBoxPreferEmail.IsChecked = kunde.PreferEmail;
            CheckBoxPreferSms.IsChecked = kunde.PreferSms;
            CheckBoxVarm.IsChecked = kunde.Varm;
            CheckBoxFrozen.IsChecked = kunde.Frozen;
            CheckBoxKold.IsChecked = kunde.Kold;
            selectedKundeId = kunde.Id;
        }



        private void SletKundeButton_Click(object sender, RoutedEventArgs e)
        {
            Kunde kunde = KundeList.SelectedItem as Kunde;
            if (kunde == null)
            {
                MessageBox.Show("Vælg kunde for at slet");
                return;
            }
            if (MessageBox.Show("Er du sikker på, du vil slette denne kunde?", "Bekræft Sletning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                kundeController.SletKunde(kunde.Id);
            }
        }

        
    }
}