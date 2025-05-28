
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterControl : CharacterBase
{



    private void Awake()
    {
        HealtBar.maxValue = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }

    #region Movement
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX , moveY ).normalized;
        rb.velocity = moveDirection * speed;

        TurnDirection(moveX);

        Sprint();
        Crouch();

        Anim.SetFloat("Speed", rb.velocity.magnitude);

    }
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
    }
    private void Crouch()   
    {
        if (Input.GetKey(KeyCode.C) && rb.velocity.magnitude > 0)
        {
            crouch = true;
            Anim.SetBool("Crouch", true);
        }
        if (!Input.GetKey(KeyCode.C) || rb.velocity.magnitude == 0)
        {
            crouch = false;
            Anim.SetBool("Crouch", false);
        }
    }
    private void TurnDirection(float moveX)
    {
        
        if(moveX < 0) 
        {
            spriteRenderer.flipX = true;
            
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
     
        }
    }
    #endregion

    protected override void Die()
    {

    }
}
