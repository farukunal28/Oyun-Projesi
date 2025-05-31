using UnityEngine;

public class IdleState : ICharacterState
{
    public void EnterState()
    {
        CharacterControl.instance.EnterIdle();
    }

    public void ExitState()
    {
        CharacterControl.instance.ExitIdle();
    }

    public void UpdateState()
    {
        CharacterControl.instance.UpdateIdle();
    }
    public void CheckStateChange()
    {
        float stamina = CharacterControl.instance.GetStamina();
        bool isThereEnoughStamina = stamina > 1;
        bool mag = CharacterControl.instance.GetIsMoving();
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && isThereEnoughStamina && mag)
        {
            CharacterControl.instance.ChangeState(new SprintState());
        }
        //Crouch
        else if (Input.GetKey(KeyCode.C) && isThereEnoughStamina && mag)
        {
            CharacterControl.instance.ChangeState(new CrouchState());
        }
        //Walk
        else if (mag)
        {
            CharacterControl.instance.ChangeState(new WalkState());
        }
    }
}
