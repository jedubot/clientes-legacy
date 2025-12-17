# Clientes.WebApi .NET 10.0 Upgrade Tasks

## Overview

Migration of Clientes.WebApi from .NET Framework 4.8.1 to .NET 10.0, involving framework upgrade, architectural transformation from ASP.NET Web API to ASP.NET Core, and dependency injection pattern changes. All components will be upgraded simultaneously in a single atomic operation.

**Progress**: 2/3 tasks complete (67%) ![0%](https://progress-bar.xyz/67)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2025-12-17 14:01)*
**References**: Plan §Phase 0

- [✓] (1) Verify .NET 10.0 SDK installed (run `dotnet --list-sdks`)
- [✓] (2) SDK version ≥ 10.0.0 present (**Verify**)

---

### [✓] TASK-002: Atomic framework and architectural migration *(Completed: 2025-12-17 14:22)*
**References**: Plan §Phase 1, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [✓] (1) Convert Clientes.WebApi.csproj to SDK-style format per Plan §Phase 1 (use `<Project Sdk="Microsoft.NET.Sdk.Web">`, simplify to ~20-30 lines)
- [✓] (2) Project file is SDK-style format (**Verify**)
- [✓] (3) Update TargetFramework from net481 to net10.0 in Clientes.WebApi.csproj
- [✓] (4) TargetFramework is net10.0 (**Verify**)
- [✓] (5) Remove incompatible packages per Plan §Package Update Reference (Autofac.WebApi2, Microsoft.AspNet.WebApi.Core, Microsoft.AspNet.WebApi.WebHost)
- [✓] (6) Remove framework-included packages per Plan §Package Update Reference (Microsoft.AspNet.WebApi, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, System.Buffers, System.Memory, System.Numerics.Vectors, System.Threading.Tasks.Extensions)
- [✓] (7) Add Microsoft.AspNetCore.Mvc.NewtonsoftJson package
- [✓] (8) Update recommended packages per Plan §Package Update Reference (Microsoft.Bcl.AsyncInterfaces 10.0.1, System.Diagnostics.DiagnosticSource 10.0.1, System.Runtime.CompilerServices.Unsafe 6.1.2)
- [✓] (9) All package references updated (**Verify**)
- [✓] (10) Create Program.cs with ASP.NET Core initialization per Plan §Phase 1 (configure services for DI, EF, JSON serialization; configure middleware for routing and controllers)
- [✓] (11) Program.cs created with correct service and middleware configuration (**Verify**)
- [✓] (12) Update all controllers per Plan §Breaking Changes Catalog (replace System.Web.Http namespaces with Microsoft.AspNetCore.Mvc, change base class from ApiController to ControllerBase, add [ApiController] attribute, update return types from IHttpActionResult to ActionResult<T> or IActionResult, update attributes [FromUri]→[FromQuery], [RoutePrefix]→[Route])
- [✓] (13) All controllers updated to ASP.NET Core patterns (**Verify**)
- [✓] (14) Delete Global.asax.cs and App_Start folder
- [✓] (15) Legacy files removed (**Verify**)
- [✓] (16) Restore all dependencies (run `dotnet restore`)
- [✓] (17) All dependencies restored successfully (**Verify**)
- [✓] (18) Build solution and fix all compilation errors per Plan §Breaking Changes Catalog (focus: System.Web.Http API replacements, routing attribute updates, return type conversions)
- [✓] (19) Solution builds with 0 errors (**Verify**)
- [✓] (20) Clean release build succeeds (run `dotnet build --configuration Release`)
- [✓] (21) Release build completes with 0 errors (**Verify**)
- [✓] (22) Commit all changes with message: "TASK-002: Migrate to .NET 10.0 - Complete framework and architectural upgrade"

---

### [▶] TASK-003: Validate migrated application
**References**: Plan §Phase 2, Plan §Testing & Validation Strategy

- [✓] (1) Start application (run `dotnet run --project Clientes.WebApi\Clientes.WebApi.csproj`)
- [✓] (2) Application starts without exceptions (**Verify**)
- [✓] (3) Test all API endpoints per Plan §Testing Matrix (GET /api/clientes, GET /api/clientes/{id}, GET /api/clientes?nome={nome}, GET /api/clientes/contar, POST /api/clientes, DELETE /api/clientes/{id})
- [✓] (4) All endpoints respond with correct HTTP status codes (**Verify**)
- [✓] (5) Verify database connectivity and CRUD operations (if applicable)
- [✓] (6) Database operations succeed (**Verify**)
- [✓] (7) Validate JSON serialization behavior matches expected format
- [✓] (8) JSON responses match expected schema (**Verify**)
- [▶] (9) Commit validation fixes (if any) with message: "TASK-003: Complete validation and testing"

---












