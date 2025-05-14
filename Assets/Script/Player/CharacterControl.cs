
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
   public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    public float speedMultiplier;



    public float maxHealth;
    public float health;

    public Animator Anim;


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
        rb.velocity = moveDirection * speedMultiplier;

        TurnDirection(moveX);

        Sprint();
        Crouch();

        Anim.SetFloat("Speed", rb.velocity.magnitude);

    }
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMultiplier /= 2;
        }
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
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

    #region Combat
    public void DealDamage(Enemy enemy, float damage)
    {
        enemy.TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }

    private void CheckHealth() 
    {
        if(health <= 0) 
        {
            Die();
        }
    }
    private void Die()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
