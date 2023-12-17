using System.Globalization;
using FlowerSlayer.Logging;
using FlowerSlayer.Objects;
using OpenTK.Graphics.OpenGL;

namespace FlowerSlayer.Managers;

//TODO add cleanup function
public static class ShaderManager
{
    private static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

    public static Shader AddShader(string name, string vertexName, string fragmentName)
    {
        Shader shader = new(vertexName, fragmentName, name);
        Shaders.Add(name, shader);
        return shader;
    }

    private static void RemoveShader(string shaderName)
    {
        var shader = Shaders[shaderName];

        if (shader == null)
        {
            Logger.LogWarning("Could not remove shade with name:" + shaderName + " because shader with this name doesn't exists!");
            return;
        }
        GL.DeleteProgram(shader.Handle);
        Logger.LogInfo("Shader with name:" + shaderName + " was successfully removed");
        Shaders.Remove(shaderName);
    }

    public static Shader GetShaderByName(string shaderName)
    {
        var shader = Shaders[shaderName];
        if (shader == null)
        {
            throw new NullReferenceException("Shader was not found. ShaderName:" + shaderName);
        }
        return shader;
    }
    public static Shader? GetShaderByHandle(int handle)
    {
        var shader = Shaders.Where(s => s.Value.Handle == handle).FirstOrDefault().Value;
        return shader;
    }
}