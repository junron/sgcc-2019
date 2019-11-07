using UnityEngine;
using UnityEngine.UI;

public class party:MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = Variables.GetTotalGifts();
    }
}