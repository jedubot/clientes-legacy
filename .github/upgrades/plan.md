# .NET Framework 4.8.1 to .NET 10.0 Migration Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Migration Plans](#project-by-project-migration-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description

Migration of **Clientes.WebApi** from **.NET Framework 4.8.1** (ASP.NET Web API) to **.NET 10.0** (ASP.NET Core). This is a comprehensive modernization involving framework upgrade, architectural transformation, and dependency injection pattern changes.

### Scope

**Projects Affected:** 1
- `Clientes.WebApi\Clientes.WebApi.csproj` (net481 ? net10.0)

**Current State:**
- Classic ASP.NET Web API project using System.Web.Http
- Entity Framework 6.5.1 (classic EF)
- Autofac for dependency injection
- Global.asax.cs application initialization
- Non-SDK-style project format
- 425 lines of code across 12 files

**Target State:**
- Modern ASP.NET Core Web API on .NET 10.0
- SDK-style project format
- ASP.NET Core built-in dependency injection
- Program.cs initialization pattern
- Compatible package versions

### Discovered Metrics

| Metric | Value | Impact |
|--------|-------|--------|
| **Projects** | 1 | Single project, no dependency complexity |
| **Project Dependencies** | 0 | No internal dependencies to coordinate |
| **Total APIs Analyzed** | 390 | Large API surface |
| **Binary Incompatible APIs** | 68 (17.4%) | High - Extensive code changes required |
| **Source Incompatible APIs** | 7 (1.8%) | Medium - Recompilation needed |
| **Total NuGet Packages** | 17 | Moderate package count |
| **Incompatible Packages** | 3 | Require replacement |
| **Upgrade Recommended Packages** | 3 | Minor version updates |
| **Framework-Included Packages** | 6 | Can be removed |
| **Estimated LOC to Modify** | 75+ | 17.6% of codebase |
| **Security Vulnerabilities** | 0 | ? None detected |

### Complexity Classification

**Classification: High Complexity (Architectural Migration)**

**Justification:**
1. **Framework Paradigm Shift**: ASP.NET Web API ? ASP.NET Core represents fundamental architectural changes, not just version upgrade
2. **API Incompatibility**: 93.3% of issues stem from System.Web APIs that don't exist in .NET Core
3. **Multiple Migration Paths**:
   - Project format (classic ? SDK-style)
   - Application initialization (Global.asax ? Program.cs)
   - DI container (Autofac ? ASP.NET Core DI)
   - Entity Framework (EF6 ? EF Core consideration)
4. **Breaking Changes**: 68 binary incompatible APIs require manual code updates
5. **No Incremental Path**: Cannot multi-target; must complete full migration

**Risk Level: High** (architectural changes + extensive API replacements)

### Selected Strategy

**All-At-Once Strategy** - Single coordinated migration operation.

