using UnityEngine;
using UnityEngine.SceneManagement;

public class WinObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        PointsCounter pointsCounter = collision.GetComponent<PointsCounter>();

        if (GameData.Instance != null)
        {
            GameData.Instance.points =
                pointsCounter != null
                    ? Mathf.RoundToInt(pointsCounter.GetCurrentPoints())
                    : 0;

            GameData.Instance.lives =
                playerHealth != null
                    ? Mathf.RoundToInt(playerHealth.GetCurrentHealth())
                    : 0;
        }

        Debug.Log("Koniec - zapisano wynik");
        AudioManager.Instance.PlaySFX("Win");
        SceneManager.LoadScene("Win");
        Destroy(gameObject);
    }
}
