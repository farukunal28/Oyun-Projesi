using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : Gun
{
    Enemy owner;
    Transform player;

    public bool isSeeing;

    [SerializeField] protected LayerMask wallMask;

    private void Start()
    {
        owner = GetComponentInParent<Enemy>();
        player = GameObject.Find("Karakter").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        RotateWeapon();

        CheckSee();

        if (isSeeing && owner.CheckRange() && gunEstablished && Random.Range(0, 100) == 0)
        {
            Fire();
        }
    }
    void RotateWeapon()
    {
        if(gunEstablished)
        {
            direction = (player.position - transform.position).normalized;


            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            CheckSwitchGun(Angle);


            transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }


    private void CheckSee()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, wallMask);

        if(hit)
        {
            isSeeing = false;
            owner.FindFlankPoint(hit.collider.GetComponent<Obstacle>());
        }
        else
        {
            owner.target = player;
            isSeeing = true;
        }
    }

}
