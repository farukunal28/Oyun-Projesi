using System.Collections;
using UnityEngine;
using TMPro;

public abstract class Gun : MonoBehaviour
{
    protected bool gunEstablished = true;
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

    [SerializeField] protected AudioSource shootVoice;

    [SerializeField] LayerMask expectedMask;

    public TextMeshProUGUI ammoText;

    public TextMeshProUGUI magazineText;
    protected void Load()
    {
        float empty = magazineSize - currentMagazine;
        if (empty < ammo)
        {
            ammo -= empty;
            currentMagazine += empty;
            TextPrinting(currentMagazine, ammo);//faruk
        }
        else
        {
            currentMagazine = ammo;
            ammo = 0;
        }
        if(currentMagazine > 0)
        {
            gunEstablished = true;
        }
    }
    protected void ThrowBullet()
    {
        if(currentMagazine > 0)
        {
            currentMagazine -= 1;
            TextPrinting(currentMagazine, ammo);//faruk
            if (currentMagazine == 0)
            {
                gunEstablished = false;
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
        gunEstablished = false;
        StartCoroutine(SetAnim());
        yield return new WaitForSeconds(setDuration);
        gunEstablished = true;
    }
    protected IEnumerator SetAnim()
    {
        while (!gunEstablished)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
    }
    protected IEnumerator ReloadDelay()
    {
        gunEstablished = false;
        StartCoroutine(ReloadAnim());
        yield return new WaitForSeconds(reloadDuration);
        gunEstablished = true;
    }
    protected IEnumerator ReloadAnim()
    {
        while (!gunEstablished)
        {
            transform.Rotate(0, 0, Time.deltaTime * 1000);
            yield return null;
        }
        Load();
    }


    protected void Fire()
    {
        shootVoice.Play();

        ThrowBullet();
        StartCoroutine(ShotDelay());
     
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, expectedMask);

        if (hit)
        {
            if (hit.collider.GetComponent<CharacterBase>())
            {
                CharacterBase target = hit.collider.GetComponent<CharacterBase>();
                target.GetShoot(damage);
            }
        }
    }



    void TextPrinting(float currentMagazine, float Ammo)
    {
        if (ammoText && magazineText)
        {
            ammoText.text = currentMagazine.ToString();

            magazineText.text = Ammo.ToString();
        }

    }//faruk


}