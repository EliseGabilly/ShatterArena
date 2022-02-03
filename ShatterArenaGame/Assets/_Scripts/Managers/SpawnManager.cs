using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {

    [SerializeField]
    private Transform environementParent;
    [SerializeField]
    private GameObject obstaclesParentPrefab;
    private Transform obstaclesParent;

    private List<Vector3> listPosition;

    public void SpawnObstacles(int nbObstacles = 7, int nbGroup = 7) {
        listPosition = new List<Vector3>();
        //remove spawn on player position
        listPosition.Add(Const.PlayerSpawn);

        GameObject obstaclesParentGO = Instantiate(obstaclesParentPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        obstaclesParent = obstaclesParentGO.transform;
        obstaclesParent.transform.parent = environementParent;
        for (int i = 0; i < nbObstacles; i++) {
            Vector3 pos = Utils.GetRandomPosition();
            if (!listPosition.Contains(pos)) {
                GameObject clone = Instantiate(ResourceSystem.Instance.GetRandomObstacle().GO, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = obstaclesParent;
                listPosition.Add(pos);
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
        for (int i = 0; i < group.ObstaclesList.Count; i++) {
            Vector3 pos = group.PositionsList[i] + groupPos;
            if (!listPosition.Contains(pos)) {
                GameObject clone = Instantiate(group.ObstaclesList[i].GO, pos, Quaternion.identity) as GameObject;
                clone.transform.parent = obstaclesParent;
                listPosition.Add(pos);
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
        while (!(minXPos > Const.MinXTerrain && maxXPos < Const.MaxXTerrain && minZPos > Const.MinZTerrain && maxZPos < Const.MaxZTerrain)) {
            groupPos = Utils.GetRandomPosition();
            minXPos = minXGroup + groupPos.x;
            maxXPos = maxXGroup + groupPos.x;
            minZPos = minZGroup + groupPos.z;
            maxZPos = maxZGroup + groupPos.z;
        }
        return groupPos;
    }

    public int GetNbObstacles() {
        //-1 as we added the player spawn
        return listPosition.Count - 1;
    }

    public Transform GetObstaclesParent() {
        return obstaclesParent;
    }
}
