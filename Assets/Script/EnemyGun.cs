using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    Enemy enemy;
    CharacterControl characterControl;



    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        characterControl = enemy.characterControl;
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        RotateWeapon();
        if (CheckFire())
        {
            Fire("Player");
        }
    }
    void RotateWeapon()
    {
        if(canFire)
        {
            direction = (characterControl.transform.position - transform.position).normalized;


            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            CheckSwitchGun(Angle);


            transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }


    public bool CheckFire()
    {
        float dis = Vector3.Distance(characterControl.transform.position, transform.position);
        
        if (dis <= range && canFire)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




}
