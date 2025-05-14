
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterControl : MonoBehaviour
{
   public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    public float Speed;



    public int Health;

    public Animator Anim;


    private void Update()
    {
        Move();
    }


    private void Move()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 MoveDirection = new Vector2(moveX , moveY ).normalized;

        rb.velocity = new Vector2(MoveDirection.x * Speed , MoveDirection.y * Speed);

        RightLeft(moveX);

        Anim.SetFloat("Speed", rb.velocity.magnitude);


        
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed /= 2;
        }




    }

    public void HealthContorol() 
    {

        if(Health <= 0) 
        {
            SceneManager.LoadScene(0);

        }

    }



    private void RightLeft(float moveX)
    {
        
        if(moveX < 0) 
        {
            spriteRenderer.flipX = true;
            
        }
        else 
        {
            spriteRenderer.flipX = false;
     
        }
    }
}
