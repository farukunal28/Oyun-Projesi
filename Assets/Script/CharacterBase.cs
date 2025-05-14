using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    public float speed;



    public float maxHealth;
    public float health;

    public Animator Anim;


    protected bool crouch;

    #region Combat
    public void DealDamage(CharacterBase target,float damage)
    {
        target.TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }
    public void GetShoot(float damage)
    {
        if (!crouch || Random.Range(0, 4) == 0)
        {
            TakeDamage(damage);
            StartCoroutine(FREAKYDAMAGEANÝMATÝON());
        }
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);//!
    }
    #endregion



    private IEnumerator FREAKYDAMAGEANÝMATÝON()
    {
        spriteRenderer.flipY = true;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.flipY = false;
    }
}
