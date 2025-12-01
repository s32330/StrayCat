using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{

    [SerializeField] private float points = 0;

    public TextMeshProUGUI CurrentPointsText;


    private void Start()
    {
        CurrentPointsText.text = $"{points}";


    }
    private void Update()
    {
        CurrentPointsText.text = $"{points}";
    }

    public void GetPoints(float newpoints)
    {
        points = points + newpoints;

    }
}