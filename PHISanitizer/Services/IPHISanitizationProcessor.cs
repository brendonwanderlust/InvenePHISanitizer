using Microsoft.AspNetCore.Mvc;

namespace PHISanitizer.Services
{
    public interface IPHISanitizationProcessor
    {
        public Task<MemoryStream> Process(IFormFileCollection files);
    }
}
