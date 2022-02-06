using UnityEngine;

/// <summary>
/// Fix in case the disc glish trhoug the arena walls
/// </summary>
public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            UIManager.Instance.OpenEnd(true);
        }
    }
}
