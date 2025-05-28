using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerGun : Gun
{

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        RotateWeapon();
        CheckFire();
        checkreload();
    }
    void checkreload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadDelay());
        }
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
    void CheckFire()
    {
        if (Input.GetMouseButtonDown(0) && canFire == true && currentMagazine > 0)
        {
            Fire("Enemy");
        }
    }


    private void Particle()
    {
        GetComponent<ParticleSystem>().Play();
    }


   
}