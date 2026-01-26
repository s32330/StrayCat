using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField]  private Image HealthBarTotal;
    [SerializeField] private Image HealthBarCurrent;

    private void Start()
    {
        
    }

    private void Update()
    {
        float ratio =
            playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();

        HealthBarCurrent.fillAmount = ratio * 0.9f;
    }
}
