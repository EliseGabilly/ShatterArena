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
        healt = obstacle.MaxHealt;
        gameObject.GetComponent<MeshRenderer>().material = obstacle.Material;
    }

    public void TakeDamage(int damage) {
        if (obstacle.IsDestructible) {
            healt -= damage;
            CheckForDamages();
        }
    }

    private void CheckForDamages() {
        if (healt <= 0) {
            Destroy(gameObject);
        } else if (healt <= obstacle.MaxHealt/2) {
            fire.SetActive(true);
        }
    }

}
