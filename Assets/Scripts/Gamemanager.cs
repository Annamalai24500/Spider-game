using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int enemiesLeft = 3;
    public TMP_Text gameOverText;

    private void Awake()
    {
        Instance = this;
        gameOverText.gameObject.SetActive(false);
    }

    public void EnemyDied()
    {
        enemiesLeft--;
        Debug.Log("Enemies left: " + enemiesLeft);

        if (enemiesLeft <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameOverText.text = "YOU WIN";
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
