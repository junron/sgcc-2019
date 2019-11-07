using System;
using UnityEngine;
using UnityEngine.UI;

public class Exercise : MonoBehaviour
{
  private static Exercise exercise;
  public Text text;
  public GameObject hinge;
  public GameObject parent;
  private int counter;

  private String[] script =
  {
    "You: Mr Tay! Nice to see you at the park again!",
    "Mr Tay: Whoa, time flies ah, so fast your grandson 6 years old already!",
    "You: Yalor, I also feel much older than before! I don't feel as strong anymore! What's your secret to your strength at such an age?",
    "Mr Tay: Heh, I can teach you one exercise! You'll feel much stronger after doing it for a while.",
    "You: Whoa, really so magic ah?!!",
    "Mr Tay: Let me show you the Side Leg Raise! It's recommended by the Health Promotion Board as one of the exercises for active aging!",
    "Mr Tay: Make sure you hold on to a chair first ah, so you can balance!",
    "Mr Tay: Simply raise your left leg like this!",
    "You: Whoa, so simple ah? Let me try!",
    "Task: Drag your left leg upwards!",
    "You: Whoa, It's really so easy! I'm doing this exercise every day!",
    "Mr Tay: Nice! Now you can share this with other people!",
  };

  private void Start()
  {
    hinge.GetComponent<Animator>().speed = 0;
    exercise = this;
    text.text = script[counter];
    counter++;
    parent.SetActive(false);
  }

  private void Update()
  {
    if (!Input.GetKeyDown("space")) return;

    text.text = script[counter];
    counter++;
    if (counter == 8)
    {
      hinge.GetComponent<Animator>().speed = 1;
    }

    if (counter == 9)
    {
      hinge.GetComponent<Animator>().speed = 0;
      hinge.SetActive(false);
      hinge.SetActive(true);
      hinge.GetComponent<Animator>().speed = 0;
    }

    if (counter == 12)
    {
      Variables.currentReport.onButtonClick = () =>
      {
        Interstitials.Initiate("Park");
      };
      Variables.exerciseGifts = 3;
      Variables.currentReport.SetGifts(3);
      Variables.giftDisplay.UpdateGifts();
    }
  }

  public static void Complete()
  {
    exercise.text.text = "Task complete!";
  }
}