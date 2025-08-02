# MyProject - .NET Project Management System

A comprehensive .NET 9.0 project management system with PostgreSQL backend, featuring Entity Framework Core, RESTful APIs, and a modern web interface.

## Project Structure

```
MyProject/
├── Infrastructure/
│   └── MyProject.Persistence/
│       ├── Entities/
│       │   ├── Project.cs
│       │   ├── Risk.cs
│       │   ├── Task.cs
│       │   ├── Resource.cs
│       │   ├── Issue.cs
│       │   ├── ChangeRequest.cs
│       │   └── QualityAssurance.cs
│       ├── Configurations/
│       │   ├── ProjectConfiguration.cs
│       │   ├── RiskConfiguration.cs
│       │   ├── TaskConfiguration.cs
│       │   ├── ResourceConfiguration.cs
│       │   ├── IssueConfiguration.cs
│       │   ├── ChangeRequestConfiguration.cs
│       │   └── QualityAssuranceConfiguration.cs
│       ├── Repositories/
│       │   ├── IProjectRepository.cs
│       │   ├── ProjectRepository.cs
│       │   ├── IRiskRepository.cs
│       │   ├── RiskRepository.cs
│       │   ├── ITaskRepository.cs
│       │   ├── TaskRepository.cs
│       │   ├── IResourceRepository.cs
│       │   ├── ResourceRepository.cs
│       │   ├── IIssueRepository.cs
│       │   ├── IssueRepository.cs
│       │   ├── IChangeRequestRepository.cs
│       │   ├── ChangeRequestRepository.cs
│       │   ├── IQualityAssuranceRepository.cs
│       │   └── QualityAssuranceRepository.cs
│       ├── ApplicationDbContext.cs
│       └── ServiceCollectionExtensions.cs
├── WebAPI/
│   └── MyProject.WebAPI/
│       ├── Controllers/
│       │   ├── ProjectsController.cs
│       │   ├── RisksController.cs
│       │   ├── TasksController.cs
│       │   ├── ResourcesController.cs
│       │   ├── IssuesController.cs
│       │   ├── ChangeRequestsController.cs
│       │   └── QualityAssurancesController.cs
│       ├── DTOs/
│       │   ├── ProjectDto.cs
│       │   ├── RiskDto.cs
│       │   ├── TaskDto.cs
│       │   ├── ResourceDto.cs
│       │   ├── IssueDto.cs
│       │   ├── ChangeRequestDto.cs
│       │   └── QualityAssuranceDto.cs
│       ├── Program.cs
│       └── appsettings.json
└── UI/
    └── MyProject.UI.ProjectManagementConsole/
        └── (Blazor Server UI components)
```

## Database Schema

