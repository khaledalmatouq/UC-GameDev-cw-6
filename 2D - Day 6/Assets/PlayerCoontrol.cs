using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoontrol : MonoBehaviour
{


    Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    bool grounded;
    public Transform groundCheck;

    public float radius;
    public LayerMask ground;

    bool isRight = true;
    SpriteRenderer sprite;

    Animator anim;
    public string toPlay;
    public string currentAnim;
    const string IDLE_ANIM = "Idle";
    const string RUN_ANIM = "Run";
    const string JUMP_ANIM = "Jump";
   




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
        playerJump();
        //playAnim();
    }

    void movePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,rb.velocity.y);

        if (isRight && Input.GetKey(KeyCode.A)) 
        {
            isRight = false;
            sprite.flipX = true;
        }

        else if (!isRight && Input.GetKey(KeyCode.D))
        {
            isRight = true;
            sprite.flipX = false;
        }

        if ( rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            playAnim(IDLE_ANIM);
        }
        else if ( rb.velocity.x != 0 && rb.velocity.y == 0)
        {
            playAnim(RUN_ANIM);
        }
    }

    //private void playAnim(string iDLE_ANIM)
    //{
    //    throw new NotImplementedException();
    //}

    void playerJump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playAnim(JUMP_ANIM);

        }
    }

    void playAnim(string toPlay)
    {
        if (currentAnim == toPlay)
        {
            return;
        }
        currentAnim = toPlay;
        anim.Play(toPlay);
    }

    
}
