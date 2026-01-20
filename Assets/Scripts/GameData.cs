using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int points;
    public int lives;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Clear()
    {
        points = 0;
        lives = 0;
    }
}
