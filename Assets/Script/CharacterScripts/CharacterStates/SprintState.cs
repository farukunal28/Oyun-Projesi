using UnityEngine;

public class SprintState : ICharacterState
{
    public void EnterState()
    {
        CharacterControl.instance.EnterSprint();
    }

    public void ExitState()
    {
        CharacterControl.instance.ExitSprint();
    }

    public void UpdateState()
    {
        CharacterControl.instance.UpdateSprint();
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
        //Crouch
        else if (Input.GetKey(KeyCode.C) && isThereEnoughStamina && mag)
        {
            CharacterControl.instance.ChangeState(new CrouchState());
        }
        //Walk
        else if (mag && (!Input.GetKey(KeyCode.LeftShift) || !isThereStamina))
        {
            CharacterControl.instance.ChangeState(new WalkState());
        }



    }
}
