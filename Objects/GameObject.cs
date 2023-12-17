
using FlowerSlayer.Components;
using FlowerSlayer.Events;
using FlowerSlayer.Managers;
using OpenTK.Mathematics;

namespace FlowerSlayer.Objects;

public class GameObject
{
    private List<Component> components = new List<Component>();
    private Vector3 _position;
    public Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            OnTransformChange();
        }
    }

    private Vector3 _scale = Vector3.One;
    public Vector3 Scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = value;
            OnTransformChange();
        }
    }

    private Vector3 _eurelAngles;
    public Vector3 EurelAngles
    {
        get
        {
            return _eurelAngles;
        }
        set
        {
            _eurelAngles = value;
            OnTransformChange();
        }
    }
    public Action OnTransformChange = delegate () { };

    public GameObject()
    {
        GameObjectManager.AddGameObject(this);
        Game.MainGameEvents.OnStart += OnStart;
        Game.MainGameEvents.OnUpdate += OnUpdate;
        Game.MainGameEvents.OnRender += OnRender;
    }

    public T AddComponent<T>(T component) where T : Component
    {
        component.gameObject = this;
        components.Add(component);
        component.postInitialization();
        return component;
    }

    public T? GetComponent<T>() where T : Component
    {
        return (T?)components.Where(x => x is T).FirstOrDefault();
    }

    public void RemoveComponent<T>(T component) where T : Component
    {
        component.OnDestroy();
        components.Remove(component);
    }

    public virtual void OnStart(OnStartEventArgs e)
    {
        components.ForEach(x => x.OnStart(e));

    }

    public virtual void OnUpdate(OnUpdateEventArgs e)
    {
        components.ForEach(x => x.OnUpdate(e));
    }

    public virtual void OnRender(OnRenderEventArgs e)
    {
        components.ForEach(x => x.OnRender(e));
    }

    public void Destroy()
    {
        OnDestroy();
        GameObjectManager.RemoveGameObject(this);
    }

    public virtual void OnDestroy()
    {
        Game.MainGameEvents.OnStart -= OnStart;
        Game.MainGameEvents.OnUpdate -= OnUpdate;
        Game.MainGameEvents.OnRender -= OnRender;
        DestroyComponents();
    }

    private void DestroyComponents()
    {
        components.ForEach(x => x.OnDestroy());
    }
}