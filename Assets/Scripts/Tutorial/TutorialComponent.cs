using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : ITutorialComponent
{
  public GameObject mask { get; private set; }
  public GameObject gObject { get; private set; }
  public Text textOutput { get; set; }
  private string text;
  private Action afterInit;
  public bool completed { get; set; } = true;

  public TutorialComponent(TutorialMask mask, string text, Action afterInit = null)
  {
    this.mask = mask == null ? null : mask.mask;
    gObject = mask == null ? null : mask.gObject;
    this.text = text;
    this.afterInit = afterInit;
  }

  public TutorialComponent(string text) : this(null, text)
  {
  }

  public TutorialComponent(TutorialMask mask) : this(mask, null)
  {
  }

  public void Show()
  {
    Time.timeScale = 0;
    if (mask) mask.SetActive(true);
    if (textOutput != null && text != null) textOutput.text = text;
    if (afterInit != null)
    {
      afterInit();
    }
  }

  public void Hide()
  {
    Time.timeScale = 1;
    if (mask) mask.SetActive(false);
    if (textOutput != null && text != null) textOutput.text = "";
  }
}