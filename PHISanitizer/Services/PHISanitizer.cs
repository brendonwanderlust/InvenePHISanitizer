using System.Text.RegularExpressions;

namespace PHISanitizer.Services
{
    public class PHISanitizer : IPHISanitizer
    {

        private readonly string[] phiFieldStrings = new string[]
        {
            "Address",
            "DOB",
            "Date of Birth",
            "Birth Date",
            "Social",
            "Social Security Number",
            "SSN",
            "Patient Name",
            "Email address",
            "Medical Record Number",
        };

        public string SanitizeLine(string line)
        {
            // sanitize all defined phi field names
            var formattedLine = line.ToLowerInvariant().Replace(" ", "");
            if (phiFieldStrings.Any(phiField => formattedLine.Contains(phiField.ToLowerInvariant().Replace(" ", ""))))
            {
                var phiSeparationIndex = line.LastIndexOf(':');
                var phiToRedact = line.Substring(phiSeparationIndex + 1);
                var phiName = line.Substring(0, phiSeparationIndex);
                var redactedPhi = Regex.Replace(phiToRedact, @"[a-zA-Z0-9]", "X");
                return $"{phiName}: {redactedPhi}"; 
            }

            // sanitize SSN
            line = Regex.Replace(line, @"\b\d{3}-\d{2}-\d{4}\b", "XXX-XX-XXXX");

            // sanitize phone numbers
            line = Regex.Replace(line, @"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b", "XXX-XXX-XXXX");
            line = Regex.Replace(line, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", "XXX-XXX-XXXX");

            // sanitize what looks like email addresses
            line = Regex.Replace(line, @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", "XXX@XXX.COM"); 
            return line;
        }
    }
}