### ai.Project Table
- **project_id** (uuid, PK) - Unique project identifier
- **project_name** (varchar(255)) - Project name
- **project_type** (varchar(100)) - Type of project
- **industry_domain** (varchar(100)) - Industry domain
- **planned_start_date** (date) - Planned start date
- **planned_end_date** (date) - Planned end date
- **actual_start_date** (date) - Actual start date
- **actual_end_date** (date) - Actual end date
- **planned_budget** (numeric(18,2)) - Planned budget
- **actual_budget** (numeric(18,2)) - Actual budget
- **project_manager_id** (uuid) - Project manager ID
- **team_size** (integer) - Team size
- **contract_type** (varchar(50)) - Contract type
- **project_status** (varchar(50)) - Project status
- **project_complexity** (varchar(50)) - Project complexity
- **description** (text) - Project description
- **notes** (text) - Project notes
- **current_phase** (varchar(100)) - Current project phase
- **organization_id** (uuid) - Organization ID
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.Risk Table
- **risk_id** (uuid, PK) - Unique risk identifier
- **project_id** (uuid, FK) - Associated project
- **risk_description** (text) - Risk description
- **risk_category** (varchar(100)) - Risk category
- **risk_owner_id** (uuid) - Risk owner ID
- **planned_probability_score** (integer) - Planned probability score
- **actual_probability_score** (integer) - Actual probability score
- **planned_impact_score** (integer) - Planned impact score
- **actual_impact_score** (integer) - Actual impact score
- **planned_risk_exposure** (numeric(18,2)) - Planned risk exposure (computed)
- **actual_risk_exposure** (numeric(18,2)) - Actual risk exposure (computed)
- **risk_status** (varchar(50)) - Risk status
- **identified_date** (date) - Risk identification date
- **mitigation_strategy** (text) - Risk mitigation strategy
- **contingency_plan** (text) - Contingency plan
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.Task Table
- **task_id** (uuid, PK) - Unique task identifier
- **project_id** (uuid, FK) - Associated project
- **task_name** (varchar(255)) - Task name
- **task_type** (varchar(100)) - Task type
- **assigned_to_user_id** (uuid) - Assigned user ID
- **parent_task_id** (uuid, FK) - Parent task ID (self-referencing)
- **planned_effort_hours** (numeric(8,2)) - Planned effort hours
- **actual_effort_hours** (numeric(8,2)) - Actual effort hours
- **planned_start_date** (date) - Planned start date
- **planned_end_date** (date) - Planned end date
- **actual_start_date** (date) - Actual start date
- **actual_end_date** (date) - Actual end date
- **task_status** (varchar(50)) - Task status
- **priority** (varchar(50)) - Task priority
- **is_milestone** (boolean) - Is milestone flag
- **description** (text) - Task description
- **comments** (text) - Task comments
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.Resource Table
- **resource_id** (uuid, PK) - Unique resource identifier
- **first_name** (varchar(100)) - First name
- **last_name** (varchar(100)) - Last name
- **email** (varchar(255), UNIQUE) - Email address
- **role_title** (varchar(100)) - Role title
- **department** (varchar(100)) - Department
- **skills** (text[]) - Skills array
- **experience_level** (varchar(50)) - Experience level
- **full_time_equivalent** (numeric(3,2)) - Full-time equivalent (0.0-1.0)
- **weekly_capacity_hours** (numeric(5,2)) - Weekly capacity hours
- **cost_rate_per_hour** (numeric(10,2)) - Cost rate per hour
- **location** (varchar(100)) - Location
- **employment_status** (varchar(50)) - Employment status
- **start_date_employment** (date) - Employment start date
- **end_date_employment** (date) - Employment end date
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.Issue Table
- **issue_id** (uuid, PK) - Unique issue identifier
- **project_id** (uuid, FK) - Associated project
- **task_id** (uuid, FK) - Associated task (optional)
- **issue_name** (varchar(255)) - Issue name
- **issue_description** (text) - Issue description
- **issue_type** (varchar(100)) - Issue type
- **root_cause** (text) - Root cause analysis
- **impact_on_schedule_days** (integer) - Schedule impact in days
- **impact_on_budget_usd** (numeric(18,2)) - Budget impact in USD
- **impact_on_scope** (text) - Scope impact description
- **impact_on_quality** (text) - Quality impact description
- **resolution_steps** (text) - Resolution steps
- **severity** (varchar(50)) - Issue severity level
- **priority** (varchar(50)) - Issue priority level
- **assigned_to_user_id** (uuid) - Assigned user ID
- **status** (varchar(50)) - Issue status
- **opened_date** (date) - Issue opening date
- **closed_date** (date) - Issue closing date
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.ChangeRequest Table
- **change_request_id** (uuid, PK) - Unique change request identifier
- **project_id** (uuid, FK) - Associated project
- **request_title** (varchar(255)) - Change request title
- **request_description** (text) - Detailed description of the change
- **reason_for_change** (text) - Justification for the change
- **estimated_impact_on_scope** (text) - Estimated scope impact
- **estimated_impact_on_schedule_days** (integer) - Estimated schedule impact in days
- **estimated_impact_on_budget_usd** (numeric(18,2)) - Estimated budget impact in USD
- **estimated_impact_on_quality** (text) - Estimated quality impact
- **requested_by_user_id** (uuid) - User who requested the change
- **request_date** (date) - Date when change was requested
- **approval_status** (varchar(50)) - Current approval status
- **approved_by_user_id** (uuid) - User who approved/rejected the change
- **approval_date** (date) - Date when change was approved/rejected
- **approval_notes** (text) - Notes from the approver
- **version_affected** (varchar(50)) - Version affected by the change
- **actual_impact_on_schedule_days** (integer) - Actual schedule impact
- **actual_impact_on_budget_usd** (numeric(18,2)) - Actual budget impact
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

### ai.QualityAssurance Table
- **qa_item_id** (uuid, PK) - Unique QA item identifier
- **project_id** (uuid, FK) - Associated project
- **task_id** (uuid, FK) - Associated task (optional)
- **item_type** (varchar(50)) - Type of QA item (Bug, Feature, Enhancement, etc.)
- **summary** (varchar(255)) - Brief summary of the QA item
- **description** (text) - Detailed description of the QA item
- **severity** (varchar(50)) - Severity level of the QA item
- **priority** (varchar(50)) - Priority level of the QA item
- **status** (varchar(50)) - Current status of the QA item
- **reported_by_user_id** (uuid) - User who reported the QA item
- **assigned_to_user_id** (uuid) - User assigned to handle the QA item
- **reported_date** (date) - Date when the QA item was reported
- **resolution_date** (date) - Date when the QA item was resolved
- **closed_date** (date) - Date when the QA item was closed
- **resolution_notes** (text) - Notes about the resolution
- **test_environment** (text) - Test environment where the issue was found
- **created_at** (timestamp) - Creation timestamp
- **updated_at** (timestamp) - Last update timestamp

