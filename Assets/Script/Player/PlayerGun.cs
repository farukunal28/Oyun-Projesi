using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float range;
    public float damage;
    public float duration;

    private Vector3 direction;



    private bool canFire = true;



    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        RotateWeapon();
        CheckFire();
    }

    void RotateWeapon()
    {
        if (canFire)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            direction = (worldPos - transform.position).normalized;


            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            CheckSwitchGun(Angle);


            transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }
    void CheckSwitchGun(float Angle) 
    {

        if(Angle >90 || Angle < -90) 
        {
            spriteRenderer.flipY = true;
        }
        else 
        {
            spriteRenderer.flipY = false;
        }

    }



    void CheckFire()
    {
        if (Input.GetMouseButtonDown(0) && canFire == true)
        {
            Fire();
        }
    }
    private void Fire()
    {
        StartCoroutine(FireWait());
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void Particle()
    {
        GetComponent<ParticleSystem>().Play();
    }
    IEnumerator FireWait() 
    {
        canFire = false;
        StartCoroutine(Reload());
        yield return new WaitForSeconds(duration);
        canFire = true;      
    }
  
    IEnumerator Reload()
    {
        while (!canFire)
        {
            transform.Rotate(0, 0, Time.deltaTime * 500);
            yield return null;
        }
    }


   
   
}