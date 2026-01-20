using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private float points = 0;
    public TextMeshProUGUI CurrentPointsText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (CurrentPointsText != null)
            CurrentPointsText.text = points.ToString();
    }

    public void GetPoints(float newPoints)
    {
        points += newPoints;
    }

    public float GetCurrentPoints()
    {
        return points;
    }

   //dodane do save
   public void SetPoints(float value)
    {
        points = value;
    }
}
