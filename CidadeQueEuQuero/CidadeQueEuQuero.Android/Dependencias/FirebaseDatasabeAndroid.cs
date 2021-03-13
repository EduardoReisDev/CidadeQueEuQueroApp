using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.Model;
using Java.Util;
using CidadeQueEuQuero.Droid.Dependencias;
using Xamarin.Forms;
using Android.Runtime;
using Android.Gms.Tasks;
using Firebase.Firestore;

[assembly: Dependency(typeof(FirebaseDatasabeAndroid))]
namespace CidadeQueEuQuero.Droid.Dependencias
{
    public class FirebaseDatasabeAndroid : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<Problema> problemas;
        bool listagemDeProblemas = false;

        public FirebaseDatasabeAndroid()
        {
            problemas = new List<Problema>();
        }

        public async Task<bool> AtualizarProblema(Problema problema)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("problema");
                collection.Document(problema.Id).Update("observacaoProblema", problema.ObservacaoProblema);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CadastrarProblema(Problema problema)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("problema");
                var problemaDocument = new JavaDictionary<string, Java.Lang.Object>
                {
                    {"autor", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid},
                    {"latitudeProblema", problema.LatitudeProblema},
                    {"longitudeProblema", problema.LongitudeProblema},
                    {"tipoProblema", problema.TipoProblema},
                    {"observacaoProblema", problema.ObservacaoProblema},
                    {"horarioProblema", DateTimeToNativeDate(problema.HorarioProblema)}
                };

                collection.Add(problemaDocument);

                return true;
            }

            catch(Java.Lang.Exception ex)
            {
                new Java.Lang.Exception("Erro desconhecido, tente novamente", ex);
                return false;
            }
        }

        public async Task<bool> ExcluirProblema(Problema problema)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("problema");
                collection.Document(problema.Id).Delete();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Problema>> ListarProblema()
        {
            try
            {
                listagemDeProblemas = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("problema");
                var query = collection.WhereEqualTo("autor", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 25; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (listagemDeProblemas)
                        break;
                }

                return problemas;
            }
            catch(Exception exc)
            {
                var teste = exc.Message;
                return new List<Problema>();
            }
        }

        public async Task<IList<Problema>> ListarTodosProblemas()
        {
            try
            {
                listagemDeProblemas = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("problema");
                collection.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 25; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (listagemDeProblemas)
                        break;
                }

                return problemas;
            }
            catch (Exception exc)
            {
                var teste = exc.Message;
                return new List<Problema>();
            }
        }

        private static Date DateTimeToNativeDate(DateTime date)
        {
            long dateTimeUtcAsMilliseconds = (long)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return new Date(dateTimeUtcAsMilliseconds);
        }

        private static DateTime NativeDateToDateTime(Date date)
        {
            DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));

            if(date == null)
            {
                date = new Date();
            }

            return reference.AddMilliseconds(date.Time);
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;

                problemas.Clear();

                foreach(var doc in documents.Documents)
                {
                    Problema problema = new Problema
                    {
                        UserId = doc.Get("autor").ToString(),
                        TipoProblema = doc.Get("tipoProblema").ToString(),
                        ObservacaoProblema = doc.Get("observacaoProblema").ToString(),
                        LatitudeProblema = Convert.ToDouble(doc.Get("latitudeProblema")),
                        LongitudeProblema = Convert.ToDouble(doc.Get("longitudeProblema")),
                        HorarioProblema = NativeDateToDateTime(doc.Get("horarioProblema") as Date),
                        Id = doc.Id,
                    };

                    problemas.Add(problema);
                }
            }
            else
            {
                problemas.Clear();
            }

            listagemDeProblemas = true;
        }
    }
}