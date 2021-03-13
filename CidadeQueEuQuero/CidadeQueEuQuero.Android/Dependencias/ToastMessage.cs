using Android.App;
using Android.Widget;
using CidadeQueEuQuero.DependencyServices;
using CidadeQueEuQuero.Droid.Dependencias;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace CidadeQueEuQuero.Droid.Dependencias
{
    public class ToastMessage : IToastMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }   
}