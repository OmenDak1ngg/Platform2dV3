using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly string ParameterIsRunning = "IsRunning";
    private readonly string ParameterAttacked = "Attacked";
    private readonly string ParameterKilled = "Killed";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(ParameterIsRunning, false);
    }

    public void RunAnimation(bool isRunnning)
    {
        _animator.SetBool(ParameterIsRunning, isRunnning);
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger(ParameterAttacked);
    }

    public void KilledAnimation()
    {
        _animator.SetTrigger(ParameterKilled);
    }

}
