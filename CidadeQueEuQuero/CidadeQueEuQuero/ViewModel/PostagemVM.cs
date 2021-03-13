using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.DependencyServices;
using CidadeQueEuQuero.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CidadeQueEuQuero.ViewModel
{
    public class PostagemVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Problema problema;
        public Problema Problema
        {
            get
            {
                return problema;
            }
            set
            {
                problema = value;
                ObservacaoProblema = problema.ObservacaoProblema;
                OnPropertyChanged("Problema");
            }
        }

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
                Problema.ObservacaoProblema = observacaoProblema;
                OnPropertyChanged("ObservacaoProblema");
                OnPropertyChanged("Problema");
            }
        }

        public ICommand AtualizarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand Compartilhar { get; set; }

        public PostagemVM()
        {
            AtualizarCommand = new Command(Atualizar, AtualizarCanExecute);
            ExcluirCommand = new Command(Excluir);
            Compartilhar = new Command(CompartilharProblemaVoid);
        }

        private async void CompartilharProblemaVoid(object obj)
        {
            await CompartilharProblema();
        }

        private async Task CompartilharProblema()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"Oi, encontrei um problema na nossa cidade. Problema: {problema.ObservacaoProblema}. Baixe o CidadeQueEuQuero e venha acompanhar!",
            });
        }

        private bool AtualizarCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ObservacaoProblema);
        }

        private async void Atualizar(Object parameter)
        {
            bool resultado = await FirebaseDatabase.AtualizarProblema(Problema);
            if (resultado)
            {
                await App.Current.MainPage.Navigation.PopAsync();
                DependencyService.Get<IToastMessage>().LongAlert("Problema atualizado com sucesso!");
            }
            else
            {
                await App.Current.MainPage.Navigation.PopAsync();
                DependencyService.Get<IToastMessage>().LongAlert("Houve um problema em atualizar o problema, tente novamente mais tarde.");
            }
        }


        private async void Excluir(Object parameter)
        {
            bool resultado = await FirebaseDatabase.ExcluirProblema(Problema);
            if (resultado)
            {
                await App.Current.MainPage.Navigation.PopAsync();
                DependencyService.Get<IToastMessage>().LongAlert("Problema excluido com sucesso!");
            }
            else
            {
                await App.Current.MainPage.Navigation.PopAsync();
                DependencyService.Get<IToastMessage>().LongAlert("Houve um problema em excluir o problema, tente novamente mais tarde.");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
