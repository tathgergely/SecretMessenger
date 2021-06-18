using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Plugin.CloudFirestore;
using SecretMessenger.Models;

namespace SecretMessenger.Services
{
    //Singleton
    public class DbHandler
    {
        private static DbHandler _instance;
        private const string CodesCollection = "codes";
        private const string MessagesCollection = "messages";
        private readonly IFirestore _firestore;
        private DbHandler()
        {
            _firestore = CrossCloudFirestore.Current.Instance;
        }
        internal static DbHandler Instance()
        {
            return _instance ?? (_instance = new DbHandler());
        }
        
        //Üzenet feltöltése a Firestoreba
        public void SendMessage(Message message, string code)
        {
            _firestore.Collection(CodesCollection).Document(code).Collection(MessagesCollection).AddAsync(message);
        }
        
        //Adott csoport összes üzenetének letöltése, dátum szeirnti sorrendben
        public async Task<ObservableCollection<Message>> GetMessages(NetworkAuthData user, string code)
        {
            IQuerySnapshot query = await _firestore.Collection(CodesCollection).Document(code).Collection(MessagesCollection)
                .OrderBy("messageDateTime").GetAsync();
            var messages = query.ToObjects<Message>().ToList();
            foreach (var message in messages)
            {
                message.IsIncoming = message.Sender.Id != user.Id;
            }
            
            return new ObservableCollection<Message>(messages);
        }
        
        //Szabad kódot generál
        public string GetAvailableCode()
        {
            return _firestore.Collection(CodesCollection).Document().Id;
        }
        
        //Callback (MessageViewModel) számára egy értesítőt küld minden csoporton belüli változáskor
        public void ListenForChanges(string code, QuerySnapshotHandler callback)
        {
            _firestore.Collection(CodesCollection).Document(code).Collection(MessagesCollection).AddSnapshotListener(callback);
        }
        
        //Ha valahogy két kliens is ugyan azt a kódot generálta volna, akkor egy utolsó ellenőrzi pontot biztosít létrehozás előtt
        public async Task<bool> DoesCodeExist(string code)
        {
            try
            {
                var get = await _firestore.Collection(CodesCollection).Document(code).Collection(MessagesCollection).GetAsync();
                return !get.IsEmpty;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}