using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private CapsuleCollider2D myCollider;
    private CapsuleCollider2D feet;
    private float gravity;
    private bool isAlive = true;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Vector2 deathKick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        GameObject temp = GameObject.Find("feet");
        if(temp != null )feet = temp.GetComponent<CapsuleCollider2D>();
        gravity = myRigidBody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) {return;}
            Run();
            FlipSprite();
            ClimbLadder();
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Platform"))) { return; }
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0, jumpSpeed);
        }
    }
    private void OnFire(InputValue value)
    {
        Instantiate(bullet,gun.position,transform.rotation);
    }
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    private void ClimbLadder()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x,moveInput.y*runSpeed);
            myRigidBody.velocity = playerVelocity;
            myRigidBody.gravityScale = 0;
            myAnimator.SetBool("isClimbing", true);
            myAnimator.speed = 1;
            if(moveInput.y==0) 
            {
                myAnimator.speed = 0;
            }
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravity;
            myAnimator.speed = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy"&& isAlive)
        {
            Die();
        }
    }
    private void Die()
    {
        isAlive = false;
        myAnimator.SetTrigger("Dying");
        myRigidBody.velocity=deathKick;
        FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
    }
}
