using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManagement; not anymore
using UnityEngine.UI;

public class Tutorial
{
  private readonly Text textOutput;
  private readonly List<ITutorialComponent> components;
  private int currentComponentIndex;
  private readonly TutorialComponent last;
  private Camera cam;
  private readonly string s;
  public GameObject player;
  public Tutorial(Text textOutput, string s, Camera cam,
    string startText =
      "Welcome to Generation Silver! I’m your in-game assistant, here to help you get through the game! Press space to continue.",
    string endText = "You have successfully completed the tutorial. Press space to play the game."
  )
  {
    this.cam = cam;
    this.textOutput = textOutput;
    components = new List<ITutorialComponent>();
    Add(new TutorialComponent(startText));
    last = new TutorialComponent(endText) {textOutput = this.textOutput};
    this.s = s;
  }

  public void Add(ITutorialComponent c)
  {
    c.textOutput = textOutput;
    c.Hide();
    components.Add(c);
  }

  private void Next(int index)
  {
    if (index >= components.Count)
    {
      if (index == components.Count)
      {
        End();
        currentComponentIndex = index;
        return;
      }

      Continue();
      return;
    }

    ITutorialComponent prev = components[currentComponentIndex];
    if (prev is InteractiveTutorialComponent && prev.completed != true)
    {
      return;
    }

    prev.Hide();
    currentComponentIndex = index;
    ITutorialComponent c = components[currentComponentIndex];
    c.Show();
  }

  public void Next()
  {
    Next(currentComponentIndex + 1);
  }

  public void Start()
  {
    Next(0);
  }

  private void End()
  {
    ITutorialComponent prev = components[currentComponentIndex];
    prev.Hide();
    last.Show();
  }

  private void Continue()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(s);
  }
}