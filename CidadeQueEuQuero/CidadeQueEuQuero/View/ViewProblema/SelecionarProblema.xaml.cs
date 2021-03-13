using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CidadeQueEuQuero.View.ViewProblema;

namespace CidadeQueEuQuero.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelecionarProblema : ContentPage
    {
        public SelecionarProblema()
        {
            InitializeComponent();
        }

        public async void PosteLuz(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaPoste());
        }

        public async void AcessoDeficiente(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaDeficiente());
        }

        public async void CalcadaProblema2(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaCalcada2());
        }

        public async void RuaSemPavimentacao(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaPavimentacao());
        }

        public async void LixoSemColeta(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaLixo());
        }

        public async void BueiroEntupido(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaBueiro());
        }

        public async void EsgotoCeuAberto(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaEsgoto());
        }

        public async void FocoDengue(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaDengue());
        }

        public async void Outros(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProblemaOutros());
        }
    }
}