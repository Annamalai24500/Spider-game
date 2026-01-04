using UnityEngine;
using System.Collections;
public class NewMonoBehaviourScript1 : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementScript movement = other.GetComponent<PlayerMovementScript>();
            if (movement != null)
            {
                StartCoroutine(SpeedBoost(movement));
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SpeedBoost(PlayerMovementScript movement)
    {
        movement.speed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        movement.speed /= speedMultiplier;
    }
}
