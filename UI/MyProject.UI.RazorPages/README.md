# Project Management Razor Pages Application

This is a Razor Pages web application that provides a user interface for managing projects. It communicates with the MyProject Web API to perform CRUD operations.

## Features

- **List Projects**: View all projects in a responsive table format
- **Create Project**: Add new projects with comprehensive form validation
- **Edit Project**: Modify existing project details
- **View Project Details**: See complete project information
- **Delete Project**: Remove projects with confirmation
- **Responsive Design**: Bootstrap-based UI that works on all devices

## Prerequisites

- .NET 9.0 SDK
- The MyProject Web API should be running (default: `https://localhost:7065`)

## Configuration

1. **API Base URL**: Update the API base URL in `appsettings.json` if your Web API is running on a different port:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7065/"
  }
}
```

2. **HTTP Client Configuration**: The HTTP client is configured in `Program.cs` to connect to the Web API.

## Running the Application

1. **Navigate to the project directory**:
   ```bash
   cd UI/MyProject.UI.RazorPages
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

4. **Access the application**:
   - Open your browser and navigate to `https://localhost:5001` (or the URL shown in the console)
   - Click on "Projects" in the navigation menu to start managing projects

## Project Structure

```
├── Models/                 # Data models and DTOs
│   ├── Project.cs         # Main project model with display attributes
│   └── ProjectDto.cs      # DTOs for API communication
├── Services/              # Service layer for API communication
│   ├── IProjectService.cs # Service interface
│   └── ProjectService.cs  # HTTP client implementation
├── Pages/
│   ├── Projects/          # Project-related Razor pages
│   │   ├── Index.cshtml   # List all projects
│   │   ├── Create.cshtml  # Create new project
│   │   ├── Edit.cshtml    # Edit existing project
│   │   ├── Details.cshtml # View project details
│   │   └── Delete.cshtml  # Delete project confirmation
│   └── Shared/           # Shared layout and components
└── wwwroot/              # Static files (CSS, JS, images)
```

## Dependencies

- **ASP.NET Core 9.0**: Web framework
- **Bootstrap 5**: UI framework (included)
- **Font Awesome**: Icons (referenced via CDN)
- **System.Text.Json**: JSON serialization for API communication

## API Endpoints Used

The application consumes the following Web API endpoints:

- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project
- `PUT /api/projects/{id}` - Update existing project
- `DELETE /api/projects/{id}` - Delete project

## Troubleshooting

1. **API Connection Issues**: 
   - Ensure the Web API is running
   - Check the API base URL in `appsettings.json`
   - Verify CORS settings in the Web API if needed

2. **Validation Errors**:
   - Check that all required fields are filled
   - Ensure date formats are valid
   - Verify numeric values are within acceptable ranges

3. **Build Issues**:
   - Run `dotnet clean` followed by `dotnet restore` and `dotnet build`
   - Check that .NET 9.0 SDK is installed

## Development Notes

- The application uses the Repository pattern via HTTP client
- All API calls are async and include proper error handling
- Form validation is implemented both client-side and server-side
- Responsive design ensures compatibility across devices
- Success and error messages are displayed using TempData and Bootstrap alerts