using System;
using UnityEngine;

public static class Variables
{
  public static int fontSize = 24;
  public static int health = 75;
  public static int happiness = 75;
  public static int wealth = 75;
  public static GameObject player;
  public static Report currentReport;
  public static GiftDisplay giftDisplay;

  public static int tutorialGifts, fallGifts, socialGifts, exerciseGifts;

  public static string GetTotalGifts()
  {
    return "" + (tutorialGifts + fallGifts + socialGifts + exerciseGifts);
  }

  public static bool AllAttempted()
  {
    return tutorialGifts > 0 && fallGifts > 0 && exerciseGifts > 0 && socialGifts > 0;
  }
}