using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health == null)
            return;
      
        health.TakeDamage(damage);
        AudioManager.Instance.PlaySFX("HurtMeow");
    }
}