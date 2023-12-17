using FlowerSlayer.Components;
using FlowerSlayer.Events;
using FlowerSlayer.Inputs;
using FlowerSlayer.Managers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FlowerSlayer.Objects;

public class Triangle : GameObject
{

    private VAO vao;
    private float red = 0;
    public Triangle()
    {

        float[] vertices = {
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left
        };

        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        vao = new VAO("Triangle", vertices, indices);
    }

    public override void OnStart(OnStartEventArgs e)
    {
        Renderer r = AddComponent(new Renderer(ShaderManager.GetShaderByName("Default"), vao));



        base.OnStart(e);
    }

    public override void OnUpdate(OnUpdateEventArgs e)
    {

        // Position = Position + new Vector3(0.0001f, 0.0f, 0.0f);

        if (Input.KeyPress(Keys.Space))
        {
            red = red == 1.0f ? 0.0f : 1.0f;
        }
        base.OnUpdate(e);
    }



}