using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyContorol : MonoBehaviour
{
    public int Heart;
    public int Speed;
    public GameObject Target;

    public float ColorWait;
    public Color CaspianColor;
    public bool EnemyCospian;

    public CharacterControl characterControl;

    private Coroutine damageCoroutine;
    private void Start()
    {
        Target = GameObject.Find("Karakter");
        characterControl = Target.GetComponent<CharacterControl>();
    }

    private void Update()
    {

        EnmeyMove();


    }

    private void EnmeyMove()
    {

        

        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
         


    }

    public void HeartContorol () 
    {

        if (Heart <= 0) 
        
        {
            Destroy(gameObject);
        
        }

    }

    public IEnumerator ColorChanging()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = CaspianColor;

        yield return new WaitForSeconds(ColorWait);

        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        HeartContorol();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Coroutine baþlat
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(Caspian());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Coroutine durdur
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    public IEnumerator Caspian()
    {
        while (true)
        {
            characterControl.Health -= 20;
            characterControl.HealthContorol();
            Debug.Log("x_x7");
            yield return new WaitForSeconds(0.5f); // her 0.5 saniyede bir hasar verir
        }
    }
}