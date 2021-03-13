using CidadeQueEuQuero.Model;
using CidadeQueEuQuero.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CidadeQueEuQuero.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Postagem : ContentPage
    {

        PostagemVM postagemVM;

        public Postagem()
        {
            InitializeComponent();
            postagemVM = new PostagemVM();
            BindingContext = postagemVM;
        }

        public Postagem(Problema problemaSelecionado)
        {
            InitializeComponent();
            postagemVM = new PostagemVM();
            BindingContext = postagemVM;
            postagemVM.Problema = problemaSelecionado;
        }
    }
}