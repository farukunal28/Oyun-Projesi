using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Camera MainCamera;
    public float FireRange;

    private Vector3 Direction;


    public float Wait;

    public bool FireFree;

    public GameObject Effect;

    public float  recoilAmount;

    public SpriteRenderer GunSpriteRenderer;


    public bool Bool;


    public ParticleSystem part;

    void Update()
    {
        
        RotateWeapon();
        Fire();
       
    }

    void RotateWeapon()
    {
        Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Direction = (mousePos - transform.position).normalized;

        float Angle =   Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;




        // Angle= Mathf.Clamp(Angle, MinAngle, MaxAngle);

        DegreeConstant(Angle);

        transform.rotation = Quaternion.Euler(0, 0, Angle);
    }



    void Fire()
    {
      

        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Direction, FireRange, LayerMask.GetMask("Enemy"));

        if (Input.GetMouseButtonDown(0) && FireFree == true)
        {
        
            Recoil(recoilAmount);
            StartCoroutine(FireWait());

        

            if (Hit.collider != null && Hit.collider.CompareTag("Enemy"))
            {
                EnemyContorol enemyContorol =  Hit.collider.GetComponent<EnemyContorol>();

                if (enemyContorol) 
                {

                    StartCoroutine(enemyContorol.ColorChanging());

                    enemyContorol.Heart -= 25;

                  
                }
            }


          


        }
    }
    private void Particle()
    {
        part.Play();
    }

    void DegreeConstant(float Angle) 
    {

        if(Angle >90 || Angle < -90) 
        {


            GunSpriteRenderer.flipY = true;
         
        }
        else 
        {

            GunSpriteRenderer.flipY = false;
         
        }

    }


    void Recoil(float recoilAmount) 
    {


        Vector3 RecoilPower = -Direction * recoilAmount;
        transform.position += RecoilPower ;

    }
  


    IEnumerator FireWait() 
    {
        FireFree = false;       
        yield return new WaitForSeconds(Wait);
    
         FireFree = true;      
    }

   
   
}