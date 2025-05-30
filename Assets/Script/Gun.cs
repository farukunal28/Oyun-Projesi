using System.Collections;
using UnityEngine;
using TMPro;

public abstract class Gun : MonoBehaviour
{
    public float setDuration;
    public float reloadDuration;
    public float range;
    public float damage;
    public float magazineSize;
    public float currentMagazine;
    public float ammo;

    protected bool gunReady = true;
    protected Vector3 direction;

    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem particle;
    protected AudioSource shootVoice;

    [SerializeField] LayerMask targetMask;


    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        shootVoice = GetComponent<AudioSource>();
    }
    protected virtual void Update()
    {
        RotateWeapon();
    }

    protected abstract void RotateWeapon();
    protected virtual void Load()
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
            gunReady = true;
        }
    }
    protected virtual void ThrowBullet()
    {
        currentMagazine -= 1;
        if (currentMagazine == 0)
        {
            gunReady = false;
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

    #region Animasyonlar
    protected IEnumerator StraightenUp()
    {
        gunReady = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SetGun());
    }
    protected IEnumerator SetGun()
    {
        StartCoroutine(SetAnim());
        yield return new WaitForSeconds(setDuration);
        gunReady = true;
    }
    protected IEnumerator SetAnim()
    {
        while (!gunReady)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
    }
    protected IEnumerator ReloadDelay()
    {
        gunReady = false;
        StartCoroutine(ReloadAnim());
        yield return new WaitForSeconds(reloadDuration);
        gunReady = true;
    }
    protected IEnumerator ReloadAnim()
    {
        while (!gunReady)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
        Load();
    }
    #endregion

    protected void Fire()
    {
        shootVoice.Play();
          particle.Play();

        ThrowBullet();
        StartCoroutine(StraightenUp());
     
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, targetMask);

        if (hit && hit.collider.GetComponent<CharacterBase>())
        {
            CharacterBase target = hit.collider.GetComponent<CharacterBase>();
            target.TakeDamage(damage);
        }
    }





}