using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
