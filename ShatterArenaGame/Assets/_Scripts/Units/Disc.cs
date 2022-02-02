using System.Collections;
using UnityEngine;

public class Disc : MonoBehaviour {

    private Rigidbody rb;
    private int baseDamage = 10;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (rb.velocity.magnitude > 0.05) {
            transform.LookAt(transform.position + rb.velocity);
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

}
