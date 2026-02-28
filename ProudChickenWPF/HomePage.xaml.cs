using ProudChickenWPF.Controller;
using ProudChickenWPF.Data;
using ProudChickenWPF.Model;
using ProudChickenWPF.Service;
using ProudChickenWPF.View;
using System.Windows;

namespace ProudChickenWPF
{

    public partial class HomePage : Window, IBrugerView, IKundeView
    {
        private BrugerController brugerController; 
        private KundeController kundeController;

        public HomePage()
        {
            InitializeComponent();
            brugerController = new BrugerController(this);
            IKundeRepository kundeRepository = new KundeRepository();
            kundeController = new KundeController(new KundeService(kundeRepository), this);
        }
        public Bruger GetBrugerDetails()
        {
            Bruger bruger = new Bruger
            {
                BrugerNavn = TextBrugerNavn.Text,
                AdgangsKode = TxtAdgangsKode.Password,
            };
            return bruger;
        }

        public void ClosePopUpLogin()
        {
            PopupLogin.IsOpen = false;
        }

        public void ShowErrorLogin(string message)
        {
            TextBlockErrorMessageLogin.Text = message;
            TextBlockErrorMessageLogin.Visibility = Visibility.Visible;
        }

       

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            brugerController.doLogin();
           
        }

        private void BrugerButton_Click(object sender, RoutedEventArgs e)
        {
            PopupLogin.IsOpen = true;
            TextBrugerNavn.Focus();
        }

        public void GoToAdminPage()
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }      

        public Kunde GetKunde()
        {
            Kunde kunde = new Kunde
            {
                KundeNavn = TextBoxKundeNavn.Text,
                VejNavn = TextBoxVejNavn.Text,
                ByNavn = TextBoxByNavn.Text,
                PostNummer = TextBoxPostNummer.Text,
                Email = TextBoxEmail.Text,                 
                TelefonNummer = TextBoxTelefonNummer.Text
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
       

        public string GetPostNummer()
        {
            return string.Empty;
        }

        public void DisplayKunder(List<Kunde> kunder)
        {
            MessageBox.Show("Tilykke! You have been signed successfully.");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            PopupSignup.IsOpen = true;
            TextBoxKundeNavn.Focus();
        }

        private void GemKunde_Click(object sender, RoutedEventArgs e)
        {
            kundeController.TilføjeOgOpdateKunde();
        }

        public void ShowErrorKunde(string message)
        {
            TextBlockErrorMessageSignUp.Text = message;
            TextBlockErrorMessageSignUp.Visibility = Visibility.Visible;
        }

        
        public void ClosePopUpKunde()
        {
            PopupSignup.IsOpen = false;
        }

        public void VisBekræftelse(string message)
        {
            //throw new NotImplementedException();
        }
    }
}
