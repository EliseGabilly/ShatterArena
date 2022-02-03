using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const {

    public static Vector3 PlayerSpawn { get; } = new Vector3(0, 0, -5);
    public static float MinXTerrain { get; } = -10f;
    public static float MaxXTerrain { get; } = 10f;
    public static float MinZTerrain { get; } = -10f;
    public static float MaxZTerrain { get; } = 10f;
    public static float Cooldown { get; } = 2;


}
