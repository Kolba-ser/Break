

using Zenject;

public static class DiRef
{
    private static DiContainer container;

    public static DiContainer Container
    {
        get => container;
        set => container ??= value;
    }
}
