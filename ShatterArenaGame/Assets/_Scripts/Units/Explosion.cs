using System.Collections;
using UnityEngine;

/// <summary>
/// Logic to make the "explosion" upgrade work
/// </summary>
public class Explosion : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null) {
            obstacle.TakeDamage(Const.ExplosionBaseDamage+Player.Instance.lvlExplosion);
            StartCoroutine(nameof(Unactive));
        }
    }

    private IEnumerator Unactive() {
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
    }
}
