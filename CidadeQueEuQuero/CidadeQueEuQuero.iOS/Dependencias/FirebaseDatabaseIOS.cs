using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.iOS.Dependencias;
using CidadeQueEuQuero.Model;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseDatabaseIOS))]
namespace CidadeQueEuQuero.iOS.Dependencias
{
    public class FirebaseDatabaseIOS : IFirestore
    {
        public Task<bool> AtualizarProblema(Problema problema)
        {
            throw new NotImplementedException();
        }

        public bool CadastrarProblema(Problema problema)
        {
            try
            {
                var keys = new[]
                {
                     new NSString("autor"),
                     new NSString("tipoProblema"),
                     new NSString("observacaoProblema"),
                     new NSString("horarioProblema"),
                };

                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(problema.TipoProblema),
                    new NSString(problema.ObservacaoProblema),
                    DateTimeToNSDate(problema.HorarioProblema)
                };

                var problemaDocument = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("problemas").AddDocument(problemaDocument);
                return true;
            }

            catch(Exception ex)
            {
                new Exception("Erro desconhecido, tente novamente", ex);
                return false;
            }
        }

        public Task<bool> ExcluirProblema(Problema problema)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Problema>> ListarProblema()
        {
            throw new NotImplementedException();
        }

        private static NSDate DateTimeToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            }
            return (NSDate)date;
        }
    }
}