
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterControl : CharacterBase
{
    public static CharacterControl instance;

    private bool crouch;
    public float stamina;
    public float maxStamina;


    private ICharacterState currentState;

    public TextMeshProUGUI text;


    [SerializeField] Slider staminaBar;

    protected override void Awake()
    {
        base.Awake();
        HealtBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        if(instance == null )
        {
            instance = this;
        }

        currentState = new IdleState();
        currentState.EnterState();
    }
    protected override void Update()
    {
        base.Update();

        currentState.UpdateState();
        currentState.CheckStateChange();
    }
    public void ChangeState(ICharacterState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
        text.text = currentState.ToString();
    }
    #region Movement
    protected override void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX , moveY ).normalized;
        rb.velocity = moveDirection * speed;

        TurnDirection(moveX);

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
        SceneManager.LoadScene(0);
    }






    private void WriteStaminaBar()//faruk 
    {
        staminaBar.value = stamina;
    }

    public float GetStamina()
    {
        return stamina;
    }
    public bool GetIsMoving()
    {
        return rb.velocity.magnitude != 0;
    }




    public void EnterIdle()
    {
        Anim.SetBool("Idle", true);
    }
    public void UpdateIdle()
    {
        if (stamina <= 10)
        {
            stamina += Time.deltaTime;
            WriteStaminaBar();
        }
        else if (stamina > 10)
        {
            stamina = 10;
            WriteStaminaBar();
        }
    }
    public void ExitIdle()
    {
        Anim.SetBool("Idle", false);
    }

    public void EnterWalk()
    {
        Anim.SetBool("Walk", true);
    }
    public void UpdateWalk()
    {
        if (stamina < 10)
        {
            stamina += Time.deltaTime;
            WriteStaminaBar();
        }
        else if (stamina > 10)
        {
            stamina = 10;
            WriteStaminaBar();
        }
    }
    public void ExitWalk()
    {
        Anim.SetBool("Walk", false);
    }

    public void EnterSprint()
    {
        speed *= 2;
        Anim.SetBool("Sprint", true);
    }
    public void UpdateSprint()
    {
        if (stamina > 0)
        {
            stamina -= Time.deltaTime;
            WriteStaminaBar();
        }
        else if (stamina < 0)
        {
            stamina = 0;
            WriteStaminaBar();
        }
    }
    public void ExitSprint()
    {
        speed /= 2;
        Anim.SetBool("Sprint", false);
    }

    public void EnterCrouch()
    {
        crouch = true;
        Anim.SetBool("Crouch", true);
    }
    public void UpdateCrouch()
    {
        if (stamina > 0)
        {
            stamina -= Time.deltaTime;
            WriteStaminaBar();
        }
        else if (stamina < 0)
        {
            stamina = 0;
            WriteStaminaBar();
        }
    }
    public void ExitCrouch()
    {
        crouch = false;
        Anim.SetBool("Crouch", false);
    }
}
