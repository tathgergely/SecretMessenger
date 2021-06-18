using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SecretMessenger.DependenceService;
using SecretMessenger.Models;
using SecretMessenger.Services;
using SecretMessenger.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SecretMessenger.ViewModels
{
    public class LoginViewModel : PropertyHandler
    {
        private readonly Authenticator _authenticator = new Authenticator();

        private async void Login(Authenticator.SocialSites site)
        {
            await _authenticator.Login(site);
            
        }
        
        public void OnFacebookLogin_Clicked() =>  Login(Authenticator.SocialSites.Facebook);
        public void OnGoogleLogin_Clicked() =>  Login(Authenticator.SocialSites.Google);
    }
}