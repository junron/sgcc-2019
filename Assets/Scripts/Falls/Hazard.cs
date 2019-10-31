using System;
using UnityEngine;

namespace Falls
{
  public class Hazard : MonoBehaviour
  {
    public bool disabled;
    public String name, description;
    private static GameObject hazardPanel;

    public void Start()
    {
      if (hazardPanel != null) return;
      hazardPanel = GameObject.Find("HazardPanel");
      hazardPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (disabled) return;
      if (other.gameObject == Variables.player)
      {
        // Freeze time
        Time.timeScale = 0;
        hazardPanel.SetActive(true);
      }
    }
  }
}