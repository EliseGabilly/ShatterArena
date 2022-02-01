using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable/Obstacles")]
public class Obstacles : ScriptableObject {

    [SerializeField]
    private int maxHealt = 20;
    public int MaxHealt { get => maxHealt; }

    [SerializeField]
    private Material material;
    public Material Material { get => material; }

    [SerializeField]
    private bool isDestructible = true;
    public bool IsDestructible { get => isDestructible; }


}
