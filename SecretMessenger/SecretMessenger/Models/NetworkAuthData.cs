using Plugin.GoogleClient.Shared;

namespace SecretMessenger.Models
{
    public class NetworkAuthData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public NetworkAuthData()
        {
        }

        public NetworkAuthData(GoogleUser googleUser)
        {
            this.Id = googleUser.Id;
            this.Name = googleUser.Name;
            this.Picture = googleUser.Picture.ToString();
        }
        public NetworkAuthData(FacebookProfile facebookProfile)
        {
            this.Id = facebookProfile.Id;
            this.Name = $"{facebookProfile.LastName} {facebookProfile.FirstName}";
            this.Picture = facebookProfile.Picture.Data.Url;
        }
    }
}