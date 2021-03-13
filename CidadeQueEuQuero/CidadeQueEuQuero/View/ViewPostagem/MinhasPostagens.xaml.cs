using CidadeQueEuQuero.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CidadeQueEuQuero.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinhasPostagens : ContentPage
    {
        MinhasPostagensVM minhasPostagensVM;

        public MinhasPostagens()
        {
            InitializeComponent();
            minhasPostagensVM = new MinhasPostagensVM();
            BindingContext = minhasPostagensVM;
        }
    }
}