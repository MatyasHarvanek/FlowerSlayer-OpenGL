using System.Linq.Expressions;
using FlowerSlayer.Helpers;

namespace FlowerSlayer.Managers;

public static class VAOManager
{
    private static Dictionary<string, VAO> _VAOs = new Dictionary<string, VAO>();


    public static VAO GetVAO(string name)
    {
        return _VAOs[name];
    }

    public static VAO AddVAO(string name, string modelName)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "Models\\" + modelName;

        string? modelText = FileHelper.GetStringFromFile(path);
        if (modelText == null)
        {
            throw new FileNotFoundException($"Model with path {path} does not exists");
        }

        List<uint> indices = new();
        List<float> vertices = new();

        ParseModelText(modelText, ref indices, ref vertices);
        VAO final = new VAO(name, vertices.ToArray(), indices.ToArray());
        _VAOs.Add(name, final);
        return final;
    }

    private static void ParseModelText(string modelText, ref List<uint> indices, ref List<float> vertices)
    {
        foreach (string currentLine in modelText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (currentLine[0] == '#')
            {
                continue;
            }

            if (currentLine.StartsWith("v "))
            {
                string proccesedLine = currentLine.Replace("v ", string.Empty);
                vertices.AddRange(proccesedLine.Split(" ").Select(x => float.Parse(x)));
                continue;
            }

            if (currentLine.StartsWith("vt"))
            {
                //TODO Add textures
                continue;
            }

            if (currentLine.StartsWith("f "))
            {
                string proccesedLine = currentLine.Replace("f ", string.Empty);
                indices.AddRange(proccesedLine.Split(" ").Select(x => uint.Parse(x.Split("/")[0]) - 1));
            }

        }
    }


}