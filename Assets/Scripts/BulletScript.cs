using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string Targettag;
    float lifetime = 2f;
    private void Start()
    {
        Destroy(gameObject, lifetime);        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on collision
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Hit" + collision.gameObject.name + "!");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag(Targettag))
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            if (health != null)
            {
                health.TakeDamage(25f);
                Debug.Log("Enemy hit! Remaining Health: " + health.currentHealth);
            }
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
            Debug.Log("Bullet hit: " + collision.gameObject.name);
        }
    }   
    private void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab,contact.point,Quaternion.LookRotation(contact.normal));
        hole.transform.SetParent(objectWeHit.transform);
    }
}
