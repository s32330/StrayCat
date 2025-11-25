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

    private void Start()
    {
        anim = GetComponent<Animator>();
        isDead = false;
        CurrentHealthText.text = $"{health}";
        
    }
    private void Update()
    {
        CurrentHealthText.text = $"{health}";
        anim.SetBool("isDead", );
        anim.SetFloat("isHurt");
    }

    

    void TakeDamage(float damage)
    {
        health = health - damage;
        if (health > 0)
            


        if (health < 0)
        {
            health = 0;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health == 0)
        {
            isDead = true;
            

        }


    }

    public void Heal(float heal)
    {
        health = health + heal;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }



}
