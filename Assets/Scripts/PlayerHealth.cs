using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 9;
    [SerializeField] private float health = 9;

    public TextMeshProUGUI CurrentHealthText;
    private Animator anim;
    private PlayerMovement movement;
    public bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        UpdateText();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateText();

        if (anim != null)
            anim.SetTrigger("isHurt");

        if (health <= 0)
            Die();
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateText();
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        if (anim != null)
            anim.SetBool("isDead", true);

        if (movement != null)
            movement.enabled = false;

        // ZAPIS DANYCH DO OCENY
        if (GameData.Instance != null)
        {
            PointsCounter pc = GetComponent<PointsCounter>();
            GameData.Instance.points = pc != null ? Mathf.RoundToInt(pc.GetCurrentPoints()) : 0;
            GameData.Instance.lives = 0;
        }

        SceneManager.LoadScene("Lose");
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // 🔴 POTRZEBNE DO SAVE / LOAD
    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);
        UpdateText();

        if (health <= 0 && !isDead)
            Die();
        else if (health > 0)
            ReviveIfNeeded();
    }

    private void ReviveIfNeeded()
    {
        if (!isDead) return;

        isDead = false;

        if (anim != null)
            anim.SetBool("isDead", false);

        if (movement != null)
            movement.enabled = true;
    }

    private void UpdateText()
    {
        if (CurrentHealthText != null)
            CurrentHealthText.text = health.ToString();
    }
}
