using UnityEngine;

public class CrouchState : ICharacterState
{
    public void EnterState()
    {
        CharacterControl.instance.EnterCrouch();
    }

    public void ExitState()
    {
        CharacterControl.instance.ExitCrouch();
    }

    public void UpdateState()
    {
        CharacterControl.instance.UpdateCrouch();
    }
    public void CheckStateChange()
    {
        float stamina = CharacterControl.instance.GetStamina();
        bool isThereStamina = stamina > 0;
        bool isThereEnoughStamina = stamina > 1;
        bool mag = CharacterControl.instance.GetIsMoving();
        //Idle
        if (!mag)
        {
            CharacterControl.instance.ChangeState(new IdleState());
        }
        //Sprint
        else if (Input.GetKey(KeyCode.LeftShift) && isThereEnoughStamina && mag)
        {
            CharacterControl.instance.ChangeState(new SprintState());
        }
        //Walk
        else if ((!Input.GetKey(KeyCode.C) && mag) || !isThereStamina)
        {
            CharacterControl.instance.ChangeState(new WalkState());
        }
    }
}
