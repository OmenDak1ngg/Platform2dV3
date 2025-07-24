using System;
using UnityEngine;

public class AidKit : Item
{
    [SerializeField] private int _healCount;

    public int HeatlCount => _healCount;

    public event Action<AidKit> Taked;

    public override void Interract()
    {
        Taked?.Invoke(this);
    }
}
