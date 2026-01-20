using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public PointsCounter pointsCounter;
    public Transform playerTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        SaveData data = new SaveData();

        // 🔹 zapis danych gracza
        data.health = playerHealth.GetCurrentHealth();
        data.points = pointsCounter.GetCurrentPoints();

        // 🔹 zapis pozycji playera
        data.position = new float[]
        {
            playerTransform.position.x,
            playerTransform.position.y,
            playerTransform.position.z
        };

        SaveSystem.Save(data);
        Debug.Log("Game Saved");
    }

    public void Load()
    {
        SaveData data = SaveSystem.Load();
        if (data == null)
        {
            Debug.LogWarning("No save data found");
            return;
        }

        // 🔹 wczytanie danych
        playerHealth.SetHealth(data.health);
        pointsCounter.SetPoints(data.points);

        // 🔹 wczytanie pozycji
        playerTransform.position = new Vector3(
            data.position[0],
            data.position[1],
            data.position[2]);

        // 🔹 reset fizyki (BARDZO WAŻNE)
        Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("Game Loaded");
    }
}
