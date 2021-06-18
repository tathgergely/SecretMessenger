using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretMessenger.DependenceService;
using SecretMessenger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretMessenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    { 
        private readonly LoginViewModel _viewModel;
        public LoginPage()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            
        }

        private void OnGoogleLogin_Clicked(object sender, EventArgs e)
        {
            _viewModel.OnGoogleLogin_Clicked();
        }

        private void OnFacebookLogin_Clicked(object sender, EventArgs e)
        {
            _viewModel.OnFacebookLogin_Clicked();
        }
    }
}