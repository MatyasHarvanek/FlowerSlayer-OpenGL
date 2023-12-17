namespace FlowerSlayer.Helpers;

public static class FileHelper
{
    public static string? GetStringFromFile(string path)
    {
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            return text;
        }
        return null;
    }
}