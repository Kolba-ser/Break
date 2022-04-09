using UnityEngine;

public abstract class Images : ScriptableObject
{
    public abstract Sprite[] Sprites { get; }
    public int Length => Sprites.Length;

    public bool TryGet(out Sprite sprite, int index)
    {
        sprite = null;
        if (index.InBounds(Sprites) && Sprites[index] != null)
        {
            sprite = Sprites[index];
            return true;
        }

        return false;
    }

    public Sprite Get(int index)
    {
        if (index.InBounds(Sprites))
        {
            return Sprites[index];
        }

        return null;
    }
}

