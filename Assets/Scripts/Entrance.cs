using UnityEngine;

public class Entrance : MonoBehaviour
{
    public GameObject player;
    public bool OnTriggerEnter2D(Collider2D other)
    {
        return other.gameObject == player;
    }
}
