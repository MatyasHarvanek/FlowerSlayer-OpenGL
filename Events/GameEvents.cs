namespace FlowerSlayer.Events;

public class GameEvents
{
    public event OnRenderHandlerer OnRender = delegate (OnRenderEventArgs e) { };
    public event OnUpdateHandlerer OnUpdate = delegate (OnUpdateEventArgs e) { };
    public event OnStartHandlerer OnStart = delegate (OnStartEventArgs e) { };
    public event OnUnloadHandlerer OnUnload = delegate (OnUnloadEventArgs e) { };
    public event OnResizeHandlerer OnResize = delegate (OnResizeEventArgs e) { };

    public void FireOnRedner(OnRenderEventArgs e)
    {
        OnRender.Invoke(e);
    }

    public void FireOnUpdate(OnUpdateEventArgs e)
    {
        OnUpdate.Invoke(e);
    }

    public void FireOnStart(OnStartEventArgs e)
    {
        OnStart.Invoke(e);
    }

    public void FireOnUnload(OnUnloadEventArgs e)
    {
        OnUnload.Invoke(e);
    }

    public void FireOnResize(OnResizeEventArgs e)
    {
        OnResize.Invoke(e);
    }
}

public delegate void OnRenderHandlerer(OnRenderEventArgs e);
public delegate void OnUpdateHandlerer(OnUpdateEventArgs e);
public delegate void OnStartHandlerer(OnStartEventArgs e);
public delegate void OnUnloadHandlerer(OnUnloadEventArgs e);
public delegate void OnResizeHandlerer(OnResizeEventArgs e);

public class OnRenderEventArgs { }
public class OnUpdateEventArgs { }
public class OnStartEventArgs { }
public class OnUnloadEventArgs { }
public class OnResizeEventArgs
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public OnResizeEventArgs(int width, int height)
    {
        Width = width;
        Height = height;
    }
}