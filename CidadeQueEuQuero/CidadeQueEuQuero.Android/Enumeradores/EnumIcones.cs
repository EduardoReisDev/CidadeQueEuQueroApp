using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CidadeQueEuQuero.Droid
{
    public class EnumIcones
    {
        public enum Icones
        {
            ProblemaPavimentacao = 1,
            ProblemaCalcada,
            ProblemaDeficiente,
            ProblemaBueiro,
            ProblemaLixo,
            ProblemaMosquito,
            ProblemaPoste,
            ProblemaEsgoto,
            ProblemaOutros
        };

        public int IconeSelecionado(int icone)
        {
            switch ((Icones)icone)
            {
                case Icones.ProblemaPavimentacao:
                    return Resource.Drawable.asfalto_pin;
                case Icones.ProblemaCalcada:
                    return Resource.Drawable.calcada_pin;
                case Icones.ProblemaDeficiente:
                    return Resource.Drawable.deficiente_pin;
                case Icones.ProblemaBueiro:
                    return Resource.Drawable.waste_pin;
                case Icones.ProblemaLixo:
                    return Resource.Drawable.lixo_pin;
                case Icones.ProblemaMosquito:
                    return Resource.Drawable.mosquito_pin;
                case Icones.ProblemaPoste:
                    return Resource.Drawable.poste_pin;
                case Icones.ProblemaEsgoto:
                    return Resource.Drawable.esgoto_pin;
                case Icones.ProblemaOutros:
                    return Resource.Drawable.outros_pin;
            }

            return Resource.Drawable.outros_pin;
        }
    }
}
