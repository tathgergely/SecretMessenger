using Newtonsoft.Json;

namespace SecretMessenger.Models
{
    public class Data
    {

        public string Url { get; set; }
    }

    public class Picture
    {
        
        public Data Data  { get; set; }
    }
}