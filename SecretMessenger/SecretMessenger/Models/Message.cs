using System;
using Plugin.CloudFirestore.Attributes;

namespace SecretMessenger.Models
{
    // ÜZenet: Küldő, Szöveg, Idő, Irány
    // Az irány az üzenet letöltésekor számítódik ki
    public class Message : PropertyHandler
    {
        [MapTo("text")]
        public string Text { get; set; }
        
        [Ignored]
        public bool IsIncoming { get; set; }
        
        
        [MapTo("messageDateTime")]
        public DateTime MessageDateTime { get; set; }
        
        
        [MapTo("sender")]
        public NetworkAuthData Sender { get; set; }
        
        public Message(string text, NetworkAuthData sender)
        {
            Text = text;
            Sender = sender;
            MessageDateTime = DateTime.Now;

        }

        public Message()
        {
        }

    }
}