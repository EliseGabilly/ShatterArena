using UnityEngine;

/// <summary>
/// Script for the disc to deffin his behaviour on collision
/// </summary>
public class Disc : MonoBehaviour {

    #region Variables
    private Rigidbody rb;
    [SerializeField]
    private GameObject explosion;
    private int baseDamage = 10;
    #endregion
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate() {
        if (rb.velocity.magnitude > 0.02) {
            transform.LookAt(transform.position + rb.velocity);
        }
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); ;
    }

    private void OnCollisionEnter(Collision collision) {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        AudioSystem.Instance.PlayHit();
        if (obstacle!=null) {
            if (Player.Instance.lvlExplosion > 1) explosion.SetActive(true);
            obstacle.TakeDamage(GetDamage());
        }
    }

    private int GetDamage() {
        return baseDamage;
    }
}
