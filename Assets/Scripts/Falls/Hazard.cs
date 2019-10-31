using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Falls
{
  public class Hazard : MonoBehaviour
  {
    public bool disabled;
    public String name, description;
    public Sprite correctAnswer;
    public Sprite[] wrongAnswers;
    private static GameObject hazardPanel;
    private static Text titleText;
    private static Image hazardImage;
    private static Text descriptionText;
    private static Button nextBtn;

    private static GameObject hazardMitigationPanel;
    private static Text titleText2;
    private static Image hazardImage2;

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
      hazardMitigationPanel = GameObject.Find("HazardMitigation");
      Assert.IsNotNull(hazardMitigationPanel);
      titleText2 = hazardMitigationPanel.transform.Find("TitleText").GetComponent<Text>();
      Assert.IsNotNull(titleText2);
      hazardImage2 = hazardMitigationPanel.transform.Find("HazardImage").GetComponent<Image>();
      Assert.IsNotNull(hazardImage2);
      hazardPanel.transform.parent.gameObject.SetActive(false);
      hazardMitigationPanel.gameObject.SetActive(false);
      nextBtn.onClick.AddListener(() =>
      {
        hazardMitigationPanel.gameObject.SetActive(true);
        hazardPanel.gameObject.SetActive(false);

        hazardImage2.sprite = GetComponent<SpriteRenderer>().sprite;
        titleText2.text = "Hazard: " + name;
        // Shuffle answers
        Sprite[] allImages = new[] {correctAnswer}.Concat(wrongAnswers).ToArray().OrderBy(a => Guid.NewGuid()).ToArray();
        int i = 0;
        foreach (Button button in hazardMitigationPanel.transform.GetComponentsInChildren<Button>())
        {
          int i1 = i;
          print(button.transform.GetComponentInChildren<Image>());
          button.transform.GetComponentInChildren<Image>().sprite = allImages[i];
          button.onClick.AddListener(() => { print(allImages[i1]); });
          i++;
        }
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