# PHI Sanitizer

A simple web application that automatically identifies and redacts PHI from text files 

1. Clone the repository
   ```
   git clone https://github.com/brendonwanderlust/InvenePHISanitizer.git
   cd into containing directory
   ```

2. Build and run the backend
   ```
   dotnet build
   dotnet run --project PHISanitizer 
   ```

3. The backend API will be available at `http://localhost:5165`

### Frontend Setup

1. Clone the repository
   ```
   git clone https://github.com/brendonwanderlust/invene-phi-redactor-ui.git 
   ```
2. Navigate to the frontend directory from VS Code
   ```
   cd your-local-frontend-directory
   ```

3. Install dependencies
   ```
   npm install
   ```

4. Start the Angular development server
   ```
   ng serve
   ```

5. Access the application by navigating to `http://localhost:4200`

## Usage

1. Open your browser and navigate to `http://localhost:4200`
2. Click the "Redact PHI" to select one or more text files containing PHI
3. The browser will automatically download a ZIP file containing the sanitized versions of your files

## PHI Sanitization Approach 

### 1. Field Name Detection

The system recognizes common PHI field names and redacts the corresponding values. 
When a line contains a known PHI field, everything after the colon is redacted with 'X' characters.

PHI fields detected include:
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

### 2. Pattern-Based Detection

For PHI that follows predictable patterns, the system uses regular expressions to identify and redact:

- Social Security Numbers: Matches the pattern XXX-XX-XXXX
- Phone Numbers: Recognizes various formats (XXX-XXX-XXXX, (XXX) XXX-XXXX, etc.)
- Email Addresses: Identifies standard email patterns and replaces with XXX@XXX.COM
 
## Design Decisions and Assumptions

### Assumptions

- Text-Based Files: The current implementation assumes all files are text-based
- Line-by-Line Processing: PHI detection works on a per-line basis, not across lines
- Common PHI Formats: Focuses on standard US formats for PHI (SSN, phone numbers, etc.)
- Colon Delimiter: For field-based PHI, assumes a colon separates the field name from the value
