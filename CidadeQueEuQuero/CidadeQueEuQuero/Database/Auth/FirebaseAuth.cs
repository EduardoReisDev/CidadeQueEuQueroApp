using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CidadeQueEuQuero.Database.Auth
{
    public interface IAuth
    {
        Task<bool> CadastroUsuario(string nome, string email, string password);
        Task<bool> AutenticacaoUsuario(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
    }

    public class FirebaseAuth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();

        public static async Task<bool> CadastroUsuario(string nome, string email, string password)
        {
            try
            {
                return await auth.CadastroUsuario(nome, email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return false;
            }
        }

        public static async Task<bool> AutenticacaoUsuario(string email, string password)
        {
            try { 
                return await  auth.AutenticacaoUsuario(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
                return false;
            }
        }

        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
