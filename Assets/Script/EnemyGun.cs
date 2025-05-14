using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    Enemy enemy;
    CharacterControl characterControl;
    public float range;
    public float damage;
    public float duration;
    private bool canFire = true;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;

    private Vector3 direction;

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
            Fire();
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

    void CheckSwitchGun(float Angle)
    {

        if (Angle > 90 || Angle < -90)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
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


    private void Fire()
    {
        StartCoroutine(FireWait());
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, LayerMask.GetMask("Player"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (characterControl)
            {
                characterControl.GetShoot(damage);
            }
        }
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
