using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected bool canFire = true;
    public float setDuration;
    public float reloadDuration;
    public float range;
    public float damage;

    public float magazineSize;
    public float currentMagazine;

    public float ammo;

    protected Vector3 direction;
    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem particle;

    protected void Load()
    {
        float empty = magazineSize - currentMagazine;
        if (empty < ammo)
        {
            ammo -= empty;
            currentMagazine += empty;
        }
        else
        {
            currentMagazine = ammo;
            ammo = 0;
        }
        if(currentMagazine > 0)
        {
            canFire = true;
        }
    }
    protected void ThrowBullet()
    {
        if(currentMagazine > 0)
        {
            currentMagazine -= 1;
            if(currentMagazine == 0)
            {
                canFire = false;
            }
        }
    }
    protected void CheckSwitchGun(float angle)
    {
        if (angle > 90 || angle < -90)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }

    }
    protected IEnumerator ShotDelay()
    {
        canFire = false;
        StartCoroutine(SetAnim());
        yield return new WaitForSeconds(setDuration);
        canFire = true;
    }
    protected IEnumerator SetAnim()
    {
        while (!canFire)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
    }
    protected IEnumerator ReloadDelay()
    {
        canFire = false;
        StartCoroutine(ReloadAnim());
        yield return new WaitForSeconds(reloadDuration);
        canFire = true;
    }
    protected IEnumerator ReloadAnim()
    {
        while (!canFire)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
        Load();
    }



    protected void Fire(string layer)
    {
        ThrowBullet();
        StartCoroutine(ShotDelay());
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, LayerMask.GetMask(layer));

        if (hit.collider != null && hit.collider.CompareTag(layer))
        {
            if (hit.collider.GetComponent<CharacterBase>())
            {
                CharacterBase target = hit.collider.GetComponent<CharacterBase>();
                target.GetShoot(damage);
            }
        }
    }
}