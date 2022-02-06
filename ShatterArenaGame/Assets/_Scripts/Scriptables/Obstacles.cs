using UnityEngine;

/// <summary>
/// Obstacles are destructibles that (once likend to a GO) are spawn on the terrain
/// </summary>
[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable/Obstacles")]
public class Obstacles : ScriptableObject {

    [SerializeField]
    private int maxHealt = 20;
    public int MaxHealt { get => maxHealt; }

    [SerializeField]
    private Material material;
    public Material Material { get => material; }

    [SerializeField]
    private GameObject go;
    public GameObject GO { get => go; }

    [SerializeField]
    private bool isDestructible = true;
    public bool IsDestructible { get => isDestructible; }

    [SerializeField]
    private int gold = 10;
    public int Gold { get => gold; }


}
