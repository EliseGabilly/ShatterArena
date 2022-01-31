using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour {

    [SerializeField]
    private Obstacles obstacle;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private int healt;

    private void Awake() {
        healt = obstacle.maxHealt;
        gameObject.GetComponent<MeshRenderer>().material = obstacle.material;
    }

    public void TakeDamage(int damage) {
        healt -= damage;
        CheckForDamages();
    }

    private void CheckForDamages() {
        if (healt <= 0) {
            Destroy(gameObject);
        } else if (healt <= obstacle.maxHealt/2) {
            fire.SetActive(true);
        }
    }

}
