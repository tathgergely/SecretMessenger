using Android.App;
using Android.Widget;
using SecretMessenger.Android.DependenceService;
using SecretMessenger.DependenceService;
 

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace SecretMessenger.Android.DependenceService
{
    public class ToastService : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context,message, ToastLength.Long)?.Show();
        }
    }
}