using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //Patrol
    public Transform LeftPoint;
    public Transform RightPoint;

    public float speed = 2f; // normalna prędkość patrolu
    public float chaseSpeed = 4f; // prędkość podczas podążania za graczem
     public float detectionRange = 5f; // zasięg wykrycia gracza

    //Komponenty
    public Transform player;
    public PlayerHealth playerHealth;
    public Animator anim;
    public LayerMask playerLayer;
    private Transform targetPoint;

    //Atak
    public Transform attackPoint;
    public float attackHitRange = 0.5f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    private float startY;
    

    void Start()
    {
        targetPoint = RightPoint;
        startY = transform.position.y;

        if (anim == null)
            anim = GetComponent<Animator>();

        // automatyczne przypisanie gracza po tagu
        if (player == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null)
                player = go.transform;
        }

        if (playerHealth == null && player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Gracz martwy → patrol
        if (playerHealth != null && playerHealth.isDead)
        {
            Patrol(speed);
            return;
        }

        // Gracz w zasięgu ataku → zatrzymaj się i atakuj
        if (distanceToPlayer <= attackRange)
        {
            FaceTarget(player.position);
            anim.SetFloat("Move", 0f);
            Attack();
        }
        // Gracz w zasięgu wykrycia → podążaj do niego z przyspieszoną prędkością
        else if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
            anim.SetFloat("Move", 1f);
        }
        // Gracz poza zasięgiem → patrol
        else
        {
            Patrol(speed);
        }
    }

    void Patrol(float currentSpeed)
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetPoint.position.x, currentSpeed * Time.deltaTime);
        float direction = newX - transform.position.x;

        transform.position = new Vector2(newX, startY);
        FaceDirection(direction);

        // Animacja ruchu zależna od faktycznego ruchu
        anim.SetFloat("Move", Mathf.Abs(direction) > 0.001f ? 1f : 0f);

        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.05f)
        {
            targetPoint = (targetPoint == LeftPoint) ? RightPoint : LeftPoint;
        }
    }

    void MoveTowardsPlayer()
    {
        // ograniczenie ruchu do punktów patrolowych
        float targetX = Mathf.Clamp(player.position.x, LeftPoint.position.x, RightPoint.position.x);

        float newX = Mathf.MoveTowards(transform.position.x, targetX, chaseSpeed * Time.deltaTime);
        float direction = newX - transform.position.x;

        transform.position = new Vector2(newX, startY);
        FaceDirection(direction);
    }

    void FaceTarget(Vector3 targetPos)
    {
        float direction = targetPos.x - transform.position.x;
        FaceDirection(direction);
    }

    void FaceDirection(float direction)
    {
        if (direction == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
        transform.localScale = scale;
    }

    void Attack()
    {
        if (playerHealth != null && playerHealth.isDead) return;

        if (Time.time >= nextAttackTime)
        {
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void DealDamage()
    {
        if (playerHealth != null && playerHealth.isDead) return;

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackHitRange, playerLayer);
        if (hit != null)
        {
            PlayerHealth ph = hit.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(40);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackHitRange);
        }

        // Wyświetlenie detectionRange w scenie
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
