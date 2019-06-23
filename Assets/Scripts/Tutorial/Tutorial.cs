using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement; not anymore
using UnityEngine.UI;

public class Tutorial
{
  private Text textOutput;
  private List<ITutorialComponent> components;
  private int currentComponentIndex;
  private TutorialComponent last;
  private Camera cam;
  private string s;
  public GameObject player;
  public Tutorial(Text textOutput, string s, Camera cam,
    string startText =
      "Welcome to Blobbo. This tutorial will guide you through how to play the game.\nPress space to continue or q to quit.",
    string endText = "You have come to the end of the tutorial. Press space to play the game."
  )
  {
    this.cam = cam;
    this.textOutput = textOutput;
    components = new List<ITutorialComponent>();
    Add(new TutorialComponent(startText));
    last = new TutorialComponent(endText);
    last.textOutput = this.textOutput;
    this.s = s;
  }

  public void Add(ITutorialComponent c)
  {
    c.textOutput = textOutput;
    c.Hide();
    components.Add(c);
  }

  public void Next(int index)
  {
    Debug.Log(index + " " + components.Count);
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

  public void End()
  {
    ITutorialComponent prev = components[currentComponentIndex];
    prev.Hide();
    last.Show();
  }

  public void Continue()
  {
    Time.timeScale = 1;
    TutorialRunner.completed = true;
    Tutorial2.complete = true;
  }
}