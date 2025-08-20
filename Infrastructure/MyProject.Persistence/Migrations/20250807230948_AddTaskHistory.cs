using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ai");

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "ai",
                columns: table => new
                {
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    organization_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    billing_plan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    contact_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "ai",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUser",
                schema: "ai",
                columns: table => new
                {
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    OrganizationId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUser", x => new { x.organization_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_OrganizationUser_Organization_OrganizationId1",
                        column: x => x.OrganizationId1,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id");
                    table.ForeignKey(
                        name: "FK_OrganizationUser_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUser_User_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_OrganizationUser_User_user_id",
                        column: x => x.user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "ai",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    project_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    industry_domain = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    planned_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    planned_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    planned_budget = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    actual_budget = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    project_manager_id = table.Column<Guid>(type: "uuid", nullable: true),
                    team_size = table.Column<int>(type: "integer", nullable: true),
                    contract_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    project_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "planned"),
                    project_complexity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    description = table.Column<string>(type: "text", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    current_phase = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_Project_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Project_User_project_manager_id",
                        column: x => x.project_manager_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "ai",
                columns: table => new
                {
                    resource_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    role_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    skills = table.Column<string[]>(type: "text[]", nullable: true),
                    experience_level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    full_time_equivalent = table.Column<decimal>(type: "numeric(3,2)", nullable: false, defaultValue: 1.0m),
                    weekly_capacity_hours = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    cost_rate_per_hour = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    employment_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    start_date_employment = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date_employment = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.resource_id);
                    table.ForeignKey(
                        name: "FK_Resource_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resource_User_user_id",
                        column: x => x.user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Milestone",
                schema: "ai",
                columns: table => new
                {
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_achieved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    completion_date = table.Column<DateOnly>(type: "date", nullable: true),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.milestone_id);
                    table.ForeignKey(
                        name: "FK_Milestone_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Milestone_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Risk",
                schema: "ai",
                columns: table => new
                {
                    risk_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    risk_description = table.Column<string>(type: "text", nullable: false),
                    risk_category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    planned_probability_score = table.Column<int>(type: "integer", nullable: true),
                    actual_probability_score = table.Column<int>(type: "integer", nullable: true),
                    planned_impact_score = table.Column<int>(type: "integer", nullable: true),
                    actual_impact_score = table.Column<int>(type: "integer", nullable: true),
                    planned_risk_exposure = table.Column<int>(type: "integer", nullable: true),
                    actual_risk_exposure = table.Column<int>(type: "integer", nullable: true),
                    mitigation_strategy = table.Column<string>(type: "text", nullable: true),
                    contingency_plan = table.Column<string>(type: "text", nullable: true),
                    trigger_events = table.Column<string>(type: "text", nullable: true),
                    risk_owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    risk_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    identified_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    last_review_date = table.Column<DateOnly>(type: "date", nullable: true),
                    closure_date = table.Column<DateOnly>(type: "date", nullable: true),
                    outcome_description = table.Column<string>(type: "text", nullable: true),
                    actual_impact_on_schedule_days = table.Column<int>(type: "integer", nullable: true),
                    actual_impact_on_budget_usd = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    lessons_learned = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.risk_id);
                    table.ForeignKey(
                        name: "FK_Risk_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Risk_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Risk_User_risk_owner_id",
                        column: x => x.risk_owner_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChangeRequest",
                schema: "ai",
                columns: table => new
                {
                    change_request_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    request_description = table.Column<string>(type: "text", nullable: false),
                    reason_for_change = table.Column<string>(type: "text", nullable: false),
                    estimated_impact_on_scope = table.Column<string>(type: "text", nullable: true),
                    estimated_impact_on_schedule_days = table.Column<int>(type: "integer", nullable: true),
                    estimated_impact_on_budget_usd = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    estimated_impact_on_quality = table.Column<string>(type: "text", nullable: true),
                    requested_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    request_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    approval_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    approved_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    approval_date = table.Column<DateOnly>(type: "date", nullable: true),
                    approval_notes = table.Column<string>(type: "text", nullable: true),
                    version_affected = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    actual_impact_on_schedule_days = table.Column<int>(type: "integer", nullable: true),
                    actual_impact_on_budget_usd = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRequest", x => x.change_request_id);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_Milestone_milestone_id",
                        column: x => x.milestone_id,
                        principalSchema: "ai",
                        principalTable: "Milestone",
                        principalColumn: "milestone_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_User_approved_by_user_id",
                        column: x => x.approved_by_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_User_requested_by_user_id",
                        column: x => x.requested_by_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                schema: "ai",
                columns: table => new
                {
                    issue_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: true),
                    issue_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    issue_description = table.Column<string>(type: "text", nullable: false),
                    issue_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    root_cause = table.Column<string>(type: "text", nullable: true),
                    impact_on_schedule_days = table.Column<int>(type: "integer", nullable: true),
                    impact_on_budget_usd = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    impact_on_scope = table.Column<string>(type: "text", nullable: true),
                    impact_on_quality = table.Column<string>(type: "text", nullable: true),
                    resolution_steps = table.Column<string>(type: "text", nullable: true),
                    severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Medium"),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Normal"),
                    assigned_to_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    opened_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    closed_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.issue_id);
                    table.ForeignKey(
                        name: "FK_Issue_Milestone_milestone_id",
                        column: x => x.milestone_id,
                        principalSchema: "ai",
                        principalTable: "Milestone",
                        principalColumn: "milestone_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Issue_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_User_assigned_to_user_id",
                        column: x => x.assigned_to_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "ai",
                columns: table => new
                {
                    task_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    task_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    assigned_to_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_task_id = table.Column<Guid>(type: "uuid", nullable: true),
                    planned_effort_hours = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    actual_effort_hours = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    planned_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    planned_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    task_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "To Do"),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Medium"),
                    is_milestone = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uuid", nullable: true),
                    issue_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_Task_Issue_issue_id",
                        column: x => x.issue_id,
                        principalSchema: "ai",
                        principalTable: "Issue",
                        principalColumn: "issue_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Task_Milestone_milestone_id",
                        column: x => x.milestone_id,
                        principalSchema: "ai",
                        principalTable: "Milestone",
                        principalColumn: "milestone_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Task_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Task_parent_task_id",
                        column: x => x.parent_task_id,
                        principalSchema: "ai",
                        principalTable: "Task",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_assigned_to_user_id",
                        column: x => x.assigned_to_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "QualityAssurance",
                schema: "ai",
                columns: table => new
                {
                    qa_item_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: true),
                    item_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    summary = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    reported_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    assigned_to_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    reported_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    resolution_date = table.Column<DateOnly>(type: "date", nullable: true),
                    closed_date = table.Column<DateOnly>(type: "date", nullable: true),
                    resolution_notes = table.Column<string>(type: "text", nullable: true),
                    test_environment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityAssurance", x => x.qa_item_id);
                    table.ForeignKey(
                        name: "FK_QualityAssurance_Project_project_id",
                        column: x => x.project_id,
                        principalSchema: "ai",
                        principalTable: "Project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualityAssurance_Task_task_id",
                        column: x => x.task_id,
                        principalSchema: "ai",
                        principalTable: "Task",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QualityAssurance_User_assigned_to_user_id",
                        column: x => x.assigned_to_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QualityAssurance_User_reported_by_user_id",
                        column: x => x.reported_by_user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "taskhistory",
                columns: table => new
                {
                    history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_task_history_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_history_Task_task_id",
                        column: x => x.task_id,
                        principalSchema: "ai",
                        principalTable: "Task",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_history_User_user_id",
                        column: x => x.user_id,
                        principalSchema: "ai",
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskTimeLog",
                schema: "ai",
                columns: table => new
                {
                    task_time_log_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resource_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_worked = table.Column<DateOnly>(type: "date", nullable: false),
                    hours_worked = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    work_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_billable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    hourly_rate = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTimeLog", x => x.task_time_log_id);
                    table.ForeignKey(
                        name: "FK_TaskTimeLog_Organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "ai",
                        principalTable: "Organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTimeLog_Resource_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "ai",
                        principalTable: "Resource",
                        principalColumn: "resource_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTimeLog_Task_task_id",
                        column: x => x.task_id,
                        principalSchema: "ai",
                        principalTable: "Task",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_approver_id",
                schema: "ai",
                table: "ChangeRequest",
                column: "approved_by_user_id");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_milestone_id",
                schema: "ai",
                table: "ChangeRequest",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_organization_id",
                schema: "ai",
                table: "ChangeRequest",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_project_id",
                schema: "ai",
                table: "ChangeRequest",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_request_date",
                schema: "ai",
                table: "ChangeRequest",
                column: "request_date");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_requestor_id",
                schema: "ai",
                table: "ChangeRequest",
                column: "requested_by_user_id");

            migrationBuilder.CreateIndex(
                name: "idx_change_requests_status",
                schema: "ai",
                table: "ChangeRequest",
                column: "approval_status");

            migrationBuilder.CreateIndex(
                name: "idx_issues_assigned_to_user_id",
                schema: "ai",
                table: "Issue",
                column: "assigned_to_user_id");

            migrationBuilder.CreateIndex(
                name: "idx_issues_milestone_id",
                schema: "ai",
                table: "Issue",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "idx_issues_opened_date",
                schema: "ai",
                table: "Issue",
                column: "opened_date");

            migrationBuilder.CreateIndex(
                name: "idx_issues_organization_id",
                schema: "ai",
                table: "Issue",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_issues_priority",
                schema: "ai",
                table: "Issue",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "idx_issues_project_id",
                schema: "ai",
                table: "Issue",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_issues_severity",
                schema: "ai",
                table: "Issue",
                column: "severity");

            migrationBuilder.CreateIndex(
                name: "idx_issues_status",
                schema: "ai",
                table: "Issue",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_issues_task_id",
                schema: "ai",
                table: "Issue",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "idx_milestone_due_date",
                schema: "ai",
                table: "Milestone",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "idx_milestone_is_achieved",
                schema: "ai",
                table: "Milestone",
                column: "is_achieved");

            migrationBuilder.CreateIndex(
                name: "idx_milestone_name",
                schema: "ai",
                table: "Milestone",
                column: "milestone_name");

            migrationBuilder.CreateIndex(
                name: "idx_milestone_organization_id",
                schema: "ai",
                table: "Milestone",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_milestone_project_id",
                schema: "ai",
                table: "Milestone",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "uq_milestone_project_name",
                schema: "ai",
                table: "Milestone",
                columns: new[] { "project_id", "milestone_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_organization_name",
                schema: "ai",
                table: "Organization",
                column: "organization_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_organization_status",
                schema: "ai",
                table: "Organization",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_is_active",
                schema: "ai",
                table: "OrganizationUser",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_org_active",
                schema: "ai",
                table: "OrganizationUser",
                columns: new[] { "organization_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_organization_id",
                schema: "ai",
                table: "OrganizationUser",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_role",
                schema: "ai",
                table: "OrganizationUser",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_user_active",
                schema: "ai",
                table: "OrganizationUser",
                columns: new[] { "user_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "idx_organizationuser_user_id",
                schema: "ai",
                table: "OrganizationUser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_OrganizationId1",
                schema: "ai",
                table: "OrganizationUser",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_UserId1",
                schema: "ai",
                table: "OrganizationUser",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "idx_planned_start_date",
                schema: "ai",
                table: "Project",
                column: "planned_start_date");

            migrationBuilder.CreateIndex(
                name: "idx_project_manager_id",
                schema: "ai",
                table: "Project",
                column: "project_manager_id");

            migrationBuilder.CreateIndex(
                name: "idx_project_name",
                schema: "ai",
                table: "Project",
                column: "project_name");

            migrationBuilder.CreateIndex(
                name: "idx_project_status",
                schema: "ai",
                table: "Project",
                column: "project_status");

            migrationBuilder.CreateIndex(
                name: "IX_Project_organization_id",
                schema: "ai",
                table: "Project",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_qa_item_type",
                schema: "ai",
                table: "QualityAssurance",
                column: "item_type");

            migrationBuilder.CreateIndex(
                name: "idx_qa_priority",
                schema: "ai",
                table: "QualityAssurance",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "idx_qa_project_id",
                schema: "ai",
                table: "QualityAssurance",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_qa_severity",
                schema: "ai",
                table: "QualityAssurance",
                column: "severity");

            migrationBuilder.CreateIndex(
                name: "idx_qa_status",
                schema: "ai",
                table: "QualityAssurance",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_qa_task_id",
                schema: "ai",
                table: "QualityAssurance",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_QualityAssurance_assigned_to_user_id",
                schema: "ai",
                table: "QualityAssurance",
                column: "assigned_to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_QualityAssurance_reported_by_user_id",
                schema: "ai",
                table: "QualityAssurance",
                column: "reported_by_user_id");

            migrationBuilder.CreateIndex(
                name: "idx_resources_department",
                schema: "ai",
                table: "Resource",
                column: "department");

            migrationBuilder.CreateIndex(
                name: "idx_resources_employment_status",
                schema: "ai",
                table: "Resource",
                column: "employment_status");

            migrationBuilder.CreateIndex(
                name: "idx_resources_last_name",
                schema: "ai",
                table: "Resource",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "idx_resources_location",
                schema: "ai",
                table: "Resource",
                column: "location");

            migrationBuilder.CreateIndex(
                name: "idx_resources_organization_id",
                schema: "ai",
                table: "Resource",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_resources_role_title",
                schema: "ai",
                table: "Resource",
                column: "role_title");

            migrationBuilder.CreateIndex(
                name: "idx_resources_skills",
                schema: "ai",
                table: "Resource",
                column: "skills")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "idx_resources_user_id",
                schema: "ai",
                table: "Resource",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "resource_email_key",
                schema: "ai",
                table: "Resource",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_user_tenant",
                schema: "ai",
                table: "Resource",
                columns: new[] { "user_id", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_risks_category",
                schema: "ai",
                table: "Risk",
                column: "risk_category");

            migrationBuilder.CreateIndex(
                name: "idx_risks_identified_date",
                schema: "ai",
                table: "Risk",
                column: "identified_date");

            migrationBuilder.CreateIndex(
                name: "idx_risks_organization_id",
                schema: "ai",
                table: "Risk",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_risks_owner_id",
                schema: "ai",
                table: "Risk",
                column: "risk_owner_id");

            migrationBuilder.CreateIndex(
                name: "idx_risks_project_id",
                schema: "ai",
                table: "Risk",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_risks_status",
                schema: "ai",
                table: "Risk",
                column: "risk_status");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_assigned_to_user_id",
                schema: "ai",
                table: "Task",
                column: "assigned_to_user_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_issue_id",
                schema: "ai",
                table: "Task",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_milestone_id",
                schema: "ai",
                table: "Task",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_organization_id",
                schema: "ai",
                table: "Task",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_parent_task_id",
                schema: "ai",
                table: "Task",
                column: "parent_task_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_planned_start_date",
                schema: "ai",
                table: "Task",
                column: "planned_start_date");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_priority",
                schema: "ai",
                table: "Task",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_project_id",
                schema: "ai",
                table: "Task",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_status",
                schema: "ai",
                table: "Task",
                column: "task_status");

            migrationBuilder.CreateIndex(
                name: "IX_task_history_created_at",
                table: "task_history",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_task_history_organization_id",
                table: "task_history",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_history_task_id",
                table: "task_history",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_history_user_id",
                table: "task_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimeLog_date_worked",
                schema: "ai",
                table: "TaskTimeLog",
                column: "date_worked");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimeLog_organization_id",
                schema: "ai",
                table: "TaskTimeLog",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimeLog_resource_id",
                schema: "ai",
                table: "TaskTimeLog",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimeLog_task_id",
                schema: "ai",
                table: "TaskTimeLog",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "idx_user_email",
                schema: "ai",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_user_first_name",
                schema: "ai",
                table: "User",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "idx_user_is_active",
                schema: "ai",
                table: "User",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "idx_user_last_name",
                schema: "ai",
                table: "User",
                column: "last_name");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Task_task_id",
                schema: "ai",
                table: "Issue",
                column: "task_id",
                principalSchema: "ai",
                principalTable: "Task",
                principalColumn: "task_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Milestone_milestone_id",
                schema: "ai",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Milestone_milestone_id",
                schema: "ai",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Organization_organization_id",
                schema: "ai",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Organization_organization_id",
                schema: "ai",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Organization_organization_id",
                schema: "ai",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_project_id",
                schema: "ai",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_project_id",
                schema: "ai",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_assigned_to_user_id",
                schema: "ai",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_assigned_to_user_id",
                schema: "ai",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Task_task_id",
                schema: "ai",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "ChangeRequest",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "OrganizationUser",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "QualityAssurance",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Risk",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "task_history");

            migrationBuilder.DropTable(
                name: "TaskTimeLog",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Milestone",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "User",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "ai");

            migrationBuilder.DropTable(
                name: "Issue",
                schema: "ai");
        }
    }
}
