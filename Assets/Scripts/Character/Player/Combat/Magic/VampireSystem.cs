using UnityEngine;

public class VampireSystem : MonoBehaviour
{
    [SerializeField] private Transform _reference;

    private void Update()
    {
        transform.position = _reference.position;
    }
}
