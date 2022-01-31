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
    private LineRenderer lineRenderer;
    private GameObject trajectory;
    private Camera mainCamera;

    private bool isAiming = false;

    #endregion


    private void Awake() {
        mainCamera = Camera.main;
        trajectory = disc.transform.GetChild(0).gameObject;
        lineRenderer = trajectory.GetComponent<LineRenderer>();
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
            yield return null;
        }
        EndAim();
    }

    private void StartAim() {
        //TODO check if start in start zone
        Vector3 startPosition = disc.transform.position;
        startPosition.y = 0.5f; // fix perspective issue
        lineRenderer.SetPosition(0, startPosition); // position of the starting point of the line

        trajectory.SetActive(true);
    }

    private void WhileAim() {
        moussePosition = GetCurrentWorldPoint3D();
        lineRenderer.SetPosition(1, moussePosition);
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
        trajectory.SetActive(false);

        Vector3 force =moussePosition - disc.transform.position ;
        force.y = 0;
        //TODO normalize the force to have a max 
        Rigidbody rb = disc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(-force.normalized*500);
    }
}
