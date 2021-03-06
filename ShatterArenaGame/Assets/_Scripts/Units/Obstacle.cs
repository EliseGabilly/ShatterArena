using System.Collections;
using UnityEngine;

/// <summary>
/// Logic and functions related to scriptable obstacles
/// </summary>
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
    private bool isStanding; //avoid destroying twice the obstacle
    #endregion

    private void Awake() {
        healt = obstacle.MaxHealt;
        gameObject.GetComponent<MeshRenderer>().material = obstacle.Material;
        anim = GetComponentInChildren<Animator>();
        isStanding = true;
    }

    public void TakeDamage(int damage) {
        if (obstacle.IsDestructible) {
            healt -= damage;
            CheckForDamages();
        }
    }

    private void CheckForDamages() {
        anim.SetTrigger("hit");
        if (healt <= 0 && isStanding) {
            StartCoroutine(nameof(DestroyObstacle));
            GameManager.Instance.GameGold += obstacle.Gold;
            GameManager.Instance.NbObstaclesLeft -=1;
            UIManager.Instance.UpdateGameValues();
            isStanding = false;
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
