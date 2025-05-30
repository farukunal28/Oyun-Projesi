using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerGun : Gun
{

    [SerializeField] TextMeshProUGUI ammoText, magazineText;
    protected override void Update()
    {
        base.Update();
        HandleFire();
        HandleReload();
    }
    protected override void RotateWeapon()
    {
        if (gunReady)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            direction = (worldPos - transform.position).normalized;


            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            CheckSwitchGun(Angle);


            transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }
    protected override void Load()
    {
        base.Load();
        TextPrinting(currentMagazine, ammo);//faruk
    }
    protected override void ThrowBullet()
    {
        base.ThrowBullet();
        TextPrinting(currentMagazine, ammo);//faruk
    }



    void HandleReload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadDelay());
        }
    }
    void HandleFire()
    {
        if (Input.GetMouseButtonDown(0) && gunReady == true && currentMagazine > 0)
        {
            Fire();
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