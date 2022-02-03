using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimManager : MonoBehaviour {

    #region Variables
    public GameObject disc { get; set; }

    private Vector3 moussePosition;
    private GameObject predictionGo;
    private LineRenderer predictionRenderer;
    private Camera mainCamera;

    private bool isAiming = false;
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
        //predictionGo = Disc.transform.GetChild(0).gameObject;
        //predictionRenderer = predictionGo.GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            if(Input.mousePosition.y < Screen.height / 4.0f) { // if botom 25% 
                //StartCoroutine(nameof(Aiming));
                if (cooldown >= Const.Cooldown) {
                    cooldown = 0;
                    Launch();
                }
            } else if (Input.mousePosition.y < 7*Screen.height / 8.0f) { // if bellow UI
                StartCoroutine(nameof(Turn));
            }
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {
            isAiming = false;
            isTurning = false;
            CameraManager.Instance.SelectVCamBasic();
        }
        cooldown += 0.001f;
    }

    private void LateUpdate() {
        cooldownSlider.value = cooldown;
    }

    public void SetDisc(GameObject disc) {
        this.disc = disc;
        rb = this.disc.GetComponent<Rigidbody>();
    }

    private IEnumerator Aiming() {
        isAiming = true;
        StartAim();
        while (isAiming) {
            currentMoussePos = GetCurrentWorldPoint();
            Predict();
            yield return null;
        }
        EndAim();
    }

    private void StartAim() {
        Rigidbody rb = disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        CameraManager.Instance.SelectVCamNoDamping();

        startMoussePos = GetCurrentWorldPoint();
        startPosition = disc.transform.position;
        startPosition.y = 0.5f; // fix perspective issue
        predictionRenderer.SetPosition(0, startPosition);// position of the starting point of the line

        predictionGo.SetActive(true);
    }

    void Predict() {
        Vector3 dir = currentMoussePos - startMoussePos;
        Vector3 pos = new Vector3(disc.transform.position.x, 0.5f, disc.transform.position.z);
        DrawReflection(pos, -dir, maxPredictionBounce);
        //} else {
        //    predictionRenderer.SetPosition(1, startPosition);
        //    predictionRenderer.SetPosition(2, startPosition);
        //}
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

    private Vector3 GetCurrentWorldPoint() {
        Vector3 screenPosDepth = Input.mousePosition;
        //screenPosDepth.z = 50; // Give it camera depth
        Ray ray = mainCamera.ScreenPointToRay(screenPosDepth);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            return hit.point;
        }

        return Vector3.zero;
    }

    private void EndAim() {
        predictionGo.SetActive(false);

        Vector3 dir = currentMoussePos - startMoussePos;

            //TODO normalize the force to have a max 
            Rigidbody rb = disc.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(-dir.normalized * 500);

    }

    private void Launch() {
        Rigidbody rb = disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(disc.transform.forward * 500);
    }

    private IEnumerator Turn() {
        isTurning = true;
        CameraManager.Instance.SelectVCamNoDamping();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        startMoussePos = Input.mousePosition;
        while (isTurning) {
            currentMoussePos = Input.mousePosition;
            disc.transform.Rotate(0, (currentMoussePos.x - startMoussePos.x) * 0.001f, 0);
            yield return null;
        }
    }
}
