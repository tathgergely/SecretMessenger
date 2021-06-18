using SecretMessenger.Views.Cells;
using Xamarin.Forms;

namespace SecretMessenger.Models
{
    //Üzenet irányától függően bejövő vagy kimenő üzenet layout-ot ad neki
    public class MessageDirectionTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _incomingDataTemplate;
        private readonly DataTemplate _outgoingDataTemplate;
        
        public MessageDirectionTemplateSelector()
        {
            _incomingDataTemplate = new DataTemplate(typeof(IncomingMessageCell));
            _outgoingDataTemplate = new DataTemplate(typeof(OutgoingMessageCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is Message messageVm))
                return null;
            return messageVm.IsIncoming ? _incomingDataTemplate : _outgoingDataTemplate;
        }
        
    }
}
