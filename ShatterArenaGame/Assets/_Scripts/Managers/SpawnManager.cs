using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {

    #region Variables
    [SerializeField]
    private Transform environementParent;
    private Transform worldParent;
    private Transform obstaclesParent;

    private List<Vector3> listPosition;
    private int obstacleCount;
    #endregion

    internal void SpawnWorld() {
        GameObject worldParentGO = Instantiate(ResourceSystem.Instance.WorldParent, Vector3.zero, Quaternion.identity) as GameObject;
        worldParent = worldParentGO.transform;
        worldParent.transform.parent = environementParent;

        GameObject floorGO = Instantiate(ResourceSystem.Instance.Floor, Vector3.zero, Quaternion.identity) as GameObject;
        Transform floor = floorGO.transform;
        floor.localScale = new Vector3(Const.Instance.WidthTerrain + 2, 0.5f, Const.Instance.WidthTerrain + 2);
        floor.parent = worldParent;

        //Walls
        GameObject wallGO1 = Instantiate(ResourceSystem.Instance.Wall, new Vector3(Const.Instance.MinTerrain, 0, 0), Quaternion.identity) as GameObject;
        Transform wall1 = wallGO1.transform;
        wall1.localScale = new Vector3(0.1f, 0.5f, Const.Instance.WidthTerrain);
        wall1.parent = worldParent;

        GameObject wallGO2 = Instantiate(ResourceSystem.Instance.Wall, new Vector3(Const.Instance.MaxTerrain, 0, 0), Quaternion.identity) as GameObject;
        Transform wall2 = wallGO2.transform;
        wall2.localScale = new Vector3(0.1f, 0.5f, Const.Instance.WidthTerrain);
        wall2.parent = worldParent;

        GameObject wallGO3 = Instantiate(ResourceSystem.Instance.Wall, new Vector3(0, 0, Const.Instance.MinTerrain), Quaternion.identity) as GameObject;
        Transform wall3 = wallGO3.transform;
        wall3.localScale = new Vector3(Const.Instance.WidthTerrain, 0.5f, 0.1f);
        wall3.parent = worldParent;

        GameObject wallGO4 = Instantiate(ResourceSystem.Instance.Wall, new Vector3(0, 0, Const.Instance.MaxTerrain), Quaternion.identity) as GameObject;
        Transform wall4 = wallGO4.transform;
        wall4.localScale = new Vector3(Const.Instance.WidthTerrain, 0.5f, 0.1f);
        wall4.parent = worldParent;
    }

    public void SpawnObstacles() {
        int nbObstacles = Const.Instance.NbObstacles;
        int nbGroup = Const.Instance.NbGrouping;
        //remove spawn on player position
        listPosition = new List<Vector3> { Const.Instance.DiscSpawn };
        obstacleCount = 0;

        GameObject obstaclesParentGO = Instantiate(ResourceSystem.Instance.ObstacleParent, Vector3.zero, Quaternion.identity) as GameObject;
        obstaclesParent = obstaclesParentGO.transform;
        obstaclesParent.transform.parent = environementParent;
        for (int i = 0; i < nbObstacles; i++) {
            Vector3 pos = Utils.GetRandomPosition();
            if (!listPosition.Contains(pos)) {
                Obstacles obstacle = ResourceSystem.Instance.GetRandomObstacle();
                GameObject clone = Instantiate(obstacle.GO, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = obstaclesParent;
                listPosition.Add(pos);
                if (obstacle.IsDestructible) obstacleCount++;
            }
        }
        for (int i = 0; i < nbGroup; i++) {
            SpawnGroup(ResourceSystem.Instance.GetRandomGrouping());
        }
    }

    private void SpawnGroup(Grouping group) {
        Vector3 groupPos = GroupPosition(group);
        if (group.ObstaclesList.Count != group.PositionsList.Count) {
            Debug.Log("ObstaclesList and PositionsList should be the same size");
            return;
        }
        if (groupPos.Equals(Vector3.positiveInfinity)) {
            //if the group can't fit in the terrain
            return;
        }
        for (int i = 0; i < group.ObstaclesList.Count; i++) {
            Vector3 pos = group.PositionsList[i] + groupPos;
            if (!listPosition.Contains(pos)) {
                Obstacles obstacle = group.ObstaclesList[i];
                GameObject clone = Instantiate(obstacle.GO, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = obstaclesParent;
                listPosition.Add(pos);
                if (obstacle.IsDestructible) obstacleCount++;
            }
        }
    }

    /// <summary>
    /// Return a position that will spawn all the obstacles in the group in bound of the terrain
    /// </summary>
    /// <param name="group"></param>
    /// <returns>Posstition as Vector 3</returns>
    private Vector3 GroupPosition(Grouping group) {
        float minXGroup = group.PositionsList[0].x;
        float maxXGroup = group.PositionsList[0].x;
        float minZGroup = group.PositionsList[0].z;
        float maxZGroup = group.PositionsList[0].z;
        foreach (Vector3 v3 in group.PositionsList) {
            if (v3.x < minXGroup) minXGroup = v3.x;
            if (v3.x > maxXGroup) maxXGroup = v3.x;
            if (v3.z < minZGroup) minZGroup = v3.z;
            if (v3.z > maxZGroup) maxZGroup = v3.z;
        }

        Vector3 groupPos = Utils.GetRandomPosition();
        float minXPos = minXGroup + groupPos.x;
        float maxXPos = maxXGroup + groupPos.x;
        float minZPos = minZGroup + groupPos.z;
        float maxZPos = maxZGroup + groupPos.z;
        //if the group can't fit in the terrain
        if (maxXGroup - minXGroup >= Const.Instance.WidthTerrain-2 || maxZGroup - minZGroup >= Const.Instance.WidthTerrain-2)
            return Vector3.positiveInfinity;
        while (!(minXPos > Const.Instance.MinTerrain && maxXPos < Const.Instance.MaxTerrain && minZPos > Const.Instance.MinTerrain && maxZPos < Const.Instance.MaxTerrain)) {
            groupPos = Utils.GetRandomPosition();
            minXPos = minXGroup + groupPos.x;
            maxXPos = maxXGroup + groupPos.x;
            minZPos = minZGroup + groupPos.z;
            maxZPos = maxZGroup + groupPos.z;
        }
        return groupPos;
    }

    public int GetNbObstacles() {
        return obstacleCount;
    }

    public Transform GetObstaclesParent() {
        return obstaclesParent;
    }
    public Transform GetWorldParent() {
        return worldParent;
    }
}
