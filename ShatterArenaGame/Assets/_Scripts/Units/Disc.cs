using UnityEngine;

public class Disc : MonoBehaviour {

    private int baseDamage = 10;

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
