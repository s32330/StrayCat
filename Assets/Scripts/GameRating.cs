using TMPro;
using UnityEngine;

public class GameRating : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ratingText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI descriptionText;

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

    [Header("Opisy ocen")]
    public string descriptionS = "Jak uda³o ci siê zrobiæ to tak perfekcyjnie?";
    public string descriptionA = "Œwietnie!...Ale czy spróbujesz wspi¹æ siê na sam szczyt?";
    public string descriptionB = "Dobrze, ale mo¿esz lepiej, prawda?";
    public string descriptionC = "Œrednio, trzeba popracowaæ.";
    public string descriptionD = "Ty to nazywasz wygran¹? Có¿, przynajmniej ¿yjê...";

    private void Start()
    {
        if (GameData.Instance == null)
            return;

        int points = GameData.Instance.points;
        int lives = GameData.Instance.lives;

        string rating = CalculateRating(points, lives);

        // UI
        if (ratingText != null)
            ratingText.text = $"OCENA: {rating}";

        if (summaryText != null)
            summaryText.text = $"Punkty: {points}\nPozosta³e ¿ycia: {lives}/9";

        SetDescription(rating);
    }

    // Funkcja licz¹ca ocenê
    private string CalculateRating(int points, int lives)
    {
        if (points >= pointsForS && lives >= livesForS) return "S";
        if (points >= pointsForA && lives >= livesForA) return "A";
        if (points >= pointsForB && lives >= livesForB) return "B";
        if (points >= pointsForC && lives >= livesForC) return "C";
        return "D";
    }

    // opis w zale¿noœci od oceny
    private void SetDescription(string rating)
    {
        if (descriptionText == null)
            return;

        switch (rating)
        {
            case "S":
                descriptionText.text = descriptionS;
                break;
            case "A":
                descriptionText.text = descriptionA;
                break;
            case "B":
                descriptionText.text = descriptionB;
                break;
            case "C":
                descriptionText.text = descriptionC;
                break;
            default:
                descriptionText.text = descriptionD;
                break;
        }
    }
}
