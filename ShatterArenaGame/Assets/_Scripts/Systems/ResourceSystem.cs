using System.Collections.Generic;
using System.Linq;
using UnityEngine;

///  Regroupe scriptable objects with their query methods 
/// MonoBehaviour to add some debug/development references else make it standard class
public class ResourceSystem : StaticInstance<ResourceSystem> {

    public GameObject Player { get; private set; }
    public List<Obstacles> Obstacles { get; private set; }
    public List<Grouping> Grouping { get; private set; }

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        Obstacles = Resources.LoadAll<Obstacles>("Obstacles").ToList();
        Debug.Log("Obstacles count " + Obstacles.Count);
        Grouping = Resources.LoadAll<Grouping>("Groupings").ToList();
        Debug.Log("Grouping count " + Grouping.Count);
        Player = Resources.Load("Units/Disc") as GameObject;
    }

    public Obstacles GetRandomObstacle() => Obstacles[Random.Range(0, Obstacles.Count)];
    public Obstacles GetObstacle(int i) => Obstacles[i];
    public Grouping GetRandomGrouping() => Grouping[Random.Range(0, Grouping.Count)];
    public Grouping GetGrouping(int i) => Grouping[i];
    public GameObject GetPlayer() => Player;
}