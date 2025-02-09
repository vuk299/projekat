using Microsoft.AspNetCore.Mvc;

namespace projekat_API.Controllers
{
    [Route("api/palindrome")]
    [ApiController]
    public class PalindromeController : ControllerBase
    {
        [HttpPost]
        public IActionResult IsPalindrome([FromBody] int num)
        {
            if (num < 0)
            {
                return Ok(false);
            }

            string numStr = num.ToString();

            for (int i = 0; i < numStr.Length / 2; i++)
            {
                if (numStr[i] != numStr[numStr.Length - 1 - i])
                {
                    return Ok(false);
                }
            }

            return Ok(true);
        }
    }
}
