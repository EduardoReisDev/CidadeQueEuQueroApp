using System.Collections.Generic;
using System.Threading.Tasks;
using CidadeQueEuQuero.Model;
using Xamarin.Forms;

namespace CidadeQueEuQuero.Database.Auth
{
    public interface IFirestore
    {
        bool CadastrarProblema(Problema problema);
        Task<bool> ExcluirProblema(Problema problema);
        Task<bool> AtualizarProblema(Problema problema);
        Task<IList<Problema>> ListarProblema();
        Task<IList<Problema>> ListarTodosProblemas();
    }

    public class FirebaseDatabase 
    {
        private static IFirestore firestore = DependencyService.Get<IFirestore>();

        public static Task<bool> AtualizarProblema(Problema problema)
        {
            return firestore.AtualizarProblema(problema);
        }

        public static bool CadastrarProblema(Problema problema)
        {
            return firestore.CadastrarProblema(problema);
        }

        public static Task<bool> ExcluirProblema(Problema problema)
        {
            return firestore.ExcluirProblema(problema);
        }

        public static Task<IList<Problema>> ListarProblema()
        {
            return firestore.ListarProblema();
        }

        public static Task<IList<Problema>> ListarTodosProblemas()
        {
            return firestore.ListarTodosProblemas();
        }
    }
}
