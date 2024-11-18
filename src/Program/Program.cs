//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Grupo 5 N1 😎.
// </copyright>
//--------------------------------------------------------------------------------

using DiscordBot;
using Library;
using Library.DiscordBot;
using Library.StaticClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Program;

/// <summary>
/// Programa de consola de demostración.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Punto de entrada al programa principal.
    /// </summary>
    private static void Main(string[] args)
    {
        Facade.Start();
        MainAsync(args).GetAwaiter().GetResult();
        // Runbot
    }

    private static async Task MainAsync(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddScoped<IBot, Bot>()
            .BuildServiceProvider();

        try
        {
            IBot bot = serviceProvider.GetRequiredService<IBot>();

            await bot.StartAsync(serviceProvider);

            Console.WriteLine("Connected to Discord");

            do
            {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("\nShutting down!");

                    await bot.StopAsync();
                    return;
                }
            } while (true);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            Environment.Exit(-1);
        }
    }
}