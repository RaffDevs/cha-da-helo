using System;

namespace ChaHelo.Application.Services;

public class LogService
{
    public static void LogHere(Type type, string message, Exception ex)
    {
        Console.WriteLine($"{nameof(type)} : {message} - {ex.Message}");
    }
}
