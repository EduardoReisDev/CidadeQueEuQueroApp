using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CidadeQueEuQuero.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        void CadastroLabel_Tapped(object sender, EventArgs args)
        {
            cadastroStackLayout.IsVisible = true;
            loginStackLayout.IsVisible = false;
        }

        void LoginLabel_Tapped(object sender, EventArgs args)
        {
            cadastroStackLayout.IsVisible = false;
            loginStackLayout.IsVisible = true;
        }
    }
}