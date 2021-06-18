using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using SecretMessenger.DependenceService;
using SecretMessenger.Models;
using SecretMessenger.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SecretMessenger.Services
{
    public class Authenticator
    {
        // Az OnLoginra feliratkozva értesítést kap a bejelentkezés állásáról
        // Ha sikeres akkor tovább navigál, ha nem, akkor Toast("Login Failed")
        private async void FacebookDataDelegate(object sender, FBEventArgs<string> e)
        {
            if (e == null) return;

            if (e.Status == FacebookActionStatus.Completed)
            {
                var facebookProfile =
                    await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                if (facebookProfile == null) return;
                var socialLoginData = new NetworkAuthData(facebookProfile);
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(socialLoginData));
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() => { DependencyService.Get<IToast>().Show("Login Failed"); });
            }

            _facebookService.OnUserData -= FacebookDataDelegate;
        }

        private async void GoogleDataDelegate(object sender, GoogleClientResultEventArgs<GoogleUser> e)
        {
            try
            {
                if (e == null || e.Status != GoogleActionStatus.Completed) throw new Exception();

                var googleUser = e.Data;
                var socialLoginData = new NetworkAuthData(googleUser);
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(socialLoginData));
            }
            catch (Exception)
            {
                MainThread.BeginInvokeOnMainThread(() => { DependencyService.Get<IToast>().Show("Login Failed"); });
            }

            _googleService.OnLogin -= GoogleDataDelegate;
        }

        private readonly IFacebookClient _facebookService = CrossFacebookClient.Current;
        private readonly IGoogleClientManager _googleService = CrossGoogleClient.Current;

        private readonly string[] _fbRequestFields =
            {"email", "first_name", "picture.type(large)", "gender", "last_name"};

        private readonly string[] _fbPermissions = {"email"};


        // Tartalmazza a bejelentkezési lehetőséget listáját
        public enum SocialSites
        {
            Facebook,
            Google
        }

        // Kijelentkezik
        public void Logout()
        {
            if (_facebookService.IsLoggedIn) _facebookService.Logout();
            if (_googleService.IsLoggedIn) _googleService.Logout();
        }

        // Bejelentkezési lehetőség alapján bejelentkezik, majd tovább navigál a HomePage-re a felhasználói adatokkal.
        // Ha hibás: Toast("Login Failed")
        public async Task Login(SocialSites site)
        {
            switch (site)
            {
                case SocialSites.Facebook:
                    try
                    {
                        if (_facebookService.IsLoggedIn)
                        {
                            _facebookService.Logout();
                        }

                        _facebookService.OnUserData += FacebookDataDelegate;
                        await _facebookService.RequestUserDataAsync(_fbRequestFields, _fbPermissions);
                    }
                    catch (Exception)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            DependencyService.Get<IToast>().Show("Login Failed");
                        });
                    }

                    break;
                case SocialSites.Google:
                    if (_googleService.IsLoggedIn)
                    {
                        _googleService.Logout();
                    }

                    _googleService.OnLogin += GoogleDataDelegate;
                    try
                    {
                        await _googleService.LoginAsync();
                    }
                    catch (Exception)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            DependencyService.Get<IToast>().Show("Login Failed");
                        });
                    }

                    break;
            }
        }
    }
}