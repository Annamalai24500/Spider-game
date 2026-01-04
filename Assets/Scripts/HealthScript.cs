using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthScript : MonoBehaviour
{
    public GameObject medkitPrefab;
    public GameObject speedBoostPrefab;

    public float dropChance = 2f;

    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayer = false;
    public static int lives = 3;
    public float respawnDelay = 3f;

    Vector3 spawnPoint;

    // UI
    public TMP_Text healthText;
    public TMP_Text livesText;       
    public TMP_Text gameOverText;    
    void Start()
    {
        currentHealth = maxHealth;

        if (isPlayer)
            spawnPoint = transform.position;

        if (gameOverText)
            gameOverText.gameObject.SetActive(false);

        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isPlayer)
        {
            lives--;
            UpdateUI();

            if (lives <= 0)
            {
                ShowGameOver("GAME OVER");
                Debug.Log("You lost");
            }

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), respawnDelay);
        }
        else
        {
            Debug.Log("Enemy died");
            DropItem();
            if(GameManager.Instance != null)
                GameManager.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        transform.position = spawnPoint;
        gameObject.SetActive(true);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthText && isPlayer)
            healthText.text = "PlayerHealth: " + currentHealth;
        

        if (isPlayer && livesText)
            livesText.text = "Lives: " + lives;
    }

    void ShowGameOver(string message)
    {
        if (gameOverText)
        {
            gameOverText.text = message;
            gameOverText.gameObject.SetActive(true);
        }
    }
    void DropItem()
    {
        if (Random.value > dropChance)
            return;

        int random = Random.Range(0, 2);

        if (random == 0 && medkitPrefab != null)
            Instantiate(medkitPrefab, transform.position, Quaternion.identity);
        else if (random == 1 && speedBoostPrefab != null)
            Instantiate(speedBoostPrefab, transform.position, Quaternion.identity);
    }

}
