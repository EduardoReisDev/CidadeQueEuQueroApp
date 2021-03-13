using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace CidadeQueEuQuero.Renderer
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public event EventHandler<TapEventArgs> Tap;
        public event EventHandler<SelectedPinEventArgs> OnSelectPin;

        public CustomMap()
        {

        }

        public CustomMap(MapSpan region) : base(region)
        {

        }

        public void OnTap(Position coordinate)
        {
            OnTap(new TapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(TapEventArgs e)
        {
            Tap?.Invoke(this, e);
        }

        public void SelectPin(CustomPin pin)
        {
            OnSelectPin?.Invoke(this, new SelectedPinEventArgs { SelectedPin = pin });
        }
    }

    public class TapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }

    public class SelectedPinEventArgs : EventArgs
    {
        public CustomPin SelectedPin { get; set; }
    }
}