## Setup Instructions

### 1. Database Connection
Update the connection string in `WebAPI/MyProject.WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=your_database;Username=postgres;Password=your_password"
  }
}
```

### 2. Entity Framework Migrations
Run the following commands in the WebAPI project directory:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Running the Application
```bash
# Run the WebAPI
cd WebAPI/MyProject.WebAPI
dotnet run

# Run the Blazor UI (optional)
cd UI/MyProject.UI.ProjectManagementConsole
dotnet run
```

## API Endpoints

### Project Endpoints
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `GET /api/projects/status/{status}` - Get projects by status
- `GET /api/projects/manager/{managerId}` - Get projects by manager
- `POST /api/projects` - Create new project
- `PUT /api/projects/{id}` - Update project
- `DELETE /api/projects/{id}` - Delete project

### Risk Endpoints
- `GET /api/risks` - Get all risks
- `GET /api/risks/{id}` - Get risk by ID
- `GET /api/risks/project/{projectId}` - Get risks by project
- `GET /api/risks/status/{status}` - Get risks by status
- `GET /api/risks/category/{category}` - Get risks by category
- `GET /api/risks/owner/{ownerId}` - Get risks by owner
- `GET /api/risks/high-risk` - Get high-risk risks
- `POST /api/risks` - Create new risk
- `PUT /api/risks/{id}` - Update risk
- `DELETE /api/risks/{id}` - Delete risk

### Task Endpoints
- `GET /api/tasks` - Get all tasks
- `GET /api/tasks/{id}` - Get task by ID
- `GET /api/tasks/project/{projectId}` - Get tasks by project
- `GET /api/tasks/status/{status}` - Get tasks by status
- `GET /api/tasks/priority/{priority}` - Get tasks by priority
- `GET /api/tasks/assigned/{userId}` - Get tasks by assigned user
- `GET /api/tasks/parent/{parentTaskId}` - Get subtasks
- `GET /api/tasks/milestones` - Get milestone tasks
- `GET /api/tasks/overdue` - Get overdue tasks
- `GET /api/tasks/with-subtasks` - Get tasks with subtasks
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task

### Resource Endpoints
- `GET /api/resources` - Get all resources
- `GET /api/resources/{id}` - Get resource by ID
- `GET /api/resources/email/{email}` - Get resource by email
- `GET /api/resources/department/{department}` - Get resources by department
- `GET /api/resources/role/{roleTitle}` - Get resources by role
- `GET /api/resources/status/{status}` - Get resources by employment status
- `GET /api/resources/location/{location}` - Get resources by location
- `GET /api/resources/skills` - Get resources by skills
- `GET /api/resources/active` - Get active resources
- `GET /api/resources/available` - Get available resources
- `GET /api/resources/search` - Search resources by name
- `POST /api/resources` - Create new resource
- `PUT /api/resources/{id}` - Update resource
- `DELETE /api/resources/{id}` - Delete resource

### Issue Endpoints
- `GET /api/issues` - Get all issues
- `GET /api/issues/{id}` - Get issue by ID
- `GET /api/issues/project/{projectId}` - Get issues by project
- `GET /api/issues/task/{taskId}` - Get issues by task
- `GET /api/issues/status/{status}` - Get issues by status
- `GET /api/issues/priority/{priority}` - Get issues by priority
- `GET /api/issues/severity/{severity}` - Get issues by severity
- `GET /api/issues/assigned/{userId}` - Get issues by assigned user
- `GET /api/issues/type/{issueType}` - Get issues by type
- `GET /api/issues/open` - Get open issues
- `GET /api/issues/critical` - Get critical issues
- `GET /api/issues/overdue` - Get overdue issues
- `GET /api/issues/search` - Search issues by description
- `POST /api/issues` - Create new issue
- `PUT /api/issues/{id}` - Update issue
- `DELETE /api/issues/{id}` - Delete issue

### ChangeRequest Endpoints
- `GET /api/changerequests` - Get all change requests
- `GET /api/changerequests/{id}` - Get change request by ID
- `GET /api/changerequests/project/{projectId}` - Get change requests by project
- `GET /api/changerequests/status/{status}` - Get change requests by status
- `GET /api/changerequests/requested-by/{userId}` - Get change requests by requestor
- `GET /api/changerequests/approved-by/{userId}` - Get change requests by approver
- `GET /api/changerequests/pending` - Get pending change requests
- `GET /api/changerequests/approved` - Get approved change requests
- `GET /api/changerequests/rejected` - Get rejected change requests
- `GET /api/changerequests/date-range` - Get change requests by date range
- `GET /api/changerequests/version/{version}` - Get change requests by version
- `GET /api/changerequests/search` - Search change requests by title
- `POST /api/changerequests` - Create new change request
- `PUT /api/changerequests/{id}` - Update change request
- `DELETE /api/changerequests/{id}` - Delete change request

