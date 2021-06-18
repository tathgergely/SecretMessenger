using Newtonsoft.Json;

namespace SecretMessenger.Models
{
    //Facebook JSON válasz osztálya. Csupán kényelmi szempontból fontos
    public class FacebookProfile
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public Picture Picture { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
    }
    
}