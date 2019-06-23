using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrace : MonoBehaviour
{
    public GameObject player;
    public bool OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player) return true;
        return false;
    }
}
