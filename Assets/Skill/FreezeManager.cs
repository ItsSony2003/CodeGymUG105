using System.Collections.Generic;

public interface IFreezable
{
    void OnFreeze();
    void OnUnfreeze();
}

public static class FreezeManager
{
    public static bool IsFrozen { get; private set; }
    private static readonly HashSet<IFreezable> _items = new();

    public static void Register(IFreezable f)
    {
        if (f == null) return;
        _items.Add(f);
        if (IsFrozen) f.OnFreeze(); // pooled spawns freeze immediately if already frozen
    }

    public static void Unregister(IFreezable f)
    {
        if (f == null) return;
        _items.Remove(f);
    }

    public static void SetFrozen(bool frozen)
    {
        if (IsFrozen == frozen) return;
        IsFrozen = frozen;

        foreach (var f in _items)
        {
            if (frozen) f.OnFreeze();
            else f.OnUnfreeze();
        }
    }
}