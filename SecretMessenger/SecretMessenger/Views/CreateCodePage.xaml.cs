
using System;
using SecretMessenger.Models;
using SecretMessenger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretMessenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCodePage : ContentPage
    {
        private readonly CreateCodeViewModel _viewModel;
        public CreateCodePage(NetworkAuthData user)
        {
            InitializeComponent();
            _viewModel = new CreateCodeViewModel(user);
            BindingContext = _viewModel;
        }

        private async void Join_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync (new MessagePage(_viewModel.User, _viewModel.Code));
        }
    }
}