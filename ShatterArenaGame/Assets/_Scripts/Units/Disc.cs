using System.Collections;
using UnityEngine;

public class Disc : MonoBehaviour {

    private Rigidbody rb;
    private int baseDamage = 10;
    [SerializeField]
    private GameObject cam;
    
    private bool isTurning;
    private Vector2 startMoussePos;
    private Vector2 currentMoussePos;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (rb.velocity.magnitude > 0.05) {
            transform.LookAt(transform.position + rb.velocity);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.y > Screen.height / 2.0f) { // if touch in botom half it will be to aim
                StartCoroutine(nameof(Turn));
            }
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {
            isTurning = false;
            CameraManager.Instance.SelectVCamBasic();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle!=null) {
            obstacle.TakeDamage(GetDamage());
        }
    }

    private int GetDamage() {
        return baseDamage;
    }

    private IEnumerator Turn() {
        isTurning = true;
        CameraManager.Instance.SelectVCamNoDamping();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        startMoussePos = Input.mousePosition;
        while (isTurning) {
            currentMoussePos = Input.mousePosition;
            transform.Rotate(0, (currentMoussePos.x - startMoussePos.x) * 0.001f, 0);
            yield return null;
        }
    }
}
