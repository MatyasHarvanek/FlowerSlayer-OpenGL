using System.Dynamic;
using System.Globalization;
using FlowerSlayer.Helpers;
using FlowerSlayer.Logging;
using OpenTK.Graphics.OpenGL;

namespace FlowerSlayer.Objects;

public class Shader
{
    public int Handle { get; private set; }
    public string Name { get; private set; }
    public Shader(string vertexName, string fragmentName, string name)
    {
        string vertexPath = AppDomain.CurrentDomain.BaseDirectory + "Shaders\\Vertex\\" + vertexName;
        string fragmentPath = AppDomain.CurrentDomain.BaseDirectory + "Shaders\\Fragment\\" + fragmentName;
        Name = name;
        Logger.LogInfo(vertexPath);
        int? vertextHandle = GetHandle(vertexPath, ShaderType.VertexShader);
        if (vertextHandle == null)
        {
            Logger.LogError(Path.GetFileName(vertexPath) + " Failed to load shader!");
            return;
        }

        int? fragmentHandle = GetHandle(fragmentPath, ShaderType.FragmentShader);
        if (fragmentHandle == null)
        {
            Logger.LogError(Path.GetFileName(fragmentPath) + " Failed to load shader!");
            GL.DeleteShader((int)vertextHandle);
            return;
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, (int)vertextHandle);
        GL.AttachShader(Handle, (int)fragmentHandle);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Logger.LogError(Name + "Failed to create a shader program! Program Log: " + infoLog);
        }

        GL.DetachShader(Handle, (int)vertextHandle);
        GL.DetachShader(Handle, (int)fragmentHandle);
        GL.DeleteShader((int)vertextHandle);
        GL.DeleteShader((int)fragmentHandle);
        Logger.LogInfo(Name + " shader program was successfully created");
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }
    
    private int? GetHandle(string path, ShaderType type)
    {
        string? shaderText = FileHelper.GetStringFromFile(path);

        if (string.IsNullOrEmpty(shaderText))
        {
            Logger.LogError(Path.GetFileName(path) + " Failed to load shader!");
            return null;
        }

        int handle = GL.CreateShader(type);
        GL.ShaderSource(handle, shaderText);
        GL.CompileShader(handle);
        GL.GetShader(handle, ShaderParameter.CompileStatus, out int status);

        if (status == 0)
        {
            Logger.LogError(Path.GetFileName(path) + " shader failed to compile! ShaderLog: " + GL.GetShaderInfoLog(Handle));
            GL.DeleteShader(handle);
            return null;
        }
        Logger.LogInfo(Path.GetFileName(path) + " shader was successfully compiled");
        return handle;
    }
}
