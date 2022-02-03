using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool InGame { get; set; } = false;
    [SerializeField]
    private AimManager aimManager;
    [SerializeField]
    private Transform playerParent;
    private GameObject player;

    private Transform obstaclesParent;
    [SerializeField]
    private int nbObstacles;
    [SerializeField]
    private int gold;

    public void StartGame() {
        InGame = true;
        Time.timeScale = 1;
        aimManager.enabled = true;
        SpawnPlayer();
        SpawnManager.Instance.SpawnObstacles();
        nbObstacles =  SpawnManager.Instance.GetNbObstacles();
        obstaclesParent = SpawnManager.Instance.GetObstaclesParent();
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

    private void DespawnAll() {
        Destroy(obstaclesParent.gameObject);
        Destroy(player);
    }
}
