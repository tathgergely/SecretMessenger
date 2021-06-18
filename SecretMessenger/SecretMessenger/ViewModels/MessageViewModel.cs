using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Plugin.CloudFirestore;
using SecretMessenger.DependenceService;
using SecretMessenger.Models;
using SecretMessenger.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SecretMessenger.ViewModels
{
    public class MessageViewModel : PropertyHandler
    {
        private ObservableCollection<Message> _messages;
         public ObservableCollection<Message> Messages {  
             set => SetProperty(ref _messages, value);
             get => _messages; }
         
         private string _outgoingText = string.Empty;
         private string _code;
         private readonly DbHandler _dbHandler = DbHandler.Instance();
         private NetworkAuthData _user;
         
         public ICommand SendCommand {get;}
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

         public async void CopyCodeToClipBoard()
         {
             await Clipboard.SetTextAsync(Code);
             DependencyService.Get<IToast>().Show("Code Copied");
         }
         public MessageViewModel(NetworkAuthData user, string code)
         {
             User = user;
             Code = code;
             SendCommand = new Command(SendMessage);
             LoadMessages();
             _dbHandler.ListenForChanges(code, (snapshot, error) => { LoadMessages(); });
         }
         
         private async void LoadMessages()
         {
             Messages =  await _dbHandler.GetMessages(User, Code);
         }
         private void SendMessage()
         {
             if (OutGoingText == null) return;
             _dbHandler.SendMessage(new Message(OutGoingText, User), Code);
             OutGoingText = String.Empty;
         }

         public string OutGoingText
         {
             get => _outgoingText;
             set => SetProperty(ref _outgoingText, value);
         }
    }
}