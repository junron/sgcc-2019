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
    private static Sprite tick, cross;
    public Sprite tick1, cross1;
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
    private static int clearedHazards;
    private Sprite sprite;
    private int attempts;
    private static Text descriptionText2;

    public void Start()
    {
      sprite = GetComponent<SpriteRenderer>().sprite;
      if (tick1) tick = tick1;
      if (cross1) cross = cross1;
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
      descriptionText2 = feedbackPanel.transform.Find("Description").GetComponent<Text>();
      feedbackImage = feedbackPanel.transform.Find("FeedbackImage").GetComponent<Image>();
      Assert.IsNotNull(feedbackImage);
      nextBtn2 = feedbackPanel.transform.Find("NextBtn").GetComponent<Button>();
      Assert.IsNotNull(nextBtn2);
      hazardPanel.transform.parent.gameObject.SetActive(false);
      hazardMitigationPanel.gameObject.SetActive(false);
      feedbackPanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (disabled) return;
      disabled = true;
      if (other.gameObject != Variables.player) return;
      // Freeze time
      Variables.player.GetComponent<MainCharacterController>().inhibit = true;
      hazardPanel.transform.parent.gameObject.SetActive(true);
      hazardImage.sprite = sprite;
      titleText.text = "Hazard discovered: " + name;
      descriptionText.text = description;
      nextBtn.onClick.RemoveAllListeners();
      nextBtn.onClick.AddListener(() =>
      {
        hazardMitigationPanel.gameObject.SetActive(true);
        hazardPanel.gameObject.SetActive(false);

        hazardImage2.sprite = sprite;
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
          button.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
          button.onClick.RemoveAllListeners();
          button.onClick.AddListener(() =>
          {
            attempts++;

            Sprite image = allImages[i1];
            if (image == correctAnswer)
            {
              clearedHazards++;
              if (attempts == 1) correctAnswers++;
              feedbackPanel.gameObject.SetActive(true);
              feedbackImage.sprite = tick;
              titleText3.text = "That's right!";
              if (name == "Wet floor")
              {
                descriptionText2.text = "Other installations like grab bars can make your bathroom safer.\n" +
                                   "The HDB EASE scheme can help reduce the costs of upgrading by up to 95%!";
              }
              else
              {
                descriptionText2.text = "";
              }
              nextBtn2.GetComponentInChildren<Text>().text = "Continue";
              nextBtn2.onClick.RemoveAllListeners();
              nextBtn2.onClick.AddListener(() =>
              {
                feedbackPanel.gameObject.SetActive(false);
                hazardPanel.gameObject.SetActive(true);
                hazardMitigationPanel.gameObject.SetActive(false);
                hazardPanel.transform.parent.gameObject.SetActive(false);
                if (clearedHazards != totalHazards)
                {
                  Variables.player.GetComponent<MainCharacterController>().inhibit = false;
                  return;
                }

                // All hazards fixed
                Variables.currentReport.onButtonClick = FallScript.Complete;
                Variables.fallGifts = Mathf.Clamp(correctAnswers, 1, 3);
                Variables.currentReport.SetGifts(Mathf.Clamp(correctAnswers, 1, 3));
                Variables.giftDisplay.UpdateGifts();
              });
            }
            else
            {
              feedbackImage.sprite = cross;
              button.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0.33f);
              feedbackPanel.gameObject.SetActive(true);
              titleText3.text = "Alamak! Not correct leh!";
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
  }
}