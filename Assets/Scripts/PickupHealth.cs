using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    public float healamount = 25f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthScript health = other.GetComponent<HealthScript>();
            if (health != null)
            {
                health.currentHealth =  Mathf.Min(health.currentHealth + healamount, health.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}
