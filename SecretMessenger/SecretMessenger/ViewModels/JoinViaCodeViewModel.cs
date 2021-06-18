using System.Threading.Tasks;
using SecretMessenger.Models;
using SecretMessenger.Services;
using SecretMessenger.Views;

namespace SecretMessenger.ViewModels
{
    public class JoinViaCodeViewModel : PropertyHandler
    {
        private NetworkAuthData _user;
        private string _code;
        private readonly DbHandler _dbHandler = DbHandler.Instance();
        public NetworkAuthData User
        {
            set => SetProperty(ref _user, value);
            get => _user;
        }
        public JoinViaCodeViewModel(NetworkAuthData user)
        {
            User = user;
        }
        public string Code
        {
            set => SetProperty(ref _code, value);
            get => _code;
        }

        public Task<bool> DoesCodeExist()
        {
            return _dbHandler.DoesCodeExist(Code);
        }
    }
}