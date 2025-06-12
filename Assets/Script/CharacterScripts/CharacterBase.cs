using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Animator Anim;


    public float speed;

    public float maxHealth;
    public float health;

    public Slider HealtBar;



    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();
    #region Combat
    public virtual void DealDamage(CharacterBase target,float damage)
    {
        target.TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        WriteHealthBar();
        CheckHealth();
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    protected abstract void Die();
    #endregion

    protected void TurnDirection(float moveX)
    {

        if (moveX < 0)
        {
            spriteRenderer.flipX = true;

        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void WriteHealthBar()//faruk 
    {
        HealtBar.value = health;
    }

    private IEnumerator FREAKYDAMAGEANÝMATÝON()
    {
        spriteRenderer.flipY = true;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.flipY = false;
    }
}
