using FlowerSlayer.Events;
using FlowerSlayer.Logging;
using FlowerSlayer.Managers;
using FlowerSlayer.Objects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FlowerSlayer.Components;

public class Renderer : Component
{
    private Shader shader;
    private VAO _VAO;
    private Matrix4 model;
    private Matrix4 view;
    private Matrix4 projection;

    /// <summary>
    /// Invoked after VAO has been binded and before draw function 
    /// </summary>
    public AfterVAOBind? afterVAOBind;

    public Renderer(Shader shader, VAO VAO)
    {
        this.shader = shader;
        _VAO = VAO;

        Game.MainGameEvents.OnResize += UpdateProjectionMatrix;
        UpdateMatrixes();
    }

    public override void postInitialization()
    {

        if (gameObject != null)
        {
            gameObject.OnTransformChange += UpdateMatrixes;
        }
    }

    private void UpdateMatrixes()
    {
        model = Matrix4.CreateScale(gameObject?.Scale ?? Vector3.One) * Matrix4.CreateRotationY(gameObject?.EurelAngles.Y ?? 0) * Matrix4.CreateTranslation(gameObject?.Position ?? Vector3.Zero);
        view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
    }

    private void UpdateProjectionMatrix(OnResizeEventArgs e)
    {
        projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), (float)e.Width / (float)e.Height, 0.01f, 100.0f);
    }

    /// <summary>
    /// Atomaticly called on render. Do not invoke manualy;
    /// Contains afterVAOBind
    /// </summary>
    /// <param name="e"></param>
    public override void OnRender(OnRenderEventArgs e)
    {
        if (!Enabled)
        {
            return;
        }

        //space for cutsom call like: uniform etc.
        shader.Use();
        if (afterVAOBind != null)
        {
            afterVAOBind(shader);
        }
        int modelMat4 = GL.GetUniformLocation(shader.Handle, "model");
        int viewMat4 = GL.GetUniformLocation(shader.Handle, "view");
        int projectionMat4 = GL.GetUniformLocation(shader.Handle, "projection");

        GL.UniformMatrix4(modelMat4, true, ref model);
        GL.UniformMatrix4(viewMat4, true, ref view);
        GL.UniformMatrix4(projectionMat4, true, ref projection);

        GL.BindVertexArray(_VAO.Handle);
        GL.DrawElements(BeginMode.Triangles, _VAO.IndicesCount, DrawElementsType.UnsignedInt, 0);
        base.OnRender(e);
    }

    public override void OnDestroy()
    {
        Logger.LogInfo("Destroy of renderer object");
        base.OnDestroy();
    }
}
public delegate void AfterVAOBind(Shader shader);