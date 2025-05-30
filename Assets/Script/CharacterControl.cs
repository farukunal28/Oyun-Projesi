
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class CharacterControl : CharacterBase
{
    enum State
    {
        Idle,
        Walk,
        Sprint,
        Crouch
    }
    State state = State.Idle;

    private bool crouch;
    public float stamina;
    public float maxStamina;

    Coroutine coroutine;

    protected override void Awake()
    {
        base.Awake();
        HealtBar.maxValue = maxHealth;
    }
    protected override void Update()
    {
        StateF();
        base.Update();

    }
    #region Movement
    protected override void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX , moveY ).normalized;
        rb.velocity = moveDirection * speed;

        TurnDirection(moveX);

        Anim.SetFloat("Speed", rb.velocity.magnitude);

    }
    #endregion
    public override void TakeDamage(float damage)
    {
        if (!crouch || Random.Range(0, 4) != 0)
        {
            base.TakeDamage(damage);
        }
    }
    protected override void Die()
    {

    }

    private IEnumerator ConsumeStamina(float value)
    {
        while (true)
        {
            stamina -= Time.deltaTime * value;
            yield return null;
        }
    }



    private void StateF()
    {
        bool shift = Input.GetKeyDown(KeyCode.LeftShift);
        bool shiftx = Input.GetKeyUp(KeyCode.LeftShift);
        bool c = Input.GetKeyDown(KeyCode.C);
        bool cx = Input.GetKeyUp(KeyCode.C);
        bool stam = stamina > 1;
        bool mag = rb.velocity.magnitude != 0;
        switch (state)
        {
            case State.Idle:
                //Sprint
                if (shift && stam && mag) {EndIdle(); Sprint(); }
                //Crouch
                else if (c && stam && mag) { EndIdle(); Crouch(); }
                //Walk
                else if (mag) { EndIdle(); Walk(); }

                break;

            case State.Walk:
                //Idle
                if(!mag) { EndWalk(); Idle(); }
                //Sprint
                else if (shift && stam && mag) { EndWalk(); Sprint(); }
                //Crouch
                else if (c && stam && mag) { EndWalk(); Crouch(); }

                break;

            case State.Sprint:
                //Idle
                if (!mag) { EndSprint(); Idle(); }
                //Crouch
                else if (c && stam && mag) { EndSprint(); Crouch(); }
                //Walk
                else if (shiftx || mag) { EndSprint(); Walk(); }

                break;

            case State.Crouch:
                //Idle
                if (!mag) { EndCrouch(); Idle(); }
                //Sprint
                if (shift && stam && mag) { EndCrouch(); Sprint(); }
                //Walk
                else if (cx || mag) { EndCrouch(); Walk(); }
                Debug.Log(cx + "" + mag);

                break;
        }
    }

    private void Idle()
    {

    }
    private void EndIdle()
    {

    }
    private void Walk()
    {

    }
    private void EndWalk()
    {

    }
    private void Sprint()
    {
        speed *= 2;
        if (coroutine == null)
        { coroutine = StartCoroutine(ConsumeStamina(1)); }
    }
    private void EndSprint()
    {
        speed /= 2;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    private void Crouch()
    {
        crouch = true;
        Anim.SetBool("Crouch", true);
        if (coroutine == null)
        { coroutine = StartCoroutine(ConsumeStamina(1)); }
    }
    private void EndCrouch()
    {
        crouch = false;
        Debug.Log("a");
        Anim.SetBool("Crouch", false);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
