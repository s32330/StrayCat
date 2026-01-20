using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private float points = 0;
    public TextMeshProUGUI CurrentPointsText;

    private void Start()
    {
        UpdateText();
    }

    public void GetPoints(float newPoints)
    {
        points += newPoints;
        UpdateText();
    }

    public float GetCurrentPoints()
    {
        return points;
    }

    public void SetPoints(float value)
    {
        points = value;
        UpdateText();
    }

    public void ResetPoints()
    {
        points = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        if (CurrentPointsText != null)
            CurrentPointsText.text = points.ToString();
    }
}
