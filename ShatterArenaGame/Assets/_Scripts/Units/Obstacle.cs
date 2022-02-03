using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Obstacles obstacle;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int healt;
    private Animator anim;
    #endregion

    private void Awake() {
        healt = obstacle.MaxHealt;
        gameObject.GetComponent<MeshRenderer>().material = obstacle.Material;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        if (obstacle.IsDestructible) {
            healt -= damage;
            CheckForDamages();
        }
    }

    private void CheckForDamages() {
        anim.SetTrigger("hit");
        if (healt <= 0) {
            StartCoroutine(nameof(DestroyObstacle));
            GameManager.Instance.GameGold += obstacle.Gold;
            GameManager.Instance.NbObstaclesLeft -=1;
            UIManager.Instance.UpdateGameValues();
        } else if (healt <= obstacle.MaxHealt/2) {
            fire.SetActive(true);
        }
    }

    private IEnumerator DestroyObstacle() {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
