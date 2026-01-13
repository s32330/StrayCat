using TMPro;
using UnityEngine;

public class GameRating : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ratingText;
    public TextMeshProUGUI summaryText;

    [Header("Progi punktów")]
    public int pointsForS = 1000;
    public int pointsForA = 700;
    public int pointsForB = 400;
    public int pointsForC = 400;

    [Header("Progi ¿yæ (1–9)")]
    [Range(1, 9)] public int livesForS = 8;
    [Range(1, 9)] public int livesForA = 6;
    [Range(1, 9)] public int livesForB = 4;
    [Range(1, 9)] public int livesForC = 4;

    void Start()
    {
        PointsCounter pointsCounter = FindObjectOfType<PointsCounter>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (pointsCounter == null || playerHealth == null)
        {
            
            return;
        }

        int points = Mathf.RoundToInt(pointsCounter.GetCurrentPoints());

        
        int lives = Mathf.RoundToInt(playerHealth.GetCurrentHealth());

        string rating = CalculateRating(points, lives);

        ratingText.text = $"OCENA: {rating}";
        summaryText.text =
            $"Punkty: {points}\n" +
            $"Pozosta³e ¿ycia: {lives}/9";
    }

    string CalculateRating(int points, int lives)
    {
        if (points >= pointsForS && lives >= livesForS)
            return "S";

        if (points >= pointsForA && lives >= livesForA)
            return "A";

        if (points >= pointsForB && lives >= livesForB)
            return "B";

        if (points >= pointsForB && lives >= livesForB)
            return "C";

        return "D";
    }
}
