using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 500;
    public float moveInput = 0;
    public float jumpForce = 300;
    public bool isJump = false;

    public PlayerHealth health;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    public GroundChecker groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead) return;

        anim.SetFloat("verticalVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", groundChecker.isGrounded);



        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
        {
            isJump = true;
        }

        moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0) 
        { anim.SetFloat("IsMove", 1); }
        else 
        { anim.SetFloat("IsMove", -1); }

    }

    private void FixedUpdate()
    {
       
        if (moveInput > 0)
        {
            sprite.flipX = false;
        }
        else if (moveInput < 0)
        {
            sprite.flipX = true;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);

        if (isJump && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
    }
}
