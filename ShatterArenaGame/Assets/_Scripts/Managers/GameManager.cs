using UnityEngine;

public class GameManager : Singleton<GameManager> {

    #region Variables
    public bool InGame { get; set; } = false;
    [SerializeField]
    private AimManager aimManager;
    [SerializeField]
    private Transform discParent;
    private GameObject disc;

    private Transform obstaclesParent;
    public float NbObstacles { get; set; }
    public float NbObstaclesLeft { get; set; }
    public int Gold { get; set; }
    #endregion

    public void StartGame() {
        InGame = true;
        Time.timeScale = 1;
        aimManager.enabled = true;
        SpawnDisc();
        SpawnManager.Instance.SpawnObstacles();
        NbObstacles = SpawnManager.Instance.GetNbObstacles();
        NbObstaclesLeft = SpawnManager.Instance.GetNbObstacles();
        obstaclesParent = SpawnManager.Instance.GetObstaclesParent();
        Gold = 0;
    }
    public void EndGame() {
        InGame = false;
        Time.timeScale = 0;
        aimManager.enabled = false;
        DespawnAll();
    }

    private void SpawnDisc() {
        Vector3 pos = Const.DiscSpawn;
        disc = Instantiate(ResourceSystem.Instance.GetDisc(), pos, Quaternion.identity) as GameObject;
        disc.transform.parent = discParent;
        aimManager.SetDisc(disc);
        CameraManager.Instance.SetFollowAndLookAt(disc.transform);
    }

    private void DespawnAll() {
        Destroy(obstaclesParent.gameObject);
        Destroy(disc);
    }
}
