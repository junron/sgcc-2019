using System;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
  private Image[] gifts;
  private Text giftNo;
  private Button continueBtn;
  public Action onButtonClick;

  private void Start()
  {
    gifts = GetComponentsInChildren<Image>();
    giftNo = this.gameObject.transform.Find("GiftNo").GetComponent<Text>();
    continueBtn = GetComponentInChildren<Button>();
    Variables.currentReport = this;
    this.gameObject.SetActive(false);
  }


  public void SetGifts(int i)
  {
    bool prevState = this.transform.parent.gameObject.activeSelf;
    giftNo.text = i.ToString();
    if (i == 1)
    {
      gifts[3].color = new Color(1f, 1f, 1f, 0.33f);
      gifts[4].color = new Color(1f, 1f, 1f, 0.33f);
    }
    else if (i == 2)
    {
      gifts[3].color = new Color(1f, 1f, 1f, 0.33f);
    }

    continueBtn.onClick.RemoveAllListeners();
    continueBtn.onClick.AddListener(() =>
    {
      this.gameObject.SetActive(false);
      this.transform.parent.gameObject.SetActive(prevState);
      onButtonClick();
    });
    this.transform.parent.gameObject.SetActive(true);
    this.gameObject.SetActive(true);
  }
}