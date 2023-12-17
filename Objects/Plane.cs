using FlowerSlayer.Components;
using FlowerSlayer.Events;
using FlowerSlayer.Inputs;
using FlowerSlayer.Managers;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FlowerSlayer.Objects;

public class Plane : GameObject
{
    public Plane()
    {
        AddComponent(new Renderer(ShaderManager.GetShaderByName("Default"), VAOManager.GetVAO("Plane")));
    }

    public override void OnUpdate(OnUpdateEventArgs e)
    {
        base.OnUpdate(e);
        Scale = Vector3.One * 0.5f;
        EurelAngles += new Vector3(0, 0.001f, 0);

        if (Input.KeyDown(Keys.W))
        {
            Position = Position + new Vector3(0.0f, 0.0f, 0.001f);
        }
        if (Input.KeyDown(Keys.S))
        {
            Position = Position + new Vector3(0.0f, 0.0f, -0.001f);
        }
        if (Input.KeyDown(Keys.A))
        {
            Position = Position + new Vector3(0.001f, 0.0f, 0.0f);
        }
        if (Input.KeyDown(Keys.D))
        {
            Position = Position + new Vector3(-0.001f, 0.0f, 0.0f);
        }
        if (Input.KeyDown(Keys.LeftShift))
        {
            Position = Position + new Vector3(0.0f, -0.001f, 0.0f);
        }
        if (Input.KeyDown(Keys.LeftControl))
        {
            Position = Position + new Vector3(0.0f, 0.001f, 0.0f);
        }
    }
}