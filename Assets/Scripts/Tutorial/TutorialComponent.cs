using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : ITutorialComponent
{
  private GameObject mask { get; set; }
  private GameObject gObject { get; set; }
  public Text textOutput { get; set; }
  private string text;
  private Action afterInit;
  public bool completed { get; set; } = true;

  public TutorialComponent(TutorialMask mask, string text, Action afterInit = null)
  {
    this.mask = mask?.mask;
    gObject = mask?.gObject;
    this.text = text;
    this.afterInit = afterInit;
  }

  public TutorialComponent(string text) : this(null, text)
  {
  }

  public void Show()
  {
    Time.timeScale = 0;
    if (this.mask) this.mask.SetActive(true);
    if (this.textOutput != null && text != null) textOutput.text = text;
    afterInit?.Invoke();
  }

  public void Hide()
  {
    Time.timeScale = 1;
    if (mask) mask.SetActive(false);
    if (textOutput != null && text != null) textOutput.text = "";
  }
}