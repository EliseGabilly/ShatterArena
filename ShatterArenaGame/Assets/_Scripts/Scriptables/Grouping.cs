using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Groupping are a list of obstacles/position which allows the obstacles to be placed in relation to each other
/// </summary>
[CreateAssetMenu(fileName = "Grouping", menuName = "Scriptable/Groupings")]
public class Grouping : ScriptableObject {

    //There should be as much obstacles as positions
    [SerializeField]
    private List<Obstacles> obstaclesList;
    public List<Obstacles> ObstaclesList { get => obstaclesList; }
    [SerializeField]
    private List<Vector3> positionsList;
    public List<Vector3> PositionsList { get => positionsList; }
}
