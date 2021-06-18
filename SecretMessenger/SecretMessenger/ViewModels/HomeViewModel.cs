using SecretMessenger.Models;
using SecretMessenger.Services;
using SecretMessenger.Views;
using Xamarin.Forms;

namespace SecretMessenger.ViewModels
{
    public class HomeViewModel : PropertyHandler
    {
        private readonly Authenticator _authenticator = new Authenticator();
        public HomeViewModel(NetworkAuthData user)
        {
            User = user;
        }

        public void Logout()
        {
            _authenticator.Logout();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
            
        }

        public NetworkAuthData User { get; }
        
    }
}