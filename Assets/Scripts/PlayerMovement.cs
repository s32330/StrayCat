using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;     
    public float jumpForce = 300f;

    [HideInInspector] public float moveInput = 0f;
    [HideInInspector] public bool isJump = false;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    public GroundChecker groundChecker;
    public PlayerHealth health;

    private void Start()
    {
        
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (anim == null) anim = GetComponent<Animator>();
        if (health == null) health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Blokada ruchu po śmierci
        if (health != null && health.isDead)
        {
            if (anim != null) anim.SetFloat("IsMove", -1);
            return;
        }

        // Odczyt ruchu gracza
        moveInput = Input.GetAxis("Horizontal");

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker != null && groundChecker.isGrounded)
            isJump = true;

        // Animacje
        if (anim != null)
        {
            anim.SetFloat("verticalVelocity", rb.velocity.y);

            if (groundChecker != null)
                anim.SetBool("isGrounded", groundChecker.isGrounded);

            anim.SetFloat("IsMove", moveInput != 0 ? 1 : -1);
        }
    }

    private void FixedUpdate()
    {
        if (health != null && health.isDead) return;

        // Obrót sprite w zależności od kierunku ruchu
        if (sprite != null)
        {
            if (moveInput > 0f) sprite.flipX = false;
            else if (moveInput < 0f) sprite.flipX = true;
        }

        // Move
        if (rb != null)
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Skok
        if (isJump && groundChecker != null && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
    }
}