using System;
using System.Threading.Tasks;
using CidadeQueEuQuero.Database.Auth;
using CidadeQueEuQuero.Droid.Dependencias;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthAndroid))]
namespace CidadeQueEuQuero.Droid.Dependencias
{
    public class FirebaseAuthAndroid : IAuth
    {
        public FirebaseAuthAndroid(){
        
        }

        public async Task<bool> AutenticacaoUsuario(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                return true;

            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro desconhecido, tente novamente", ex);
            }
        }

        public async Task<bool> CadastroUsuario(string nome, string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(nome);
                var build = profileUpdates.Build();
                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);

                return true;

            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("Erro desconhecido, tente novamente", ex);
            }
        }

        public string GetCurrentUserId()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;
        }
    }
}