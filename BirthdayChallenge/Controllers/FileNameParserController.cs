using FileNameParser;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace BirthdayChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileNameParserController : ControllerBase
    {
        [HttpGet("FileNameParserHandler")]
        public async Task<IActionResult> FileNameParserHandler(string fileName)
        {
            try
            {
                string parsedName = await FileNameParserService.ParseFileNameAsync(fileName);
                return Ok(new { FileName = parsedName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
