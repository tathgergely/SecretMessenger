using System.Runtime.CompilerServices;
using System.Windows.Input;
using SecretMessenger.Models;
using SecretMessenger.Services;
using Xamarin.Forms;

namespace SecretMessenger.ViewModels
{
    public class CreateCodeViewModel : PropertyHandler
    {
        public ICommand GenerateCodeCommand {get;}
        private readonly DbHandler _dbHandler = DbHandler.Instance();
        private bool _isCodeAvailable;
        private string _code;
        private NetworkAuthData _user;
        
        public NetworkAuthData User
        {
            set => SetProperty(ref _user, value);
            get => _user;
        }
        public string Code
        {
            set => SetProperty(ref _code, value);
            get => _code;
        }
        public bool IsCodeAvailable
        {
            set => SetProperty(ref _isCodeAvailable, value);
            get => _isCodeAvailable;
        }
        
        
        public CreateCodeViewModel(NetworkAuthData user)
        {
            IsCodeAvailable = false;
            User = user;
            GenerateCodeCommand = new Command(GenerateCode);
        }

        

        private void GenerateCode()
        {
            Code = _dbHandler.GetAvailableCode();
            IsCodeAvailable = true;
        }
    }
}