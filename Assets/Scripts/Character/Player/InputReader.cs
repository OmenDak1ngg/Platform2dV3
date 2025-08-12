using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal = nameof(Horizontal);
    private readonly KeyCode JumpKey = KeyCode.Space;
    private readonly KeyCode AttackKey = KeyCode.F;
    private readonly KeyCode VampireKey = KeyCode.LeftAlt;

    public event Action Jumped;
    public event Action Attacked;
    public event Action Vampired;

    public bool IsTryedToJump { get; private set; }
    public float Direction { get; private set; }
    
    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpKey))
        {
           Jumped?.Invoke();
        }

        if (Input.GetKeyDown(AttackKey))
        {
            Attacked?.Invoke();
        }

        if (Input.GetKeyDown(VampireKey))
        {
            Vampired?.Invoke();
        }
    }

}
