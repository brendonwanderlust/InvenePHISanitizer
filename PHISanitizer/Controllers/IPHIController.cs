using Microsoft.AspNetCore.Mvc;

namespace PHISanitizer.Controllers
{
    public interface IPHIController
    {
        public Task<IActionResult> Sanitize(IFormFileCollection files);
    }
}
