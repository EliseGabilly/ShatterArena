using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool InGame { get; set; } = false;
    [SerializeField]
    private AimManager aimManager;
    [SerializeField]
    private Transform environementParent;
    [SerializeField]
    private GameObject obstaclesParentPrefab;
    private Transform obstaclesParent;
    [SerializeField]
    private Transform playerParent;
    private GameObject player;

    public void StartGame() {
        InGame = true;
        Time.timeScale = 1;
        aimManager.enabled = true;
        SpawnPlayer();
        SpawnObstacles();
    }
    public void EndGame() {
        InGame = false;
        Time.timeScale = 0;
        aimManager.enabled = false;
        DespawnAll();
    }


    private void SpawnPlayer() {
        Vector3 pos = Const.PlayerSpawn;
        player = Instantiate(ResourceSystem.Instance.GetPlayer(), pos, Quaternion.identity) as GameObject;
        player.transform.parent = playerParent;
        aimManager.SetDisc(player);
        CameraManager.Instance.SetFollowAndLookAt(player.transform);
    }

    private void SpawnObstacles(int nbObstacles = 2, int nbGroup = 3) {
        GameObject obstaclesParentGO = Instantiate(obstaclesParentPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        obstaclesParent = obstaclesParentGO.transform;
        obstaclesParent.transform.parent = environementParent;
        for (int i = 0; i < nbObstacles; i++) {
            Vector3 pos = Utils.GetRandomPosition();
            GameObject clone = Instantiate(ResourceSystem.Instance.GetRandomObstacle().GO, pos, Quaternion.identity) as GameObject;
            clone.transform.parent = obstaclesParent;
        }
        for (int i = 0; i < nbGroup; i++) {
            SpawnGroup(ResourceSystem.Instance.GetRandomGrouping());
        }
    }

    private void SpawnGroup(Grouping group) {
        Vector3 groupPos = Utils.GetRandomPosition();
        //TODO check if not outside of terrain
        if (group.ObstaclesList.Count != group.PositionsList.Count) {
            Debug.Log("ObstaclesList and PositionsList should be the same size");
            return;
        }
        for(int i =0; i<group.ObstaclesList.Count; i++) {
            Vector3 pos = group.PositionsList[i] + groupPos;
            GameObject clone = Instantiate(group.ObstaclesList[i].GO, pos, Quaternion.identity) as GameObject;
            clone.transform.parent = obstaclesParent;
        }
    }

    private void DespawnAll() {
        Destroy(obstaclesParent.gameObject);
        Destroy(player);
    }
}
