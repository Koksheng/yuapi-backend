using Microsoft.AspNetCore.Mvc;
using yuapi_client_sdk.Model;
using yuapi_client_sdk.Utils;

namespace yuapi_interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetNameByGet([FromQuery] string name)
        {
            //var headerValue = Request.Headers["yupi"];
            //Console.WriteLine(headerValue);
            return Ok($"GET 你的名字是 ur name is {name}");
        }

        [HttpPost]
        public ActionResult<string> GetNameByPost([FromQuery] string name)
        {
            return Ok($"POST 你的名字是 ur name is {name}");
        }

        [HttpPost("user")]
        public ActionResult<string> GetUsernameByPost([FromBody] User user)
        {
            //// Uncomment and complete the following lines as per your requirement:
            
            //var accessKey = Request.Headers["accessKey"].ToString();
            ////var secretKey = Request.Headers["secretKey"].ToString();
            //var nonce = Request.Headers["nonce"].ToString();
            //var timestamp = Request.Headers["timestamp"].ToString();
            //var sign = Request.Headers["sign"].ToString();
            //var body = Request.Headers["body"].ToString();

            //// todo 实际情况应该是去数据库中查是否已分配给用户
            //if (accessKey != "yupi")
            //{
            //    throw new Exception("无权限");
            //}

            //if (long.Parse(nonce) > 10000)
            //{
            //    throw new Exception("无权限");
            //}

            //// Add your timestamp check logic here

            //// Generate server sign and compare
            //var serverSign = SignUtils.GenSign(body, "abcdefgh");
            //if (sign != serverSign)
            //{
            //    throw new Exception("无权限");
            //}

            // Increment invoke count


            string result = $"POST 用户名字是 username is {user.username}";
            //
            return Ok(result);
        }
    }
}
