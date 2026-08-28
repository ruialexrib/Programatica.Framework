<div align="center">

<img src="logo.png" alt="Programatica Framework" width="180" />

# Programatica Framework

### A modular .NET application development starter framework.

[![.NET](https://github.com/ruialexrib/Programatica.Framework/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ruialexrib/Programatica.Framework/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
![GitHub repo size](https://img.shields.io/github/repo-size/ruialexrib/Programatica.Framework)
![GitHub top language](https://img.shields.io/github/languages/top/ruialexrib/Programatica.Framework)
![GitHub downloads](https://img.shields.io/github/downloads/ruialexrib/Programatica.Framework/total)

Developed by [Rui Ribeiro](https://github.com/ruialexrib)

</div>

---

## About

**Programatica Framework** is a modular .NET starter framework designed to provide common application infrastructure out of the box.

It brings together reusable components for the application core, data access, services, MVC applications and utility extensions, reducing the amount of repetitive setup required when starting a new project.

The goal is simple: provide a reusable foundation so development can focus on **business entities, application rules and domain-specific functionality** instead of repeatedly implementing common infrastructure.

## Main Components

Programatica Framework is organized into five main packages:

| Package | Purpose | NuGet |
| --- | --- | --- |
| **Programatica.Framework.Core** | Core abstractions, adapters, attributes, exceptions and shared functionality | ![NuGet](https://img.shields.io/nuget/v/Programatica.Framework.Core) |
| **Programatica.Framework.Core.Extensions** | Reusable .NET extension methods | ![NuGet](https://img.shields.io/nuget/v/Programatica.Framework.Core.Extensions) |
| **Programatica.Framework.Data** | Data access infrastructure, DbContext support and repository functionality | ![NuGet](https://img.shields.io/nuget/v/Programatica.Framework.Data) |
| **Programatica.Framework.Services** | Reusable service-layer infrastructure | ![NuGet](https://img.shields.io/nuget/v/Programatica.Framework.Services) |
| **Programatica.Framework.Mvc** | Components and helpers for MVC applications | ![NuGet](https://img.shields.io/nuget/v/Programatica.Framework.Mvc) |

## Features

The framework provides reusable building blocks for common application development requirements, including:

- Base domain objects
- Base `DbContext` ready for dependency injection
- Generic Repository infrastructure
- Base service layer
- Dependency injection support
- Model and collection extensions
- Common system models
- Audit support
- Entity change tracking
- Authentication and user adapters
- Date and time abstraction
- JSON serialization abstraction
- Security utilities
- Framework-specific exceptions
- MVC application helpers

## Architecture

```text
Application
    │
    ├── Programatica.Framework.Mvc
    │
    ├── Programatica.Framework.Services
    │
    ├── Programatica.Framework.Data
    │
    ├── Programatica.Framework.Core.Extensions
    │
    └── Programatica.Framework.Core
```

The packages are separated by responsibility, allowing applications to consume only the parts of the framework they require.

## NuGet Packages

The framework components are distributed independently through NuGet.

```bash
dotnet add package Programatica.Framework.Core
dotnet add package Programatica.Framework.Core.Extensions
dotnet add package Programatica.Framework.Data
dotnet add package Programatica.Framework.Services
dotnet add package Programatica.Framework.Mvc
```

Depending on the application, it may not be necessary to install every package.

## Getting Started

Clone the repository:

```bash
git clone https://github.com/ruialexrib/Programatica.Framework.git
cd Programatica.Framework
```

Restore the dependencies:

```bash
dotnet restore Programatica.Framework.sln
```

Build the solution:

```bash
dotnet build Programatica.Framework.sln
```

## Repository Structure

```text
Programatica.Framework/
├── Programatica.Framework.Core/
├── Programatica.Framework.Core.Extensions/
├── Programatica.Framework.Data/
├── Programatica.Framework.Services/
├── Programatica.Framework.Mvc/
├── Programatica.Framework.sln
├── .github/workflows/
├── LICENSE
└── README.md
```

## Sample Applications

Example applications demonstrating the framework are available in separate repositories:

- [Programatica.DummyApp.Mvc](https://github.com/ruialexrib/Programatica.DummyApp.Mvc)
- [Programatica.DummyApp.Console](https://github.com/ruialexrib/Programatica.DummyApp.Console)

## License

Distributed under the [MIT License](LICENSE).

Copyright © 2020 [Rui Ribeiro](https://github.com/ruialexrib).
