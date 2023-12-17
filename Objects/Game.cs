using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlowerSlayer.Events;
using FlowerSlayer.Inputs;
using FlowerSlayer.Logging;
using FlowerSlayer.Managers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FlowerSlayer.Objects;



public class Game : GameWindow
{
    public static GameEvents MainGameEvents = new();
    public static Vector2i ViewPortSize = new Vector2i(640, 320);
    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        Input.Initialize(this);
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.09f, 0.09f, 0.09f, 0.8f);

        VAOManager.AddVAO("Cube", "Cube.obj");
        VAOManager.AddVAO("Plane", "Plane.obj");

        ShaderManager.AddShader("Default", "shader.vert", "shader.frag");
        GameObjectManager.AddGameObject(new Cube()).Position = new Vector3(-1f, 0f, 0f);
        MainGameEvents.FireOnStart(new());
    }
    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        MainGameEvents.FireOnUpdate(new());
        Input.Update();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        MainGameEvents.FireOnRedner(new());
        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        MainGameEvents.FireOnResize(new OnResizeEventArgs(e.Width, e.Height));
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        MainGameEvents.FireOnUnload(new());
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            Console.ReadLine();
        }
    }
}