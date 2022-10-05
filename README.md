# Integration tests using top-level statements

Top-level statements are a handy compiler feature introduced with .NET 6.0 that allows us to implicitly specify our app entry-point by writing statements outside of a type declaration:

```csharp
// So, rather than this...
using System;

namespace SomeNamespace
{
	class SomeClass
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello world!");
		}
	}
}
```

```csharp
// ...now we only have to write this
Console.WriteLine("Hello world!");
```

To simplify things, the CLR will automatically handle preprocessor directives (in this case, `using` statements for `System` classes) as well as automatically generate a Main(), or *entry-point*, method on our behalf as long as we write our top-level statements **before any type declarations**.

## Implementation

The console app itself is an implementation of Microsoft's [magic .NET answer machine](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/top-level-statements) introducing developers to top-level statements. This implementation goes slightly further and implements an IoC Container, the [default IHost implementation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0), to map a configuration source to a DTO - helping to segregate our business logic from our application logic.

There are an abundance of great tutorials for console apps using top-level statements, however one thing missing from the conversation is how to leverage our entry-point method for integration testing. Testing a main method is something of a taboo in the field of software engineering, however it's important not to let high-level output be forgotten about when implementing a testing strategy, which usually focuses on TDD and incrementing functionality unit-by-unit. The problem is that, in real life production enviroments, smaller units are refactored constantly and cannot be relied on as a metric for testing business logic. As an example, no web application developer would forget to test HTTP contexts when building an API controller, and this is essentially what Main() is for our humble console app - the place where multiple components come together, and where it's our job as the developer to verify the output is what we expect.

## Bootstrap

Simply clone and type `dotnet run --project ./MagicAnswerMachine.Client/MagicAnswerMachine.Client.csproj -- Are you working?` into your command line, then let the man speak for himself (he loves to sarcastically tell me *"My reply is no."* ¯\_(ツ)_/¯).

If you want to print test case outcomes to your CLI run `dotnet test ./MagicAnswerMachine.Tests`[^1].

[^1] Using an 'xunit.runner.json' configuration file, you can [specify that xUnit run tests in order rather than parallel](https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel).