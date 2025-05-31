using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy : CharacterBase
{

    public Transform target;
    public CharacterControl characterControl;
    private EnemyGun gun;

    protected override void Awake()
    {
        base.Awake();
        if (target == null)
        { target = GameObject.Find("Karakter").transform; }

        characterControl = target.GetComponent<CharacterControl>();
        gun = GetComponentInChildren<EnemyGun>();
    }

    protected override void Move()
    {
        TurnDirection(target.position.x - transform.position.x);
        if (!CheckRange() || !gun.isSeeing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
    
    public bool CheckRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= (gun.range * 0.7f);
    }


    public void FindFlankPoint(Obstacle obs)
    {
        List<Transform> list = obs.FindSeeingFlankPoints();

        Transform closest = list[0];

        for (int i = 1; i < list.Count - 1; i++)
        {
            if(Vector2.Distance(transform.position, list[i].position) < Vector3.Distance(transform.position, closest.position))
            {
                closest = list[i];
            }
        }
        target = closest;
    }

}