using UnityEngine;

public class Wave : MonoBehaviour
{
    private Vector3 InitialPosition;
    public float Speed = 1;
    public float Amount = 100;

    private void Awake()
    {
        InitialPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = InitialPosition + transform.up * Mathf.Sin(Time.time * Speed) * Amount;
    }
}
