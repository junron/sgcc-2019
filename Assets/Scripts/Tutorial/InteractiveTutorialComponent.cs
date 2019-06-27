using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveTutorialComponent : ITutorialComponent
{
  public GameObject mask { get; private set; }
  public GameObject gObject { get; private set; }
  public Text textOutput { get; set; }
  private string promptText;
  private string completedText;
  private Action<Action> afterInit;
  private float prevTimeScale;
  public bool completed { get; set; }

  public InteractiveTutorialComponent(TutorialMask mask, string promptText, string completedText,
    Action<Action> afterInit)
  {
    this.mask = mask.mask;
    gObject = mask.gObject;
    this.promptText = promptText;
    this.completedText = completedText;
    this.afterInit = afterInit;
  }

  public void Show()
  {
    prevTimeScale = Time.timeScale;
    Time.timeScale = 1;
    mask.SetActive(true);
    textOutput.text = promptText;
    afterInit(() =>
    {
      Debug.Log("completed");
      textOutput.text = completedText;
      completed = true;
    });
  }

  public void Hide()
  {
    Time.timeScale = prevTimeScale;
    mask.SetActive(false);
    textOutput.text = "";
  }
}