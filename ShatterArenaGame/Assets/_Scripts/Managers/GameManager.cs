using UnityEngine;

public class GameManager : Singleton<GameManager> {

    #region Variables
    public bool InGame { get; set; } = false;
    [SerializeField]
    private AimManager aimManager;
    [SerializeField]
    private Transform discParent;
    private GameObject disc;

    public float NbObstacles { get; set; }
    public float NbObstaclesLeft { get; set; }
    public int GameGold { get; set; }
    public int NbThrowLeft { get; set; }
    #endregion

    protected override void Awake() {
        base.Awake();
    }
    private void Start() {
        SaveSystem.LoadData();
    }

    public void StartGame() {
        InGame = true;
        Time.timeScale = 1;
        aimManager.enabled = true;
        CameraManager.Instance.SizeMinMapCam();
        SpawnDisc();
        SpawnManager.Instance.SpawnWorld();
        SpawnManager.Instance.SpawnObstacles();
        NbObstacles = SpawnManager.Instance.GetNbObstacles();
        NbObstaclesLeft = SpawnManager.Instance.GetNbObstacles();
        GameGold = 0;
        NbThrowLeft = Const.NbThrow + Player.Instance.lvlNbThrow;
        if (Player.Instance.level == 1) UIManager.Instance.OpenInfoGame(true);
    }
    public void EndGame() {
        InGame = false;
        Time.timeScale = 0;
        aimManager.enabled = false;
        Player.Instance.ChangeGold(GameGold);
        DespawnAll();
    }
    public void FinishGame() {
        InGame = false;
        aimManager.enabled = false;
    }

    private void SpawnDisc() {
        Vector3 pos = Const.Instance.DiscSpawn;
        disc = Instantiate(ResourceSystem.Instance.Disc, pos, Quaternion.identity) as GameObject;
        disc.transform.parent = discParent;
        aimManager.SetDisc(disc);
        CameraManager.Instance.SetFollowAndLookAt(disc.transform);
    }

    private void DespawnAll() {
        Destroy(SpawnManager.Instance.GetObstaclesParent().gameObject);
        Destroy(SpawnManager.Instance.GetWorldParent().gameObject);
        Destroy(disc);
    }
}
