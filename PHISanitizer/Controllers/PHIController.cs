using Microsoft.AspNetCore.Mvc;
using PHISanitizer.Services;

namespace PHISanitizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PHIController : ControllerBase
    {
        private readonly IPHISanitizationProcessor sanitizationHandler;

        public PHIController(IPHISanitizationProcessor sanitizationHandler)
        {
            this.sanitizationHandler = sanitizationHandler;
        }

        [HttpPost(nameof(Sanitize))]
        public async Task<IActionResult> Sanitize(IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files received.");
            }

            try
            {
                return File(await sanitizationHandler.Process(files), "application/zip", "sanitized_files.zip");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
