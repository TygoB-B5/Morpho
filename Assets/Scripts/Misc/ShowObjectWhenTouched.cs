using UnityEngine;

public class ShowObjectWhenTouched : MonoBehaviour
{
    public GameObject Object;

    private void Start()
    {
        Object.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Object.SetActive(true);
    }
}
