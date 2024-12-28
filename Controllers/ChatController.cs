using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Websocket_UI.Models;
using Websocket_UI.Services;

namespace Websocket_UI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly RedisService _redisService;

        public ChatController(RedisService redisService)
        {
            _redisService = redisService;
        }
        [HttpGet("GetChat")]
        public ActionResult GetChat()
        {
            var response = new ResponseModel();
            try
            {
                string res = _redisService.Get("ChatMessage");
                if (res == null)
                {
                    throw new Exception("Data Not Found.");
                }
                else
                {
                    response.status = 200;
                    response.success = true;
                    response.data = JsonConvert.DeserializeObject<List<ChatModel>>(res);
                    response.message = "Success.";
                }
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.success = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost("SetChat")]
        public ActionResult SetChat([FromBody] ChatModel data)
        {
            var response = new ResponseModel();
            try
            {
                string chat = _redisService.Get("ChatMessage");
                var listChat = new List<ChatModel>();
                if (chat != null)
                {
                    listChat = JsonConvert.DeserializeObject<List<ChatModel>>(chat);
                }
                listChat.Add(data);
                string json = JsonConvert.SerializeObject(listChat);
                _redisService.Set("ChatMessage", json);
                response.success = true;
                response.status = 200;
                response.message = "Success.";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.status = 500;
                response.message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost("DelChat")]
        public ActionResult DelChat()
        {
            var response = new ResponseModel();
            try
            {
                _redisService.Delete("ChatMessage");
                response.status = 200;
                response.success = true;
                response.message = "Success.";
            }
            catch (Exception ex)
            {
                response.status = 500;
                response.success = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
