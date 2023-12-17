namespace FlowerSlayer.Logging;

public static class Logger
{
    public static void LogInfo(string message)
    {
        Log(ConsoleColor.Cyan, "Info", message);
    }

    public static void LogWarning(string message)
    {
        Log(ConsoleColor.Magenta, "Warning", message);
    }
    public static void LogError(string message)
    {
        Log(ConsoleColor.Red, "Error", message);
    }

    private static void Log(ConsoleColor tagColor, string tagName, string message)
    {
        Console.ForegroundColor = tagColor;
        Console.WriteLine("");
        Console.Write(DateTime.Now.ToString("d.M yyyy h:m:s"));
        Console.Write($" [{tagName}] ");
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

// public enum LogType
// {
//     Info = 0,
//     Warning = 1,
//     Error = 2
// }