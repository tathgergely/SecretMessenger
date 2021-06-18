namespace SecretMessenger.DependenceService
{
    //Platform függő Toast megvalósítás
    public interface IToast
    {
        void Show(string message);
    }
}