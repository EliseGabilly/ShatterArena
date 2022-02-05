using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AimManager : MonoBehaviour {

    #region Variables
    public GameObject Disc { get; set; }

    private Vector3 moussePosition;
    private GameObject predictionGo;
    private LineRenderer predictionRenderer;
    private Camera mainCamera;

    private readonly int maxPredictionBounce = 2;
    private readonly float maxPredictionStepDistance = 100f;
    private Vector3 startPosition;
    private Vector2 startMoussePos;
    private Vector2 currentMoussePos;

    private Rigidbody rb;
    private bool isTurning;

    [SerializeField]
    private Slider cooldownSlider;
    private float cooldown;
    #endregion

    private void Awake() {
        mainCamera = Camera.main;
        cooldown = Const.Cooldown;
    }

    private void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            if(Input.mousePosition.y < Screen.height / 4.0f) { // if botom 25% 
                if (GameManager.Instance.NbThrowLeft == 0) {
                    UIManager.Instance.OpenEnd();
                } else if( cooldown >= Const.Cooldown) {
                    cooldown = 0;
                    Launch();
                    if (Player.Instance.level == 1) UIManager.Instance.OpenInfoGame(false);
                }
            } else if (Input.mousePosition.y < 7*Screen.height / 8.0f) { // if bellow UI
                StartCoroutine(nameof(Turn));
            }
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {
            isTurning = false;
            CameraManager.Instance.SelectVCamBasic();
        }
        cooldown += 0.005f;
    }

    private void LateUpdate() {
        cooldownSlider.value = cooldown;
    }

    public void SetDisc(GameObject disc) {
        this.Disc = disc;
        rb = this.Disc.GetComponent<Rigidbody>();
        predictionGo = Disc.transform.GetChild(0).gameObject;
        predictionRenderer = predictionGo.GetComponent<LineRenderer>();
    }

    void Predict() {
        Vector3 dir = currentMoussePos - startMoussePos;
        Vector3 pos = new Vector3(Disc.transform.position.x, 0.1f, Disc.transform.position.z);
        DrawReflection(pos, Disc.transform.forward, maxPredictionBounce);
    }

    private void DrawReflection(Vector3 position, Vector3 direction, int reflectionsRemaining) {
        if (reflectionsRemaining == 0) {
            return;
        }

        Ray ray = new Ray(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxPredictionStepDistance)) {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        } else {
            position += direction * maxPredictionStepDistance;
        }

        predictionRenderer.SetPosition(maxPredictionBounce - reflectionsRemaining+1, position);

        DrawReflection(position, direction, reflectionsRemaining - 1);
    }

    private void Launch() {
        GameManager.Instance.NbThrowLeft -= 1;
        UIManager.Instance.ShowThrowLeft(GameManager.Instance.NbThrowLeft);
        Rigidbody rb = Disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(Disc.transform.forward * 500);
    }

    private IEnumerator Turn() {
        isTurning = true;
        CameraManager.Instance.SelectVCamNoDamping();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        predictionGo.SetActive(true);
        startPosition = Disc.transform.position;
        startPosition.y = 0.1f; // fix perspective issue
        predictionRenderer.SetPosition(0, startPosition);// position of the starting point of the line
        float turnRatio = 0.001f * (Player.Instance.isInverseCam ? -1 : 1);

        startMoussePos = Input.mousePosition;
        while (isTurning) {
            currentMoussePos = Input.mousePosition;
            Disc.transform.Rotate(0, (currentMoussePos.x - startMoussePos.x) * turnRatio, 0);
            Predict();
            yield return null;
        }
        predictionGo.SetActive(false);
    }
}
