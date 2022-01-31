using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManager : MonoBehaviour {

    #region Variables

    [SerializeField]
    private Transform startZone;
    [SerializeField]
    private GameObject disc;

    private Vector3 moussePosition;
    private GameObject predictionGo;
    private LineRenderer predictionRenderer;
    private Camera mainCamera;

    private bool isAiming = false;
    private readonly int maxPredictionBounce = 2;
    private readonly float maxPredictionStepDistance = 10f;

    #endregion

    private void Awake() {
        mainCamera = Camera.main;
        predictionGo = disc.transform.GetChild(0).gameObject;
        predictionRenderer = predictionGo.GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            StartCoroutine(nameof(Aiming));
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {
            isAiming = false;
        }

    }

    private IEnumerator Aiming() {
        isAiming = true;
        StartAim();
        while (isAiming) {
            WhileAim();
            Predict();
            yield return null;
        }
        EndAim();
    }

    private void StartAim() {
        //TODO check if start in start zone

        Rigidbody rb = disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Vector3 startPosition = disc.transform.position;
        startPosition.y = 0.5f; // fix perspective issue
        predictionRenderer.SetPosition(0, startPosition);// position of the starting point of the line

        predictionGo.SetActive(true);
    }

    private void WhileAim() {
        moussePosition = GetCurrentWorldPoint3D();
    }

    void Predict() {
        Vector3 direction = (moussePosition - disc.transform.position).normalized;
        direction.y = 0;
        DrawReflection(disc.transform.position, -direction, maxPredictionBounce);
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

    private Vector3 GetCurrentWorldPoint3D() {
        Vector3 screenPosDepth = Input.mousePosition;
        screenPosDepth.z = -mainCamera.transform.position.z; // Give it camera depth
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(screenPosDepth);
        return mousePos;
    }
    private Vector2 GetCurrentWorldPoint2D() {
        Vector3 screenPosDepth = Input.mousePosition;
        screenPosDepth.z = -mainCamera.transform.position.z; // Give it camera depth
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(screenPosDepth);
        return mousePos;
    }

    private void EndAim() {
        predictionGo.SetActive(false);

        Vector3 force = moussePosition - disc.transform.position ;
        force.y = 0;
        //TODO normalize the force to have a max 
        Rigidbody rb = disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(-force.normalized*500);
    }
}
