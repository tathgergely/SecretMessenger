using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretMessenger.Models;
using SecretMessenger.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretMessenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        private readonly MessageViewModel _viewModel;
        public MessagePage(NetworkAuthData user, string code)
        {
            InitializeComponent();
            _viewModel = new MessageViewModel(user, code);
            BindingContext = _viewModel;
        }

        private void  CopyCode_Clicked(object sender, EventArgs e)
        {
            _viewModel.CopyCodeToClipBoard();
           
        }
    }
}