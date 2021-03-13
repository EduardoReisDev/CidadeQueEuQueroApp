using Xamarin.Forms;
using CidadeQueEuQuero.Renderer;
using XamarinBorderlessEntry.Droid.ControlHelpers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

#pragma warning disable CS0612 // O tipo ou membro é obsoleto
[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
#pragma warning restore CS0612 // O tipo ou membro é obsoleto
namespace XamarinBorderlessEntry.Droid.ControlHelpers
{
    
    [System.Obsolete]
    public class XEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}