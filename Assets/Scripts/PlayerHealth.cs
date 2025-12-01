using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;

    public TextMeshProUGUI CurrentHealthText;
    private Animator anim;
    public bool isDead = false;
    private PlayerMovement movement; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        isDead = false;
        CurrentHealthText.text = health.ToString();
    }

    private void Update()
    {
        CurrentHealthText.text = health.ToString();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        anim.SetTrigger("isHurt");

        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;
    }

    private void Die()
    {
        isDead = true;

        anim.SetBool("isDead", true);
        if (movement != null)
        {
            movement.enabled = false; 
        }

        // Zatrzymanie fizyki
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            
        }
    }
}