### QualityAssurance Endpoints
- `GET /api/qualityassurances` - Get all QA items
- `GET /api/qualityassurances/{id}` - Get QA item by ID
- `GET /api/qualityassurances/project/{projectId}` - Get QA items by project
- `GET /api/qualityassurances/task/{taskId}` - Get QA items by task
- `GET /api/qualityassurances/status/{status}` - Get QA items by status
- `GET /api/qualityassurances/type/{itemType}` - Get QA items by type
- `GET /api/qualityassurances/severity/{severity}` - Get QA items by severity
- `GET /api/qualityassurances/priority/{priority}` - Get QA items by priority
- `GET /api/qualityassurances/reported-by/{userId}` - Get QA items by reporter
- `GET /api/qualityassurances/assigned-to/{userId}` - Get QA items by assignee
- `GET /api/qualityassurances/open` - Get open QA items
- `GET /api/qualityassurances/closed` - Get closed QA items
- `GET /api/qualityassurances/date-range` - Get QA items by date range
- `GET /api/qualityassurances/environment/{testEnvironment}` - Get QA items by test environment
- `GET /api/qualityassurances/search` - Search QA items by summary
- `POST /api/qualityassurances` - Create new QA item
- `PUT /api/qualityassurances/{id}` - Update QA item
- `DELETE /api/qualityassurances/{id}` - Delete QA item

## Features

### Project Management
- Complete project lifecycle management
- Budget tracking with planned vs actual costs
- Project status and phase tracking
- Team size and contract type management

### Risk Management
- Comprehensive risk assessment and tracking
- Probability and impact scoring
- Risk exposure calculations
- Mitigation strategy and contingency planning

### Task Management
- Hierarchical task structure with parent-child relationships
- Milestone tracking for project phases
- Effort tracking with planned vs actual hours
- Priority and status management
- Overdue task detection

### Resource Management
- Complete resource profile management
- Skills tracking with array support
- Capacity and cost rate management
- Employment status and availability tracking
- Department and role-based filtering

### Issue Management
- Comprehensive issue tracking and resolution
- Impact assessment on schedule, budget, scope, and quality
- Root cause analysis and resolution steps
- Severity and priority classification
- Assignment and status tracking
- Overdue issue detection

### Change Request Management
- Formal change control process
- Impact assessment on scope, schedule, budget, and quality
- Approval workflow with status tracking
- Version control integration
- Requestor and approver tracking
- Actual vs estimated impact comparison

### Quality Assurance Management
- Comprehensive QA item tracking and management
- Support for different QA item types (Bug, Feature, Enhancement, etc.)
- Severity and priority classification
- Assignment and status tracking
- Test environment tracking
- Resolution and closure workflow
- Date range filtering and reporting

### Data Integrity
- Foreign key relationships ensure data integrity between projects, risks, tasks, resources, issues, change requests, and quality assurance items
- Task hierarchy supports parent-child relationships for complex project structures
- Resource skills support PostgreSQL array types for flexible skill management
- Effort tracking uses `numeric(8,2)` for precise hour tracking
- Full-time equivalent tracking with `numeric(3,2)` for resource allocation
- Issue impact tracking with precise numeric values for schedule and budget impacts
- Change request approval workflow with date validation
- QA item status validation with check constraints

### PostgreSQL Features
- UUID primary keys for distributed systems
- Array types for skills and other multi-value fields
- Computed columns for risk exposure calculations
- GIN indexes for efficient array searching
- Check constraints for data validation
- Triggers for automatic timestamp updates

## Dependencies

### Infrastructure Layer
- `Microsoft.EntityFrameworkCore` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.0)

### WebAPI Layer
- `Microsoft.AspNetCore.OpenApi` (9.0.0)
- `Swashbuckle.AspNetCore` (6.5.0)

### UI Layer
- `Microsoft.AspNetCore.Components.Web` (9.0.0)
- `Microsoft.AspNetCore.Components.Server` (9.0.0)

## Development Notes

- All entities use UUID primary keys for better scalability
- PostgreSQL-specific features like arrays and computed columns are fully supported
- Repository pattern provides clean separation of concerns
- DTOs ensure proper API contract management
- Comprehensive validation using Data Annotations
- Async/await patterns throughout for better performance
- Proper error handling and HTTP status codes
- Swagger/OpenAPI documentation included 