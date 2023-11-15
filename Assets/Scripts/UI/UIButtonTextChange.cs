using TMPro;
using UnityEngine;

public class UIButtonTextChange : MonoBehaviour
{
    public Color[] Colors;

    public void SetChildTextColor(int index)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = Colors[index];
    }
}
