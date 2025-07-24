using System;

public class Coin : Item
{
    public event Action<Coin> Taked;

    public override void Interract()
    {
        gameObject.SetActive(false);
        Taked?.Invoke(this);
    }
}
