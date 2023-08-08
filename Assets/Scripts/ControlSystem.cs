using UnityEngine;

internal static class ControlSystem 
{
    public static KeyCode tace = KeyCode.E;
    public static KeyCode install = KeyCode.Q;
    public static KeyCode drop = KeyCode.G;

    public static KeyCode Tace { get => tace; set => tace = value; }
    public static KeyCode Install { get => install; set => install = value; }
    public static KeyCode Drop { get => Drop; set => Drop = value; }
}
