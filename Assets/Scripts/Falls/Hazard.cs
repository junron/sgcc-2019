using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Falls
{
  public class Hazard : MonoBehaviour
  {
    public bool disabled;
    public String name, description;
    private static GameObject hazardPanel;
    private static Text titleText;
    private static Image hazardImage;
    private static Text descriptionText;
    private static Button nextBtn;

    public void Start()
    {
      if (hazardPanel != null) return;
      hazardPanel = GameObject.Find("HazardPanel");
      Assert.IsNotNull(hazardPanel);
      titleText = hazardPanel.transform.Find("TitleText").GetComponent<Text>();
      Assert.IsNotNull(titleText);
      hazardImage = hazardPanel.transform.Find("HazardImage").GetComponent<Image>();
      Assert.IsNotNull(hazardImage);
      descriptionText = hazardPanel.transform.Find("Description").GetComponent<Text>();
      Assert.IsNotNull(descriptionText);
      nextBtn = hazardPanel.transform.Find("NextBtn").GetComponent<Button>();
      Assert.IsNotNull(nextBtn);
      hazardPanel.transform.parent.gameObject.SetActive(false);
      nextBtn.onClick.AddListener(() =>
      {
        print("CLicked");
      });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (disabled) return;
      if (other.gameObject != Variables.player) return;
      // Freeze time
      Time.timeScale = 0;
      hazardPanel.transform.parent.gameObject.SetActive(true);
      hazardImage.sprite = GetComponent<SpriteRenderer>().sprite;
      titleText.text = "Hazard discovered: " + name;
      descriptionText.text = description;
    }
  }
}