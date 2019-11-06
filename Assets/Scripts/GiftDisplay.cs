using UnityEngine;
using UnityEngine.UI;

public class GiftDisplay : MonoBehaviour
{
  private Text giftNo;

  private void Start()
  {
    Variables.giftDisplay = this;
    giftNo = GetComponentInChildren<Text>();
    giftNo.text = Variables.GetTotalGifts();
  }

  public void UpdateGifts()
  {
    giftNo.text = Variables.GetTotalGifts();
  }

}