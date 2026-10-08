# Testcontainers C# .NET 10 Example

## Quick Start

```bash
dotnet restore

./check.sh

dotnet test

dotnet watch run --project src/MyApp
```

## Init Project

```bash
dotnet new blazor -o src/MyApp --interactivity Server
dotnet new mstest -o tests/MyApp.Tests

dotnet add tests/MyApp.Tests reference src/MyApp

dotnet new sln -n MyApp
dotnet sln MyApp.slnx add src/MyApp
dotnet sln MyApp.slnx add tests/MyApp.Tests
```

```bash
# create UseArtifactsOutput true in Directory.Build.props
dotnet new buildprops --use-artifacts

# enable CPM (Central Package Management) in Directory.Packages.props
dotnet new packagesprops

# to ensure dotnet version
dotnet new globaljson
```

```bash
dotnet package list --outdated

dotnet package update
```

```bash
dotnet add src/MyApp package SonarAnalyzer.CSharp

dotnet add src/MyApp package Microsoft.EntityFrameworkCore.Design
dotnet add src/MyApp package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add tests/MyApp.Tests package Testcontainers.PostgreSql
```
