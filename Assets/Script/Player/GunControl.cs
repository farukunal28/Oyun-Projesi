using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Camera MainCamera;
    public float range;
    public float damage;

    private Vector3 direction;


    public float Wait;

    private bool canFire = true;



    [SerializeField] private SpriteRenderer spriteRenderer;



    [SerializeField] private ParticleSystem particle;


    void Update()
    {
        RotateWeapon();
        CheckFire();

    }

    void RotateWeapon()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        direction = (worldPos - transform.position).normalized;


        float Angle =   Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        CheckSwitchGun(Angle);


        transform.rotation = Quaternion.Euler(0, 0, Angle);
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
            OnClick();
        }
    }
    private void OnClick()
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
        yield return new WaitForSeconds(Wait);
    
         canFire = true;      
    }

  



   
   
}