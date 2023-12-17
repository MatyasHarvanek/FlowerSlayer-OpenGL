using FlowerSlayer.Components;
using FlowerSlayer.Events;
using FlowerSlayer.Objects;

namespace FlowerSlayer.Components;

public class Component
{
    public bool Enabled = true;
    public GameObject? gameObject;

    public virtual void postInitialization()
    {
        
    }

    public virtual void OnStart(OnStartEventArgs e)
    {

    }

    public virtual void OnUpdate(OnUpdateEventArgs e)
    {

    }

    public virtual void OnRender(OnRenderEventArgs e)
    {

    }

    public virtual void OnDestroy()
    {
    }
}