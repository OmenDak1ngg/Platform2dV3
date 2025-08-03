using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal = nameof(Horizontal);

    public event Action Jumped;
    public event Action Attacked;
    public event Action Vampired;

    public bool IsTryedToJump { get; private set; }
    public float Direction { get; private set; }
    
    private void Update()
    {
        Direction = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
           Jumped?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attacked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Vampired?.Invoke();
        }
    }

}
