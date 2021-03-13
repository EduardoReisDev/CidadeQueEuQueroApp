using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using CidadeQueEuQuero.Database.Auth;

namespace CidadeQueEuQuero.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string nome;
        public string Nome
        {
            get { 
                return nome; 
            }

            set { 
                nome = value;
                OnPropertyChanged("Nome");
                OnPropertyChanged("CanRegister");
            }
        }

        private string email;

        public string Email
        {
            get { 
                return email; 
            }

            set { 
                email = value;
                OnPropertyChanged("Email");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }

        private string password;

        public string Password
        {
            get { 
                return password; 
            }

            set { 
                password = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { 
                return confirmPassword;
            }

            set {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
                OnPropertyChanged("CanRegister");
            }
        }

        public bool CanLogin
        {
            get { 
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
        }

        public bool CanRegister
        {
            get { 
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrEmpty(Nome); 
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login, LoginCanExecute);
            RegisterCommand = new Command(Register, RegisterCanExecute);
        }

        private async void Register(object parameter)
        {
            if (ConfirmPassword != Password)
            {
                await App.Current.MainPage.DisplayAlert("Erro", "As senhas estão diferentes", "Ok");
            }
            else
            {
                bool result = await FirebaseAuth.CadastroUsuario(Nome, Email, Password);
                if (result)
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private bool RegisterCanExecute(object parameter)
        {
            return CanRegister;
        }

        private async void Login(object parameter)
        {
            bool result = await FirebaseAuth.AutenticacaoUsuario(Email, Password);
            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private bool LoginCanExecute(object parameter)
        {
            return CanLogin;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
