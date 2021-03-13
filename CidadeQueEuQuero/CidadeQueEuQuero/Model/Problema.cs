using System;

namespace CidadeQueEuQuero.Model
{
    public class Problema
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double LatitudeProblema { get; set; }
        public double LongitudeProblema { get; set; }
        public string TipoProblema { get; set; }
        public string ObservacaoProblema { get; set; }
        public string FotoProblema { get; set; }
        public DateTime HorarioProblema { get; set; }
        public int TipoProblemaInt
        {
            get
            {
                int retorno = 1;

                if (!string.IsNullOrEmpty(TipoProblema))
                {
                    switch (TipoProblema)
                    {
                        case "ProblemaAsfalto":
                            retorno = 1;
                            break;
                        case "ProblemaCalcada":
                            retorno = 2;
                            break;
                        case "ProblemaDeficiente":
                            retorno = 3;
                            break;
                        case "ProblemaEsgoto":
                            retorno = 4;
                            break;
                        case "ProblemaLixo":
                            retorno = 5;
                            break;
                        case "ProblemaDengue":
                            retorno = 6;
                            break;
                        case "ProblemaPoste":
                            retorno = 7;
                            break;
                        case "ProblemaWaste":
                            retorno = 8;
                            break;
                        case "ProblemaOutros":
                            retorno = 9;
                            break;
                    };
                }

                return retorno;
            }            
        }
    }
}