**Rationale:**
- Single project eliminates dependency coordination complexity
- No intermediate multi-targeting possible (ASP.NET Framework ? ASP.NET Core)
- All changes are interdependent (can't partially migrate System.Web APIs)
- Cleaner to execute atomically than attempt staged approach
- Project size manageable for comprehensive testing

### Critical Issues

**?? High Priority:**
1. **System.Web.Http API Replacement** - 68 binary incompatible APIs across controllers, routing, and configuration
2. **Project Format Conversion** - Must convert to SDK-style before framework upgrade
3. **Application Initialization** - Complete rewrite of Global.asax.cs ? Program.cs pattern
4. **DI Container Migration** - Replace Autofac.WebApi2 with ASP.NET Core DI

**?? Medium Priority:**
5. **Entity Framework Decision** - Continue with EF6 (compatible) or migrate to EF Core
6. **Package Replacements** - Remove incompatible Web API packages, add ASP.NET Core equivalents
7. **JSON Serialization** - Migrate from Newtonsoft.Json configuration to System.Text.Json or configure Newtonsoft in Core

**? No Security Issues** - Zero vulnerabilities detected in current packages

### Recommended Approach

**Single-Phase Atomic Migration** with comprehensive validation:

1. **Phase 0: Preparation** - SDK installation verification
2. **Phase 1: Atomic Upgrade** - All structural, package, and code changes in coordinated batch
3. **Phase 2: Validation** - Comprehensive testing and verification

### Iteration Strategy

**Fast Batch with Deep Breaking Changes Focus:**
- **Iteration 1**: Foundation sections (this iteration - complete)
- **Iteration 2**: Detailed project plan, package reference, breaking changes catalog
- **Iteration 3**: Risk management, testing strategy, success criteria

**Expected Total Iterations:** 3 (simple project structure allows focused execution)

**Complexity Drivers:** Not project count, but depth of architectural changes and API incompatibilities requiring detailed transformation patterns.

---

## Migration Strategy

### Approach Selection

**Selected Strategy: All-At-Once Strategy**

### Rationale

**Why All-At-Once:**

1. **Single Project Scope**: Only one project to migrate eliminates dependency coordination complexity
2. **No Multi-Targeting Possible**: ASP.NET Framework ? ASP.NET Core is an architectural shift, not a version upgrade—cannot maintain dual targets
3. **Atomic Dependencies**: All changes are interdependent:
   - SDK-style conversion enables modern framework targeting
   - Framework change requires package replacements
   - Package changes necessitate API updates
   - API updates affect all controllers simultaneously
4. **Manageable Size**: 425 LOC, 12 files is small enough to test comprehensively in single pass
5. **Cleaner Testing**: Single migration state to validate vs. managing multiple intermediate configurations

**Why Not Incremental:**
- No dependency tiers to phase
- Cannot partially migrate System.Web APIs (all-or-nothing)
- Would create artificial checkpoints without validation value

### All-At-Once Strategy Rationale

**Strategy Characteristics:**
- All project files updated simultaneously
- All package references updated in single operation
- All code changes applied together
- Single build/test cycle
- No intermediate states

**Specific Considerations for This Migration:**
1. **Architectural Uniformity**: ASP.NET Core requires consistent patterns across entire application (can't mix System.Web.Http and Microsoft.AspNetCore.Mvc)
2. **Configuration Coherence**: Global.asax.cs ? Program.cs is atomic replacement
3. **DI Container Consistency**: Must replace Autofac.WebApi2 integration across entire app
4. **Testing Efficiency**: Validate entire API surface once vs. managing partial migration states

### Dependency-Based Ordering

**Ordering Principles Applied:**
Since this is a single project, ordering applies to **operations within the migration**, not between projects:

**Operation Sequence:**
1. **Project Structure** (foundation for all other changes)
   - Convert to SDK-style project
   - Update TargetFramework property
2. **Package Dependencies** (enables code compilation)
   - Remove incompatible packages
   - Add ASP.NET Core packages
   - Update recommended packages
3. **Application Initialization** (establishes runtime behavior)
   - Create Program.cs
   - Configure services and middleware
   - Remove Global.asax.cs
4. **Code Updates** (leverage new APIs)
   - Migrate controllers (System.Web.Http ? Microsoft.AspNetCore.Mvc)
   - Update routing attributes
   - Fix compilation errors
5. **Build Verification** (confirm structural correctness)
   - Restore dependencies
   - Build solution
6. **Validation** (confirm functional correctness)
   - Run application
   - Test API endpoints

**All-At-Once Ordering Principles:**
- Operations within atomic upgrade follow natural dependency order
- Each operation builds on previous one's completion
- No operation can be validated independently (must reach buildable state)
- Success = entire sequence completes with 0 build errors

### Parallel vs Sequential Execution

**Execution Mode: Sequential (Required)**

**Justification:**
- Single project = no parallelization opportunities
- Operations are dependent (must execute in order)
- Atomic upgrade requires coordinated sequential execution

**No Parallel Phases:** All changes occur in Phase 1 atomic upgrade.

### Phase Definitions

**Phase 0: Preparation**

**Purpose:** Ensure environment readiness

**Activities:**
- Verify .NET 10.0 SDK installed
- Confirm branch setup (upgrade-to-NET10)
- Backup current state (already committed)

**Duration:** Minutes  
**Deliverable:** Environment ready for migration  
**Success Criteria:** SDK verification passes

---

**Phase 1: Atomic Upgrade**

**Purpose:** Execute complete framework and architectural migration

**Scope:** All project structure, package, code, and configuration changes

**Operations** (performed as single coordinated batch):

1. **Convert project to SDK-style**
   - Simplify .csproj to modern format
   - Remove unnecessary legacy elements
   
2. **Update target framework**
   - Change `<TargetFramework>` from net481 to net10.0
   
3. **Update package references**
   - Remove incompatible packages
   - Add ASP.NET Core framework reference
   - Add required ASP.NET Core packages
   - Update recommended package versions
   
4. **Migrate application initialization**
   - Create `Program.cs` with ASP.NET Core setup
   - Configure services (DI, EF, JSON serialization)
   - Configure middleware (routing, controllers)
   - Remove `Global.asax.cs` and `App_Start` folder
   
5. **Update controller code**
   - Replace `System.Web.Http` namespaces with `Microsoft.AspNetCore.Mvc`
   - Update base class: `ApiController` ? `ControllerBase`
   - Update return types: `IHttpActionResult` ? `IActionResult` or `ActionResult<T>`
   - Update attributes: `[FromUri]` ? `[FromQuery]`, etc.
   - Update action result methods: `Ok()`, `NotFound()` (same names, different types)
   
6. **Update repository/service code** (if needed)
   - Verify Entity Framework 6 compatibility
   - Adjust any System.Web dependencies in service layer
   
7. **Build solution and fix compilation errors**
   - Restore NuGet packages
   - Build project
   - Address any remaining API incompatibilities
   
8. **Rebuild and verify**
   - Confirm solution builds with 0 errors
   - Confirm no warnings related to deprecated APIs

**Duration:** Bulk of migration effort (relative: High complexity)  
**Deliverable:** Solution builds successfully on .NET 10.0  
**Success Criteria:** 
- Project is SDK-style
- TargetFramework is net10.0
- All packages compatible
- Solution builds with 0 errors
- No deprecated API warnings

---

**Phase 2: Validation**

**Purpose:** Confirm functional correctness of migrated application

**Activities:**
- Run application
- Test all API endpoints
- Verify database connectivity (if applicable)
- Validate JSON serialization behavior
- Confirm dependency injection working

**Duration:** Testing and verification (relative: Medium)  
**Deliverable:** Fully functional .NET 10.0 API  
**Success Criteria:**
- Application starts without errors
- All endpoints respond correctly
- Database operations succeed
- JSON responses match expected format
- No runtime errors

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

Testing occurs at three levels: build-time validation, runtime smoke testing, and comprehensive functional testing.

---

### Phase-by-Phase Testing Requirements

**Phase 0: Preparation**

**Testing:**
- ? Verify .NET 10.0 SDK installed: `dotnet --list-sdks`
- ? Confirm SDK version ? 10.0.0

**Success Criteria:**
- SDK verification command succeeds
- Version 10.x.x present in output

---

**Phase 1: Atomic Upgrade**

**Build-Time Validation:**

After each major operation (SDK conversion, package updates, code changes):

1. **Restore Validation**
   ```bash
   dotnet restore Clientes.WebApi\Clientes.WebApi.csproj
   ```
   - ? All packages restore successfully
   - ? No package conflict warnings
   - ? No version incompatibility errors

2. **Build Validation**
   ```bash
   dotnet build Clientes.WebApi\Clientes.WebApi.csproj
   ```
   - ? Build succeeds (exit code 0)
   - ? Zero compilation errors
   - ? No deprecated API warnings
   - ? Output confirms `net10.0` target

3. **Clean Build Validation** (final check)
   ```bash
   dotnet clean Clientes.WebApi\Clientes.WebApi.csproj
   dotnet build Clientes.WebApi\Clientes.WebApi.csproj --configuration Release
   ```
   - ? Clean build succeeds
   - ? Release configuration builds
   - ? No warnings related to migration

**Success Criteria:**
- Project is SDK-style format
- TargetFramework is net10.0
- All incompatible packages removed
- All recommended packages updated
- Solution builds with 0 errors
- No migration-related warnings

---

**Phase 2: Validation**

**Runtime Smoke Testing:**

1. **Application Startup**
   ```bash
   dotnet run --project Clientes.WebApi\Clientes.WebApi.csproj
   ```
   - ? Application starts without exceptions
   - ? Web server listening on configured port
   - ? No startup errors in console
   - ? DI container resolves all services

2. **Health Check** (if endpoint exists, or basic connectivity)
   - ? Application responds to HTTP requests
   - ? Routing middleware functioning

**Comprehensive Functional Testing:**

**API Endpoint Testing Matrix:**

| Endpoint | Method | Route | Test Scenario | Expected Result | Validation |
|----------|--------|-------|---------------|-----------------|------------|
| **ListarTodos** | GET | `/api/clientes` | Get all clients | 200 OK with array | JSON array, correct schema |
| **ListarTodos** | GET | `/api/clientes` | Empty database | 404 Not Found | Correct status code |
| **BuscarPorID** | GET | `/api/clientes/1` | Get existing client | 200 OK with object | JSON object, correct schema |
| **BuscarPorID** | GET | `/api/clientes/999` | Get non-existent client | 404 Not Found | Correct status code |
| **BuscarPorNome** | GET | `/api/clientes?nome=test` | Search by name | 200 OK with array | Query param binding works |
| **BuscarPorNome** | GET | `/api/clientes?nome=xyz` | No matches | 404 Not Found | Correct status code |
| **ContarClientes** | GET | `/api/clientes/contar` | Count clients | 200 OK with count | JSON: `{"Count": n}` |
| **Salvar** | POST | `/api/clientes` | Create new client | 200 OK with ID | JSON: `{"ID": n}`, DB persisted |
| **Deletar** | DELETE | `/api/clientes/1` | Delete existing client | 200 OK | DB record removed |
| **Deletar** | DELETE | `/api/clientes/999` | Delete non-existent | 404 Not Found | Correct status code |

**Testing Methods:**

**Option 1: Manual Testing with curl/Postman**
```bash
# GET all
curl -X GET http://localhost:5000/api/clientes

# GET by ID
curl -X GET http://localhost:5000/api/clientes/1

# POST new
curl -X POST http://localhost:5000/api/clientes \
  -H "Content-Type: application/json" \
  -d '{"Nome": "Test", "Email": "test@example.com"}'

# DELETE
curl -X DELETE http://localhost:5000/api/clientes/1
```

**Option 2: Automated Testing**
- If unit tests exist: Update test project to .NET 10, run `dotnet test`
- If integration tests exist: Update and run against migrated API
- If no tests: Consider creating basic smoke tests for critical paths

**Database Testing:**

If application uses database (Entity Framework detected):

1. **Connection Test**
   - ? Application connects to database
   - ? DbContext initializes correctly
   - ? No connection string errors

2. **CRUD Operations**
   - ? Create: New records persist
   - ? Read: Queries return correct data
   - ? Update: Changes save correctly (if update endpoint exists)
   - ? Delete: Records removed successfully

3. **EF6 Behavior Validation**
   - ? Lazy loading works (if enabled)
   - ? Navigation properties load
   - ? Transactions work correctly
   - ? No unexpected EF6-on-.NET-Core issues

**JSON Serialization Validation:**

1. **Response Format**
   - ? Property names correct (camelCase or PascalCase as expected)
   - ? Date formats consistent with previous behavior
   - ? Null handling matches expectations
   - ? Nested objects serialize correctly

2. **Compare Before/After** (if possible)
   - Capture JSON response from .NET Framework version
   - Capture JSON response from .NET 10 version
   - Compare for equivalence

3. **Special Cases**
   - ? Empty collections serialize correctly
   - ? Large objects don't cause serialization errors
   - ? BSON support works (if Newtonsoft.Json.Bson used)

**Performance Validation:**

*Not blocking for migration, but good to baseline:*

1. **Response Time**
   - Measure endpoint response times
   - Compare to .NET Framework baseline (if available)
   - Expect similar or better performance

2. **Memory Usage**
   - Monitor application memory during load
   - Check for memory leaks (sustained operation)

**Success Criteria:**
- All endpoints respond with correct HTTP status codes
- JSON responses match expected schema
- Database operations succeed (CRUD)
- No runtime exceptions
- No performance degradation (compared to baseline)

---

### Validation Checklists

**Pre-Migration Checklist:**
- [ ] .NET 10.0 SDK installed
- [ ] Current code committed to `main` branch
- [ ] Backup/snapshot created (if production data)
- [ ] `upgrade-to-NET10` branch created
- [ ] Dependencies documented (for rollback reference)

**Post-SDK-Conversion Checklist:**
- [ ] Project file is SDK-style format
- [ ] Project loads in IDE without errors
- [ ] Git shows expected changes (simplified .csproj)

**Post-Package-Update Checklist:**
- [ ] Incompatible packages removed
- [ ] Framework-included packages removed
- [ ] Recommended packages updated to latest versions
- [ ] New ASP.NET Core packages added
- [ ] `dotnet restore` succeeds

**Post-Code-Migration Checklist:**
- [ ] Program.cs created with correct DI registrations
- [ ] Controllers updated (namespaces, attributes, return types)
- [ ] Global.asax.cs deleted
- [ ] App_Start folder deleted
- [ ] No `using System.Web.Http` statements remain
- [ ] `dotnet build` succeeds with 0 errors

**Pre-Merge Checklist:**
- [ ] All smoke tests pass
- [ ] All functional tests pass
- [ ] Database connectivity verified
- [ ] JSON serialization validated
- [ ] No runtime errors observed
- [ ] Code reviewed (if team process requires)
- [ ] Documentation updated (README, deployment docs)

---

### Test Data Requirements

**Minimum Test Data:**
- At least 1 existing client record (for GET by ID, DELETE)
- At least 2 client records (for GET all, search)
- Client with searchable name (for BuscarPorNome)

**Test Database:**
- Use development/test database (not production)
- Ensure connection string points to correct environment
- Verify database schema exists (if using Code-First, migrations applied)

---

### Continuous Validation During Migration

**After each code change:**
1. Save files
2. Run `dotnet build`
3. Fix errors immediately
4. Don't accumulate errors

**After major milestones:**
1. Commit changes to branch
2. Run full build
3. Document any issues encountered

**Before final commit:**
1. Clean build
2. Full smoke test
3. Validate all endpoints
4. Review changes (`git diff`)

---

## Source Control Strategy

### Branching Strategy

**Primary Branches:**
- **`main`** - Production-ready code, remains on .NET Framework 4.8.1 until migration complete and validated
- **`upgrade-to-NET10`** - Migration working branch, all changes isolated here

**Branch Workflow:**
```
main (net481) ?????> upgrade-to-NET10 (net481 ? net10.0)
                ?
                ???> (After validation) merge back to main (net10.0)
```

**Protection:**
- `main` branch remains untouched during migration
- Provides safe rollback point
- Can continue hotfixes on `main` if needed (though merge conflicts likely)

**Branch Lifecycle:**
1. Create `upgrade-to-NET10` from `main` ? **COMPLETED**
2. Perform all migration work on `upgrade-to-NET10`
3. Validate thoroughly on branch
4. Merge to `main` only after full validation
5. Optionally delete `upgrade-to-NET10` after successful merge (or keep for reference)

---

### Commit Strategy

**Approach: Single Comprehensive Commit** (All-At-Once Strategy alignment)

**Rationale:**
- All changes are interdependent (cannot validate partial migration)
- Atomic upgrade means atomic commit makes logical sense
- Enables clean rollback (single commit to revert)
- Git history shows clear "before/after" migration point

**Commit Structure:**
- **Option 1: Single Atomic Commit (Recommended for All-At-Once)**
```
Commit 1: "Migrate to .NET 10.0 - Complete framework upgrade"
  - Convert project to SDK-style
  - Update target framework to net10.0
  - Update package references
  - Create Program.cs
  - Migrate controllers to ASP.NET Core
  - Remove Global.asax.cs and App_Start
  - Update all API references
```

- **Option 2: Phased Commits (If preferred for granular history)**
```
Commit 1: "Convert project to SDK-style format"
Commit 2: "Update target framework and packages"
Commit 3: "Migrate application initialization to Program.cs"
Commit 4: "Update controllers to ASP.NET Core"
Commit 5: "Remove legacy files"
```

**Recommendation:** **Single comprehensive commit** aligns with All-At-Once strategy, simpler rollback.

---

### Commit Message Format

**Template:**
```
Migrate to .NET 10.0 from .NET Framework 4.8.1

BREAKING CHANGE: Complete framework migration to .NET 10.0

Changes:
- Converted Clientes.WebApi.csproj to SDK-style format
- Updated target framework: net481 ? net10.0
- Replaced System.Web.Http with Microsoft.AspNetCore.Mvc
- Migrated from Autofac to ASP.NET Core built-in DI
- Created Program.cs for application initialization
- Removed Global.asax.cs and App_Start/WebApiConfig.cs
- Updated package references:
  * Removed: Autofac.WebApi2, Microsoft.AspNet.WebApi.Core, Microsoft.AspNet.WebApi.WebHost
  * Added: Microsoft.AspNetCore.Mvc.NewtonsoftJson
  * Updated: Microsoft.Bcl.AsyncInterfaces (10.0.1), System.Diagnostics.DiagnosticSource (10.0.1)
- Updated controller API patterns (IHttpActionResult ? ActionResult<T>)
- Migrated routing attributes ([RoutePrefix] ? [Route])
- Updated parameter binding ([FromUri] ? [FromQuery])

Validated:
- Build: 0 errors, 0 warnings
- Runtime: All API endpoints functional
- Database: Entity Framework 6 compatible
- JSON: Serialization behavior verified

Migration plan: .github/upgrades/plan.md
Assessment: .github/upgrades/assessment.md
```

**Key Elements:**
- Clear subject line
- "BREAKING CHANGE" marker (semantic versioning signal)
- Itemized changes
- Validation confirmation
- Reference to migration documentation

---

### Commit Checkpoints

**When to Commit:**

- **Option 1: Single Commit After Full Validation**
1. Complete all Phase 1 changes
2. Verify build succeeds (0 errors)
3. Run Phase 2 validation (all endpoints tested)
4. **THEN** commit

**Checkpoints:**
- Pre-commit: Build succeeds, smoke tests pass
- Commit message: Comprehensive change list
- Post-commit: Tag commit for easy reference

- **Option 2: Incremental Commits (if issues require experimentation)**
1. Commit after SDK conversion (if successful and validated)
2. Commit after package updates (if restore succeeds)
3. Commit after code migration (if build succeeds)
4. Squash commits before merge (clean history)

**Recommendation for All-At-Once:** Option 1 (single commit after full validation)

---

### Review and Merge Process

**Pre-Merge Review:**

**Self-Review Checklist:**
- [ ] Review all file changes: `git diff main..upgrade-to-NET10`
- [ ] Verify no unintended changes (debugging code, temp files)
- [ ] Confirm all legacy files removed
- [ ] Check for hardcoded values (connection strings, URLs)
- [ ] Validate .gitignore covers new build artifacts (`bin/`, `obj/`)

**Code Review** (if team process requires):
- Create pull request: `upgrade-to-NET10` ? `main`
- Review focus areas:
  - SDK-style project correctness
  - DI registrations match original Autofac config
  - Controller API contracts unchanged (routes, parameters)
  - Removed files don't contain critical logic
- Validation evidence: Build logs, test results

**Merge Criteria:**
- ? All Phase 1 success criteria met (builds with 0 errors)
- ? All Phase 2 success criteria met (all endpoints validated)
- ? Code review approved (if applicable)
- ? Documentation updated
- ? No security vulnerabilities introduced

**Merge Method:**
- **Option 1: Merge Commit (preserves branch history)**
```bash
git checkout main
git merge upgrade-to-NET10 --no-ff -m "Merge .NET 10.0 migration"
```

- **Option 2: Squash Merge (clean single commit in main history)**
```bash
git checkout main
git merge upgrade-to-NET10 --squash
git commit -m "Migrate to .NET 10.0 - Complete framework upgrade"
```

- **Option 3: Rebase (linear history)**
```bash
git checkout upgrade-to-NET10
git rebase main
git checkout main
git merge upgrade-to-NET10 --ff-only
```

**Recommendation:** **Merge commit** or **squash merge** (preserves clear migration point in history).

---

### All-At-Once Strategy Source Control Guidance

**Atomic Migration = Atomic Commit Pattern:**

Since All-At-Once strategy performs entire migration in single coordinated operation:

1. **Work in Progress:**
   - All changes happen on `upgrade-to-NET10` branch
   - No commits until migration reaches stable state
   - Use `git stash` if need to experiment with alternatives

2. **Stable Checkpoint:**
   - Commit when build succeeds AND smoke tests pass
   - Single comprehensive commit captures entire transformation
   - Commit message documents all changes (package updates, code changes, file deletions)

3. **Validation:**
   - Keep commit on branch during Phase 2 validation
   - If issues found, amend commit with fixes (before merge)
   - Only merge to `main` after full validation complete

4. **History:**
   - `main` history shows single clear migration commit
   - Easy to identify "before migration" vs "after migration" states
   - Clean rollback: revert single commit if needed

**Commit Timeline Example:**
```
main: A -- B -- C -- D -- E (net481, before merge)
               \
upgrade-to-NET10:  \-- M1 (net10.0, atomic migration commit)
                       |
                       | (validation period)
                       |
main: A -- B -- C -- D -- E -- M1 (net10.0, after merge)
```

---

### Post-Merge Actions

**After successful merge to `main`:**

1. **Tag Release:**
   ```bash
   git tag -a v2.0.0-net10 -m "Migrated to .NET 10.0"
   git push origin v2.0.0-net10
   ```

2. **Update Documentation:**
   - README.md: Update .NET version requirements
   - Deployment docs: Update runtime requirements
   - Developer setup: .NET 10.0 SDK required

3. **Branch Cleanup** (optional):
   ```bash
   git branch -d upgrade-to-NET10  # Delete local branch
   git push origin --delete upgrade-to-NET10  # Delete remote branch
   ```
   Or keep for historical reference.

4. **CI/CD Updates:**
   - Update build pipelines to use .NET 10.0 SDK
   - Update deployment targets (runtime requirements)
   - Update Docker images (if containerized)

---

### Handling Conflicts

**If `main` receives updates during migration:**

1. **Assess Changes:**
   - Review what changed on `main`
   - Determine if affects migration

2. **Merge `main` into `upgrade-to-NET10`:**
   ```bash
   git checkout upgrade-to-NET10
   git merge main
   # Resolve conflicts
   # Re-test migration
   ```

3. **Re-validate:**
   - Build and test after conflict resolution
   - Ensure migration still correct

**Prevention:** 
- Communicate migration in progress
- Consider branch protection or freeze on `main` during migration
- Complete migration in reasonable timeframe (minimize drift)

---

### Backup Strategy

**Pre-Migration Backup:**
- Current state committed to `main` ? **COMPLETED**
- Branch `upgrade-to-NET10` created ? **COMPLETED**

**Additional Backups** (if critical environment):
- Tag `main` before merge: `git tag pre-net10-migration`
- Export current build artifacts (if deploying to production)
- Database backup (if schema changes expected, though unlikely for this migration)

**Recovery:**
- Branch deletion recovery: `git reflog`, `git checkout -b upgrade-to-NET10 <commit-hash>`
- Commit revert: `git revert <commit-hash>`
- Force reset (last resort): `git reset --hard <commit-hash>`

---

## Success Criteria

### Technical Criteria

**Project Structure:**
- ? `Clientes.WebApi.csproj` is SDK-style format
  - Uses `<Project Sdk="Microsoft.NET.Sdk.Web">`
  - Simplified structure (~20-30 lines vs 100+)
  - No explicit file inclusions for standard patterns
- ? Target framework is `net10.0`
  - `<TargetFramework>net10.0</TargetFramework>` in project file
  - Build output confirms .NET 10.0 runtime

**Package References:**
- ? All incompatible packages removed:
  - No `Autofac.WebApi2`
  - No `Microsoft.AspNet.WebApi.Core`
  - No `Microsoft.AspNet.WebApi.WebHost`
- ? Framework-included packages removed:
  - No `Microsoft.AspNet.WebApi`
  - No `Microsoft.CodeDom.Providers.DotNetCompilerPlatform`
  - No `System.Buffers`, `System.Memory`, `System.Numerics.Vectors`, `System.Threading.Tasks.Extensions`
- ? Recommended packages updated:
  - `Microsoft.Bcl.AsyncInterfaces` 10.0.1 (or latest compatible)
  - `System.Diagnostics.DiagnosticSource` 10.0.1 (or latest compatible)
  - `System.Runtime.CompilerServices.Unsafe` 6.1.2 (or latest compatible)
- ? New packages added:
  - `Microsoft.AspNetCore.Mvc.NewtonsoftJson` (for JSON compatibility)
- ? Compatible packages retained:
  - `EntityFramework` 6.5.1
  - `Newtonsoft.Json` 13.0.4
  - Others as needed

**Code Quality:**
- ? All files use correct namespaces:
  - No `using System.Web.Http` statements
  - Controllers use `using Microsoft.AspNetCore.Mvc`
- ? Controllers properly migrated:
  - Inherit from `ControllerBase`
  - Use `[ApiController]` attribute
  - Return `ActionResult<T>` or `IActionResult`
  - Use `[FromQuery]` instead of `[FromUri]`
  - Use `[Route]` on class (not `[RoutePrefix]`)
- ? Application initialization migrated:
  - `Program.cs` exists with proper setup
  - `Global.asax.cs` deleted
  - `App_Start/WebApiConfig.cs` deleted
- ? Dependency injection configured:
  - Services registered in `Program.cs`
  - No Autofac.WebApi2 references
  - DI container resolves all required services

**Build Success:**
- ? `dotnet restore` completes without errors
- ? `dotnet build` completes without errors
  - Exit code: 0
  - Error count: 0
- ? No migration-related warnings
  - No deprecated API warnings
  - No package compatibility warnings
- ? Build output confirms net10.0 target
- ? Clean release build succeeds:
  - `dotnet build --configuration Release` succeeds

**All-At-Once Strategy Criteria:**
- ? All project files updated simultaneously
- ? All package references updated in single operation
- ? All code changes applied together
- ? Solution reaches buildable state atomically (no intermediate half-migrated states)

---

### Quality Criteria

**Code Quality Maintained:**
- ? No code duplication introduced
- ? Service/repository layers unchanged (isolated from framework)
- ? Business logic intact
- ? Error handling patterns preserved

**Test Coverage Maintained:**
- ? If unit tests existed: Tests updated to .NET 10 and passing
- ? If integration tests existed: Tests updated and passing
- ? If no automated tests: Manual validation complete

**Documentation Updated:**
- ? README.md reflects .NET 10.0 requirement
- ? Developer setup instructions updated
- ? Deployment documentation updated (if exists)
- ? Migration plan documented (this file)
- ? Assessment referenced for technical details

**API Contract Preserved:**
- ? All endpoints remain at same routes
- ? HTTP methods unchanged (GET, POST, DELETE)
- ? Request/response schemas unchanged
- ? HTTP status codes behave identically
- ? JSON serialization produces compatible output

**Security Maintained:**
- ? No new security vulnerabilities introduced
- ? `dotnet list package --vulnerable` shows 0 vulnerabilities
- ? HTTPS configuration preserved (if applicable)
- ? Authentication/authorization unchanged (if implemented)

---

### Process Criteria

**All-At-Once Strategy Followed:**
- ? All changes performed in coordinated batch
- ? No partial migration states
- ? Single validation checkpoint (buildable + functional)
- ? All-At-Once ordering principles applied:
  - Project structure updated first
  - Packages updated second
  - Code migrated third
  - Build verification fourth

**Source Control Strategy Followed:**
- ? All work performed on `upgrade-to-NET10` branch
- ? `main` branch remains untouched until validation complete
- ? Single comprehensive commit (or squashed commits)
- ? Commit message documents all changes
- ? Code reviewed (if team process requires)

**Validation Complete:**
- ? Build validation passed
- ? Runtime smoke tests passed
- ? All API endpoints tested
- ? Database connectivity verified
- ? JSON serialization validated
- ? No runtime exceptions observed

---

### Functional Criteria

**Application Runs Successfully:**
- ? `dotnet run` starts application without errors
- ? Web server listens on expected port
- ? No startup exceptions
- ? Dependency injection resolves all services

**All Endpoints Functional:**

| Endpoint | Validation Criteria | Status |
|----------|---------------------|--------|
| **GET /api/clientes** | Returns list of clients or 404 | ? |
| **GET /api/clientes/{id}** | Returns client or 404 | ? |
| **GET /api/clientes?nome={nome}** | Search works, query param binding | ? |
| **GET /api/clientes/contar** | Returns count in expected format | ? |
| **POST /api/clientes** | Creates client, returns ID | ? |
| **DELETE /api/clientes/{id}** | Deletes client or returns 404 | ? |

**Database Operations:**
- ? Database connection succeeds
- ? Entity Framework 6 queries execute
- ? CRUD operations work correctly
- ? Transactions commit successfully
- ? Connection pooling works (no connection leaks)

**JSON Serialization:**
- ? Responses serialize to JSON correctly
- ? Property naming matches expectations (camelCase/PascalCase)
- ? Date formats compatible with clients
- ? Null handling matches previous behavior
- ? Complex objects (if any) serialize correctly

**Performance Acceptable:**
- ? Response times comparable to .NET Framework version (baseline)
- ? No memory leaks observed
- ? No excessive CPU usage

---

### Deployment Readiness

**Environment Requirements Documented:**
- ? .NET 10.0 Runtime required for deployment
- ? Hosting environment compatibility verified (IIS, Kestrel, Docker, etc.)
- ? Connection strings configured correctly
- ? Environment variables documented (if used)

**CI/CD Updated** (if applicable):
- ? Build pipeline uses .NET 10.0 SDK
- ? Deployment scripts updated for .NET 10 runtime
- ? Docker images updated (if containerized)

---

### Definition of Done

**The migration is complete and successful when:**

1. **All Technical Criteria Met:**
   - SDK-style project
   - net10.0 target
   - Packages updated
   - Code migrated
   - Builds with 0 errors

2. **All Quality Criteria Met:**
   - Code quality maintained
   - Documentation updated
   - API contracts preserved
   - Security maintained

3. **All Functional Criteria Met:**
   - Application runs
   - All endpoints work
   - Database operations succeed
   - JSON serialization correct

4. **All Process Criteria Met:**
   - All-At-Once strategy followed
   - Source control strategy followed
   - Validation complete

5. **Ready for Deployment:**
   - Environment requirements documented
   - CI/CD updated (if applicable)
   - Team trained on .NET 10 deployment

**Merge to `main` Approved When:**
- All above criteria satisfied
- Code review approved (if required)
- Stakeholder signoff obtained (if required)

---

### Post-Migration Success Indicators

**Short-Term (First Week):**
- ? Application runs in production without errors
- ? All monitoring shows healthy status
- ? No increase in error rates
- ? Performance metrics stable or improved

**Medium-Term (First Month):**
- ? No regression bugs reported
- ? Development velocity maintained or improved
- ? Team comfortable with ASP.NET Core patterns
- ? Can leverage .NET 10 features for future enhancements

**Long-Term Benefits:**
- ? Access to latest .NET features and performance improvements
- ? Long-term support (LTS) until 2031
- ? Improved cross-platform deployment options
- ? Modern development experience
- ? Easier to attract/retain developers (modern stack)
