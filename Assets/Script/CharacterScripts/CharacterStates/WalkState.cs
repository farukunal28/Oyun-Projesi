using UnityEngine;

public class WalkState : ICharacterState
{
    public void EnterState()
    {
        CharacterControl.instance.EnterWalk();
    }

    public void ExitState()
    {
        CharacterControl.instance.ExitWalk();
    }

    public void UpdateState()
    {
        CharacterControl.instance.UpdateWalk();
    }
    public void CheckStateChange()
    {
        float stamina = CharacterControl.instance.GetStamina();
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
        //Crouch
        else if (Input.GetKey(KeyCode.C) && isThereEnoughStamina && mag)
        {
            CharacterControl.instance.ChangeState(new CrouchState());
        }
    }
}
