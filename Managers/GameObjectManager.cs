using FlowerSlayer.Events;
using FlowerSlayer.Objects;

namespace FlowerSlayer.Managers;

public static class GameObjectManager
{
    private static List<GameObject> GameObjects = new();

    static GameObjectManager()
    {
        Game.MainGameEvents.OnUnload += OnUnload;
    }

    public static GameObject AddGameObject(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
        return gameObject;
    }

    public static void RemoveGameObject(GameObject gameObject)
    {
        GameObjects.Remove(gameObject);
    }
    private static void OnUnload(OnUnloadEventArgs e)
    {
        GameObjects.ForEach(x => x.OnDestroy());
    }

}