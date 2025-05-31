public interface ICharacterState
{
    void EnterState();
    void ExitState();
    void UpdateState();
    void CheckStateChange();
}
