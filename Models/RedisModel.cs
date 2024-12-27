namespace Websocket_UI.Models
{
    public class RedisModel
    {
        public string username { get; set; }
        public string host { get; set; }
        public string password { get; set; }
        public string port { get; set; }
        public bool tls { get; set; }
    }
    public class ChatModel{
        public string id { get; set; }
        public string message { get; set; }
        public string time { get; set; }
    }
    public class ResponseModel
    {
        public int status { get; set; }
        public bool success { get; set; }
        public object data { get; set; }
        public string? message { get; set; }
    }
}
