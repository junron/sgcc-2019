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
    private static GameObject feedbackPanel;
    private static Text titleText3;
    private static Image feedbackImage;
    private static Button nextBtn2;
    private static int correctAnswers;
    private static int totalHazards;
    private int attempts;

    public void Start()
    {
      totalHazards++;
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
      feedbackPanel = GameObject.Find("Result");
      Assert.IsNotNull(feedbackPanel);
      titleText3 = feedbackPanel.transform.Find("TitleText").GetComponent<Text>();
      Assert.IsNotNull(titleText3);
      feedbackImage = feedbackPanel.transform.Find("FeedbackImage").GetComponent<Image>();
      Assert.IsNotNull(feedbackImage);
      nextBtn2 = feedbackPanel.transform.Find("NextBtn").GetComponent<Button>();
      Assert.IsNotNull(nextBtn2);
      hazardPanel.transform.parent.gameObject.SetActive(false);
      hazardMitigationPanel.gameObject.SetActive(false);
      feedbackPanel.gameObject.SetActive(false);
      nextBtn.onClick.AddListener(() =>
      {
        hazardMitigationPanel.gameObject.SetActive(true);
        hazardPanel.gameObject.SetActive(false);

        hazardImage2.sprite = GetComponent<SpriteRenderer>().sprite;
        titleText2.text = "Hazard: " + name;
        // Shuffle answers
        Sprite[] allImages = new[] {correctAnswer}.Concat(wrongAnswers).ToArray().OrderBy(a => Guid.NewGuid())
          .ToArray();
        int i = 0;
        foreach (Button button in hazardMitigationPanel.transform.GetComponentsInChildren<Button>())
        {
          int i1 = i;
          button.transform.GetComponentInChildren<Image>().sprite = allImages[i];
          button.GetComponentInChildren<Text>().text = allImages[i].name;
          button.onClick.AddListener(() =>
          {
            attempts++;

            Sprite image = allImages[i1];
            if (image == correctAnswer)
            {
              if (attempts == 1) correctAnswers++;
              feedbackPanel.gameObject.SetActive(true);
              titleText3.text = "That's right!";
              descriptionText.text = description;
              nextBtn2.GetComponentInChildren<Text>().text = "Continue";
              nextBtn2.onClick.RemoveAllListeners();
              nextBtn2.onClick.AddListener(() =>
              {
                feedbackPanel.gameObject.SetActive(false);
                hazardPanel.gameObject.SetActive(true);
                hazardMitigationPanel.gameObject.SetActive(false);
                hazardPanel.transform.parent.gameObject.SetActive(false);
                correctAnswers++;
                if (correctAnswers != totalHazards) return;
                // All hazards fixed
                Variables.currentReport.onButtonClick = () => { print("yay"); };
                Variables.currentReport.SetGifts(Mathf.Clamp(correctAnswers,1,3));
              });
            }
            else
            {
              button.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0.33f);
              feedbackPanel.gameObject.SetActive(true);
              titleText3.text = "That's not quite right!";
              descriptionText.text = "";
              nextBtn2.GetComponentInChildren<Text>().text = "Try again";
              nextBtn2.onClick.RemoveAllListeners();
              nextBtn2.onClick.AddListener(() => { feedbackPanel.gameObject.SetActive(false); });
            }
          });
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