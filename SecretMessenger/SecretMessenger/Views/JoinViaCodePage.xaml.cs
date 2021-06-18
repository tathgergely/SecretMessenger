using System;
using SecretMessenger.Models;
using SecretMessenger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretMessenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinViaCodePage : ContentPage
    {
        private readonly JoinViaCodeViewModel _viewModel;
        public JoinViaCodePage(NetworkAuthData user)
        {
            InitializeComponent();
            _viewModel = new JoinViaCodeViewModel(user);
            BindingContext = _viewModel;

        }

        private async void Join_Clicked(object sender, EventArgs e)
        {
            var doesCodeExist = await _viewModel.DoesCodeExist();
            if (doesCodeExist)
            {
                await Navigation.PushAsync(new MessagePage(_viewModel.User, _viewModel.Code) );
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Code", "Code does not exist", "Ok");
            }
        }
    }
}