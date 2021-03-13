using CidadeQueEuQuero.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace CidadeQueEuQuero.Renderer
{
    public class CustomPin : Pin
    {
        public int PinTipo { get; set; }
        public Problema Problema { get; set; }
    }
}
