using System.Collections.ObjectModel;
using System.ComponentModel;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.Model;
using CidadeQueEuQuero.View;

namespace CidadeQueEuQuero.ViewModel
{
    public class MinhasPostagensVM : INotifyPropertyChanged
    {
        public ObservableCollection<Problema> Problemas { get; set; }

        private Problema problemaSelecionado;
        public Problema ProblemaSelecionado
        {
            get
            {
                return problemaSelecionado;
            }
            set
            {
                problemaSelecionado = value;
                OnPropertyChanged("ProblemaSelecionado");
                if(problemaSelecionado != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new Postagem(problemaSelecionado));
                }
            }
        }

        public MinhasPostagensVM()
        {
            Problemas = new ObservableCollection<Problema>();
            ListarProblemas();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void ListarProblemas()
        {
            if (!FirebaseAuth.IsAuthenticated())
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Algo deu errado, por favor, tente novamente mais tarde", "Ok");
            }
            else
            {
                var problemas = await FirebaseDatabase.ListarProblema();

                Problemas.Clear();
                foreach (var p in problemas)
                {
                    Problemas.Add(p);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
