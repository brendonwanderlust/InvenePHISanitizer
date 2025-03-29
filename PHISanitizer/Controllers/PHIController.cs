using Microsoft.AspNetCore.Mvc;
using PHISanitizer.Services;

namespace PHISanitizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PHIController : ControllerBase
    {
        private readonly IPHISanitizationHandler sanitizationHandler;

        public PHIController(IPHISanitizationHandler sanitizationHandler)
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
                return File(await sanitizationHandler.Handle(files), "application/zip", "sanitized_files.zip");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
