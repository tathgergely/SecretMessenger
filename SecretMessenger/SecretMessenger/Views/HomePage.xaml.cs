using System;
using SecretMessenger.Models;
using SecretMessenger.ViewModels;
using Xamarin.Forms.Xaml;

namespace SecretMessenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        private readonly HomeViewModel _viewModel;
        
        public HomePage(NetworkAuthData socialLoginData)
        {
            InitializeComponent();
            _viewModel = new HomeViewModel(socialLoginData);
            BindingContext = _viewModel;
            
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            _viewModel.Logout(); 
        }

        private async void Create_Lock_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new CreateCodePage(_viewModel.User));
        }

        private async void Join_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new JoinViaCodePage(_viewModel.User));
        }
    }
}