using System;
using CidadeQueEuQuero.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CidadeQueEuQuero.View.ViewProblema
{
    public partial class ProblemaCalcada2 : ContentPage
    {
        ProblemaCalcadaVM problemaCalcadaVM;

        public ProblemaCalcada2()
        {
            InitializeComponent();
            problemaCalcadaVM = new ProblemaCalcadaVM();
            BindingContext = problemaCalcadaVM;
            GeoLoc();
        }

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
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(fPosition, Distance.FromMiles(0.5)));

                    Pin pin = new Pin
                    {
                        Label = "Calçada com problemas",
                        Type = PinType.Place,
                        Position = new Position(lblLat, lblLong)
                    };
                    map.Pins.Add(pin);
                }
                else
                {
                    await DisplayAlert("Atenção!", "Não conseguimos encontrar sua localização. Ative a localização do seu dispositivo e abra o aplicativo novamente.", "Ok");
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
    }
}
