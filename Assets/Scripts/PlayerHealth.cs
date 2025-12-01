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

    private PlayerMovement movement; // referencja do PlayerMovement

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        isDead = false;
        CurrentHealthText.text = health.ToString();
    }

    private void Update()
    {
        // tylko aktualizacja UI
        CurrentHealthText.text = health.ToString();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        // Odpalenie animacji Hurt
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

        // Animator: ustaw bool tylko raz
        anim.SetBool("isDead", true);

        // Zablokowanie ruchu
        if (movement != null)
        {
            movement.enabled = false; // blokuje Update i FixedUpdate
        }

        // Zatrzymanie fizyki
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; // opcjonalnie, ¿eby gracz nie spada³
        }
    }
}
