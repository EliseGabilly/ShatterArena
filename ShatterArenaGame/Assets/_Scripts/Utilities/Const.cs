using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : Singleton<Const> {

    public static float Cooldown { get; } = 1;
    public static float DestructionObjectif { get; } = 75;
    public static int NbThrow { get; } = 5;
    public static int ExplosionBaseDamage { get; } = 5;


    #region Terrain
    public Vector3 DiscSpawn { get => new Vector3(0, 0, -Mathf.RoundToInt(WidthTerrain / 4)); }
    public int MinTerrain { get => (int)(-WidthTerrain / 2); }
    public int MaxTerrain { get => (int)WidthTerrain / 2; }
    #endregion

    #region Level
    public int WidthTerrain { get => 3 + (Player.Instance.level * 2); }
    public int NbObstacles { get => (int)(Player.Instance.level * 1.7); } 
    public int NbGrouping { get => (int)(Player.Instance.level * 2.3); } 

    #endregion
}
