using OpenTK.Graphics.OpenGL;

namespace FlowerSlayer.Managers;

public class VAO
{
    public string Name { get; private set; } = string.Empty;
    public int Handle { get; private set; }
    public int IndicesCount { get; private set; }
    public float[] Vertecies { get; private set; } = Array.Empty<float>();
    public uint[] Indicies { get; private set; } = Array.Empty<uint>();

    public VAO(string name, float[] vertecies, uint[] indices)
    {
        Name = name;
        Vertecies = vertecies;
        Indicies = indices;
        IndicesCount = Indicies.Length;

        List<float> colors = new List<float>{
        0,
        1,
        0,
        0,
        1,
        0,
        0,
        1,
        0,
        };



        for (int i = 0; i < vertecies.Length - 9; i += 3)
        {

            colors.Add(1);
            colors.Add(0);
            colors.Add(1);
        }

        //binding of VAO
        Handle = GL.GenVertexArray();
        GL.BindVertexArray(Handle);

        //vertex buffer
        int vertexArrayBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexArrayBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertecies.Length, vertecies, BufferUsageHint.StaticDraw);

        //enable attrib pointer
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        //indices Buffer EBO
        int indicesBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indicesBuffer);
        GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * IndicesCount, indices, BufferUsageHint.StaticDraw);

        //
        int colorBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * colors.Count, colors.ToArray(), BufferUsageHint.StaticDraw);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        //TODO Test out if has hard color edges when used correctly with idices
        // GL.VertexAttribBinding(1, 0);
        // GL.VertexAttribDivisor(1, 1);
    }

    public void Use()
    {

    }
}