namespace DK_API.Controllers.Temp
{
    public class IpInfoEntity
    {
        // IpInfoEntity myDeserializedClass = JsonConvert.DeserializeObject<IpInfoEntity>(myJsonResponse); 
        public string ip { get; set; }
        public string hostname { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
    }
}