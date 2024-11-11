# Qué hay configurado en esta plantilla

1. Un proyecto de biblioteca (creado con [`dotnet new classlib --name Library`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22)) en la carpeta `src\Library`
2. Un proyecto de aplicación de consola (creado con [`dotnet new console --name Program`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22)) en la carpeta `src\Program`
3. Un proyecto de prueba en [NUnit](https://nunit.org/) (creado con [`dotnet new nunit --name LibraryTests`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22)) en la carpeta `test\LibraryTests`
4. Un proyecto de [Doxygen](https://www.doxygen.nl/index.html) para generación de sitio web de documentación en la carpeta `docs`
5. Análisis estático con [Roslyn analyzers](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview) en los proyectos de biblioteca y de aplicación
6. Análisis de estilo con [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/README.md) en los proyectos de biblioteca y de aplicación
7. Una solución `ProjectTemplate.sln` que referencia todos los proyectos de C# y facilita la compilación con [`dotnet build`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build).
8. Tareas preconfiguradas para compilar y ejecutar los proyectos, ejecutar las pruebas, y generar documentación desde VSCode en la carpeta `.vscode`
9. Análisis de cobertura de los casos de prueba mediante []() que aparece en los márgenes con el complemento de VS Code [Coverage Gutters](https://marketplace.visualstudio.com/items?itemName=ryanluker.vscode-coverage-gutters).
10. Ejecución automática de compilación y prueba mediante [GitHub Actions](https://docs.github.com/en/actions) configuradas en el repositorio al hacer [push](https://github.com/git-guides/git-push) o [pull request](https://docs.github.com/en/github/collaborating-with-pull-requests).

Vean este 🎥 [video](https://web.microsoftstream.com/video/55c6a06c-07dc-4f95-a96d-768f198c9044) que explica el funcionamiento de la plantilla.

## Convenciones

[Convenciones de código en C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)

[Convenciones de nombres en C#](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

## Dónde encontrar información sobre los errores/avisos al compilar

[C# Compiler Errors (CS*)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/)

[Roslyn Analyzer Warnings (CA*)](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/categories)

[StyleCop Analyzer Warnings (SA*)](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md)

# Cómo deshabilitar temporalmente los avisos al compilar

## Roslyn Analyzer

Comentar las siguientes líneas en los archivos de proyecto (`*.csproj`)
```
    <EnableNETAnalyzers>true</EnableNETAnalyzers>
    <AnalysisMode>AllEnabledByDefault</AnalysisMode>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
```

## StyleCop Analyzer

Comentar la línea `<PackageReference Include="StyleCop.Analyzers" Version="1.1.118"/>` en los archivos de proyecto (`*.csproj`)
