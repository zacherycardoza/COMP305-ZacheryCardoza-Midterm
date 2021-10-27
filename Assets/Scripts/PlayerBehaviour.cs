using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform GroundOrigin;
    public float GroundRadius;
    public LayerMask GroundLayerMask;


    private Animator animatorController;
    private Rigidbody2D rigidBody2D;


    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = 0.0f;
        float y = 0.0f;

        SetIsGrounded();
        if (isGrounded)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Jump");
            // check if player is moving
            if (x != 0)
            {
                x = FlipAnimation(x);

                // shift to run animation
                animatorController.SetFloat("xSpeed", 1); // run
            }
            else
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    animatorController.SetBool("isDucking", true);
                }
                else
                {
                    animatorController.SetBool("isDucking", false);
                    animatorController.SetFloat("xSpeed", 0); // idle
                }
            }

            animatorController.SetBool("isGrounded", true);
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal") * 0.01f; // less input when falling or jumping

            if (x != 0)
            {
                x = FlipAnimation(x);
            }

            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x * 0.90f, rigidBody2D.velocity.y);

            animatorController.SetBool("isGrounded", false); // jump
        }

        Vector2 movementVector = new Vector2(0f, y * verticalForce);
        rigidBody2D.AddForce(movementVector);
        rigidBody2D.velocity = new Vector2(x * horizontalForce, rigidBody2D.velocity.y);
    }

    private float FlipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    public void SetIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(GroundOrigin.position, GroundRadius, Vector2.down, GroundRadius, GroundLayerMask);
        isGrounded = (hit) ? true : false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(GroundOrigin.position, GroundRadius);
    }
}
