// -------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="OldPhonePadApp">
// Copyright (c) OldPhonePadApp. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OldPhonePadApp
{
    /// <summary>
    /// Entry point of the OldPhonePadApp program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of the application.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static void Main(string[] args)
        {
            // Configure dependency injection and logging
            var serviceProvider = new ServiceCollection()
                .AddLogging(config =>
                {
                    config.ClearProviders();
                    config.AddConsole();
                    config.SetMinimumLevel(LogLevel.Warning);
                })
                .AddSingleton<OldPhonePadProcessor>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var processor = serviceProvider.GetRequiredService<OldPhonePadProcessor>();

            logger.LogInformation("Application started.");

            Console.WriteLine("Welcome to the Old Phone Pad App!");
            Console.WriteLine("Type 'exit' to quit.");
            Console.WriteLine("Enter your input sequence (e.g., 4433555 555666#):");

            while (true)
            {
                try
                {
                    Console.Write("Input: ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
                    {
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;
                    }

                    if (!input.EndsWith("#"))
                    {
                        Console.WriteLine("Invalid input. Make sure your input ends with '#'.");
                        continue;
                    }

                    string result = processor.ProcessInput(input);

                    Console.WriteLine($"Decoded Message: {result}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while processing the input.");
                    Console.WriteLine("An error occurred. Please try again.");
                }
            }

            logger.LogInformation("Application finished.");
        }
    }
}
