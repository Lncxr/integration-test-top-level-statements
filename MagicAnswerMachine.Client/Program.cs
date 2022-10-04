// Top-level statements
// ---------------------
// This is a compiler feature introduced in C# 9 (.NET 5)
// that allows us to implicitly specify our app entry-point 
// by writing statements outside of a type declaration
// _____________________
// See https://aka.ms/new-console-template for more information

#region Using statements
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MagicAnswerMachine.Client.Models;
#endregion

#region Main() routine
// CLR will automatically include 'string[] args' in the generated entry-point method's signature
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.Sources.Clear();

        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices(services =>
    {
        services.AddScoped<IResponse, Response>();
    })
    .Build();

try
{
    // Guard case
    if (args is null || args.Length <= 0)
        throw new Exception("No args supplied!");

    foreach (var s in args)
        Console.Write($"{s} ");

    Console.WriteLine();

    Console.WriteLine($"{host.Services.GetRequiredService<IResponse>().ReturnRandomAnswer()}");
}
catch (Exception exception)
{
    Console.WriteLine($"Exception in main routine : {exception}");
    throw;
}
finally
{
    // host.RunAsync();
    host.Dispose();
}
#endregion

// We'll need to override the protection level (the default access
// modifier is 'internal') so we can test this class via reflection
public partial class Program { }

