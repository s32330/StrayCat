using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform LeftPoint;
    public Transform RightPoint;
    public float speed = 2f;

    [Header("Attack Settings")]
    public Transform player;        // gracz
    public float attackRange = 2f;  // zasieg ataku
    public float attackCooldown = 1f;
    public Animator anim;

    private Transform targetPoint;   // aktualny cel patrolu
    private float startY;            // stała wysokość enemy
    private float nextAttackTime = 0f;

    void Start()
    {
        targetPoint = RightPoint;    // zaczynamy patrol w prawo
        startY = transform.position.y;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Gracz w zasięgu → przerywamy patrol i patrzymy na gracza
            FacePlayer();
            Attack();
        }
        else
        {
            // normalny patrol
            Patrol();
        }
    }

    void Patrol()
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetPoint.position.x, speed * Time.deltaTime);
        float direction = newX - transform.position.x;

        transform.position = new Vector2(newX, startY);

        // obrót w stronę ruchu po osi X
        FaceDirection(direction);

        // zmiana punktu docelowego po dotarciu
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.05f)
        {
            targetPoint = (targetPoint == LeftPoint) ? RightPoint : LeftPoint;
        }
    }

    void FacePlayer()
    {
        float direction = player.position.x - transform.position.x;
        FaceDirection(direction);
    }

    void FaceDirection(float direction)
    {
        if (direction == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction); // zachowuje szerokość, zmienia tylko znak
        transform.localScale = scale;
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Enemy attacks!");
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
