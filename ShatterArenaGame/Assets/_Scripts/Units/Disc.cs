using UnityEngine;

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
        if (Player.Instance.lvlExplosion > 1) explosion.SetActive(true);
        AudioSystem.Instance.PlayHit();
        if (obstacle!=null) {
            obstacle.TakeDamage(GetDamage());
        }
    }

    private int GetDamage() {
        return baseDamage;
    }
}
