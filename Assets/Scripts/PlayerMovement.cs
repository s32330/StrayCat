using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 500f;
    public float jumpForce = 300f;

    [HideInInspector] public float moveInput = 0f;
    [HideInInspector] public bool isJump = false;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    public GroundChecker groundChecker;
    public PlayerHealth health;  // referencja do PlayerHealth

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // jeœli gracz martwy – blokujemy ruch i animacje
        if (health != null && health.isDead)
        {
            anim.SetFloat("IsMove", -1); // zatrzymanie animacji ruchu
            return;
        }

        // Animacje
        anim.SetFloat("verticalVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", groundChecker.isGrounded);

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
            isJump = true;

        // Ruch poziomy
        moveInput = Input.GetAxis("Horizontal");
        anim.SetFloat("IsMove", moveInput != 0 ? 1 : -1);
    }

    private void FixedUpdate()
    {
        if (health != null && health.isDead) return; // blokada ruchu po œmierci

        // Obrót sprite w zale¿noœci od kierunku ruchu
        if (moveInput > 0f)
            sprite.flipX = false;
        else if (moveInput < 0f)
            sprite.flipX = true;

        // Ruch
        rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);

        // Skok
        if (isJump && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
    }
}
