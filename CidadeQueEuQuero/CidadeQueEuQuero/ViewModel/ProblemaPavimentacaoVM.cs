using System;
using System.ComponentModel;
using System.Windows.Input;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.DependencyServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CidadeQueEuQuero.ViewModel
{
    class ProblemaPavimentacaoVM : INotifyPropertyChanged
    {
        private string observacaoProblema;
        public string ObservacaoProblema
        {
            get
            {
                return observacaoProblema;
            }
            set
            {
                observacaoProblema = value;
                OnPropertyChanged("ObservacaoProblema");
            }
        }

        public ICommand SalvarProblemaCommand { get; set; }

        public ProblemaPavimentacaoVM()
        {
            SalvarProblemaCommand = new Command(SalvarProblema, SalvarProblemaCanExecute);
        }

        private bool SalvarProblemaCanExecute(object arg)
        {
            return !string.IsNullOrEmpty(ObservacaoProblema);
        }

        private async void SalvarProblema(object obj)
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            bool resultado = FirebaseDatabase.CadastrarProblema(new Model.Problema
            {
                ObservacaoProblema = ObservacaoProblema,
                UserId = FirebaseAuth.GetCurrentUserId(),
                HorarioProblema = DateTime.Now,
                TipoProblema = "ProblemaPavimentacao",
                LatitudeProblema = location.Latitude,
                LongitudeProblema = location.Longitude,
            });

            if (resultado)
            {
                DependencyService.Get<IToastMessage>().LongAlert("Problema cadastrado com sucesso!");
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Algo deu errado, por favor, tente novamente", "Ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
