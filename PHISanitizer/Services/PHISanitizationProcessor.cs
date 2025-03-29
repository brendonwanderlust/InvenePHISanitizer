

using System.IO.Compression;
using System.Text;

namespace PHISanitizer.Services
{
    public class PHISanitizationProcessor : IPHISanitizationProcessor
    { 
        private readonly IPHISanitizer sanitizer;

        public PHISanitizationProcessor(IPHISanitizer sanitizer)
        {
            this.sanitizer = sanitizer;
        } 

        public async Task<MemoryStream> Process(IFormFileCollection files)
        {
            var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    string sanitizedFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_sanitized.txt";

                    var zipEntry = archive.CreateEntry(sanitizedFileName, CompressionLevel.Optimal);
                    using var entryStream = zipEntry.Open();
                    using var writer = new StreamWriter(entryStream, Encoding.UTF8);

                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using var reader = new StreamReader(stream);
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();
                        if (line != null)
                        {
                            string sanitizedLine = sanitizer.SanitizeLine(line);
                            await writer.WriteLineAsync(sanitizedLine);
                        }
                    }
                }
            }

            zipStream.Position = 0;
            return zipStream;
        }
    }
}