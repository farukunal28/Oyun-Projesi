using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : CharacterBase
{

    public Transform target;
    public CharacterControl characterControl;
    private EnemyGun gun;

    private void Start()
    {
        if (target == null)
        { target = GameObject.Find("Karakter").transform; }
        characterControl = target.GetComponent<CharacterControl>();
        gun = GetComponentInChildren<EnemyGun>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, target.transform.position) >= gun.range * 0.7f)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }



}