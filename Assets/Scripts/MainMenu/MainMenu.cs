using System;
using UnityEngine;
using UnityEngine.AI;

namespace MainMenu
{
  public class MainMenu : MonoBehaviour
  {
    private int state;
    private Rigidbody2D rb2d;

    private void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      rb2d.velocity = Vector2.down * 40;
    }

    private void Update()
    {
      if (state == 0 && Mathf.Approximately(transform.position.y,140))
      {
        state = 1;
        rb2d.velocity = Vector2.right * 40;
      }
    }
  }
}