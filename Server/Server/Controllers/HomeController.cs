using Server.Data;
using Server.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableRateLimiting("LimitConnections")]
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CheckPalindrome(RequestModel inputData)
        {
            bool isPalindrome = false;

            if (!string.IsNullOrEmpty(inputData.Text))
                isPalindrome = await inputData.Text.IsPalindrome();

            string jsonResult = JsonConvert.SerializeObject(new { IsPalindrome = isPalindrome });

            await Task.Delay(2000);

            return Ok(jsonResult);
        }
    }
}