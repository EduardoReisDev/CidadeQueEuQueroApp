using System;
using System.Threading.Tasks;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.iOS.Dependencias;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthIOS))]
namespace CidadeQueEuQuero.iOS.Dependencias
{
    public class FirebaseAuthIOS : IAuth
    {
        public FirebaseAuthIOS()
        {

        }

        public async Task<bool> AutenticacaoUsuario(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return true;
            }
            catch (NSErrorException ex)
            {
                string message = ex.Message.Substring(ex.Message.IndexOf("NSLocalizedDescription=", StringComparison.CurrentCulture));
                message = message.Replace("NSLocalizedDescription=", "").Split('.')[0];
                message += ".";
                throw new Exception(message);
            }
            catch (Exception)
            {
                throw new Exception("Erro desconhecido, tente novamente.");
            }
        }

        public async Task<bool> CadastroUsuario(string nome, string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                var changeRequest = Firebase.Auth.Auth.DefaultInstance.CurrentUser.ProfileChangeRequest();
                changeRequest.DisplayName = nome;
                await changeRequest.CommitChangesAsync();
                return true;
            }
            catch (NSErrorException ex)
            {
                string message = ex.Message.Substring(ex.Message.IndexOf("NSLocalizedDescription=", StringComparison.CurrentCulture));
                message = message.Replace("NSLocalizedDescription=", "").Split('.')[0];
                message += ".";
                throw new Exception(message);
            }
            catch (Exception)
            {
                throw new Exception("Erro desconhecido, tente novamente.");
            }
        }

        public string GetCurrentUserId()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;
        }
    }
}