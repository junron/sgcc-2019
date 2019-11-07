using System;
using UnityEngine;
using UnityEngine.UI;

public class FallScript : MonoBehaviour
{
  private static FallScript fallScript;
  public Text text;
  public GameObject article;
  public GameObject player;
  private int counter;

  private String[] script =
  {
    "You: Wah, Mr Tay! Long time no see ah!",
    "Mr Tay: Yeah! Today I got something ask you.",
    "Mr Tay: After I see this article ah, I very scared leh. \n I don't know if my house is safe from falls!",
    "You: Hmm this sounds quite serious! I didn't know that 1 in 3 trauma cases involve people our age!",
    "You: No fear, Mr Tay! I will help you find all the fall hazards in your house!",
    "Mr Tay: Wah, thank you so much!",
    "Task: Move around the house and help Mr Tay identify 4 fall hazards!"
  };

  private void Start()
  {
    fallScript = this;
    player.GetComponent<MainCharacterController>().inhibit = true;
    text.text = script[counter];
    counter++;
  }

  private void Update()
  {
    if (!Input.GetKeyDown("space")) return;
    switch (counter)
    {
      case 6:
        player.GetComponent<MainCharacterController>().inhibit = false;
        break;
      case 8:
        text.text = "You: No problem lah! Neighbours always help one another!";
        counter++;
        return;
      case 9:
        text.text = "Press space to return back to the park!";
        counter++;
        return;
      case 10:
        print("yay");
        counter++;
        return;
      case 11:
      case 7:
        return;
    }

    text.text = script[counter];
    counter++;
    if (counter == 3)
    {
      article.SetActive(true);
    }

    if (counter == 5)
    {
      article.SetActive(false);
    }
  }

  public static void Complete()
  {
    fallScript.text.text = "Mr Tay: Thank you so much! Now my house is much safer!";
    fallScript.counter++;
  }
}