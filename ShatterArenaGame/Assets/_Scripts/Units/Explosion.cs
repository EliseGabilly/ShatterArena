using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null) {
            obstacle.TakeDamage(Const.ExplosionBaseDamage+Player.Instance.lvlExplosion);
            StartCoroutine(nameof(Unactive));
        }
    }

    private IEnumerator Unactive() {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
