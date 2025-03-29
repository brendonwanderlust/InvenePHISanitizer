using Microsoft.AspNetCore.Mvc;

namespace PHISanitizer.Services
{
    public interface IPHISanitizationHandler
    {
        public Task<MemoryStream> Handle(IFormFileCollection files);
    }
}
