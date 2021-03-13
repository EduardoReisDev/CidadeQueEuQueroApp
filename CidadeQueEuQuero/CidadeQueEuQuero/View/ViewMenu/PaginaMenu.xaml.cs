using System;
using CidadeQueEuQuero.Database.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CidadeQueEuQuero.View.ViewMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaMenu : ContentPage
    {
        public PaginaMenu()
        {
            InitializeComponent();
        }

        public async void GoHome(object sender, EventArgs args)
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public void GoAvalie(object sender, EventArgs args)
        {
            Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/"));
        }

        public async void GoPosts(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new MinhasPostagens());
        }

        [Obsolete]
        public void GoFeedback(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("mailto:eduardoreisdev@gmail.com?subject=CidadeQueEuQuero_Feedback"));
        }

        public async void GoSair(object sender, EventArgs args)
        {
            string action = await DisplayActionSheet("Sair do App?", "Cancelar", null, "Sim, quero sair.", "Não");
            if (action == "Sim, quero sair.")
            {
                System.Environment.Exit(0);
            }
        }
    }
}