using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Regroupe scriptable objects with their query methods 
/// MonoBehaviour to add some debug/development references else make it standard class
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem> {

    //Environment
    public GameObject Floor { get; private set; }
    public GameObject Wall { get; private set; }
    public GameObject DeadZone { get; private set; }
    public GameObject ObstacleParent { get; private set; }
    public GameObject WorldParent { get; private set; }
    //Scriptables
    public List<Obstacles> Obstacles { get; private set; }
    public List<Grouping> Grouping { get; private set; }
    //Other
    public GameObject Disc { get; private set; }

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        Floor = Resources.Load("Terrain/Floor") as GameObject;
        Wall = Resources.Load("Terrain/Wall") as GameObject;
        DeadZone = Resources.Load("Terrain/DeadZone") as GameObject;
        ObstacleParent = Resources.Load("Terrain/Obstacles") as GameObject;
        WorldParent = Resources.Load("Terrain/World") as GameObject;

        Obstacles = Resources.LoadAll<Obstacles>("Obstacles").ToList();
        Grouping = Resources.LoadAll<Grouping>("Groupings").ToList();

        Disc = Resources.Load("Units/Disc") as GameObject;
    }

    public Obstacles GetRandomObstacle() => Obstacles[Random.Range(0, Obstacles.Count)];
    public Obstacles GetObstacle(int i) => Obstacles[i];
    public Grouping GetRandomGrouping() => Grouping[Random.Range(0, Grouping.Count)];
    public Grouping GetGrouping(int i) => Grouping[i];
    public GameObject GetDisc() => Disc;
}