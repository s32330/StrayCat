using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage = 40;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dotknieto: " + collision.name);

        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Trafiono gracza!");


        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health == null)
        {
            return;
        }

        health.TakeDamage(damage);
    }

    
    }
