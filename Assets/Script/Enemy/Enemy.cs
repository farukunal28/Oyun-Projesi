using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private float health = 10;
    public float Speed;

    public Transform target;
    public CharacterControl characterControl;
    private void Start()
    {
        if (target == null)
        { target = GameObject.Find("Karakter").transform; }
        characterControl = target.GetComponent<CharacterControl>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }



    #region Combat
    public void DealDamage(Enemy enemy, float damage)
    {
        characterControl.TakeDamage(1);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}