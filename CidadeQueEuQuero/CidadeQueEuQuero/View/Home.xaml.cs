using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using CidadeQueEuQuero.ViewModel;
using CidadeQueEuQuero.View.ViewMenu;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.Renderer;

namespace CidadeQueEuQuero.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        HomeViewModel homeViewModel;
        FirebaseDatabase database = new FirebaseDatabase();

        public async void GeoLoc()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    double lblLat;
                    double lblLong;
                    
                    lblLat = location.Latitude;
                    lblLong = location.Longitude;

                    var fPosition = new Position(location.Latitude, location.Longitude);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(fPosition, Distance.FromMiles(1)));
                }

                else
                {
                    await DisplayAlert("Atenção!","Não conseguimos encontrar sua localização. Ative a localização do seu dispositivo e abra o aplicativo novamente.","Ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Aplicativo não suportado!", fnsEx.ToString(), "Ok");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Você não permitiu a localização. Feche o aplicativo e abra novamente!", pEx.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops! Houve um problema! Feche o aplicativo e abra novamente.", ex.ToString(), "Ok");
            }
        }

        public Home()
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel();
            BindingContext = homeViewModel;
            GeoLoc();
        }

        protected override async void OnAppearing()
        {
            GeoLoc();
            base.OnAppearing();

            if (!FirebaseAuth.IsAuthenticated())
            {
                await Task.Delay(300);
                await Navigation.PushAsync(new Login());
            } else
            {
                PinsMap();
            }
        }

        public async void GoCadastroProblema(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SelecionarProblema());
        }

        public async void GoMenuMaster(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PaginaMenu());
        }

        public async void PinsMap()
        {
            map.Pins.Clear();
            var listaProblemas = await FirebaseDatabase.ListarTodosProblemas();

            foreach (var problema in listaProblemas)
            {
                var pin = new CustomPin
                {
                    Label = problema.ObservacaoProblema,
                    Type = PinType.Place,
                    Position = new Position(problema.LatitudeProblema, problema.LongitudeProblema),
                    PinTipo = problema.TipoProblemaInt
                };

                map.Pins.Add(pin);
            }
        }
    }
}
