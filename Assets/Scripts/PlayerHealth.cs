using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;

    public TextMeshProUGUI CurrentHealthText;
    private Animator anim;
    private PlayerMovement movement;
    public bool isDead = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        isDead = false;
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (CurrentHealthText != null)
            CurrentHealthText.text = health.ToString();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        anim.SetTrigger("isHurt");

        if (health == 0)
            Die();
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    private void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);

        if (movement != null)
            movement.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        SceneManager.LoadScene("Lose");
    }

    // do save

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, maxHealth);

        if (health == 0 && !isDead)
            Die();
    }
}
