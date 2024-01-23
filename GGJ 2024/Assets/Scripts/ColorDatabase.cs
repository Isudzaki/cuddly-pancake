using UnityEngine;

public static class ColorDatabase
{
    public static TileColor[] Colors { get; private set; }

    //Loading all colors from Resources folder
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Initialize() => Colors = Resources.LoadAll<TileColor>("Colors");
}
